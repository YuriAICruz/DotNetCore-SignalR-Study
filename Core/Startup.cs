using System;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.AspNetCore.Rewrite.Internal;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using WebServerStudy.Core.Hub;
using WebServerStudy.Models;

namespace WebServerStudy.Core
{
    public class Startup
    {
        private IConfiguration _config;

        public Startup(IConfiguration config)
        {
            _config = config;
        }


        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
//                services.AddCors(options =>
//                {
//                    options.AddPolicy("AllowAll",
//                        builder =>
//                        {
//                            builder
//                                .AllowAnyOrigin()
//                                .AllowAnyMethod()
//                                .AllowAnyHeader()
//                                .AllowCredentials();
//                        });
//                });

            services.AddIdentity<IdentityUser, IdentityRole>(options =>
            {
                options.Password.RequiredLength = 4;
                options.Password.RequiredUniqueChars = 4;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireDigit = false;
            }).AddEntityFrameworkStores<AppDbContext>();

            services.AddDbContextPool<AppDbContext>(options =>
            {
                options.UseSqlite(_config.GetConnectionString("SQLiteDbConnection"),
                    b => { b.MigrationsAssembly("Core"); });
            });

            var controllerAssembly = Assembly.Load("Controllers");

            services.AddMvc().AddApplicationPart(controllerAssembly).AddControllersAsServices();
            services.AddSignalR();

            //services.AddSingleton<IPlayerRepository, MockPlayerRepository>();
            services.AddScoped<IPlayerRepository, SqlLitePlayerRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            
            app.UseAuthentication();

            app.UseStaticFiles();

            app.UseMvc();


//            Default routes method
//            app.UseMvc(routes =>
//            {
//                routes.MapRoute("default", "{controller=Home}/{action=Index}/{id?}");
//            });

//            app.UseMvcWithDefaultRoute();


            app.UseSignalR(routes => { routes.MapHub<ChatHub>("/chat"); });


            
//          A simple redirect
//          app.UseStatusCodePagesWithRedirects("~/");

            app.UseMiddleware<NotFoundMiddleware>();


//           last fallout middleware for default response
//            app.Run(async (context) =>
//            {
//                await context.Response.WriteAsync(
//                    _config["TestKey"]
//                );
//            });
        }
    }
}