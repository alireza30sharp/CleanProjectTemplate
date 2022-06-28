using Domain.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.IdentityConfigs
{
    public static class IdentityConfig
    {
        public static IServiceCollection AddIdentityService(this IServiceCollection Services, IConfiguration Configuration)
        {
            string connection = Configuration["ConnectionStrings:DefaultConnection"];

            Services.AddDbContext<IdentityDataBaseContext>(option =>
            {
                option.UseSqlServer(connection, b => b.MigrationsAssembly("Persistence"));
            });


            /***
             * Identity Config 
             * 
             */
            Services.AddIdentity<User, IdentityRole>()
            .AddEntityFrameworkStores<IdentityDataBaseContext>()
            .AddDefaultTokenProviders()
            .AddRoles<IdentityRole>()
            .AddErrorDescriber<CustomIdentityError>();
            ;
            Services.Configure<IdentityOptions>(option =>
            {

                option.Password.RequireDigit = false;
                option.Password.RequiredLength = 5;
                option.Password.RequireLowercase = false;
                option.Password.RequireUppercase = false;
                option.Password.RequiredUniqueChars = 1;
                option.Password.RequireNonAlphanumeric = false;
                option.Lockout.MaxFailedAccessAttempts = 3;
                option.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
            });


            return Services;
        }
    }
}
