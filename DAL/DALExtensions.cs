using DAL.Context;
using DAL.Models;
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
                services.AddDbContext<MyContext>(options => options.UseSqlServer(configuration.GetConnectionString("Dev")));
                services.AddIdentityCore<AppUser>()
                    .AddEntityFrameworkStores<MyContext>();
            }
        }
    }
}
