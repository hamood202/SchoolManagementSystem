using BL.Mapping;
using Serilog;
using Microsoft.EntityFrameworkCore;
using BL.Dto;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using DataAccessLayer.UserModel;
using DataAccessLayer;
using BL.Sevices;
using DataAccessLayer.Interfsce;
using DataAccessLayer.Repositories;
using BL.Interfase;
using WebAPI.Sevices;
using SchoolManagementSystem.Sevices;

namespace Ui.Sevices
{
    public class RegisterServicesHelper
    {
        public static void RegisterServices(WebApplicationBuilder builder)
        {
            builder.Services.AddHttpClient("ApiClient",client =>
            {
                client.BaseAddress = new Uri(builder.Configuration["ApiSettings:BaseUrl"]);
            }); 

            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = "/account/login";
                    options.AccessDeniedPath = "/access-denied";
                });

            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.Password.RequiredLength = 8;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.User.RequireUniqueEmail = true;
            }).AddEntityFrameworkStores<SchoolManagementSystemContext>();

            builder.Services.AddDbContext<SchoolManagementSystemContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
                    
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .WriteTo.MSSqlServer(
                connectionString: builder.Configuration.GetConnectionString("DefaultConnection"),
                tableName: "Log",
                autoCreateSqlTable: true)
                .CreateLogger();
            builder.Host.UseSerilog();
                        builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);
            builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            builder.Services.AddScoped(typeof(ITableRepository<>), typeof(TableRepository<>));

            builder.Services.AddScoped<IAttendance, AttendanceService>();
            builder.Services.AddScoped<IClass, ClassService>();
            builder.Services.AddScoped<ICity, CityService>();
            builder.Services.AddScoped<IStudent, StudentService>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<GenericApiClient>();
            builder.Services.AddSingleton<TokenService>();
            builder.Services.AddScoped<IGrade, GradeService>();
            builder.Services.AddScoped<ISchedule, ScheduleService>();
            builder.Services.AddScoped<ISubject, SubjectService>();
            builder.Services.AddScoped<ITeacher, TeacherService>();
            builder.Services.AddScoped<ITeacherSubject, TeacherSubjectService>();
        }
    }
}
