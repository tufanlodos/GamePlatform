using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL.Context;
using Entities.Entity;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace PersonelUI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            //EKLEMELER: package yüklemesi yok, entity framework core u ve aspnetcore identity yi direk usign verdi. dal ve entities referanslara eklendi. connstr yazıldı. (ters slashle yazılan bir şey, json içine yazıldığı için iki taneli.) 
            // add dbcontext
            services.AddDbContext<GamePlatformDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("GamePlatformConnStr")));

            // add identity
            services.AddIdentity<AppUser, AppRole>()
            .AddEntityFrameworkStores<GamePlatformDbContext>(); //stores başka bir yerde instance alabiliyor mu??

            services.Configure<IdentityOptions>(options => {
                // Password settings.
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 3;
                options.Password.RequiredUniqueChars = 0;

                // Lockout settings.
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);

                // User settings.
                options.User.RequireUniqueEmail = true;
            });

            services.ConfigureApplicationCookie(options => {
                // Cookie settings
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(2880);

                options.LoginPath = "/Account/SignIn"; //auth a takılırsa
                options.AccessDeniedPath = "/Account/AccessDenied"; //authorization a takılırsa
                options.SlidingExpiration = true; //her istekte cookienin süresini resetleme okeyi
            });
            //EKLEMELER BİTİŞ. ayrıca alttaki 73. satır da yeni eklendi 72nin peşine. yani mvc serialize eden bir şey görünce yok sayıyor.

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .AddJsonOptions(x => x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            app.UseCookiePolicy();
            //ekleme:
            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                //Ekleme:
                routes.MapRoute(
                    name: "area",
                    template: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
