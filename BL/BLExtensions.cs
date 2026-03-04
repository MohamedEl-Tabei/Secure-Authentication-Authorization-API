using BL.IManagers;
using BL.Managers;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace BL
{
    public static class BLExtensions
    {
        extension(IServiceCollection services)
        {
            public void AddBLServices()
            {
                services.AddScoped<IAppUserManager, AppUserManager>();

            }
        }
    }
}
