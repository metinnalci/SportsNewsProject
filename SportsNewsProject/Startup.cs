using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SportsNewsProject.Models.ORM.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsNewsProject
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().AddRazorRuntimeCompilation();
            services.AddControllersWithViews()
            .AddNewtonsoftJson(options =>
            options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            );
            services.AddRazorPages();
            services.AddDbContext<SportsNewsContext>(options =>
            //options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection")));
            services.AddMemoryCache();
            services.AddAuthentication("UserScheme")
                .AddCookie("UserScheme", options =>
                {
                    options.LoginPath = "/Login/Login/";
                    options.Cookie.Name = "UsersCookie";
                });
            services.AddAuthentication("AdminScheme")
                .AddCookie("AdminScheme", options =>
                {
                    options.LoginPath = "/Admin/Login/Index/";
                    options.Cookie.Name = "AdminsCookie";
                });
            services.AddAuthorization(options =>
            {
                options.AddPolicy("UserAccess", policy =>
                {
                    policy.AuthenticationSchemes.Add("UserScheme");
                    policy.RequireAuthenticatedUser();
                });
                options.AddPolicy("AdminAccess", policy => 
                {
                    policy.AuthenticationSchemes.Add("AdminScheme");
                    policy.RequireAuthenticatedUser();
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                //app.UseStatusCodePagesWithReExecute("/error/{0}");
            }
            else
            {
                app.UseStatusCodePagesWithReExecute("/error/{0}");
            }

            app.UseRouting();
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute("default", "{Controller=Home}/{Action=Index}/{id?}");
                endpoints.MapAreaControllerRoute(
                    name: "Admin",
                    areaName: "Admin",
                    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
                    );
                endpoints.MapRazorPages();
            });
        }
    }
}
