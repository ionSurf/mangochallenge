using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Repositories;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Helpers;
using LoggerService;
using UserServiceLib;
using Filters.ActionFilters;
using Filters.ExceptionFilters;
using Entities.DataTransferObjects;
using Microsoft.AspNetCore.Hosting;

namespace MangoChallenge.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("MangoPolicy",
                    builder =>
                    {
                        builder.WithOrigins("http://localhost:5000",
                                            "http://localhost:5001",
                                            "http://localhost:8080",
                                            "http://localhost:3000",
                                            "http://0.0.0.0:3000",
                                            "https://localhost:5000",
                                            "https://localhost:5001",
                                            "https://localhost:8080",
                                            "https://localhost:3000",
                                            "https://0.0.0.0:3000")
                                            .AllowAnyMethod()
                                            .AllowAnyHeader()
                                            .AllowCredentials()
                                            .Build();
                    });

                options.AddPolicy("AnotherPolicy",
                    builder =>
                    {
                        builder.WithOrigins("https://localhost")
                                            .AllowAnyHeader()
                                            .AllowAnyMethod();
                    });
            });
        }

        public static void ConfigureIISIntegration(this IServiceCollection services)
        {
            services.Configure<IISOptions>(options => { });
        }

        public static void ConfigureMySqlContext(this IServiceCollection services, IConfiguration config)
        {
            var connectionString = config["mysqlconnection:connectionString"];
            services.AddDbContext<RepositoryContext>(o => o.UseMySql(connectionString));
        }

        public static void ConfigureMssqlContext(this IServiceCollection services, IConfiguration config) {
            string connectionString = config["mssqlconnection:connectionString"];
            services.AddDbContext<RepositoryContext>(o => o.UseSqlServer(connectionString));
        }
        public static void ConfigureMssqlContextForProduction(this IServiceCollection services, IConfiguration config) {
            string connectionString = config["mssqlconnection:PENVconnectionString"];
            services.AddDbContext<RepositoryContext>(o => o.UseSqlServer(connectionString));
        }

        public static void ConfigureRepositoryWrapper(this IServiceCollection services)
        {
            services.AddScoped<IRepositoryWrapper, RepositoryWrapper>();
        }
        public static void ConfigureLoggerService(this IServiceCollection services)
        {
            services.AddSingleton<ILoggerManager, LoggerManager>();
        }

        public static void ConfigureMultiPartBodyLength(this IServiceCollection services) {
            services.Configure<FormOptions>(o => {
                o.ValueLengthLimit = int.MaxValue;
                o.MultipartBodyLengthLimit = int.MaxValue;
                o.MemoryBufferThreshold = int.MaxValue;
            });
        }

        public static void ConfigureJWT(this IServiceCollection services, IConfiguration config) {
            // configure strongly typed settings objects
            var appSettingsSection = config.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);

            var appSettings = appSettingsSection.Get<AppSettings>();
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);
            //var key = Encoding.ASCII.GetBytes("$AdEsH/e)vq{$DBT9axTe&DL.XwMvF");

            services.AddAuthentication(opt => {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    //ValidAudience,
                    //ValidIssuer,
                    //ValidateIssuer = true,
                    //ValidateAudience = true,
                    //ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
        
                    //ValidIssuer = "http://localhost:5000",
                    //ValidAudience = "http://localhost:5000",    
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    //IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("superSecretKey@345"))
                    IssuerSigningKey = new SymmetricSecurityKey(key)
                };
            });

            // configure DI for application services
            services.AddScoped<IUserService, UserService>();
        }

        public static void ConfigureActionFilters(this IServiceCollection services ) {
            services.AddScoped<UserValidationFilterAttribute>();
            services.AddScoped<PortraitValidationFilterAttribute<PortraitForCreationDto>>();
            services.AddScoped<PortraitValidationFilterAttribute<PortraitForUpdateDto>>();
            services.AddScoped<PortraitValidateEntityExistsAttribute>();
            services.AddScoped<ApiExceptionFilter>();
        }
    }
}