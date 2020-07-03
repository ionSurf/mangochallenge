using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NLog;
using System.IO;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.FileProviders;
using MangoChallenge.Extensions;
using LoggerService;
using Filters.ExceptionFilters;

namespace MangoChallenge
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            //LogManager.LoadConfiguration(String.Concat(Directory.GetCurrentDirectory(), "/mangochallenge", "/nlog.config"));
            Configuration = configuration;
            CurrentEnvironment = env;
        }

        public IConfiguration Configuration { get; }
        private IWebHostEnvironment CurrentEnvironment { get;set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.ConfigureLoggerService();
            
            services.ConfigureCors();
            services.ConfigureIISIntegration();
            //services.ConfigureMySqlContext( Configuration );
            if( !CurrentEnvironment.IsProduction() ) {
                services.ConfigureMssqlContext( Configuration );
            } else {
                services.ConfigureMssqlContextForProduction( Configuration );
            }
            services.ConfigureRepositoryWrapper();

            services.AddAutoMapper(typeof(Startup));

            services.ConfigureMultiPartBodyLength();

            services.ConfigureJWT( Configuration );

            services.ConfigureActionFilters();

            services.AddControllers(
                config => config.Filters.Add(typeof(ApiExceptionFilter))
            );
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerManager logger)
        {
            Console.WriteLine(env.EnvironmentName.ToString());
            if (env.IsStaging()) {
                Console.WriteLine(Directory.GetCurrentDirectory().ToString());
                LogManager.LoadConfiguration(String.Concat(Directory.GetParent( Directory.GetCurrentDirectory().ToString() ), "/nlog.config"));
            } else {
                Console.WriteLine(Directory.GetCurrentDirectory().ToString());
                LogManager.LoadConfiguration(String.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));
            }

            if (env.IsDevelopment()) app.UseDeveloperExceptionPage();

            //app.ConfigureExceptionHandler(logger);
            app.ConfigureExceptionMiddleware();

            app.UseHttpsRedirection();

            app.UseStaticFiles();
            if (env.IsStaging()) {
                app.UseStaticFiles(new StaticFileOptions()
                {
                    FileProvider    = new PhysicalFileProvider(Path.Combine(Directory.GetParent( Directory.GetCurrentDirectory() ).ToString(), @"media")),
                    RequestPath     = new PathString("/media")
                });
            } else {
                app.UseStaticFiles(new StaticFileOptions()
                {
                    FileProvider    = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"media")),
                    RequestPath     = new PathString("/media")
                });
            }

            app.UseForwardedHeaders(new ForwardedHeadersOptions {
                //ForwardedHeaders = ForwardedHeaders.All
            });

            app.UseRouting();

            app.UseCors();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
