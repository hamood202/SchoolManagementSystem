using Microsoft.EntityFrameworkCore;
using Serilog;
using DataAccessLayer;
using WebAPIi.Sevices;
using DataAccessLayer.UserModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Text;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend",
        builder =>
        {
            builder.WithOrigins("https://localhost:7160/")
            .AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

RegisterServicesHelper.RegisterServices(builder);

Log.Logger = new LoggerConfiguration()
  .WriteTo.Console()
  .WriteTo.MSSqlServer(
  connectionString: builder.Configuration.GetConnectionString("DefaultConnection"),
  tableName: "Log",
  autoCreateSqlTable: true)
  .CreateLogger();

builder.Host.UseSerilog();


builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = "WebAPI",
            ValidateAudience = false,
            ValidateLifetime = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtSetting:SecretKey"])),
            ValidateIssuerSigningKey = true
        };
    });


//builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowFrontend");
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();


using (var scop = app.Services.CreateScope())
{
    var services = scop.ServiceProvider;
    var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
    var dbContext = services.GetRequiredService<SchoolManagementSystemContext>();


    await dbContext.Database.MigrateAsync();

    await ContextConfig.SeedDataAsync(dbContext, userManager, roleManager);
}


app.Run();
