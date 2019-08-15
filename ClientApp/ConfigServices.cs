using aspnetcore_vue.Models.Identity;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace aspnetcore_vue
{
    public static class ConfigServices
    {
        public static void ConfigureAppIdentity(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddIdentity<AppUser, AppRole>(options =>
            {
                options.User.RequireUniqueEmail = true;

            }).AddEntityFrameworkStores<IdentityAppContext>();

            services.AddDbContext<IdentityAppContext>(cfg =>
            {
                cfg.UseSqlServer(configuration.GetConnectionString("IdentityContext"));

            });
        }


        public static void ConfigureUnauthorizedApi(this IServiceCollection services)
        {
            services.ConfigureApplicationCookie(cfg =>
            {
                cfg.Events = new CookieAuthenticationEvents
                {
                    OnRedirectToLogin = ctx =>
                    {
                        if (ctx.Request.Path.StartsWithSegments("/api"))
                        {
                            ctx.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                        }
                        else
                        {
                            ctx.Response.Redirect(ctx.RedirectUri);
                        }
                        return Task.FromResult(0);
                    }
                };
            });

        }
    }
}
