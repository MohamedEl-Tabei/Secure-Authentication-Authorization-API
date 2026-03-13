using BL.DTO;
using BL.IManagers;
using BL.Managers;
using BL.Validators;
using FluentValidation;
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
                #region Managers
                services.AddScoped<IAppUserManager, AppUserManager>();
                services.AddScoped<IOTPManager, OTPManager>();
                services.AddScoped<ISMSManager, SMSManager>();
                services.AddScoped<IDTOValidators, DTOValidators>();
                #endregion
                #region Validators
                services.AddValidatorsFromAssemblyContaining<DTOUserSignUpValidator>();
                #endregion
            }
        }
    }
}
