using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GamePlatform.DAL.Context;
using GamePlatform.Entities.Entity;
using GamePlatform.Web.Api.Security;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace GamePlatform.Web.Api {
    public class Startup {
        public Startup(IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }


        public void ConfigureServices(IServiceCollection services) {
            services.AddDbContext<GamePlatformDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("GamePlatformConnStr")));

            services.AddIdentity<AppUser, AppRole>()
                .AddEntityFrameworkStores<GamePlatformDbContext>();

            services.AddCors(options => {
                options.AddDefaultPolicy(policy => {
                    policy.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
                });
            });
            
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options => {
                    options.TokenValidationParameters = new TokenValidationParameters {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateIssuerSigningKey = true,

                        ValidIssuer = "system",
                        ValidAudience = "reader",
                        IssuerSigningKey = KeyProvider.SymmetricSecurityKey
                    };
                });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
                .AddJsonOptions(options => {
                    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env) {
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            }
            app.UseCors();
            app.UseAuthentication();
            app.UseMvc();
        }
    }
}
