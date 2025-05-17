using DataAccessLayer;
using Microsoft.EntityFrameworkCore;
using DataAccessLayer.UserModel;
using Microsoft.AspNetCore.Identity;
using Ui.Sevices;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<SchoolManagementSystemContext>(options =>
           options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

RegisterServicesHelper.RegisterServices(builder);


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{

    endpoints.MapControllerRoute(
 name: "admin",
 pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

    endpoints.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
}
);

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
