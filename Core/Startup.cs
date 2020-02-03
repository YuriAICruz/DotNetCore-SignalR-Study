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
        
        public void ConfigureServices(IServiceCollection services)
        {
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
            services.AddScoped<ICharacterRepository, SqLiteCharacterRepository>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            
            app.UseAuthentication();

            app.UseStaticFiles();

            app.UseMvc();

            app.UseSignalR(routes => { routes.MapHub<ChatHub>("/chat"); });
            app.UseSignalR(routes => { routes.MapHub<PeerToServerHub>("/networking"); });

            //app.UseMiddleware<NotFoundMiddleware>();
        }
    }
}