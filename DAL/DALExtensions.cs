using DAL.Context;
using DAL.IRepositories;
using DAL.Models;
using DAL.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    public static class DALExtensions
    {
        extension(IServiceCollection services)
        {
            public void AddDALServices(IConfiguration configuration)
            {
                #region Context
                services.AddDbContext<MyContext>(options => options.UseSqlServer(configuration.GetConnectionString("Dev")));
                #endregion
                #region Identity
                services.AddIdentityCore<AppUser>()
                    .AddEntityFrameworkStores<MyContext>();
                #endregion
                #region Configuration
                services.Configure<IdentityOptions>(options =>
                {
                    #region Password
                    options.Password.RequireDigit = true;
                    options.Password.RequireLowercase = true;
                    options.Password.RequireNonAlphanumeric = true;
                    options.Password.RequireUppercase = true;
                    options.Password.RequiredLength = 8;
                    options.Password.RequiredUniqueChars = 1;
                    #endregion
                    #region Email
                    options.User.RequireUniqueEmail = true;
                    #endregion
                    #region UserName
                    options.User.AllowedUserNameCharacters =
                    "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
                    #endregion
                    #region Lockout
                    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                    options.Lockout.MaxFailedAccessAttempts = 5;
                    options.Lockout.AllowedForNewUsers = true;
                    #endregion

                });

                #endregion
                #region Repositories
                services.AddScoped<IAppUserRepository, AppUserRepository>();
                services.AddScoped<IOTPRepository, OTPRepository>();
                
                #endregion
                services.AddScoped<IUnitOfWork, UnitOfWork>();
            }
        }
    }
}
