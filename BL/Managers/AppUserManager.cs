using BL.DTO;
using BL.Exceptions;
using BL.Extensions;
using BL.IManagers;
using DAL.Models;

using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BL.Managers
{
    public class AppUserManager : IAppUserManager
    {
        private readonly IPasswordHasher<AppUser> _passwordHasher;
        private readonly IDTOValidators _dTOValidators;

        public AppUserManager(IPasswordHasher<AppUser> passwordHasher,IDTOValidators dTOValidators)
        {
            _passwordHasher = passwordHasher;
            _dTOValidators = dTOValidators;
        }

        public async Task SignUp(DTOUserSignUp dTOUserSignUp)
        {
            #region Validation
            _dTOValidators.ValidateAndThrowDTOUserSignUp(dTOUserSignUp);
            #endregion
            #region User
            var user = new AppUser
            {
                UserName = $"{dTOUserSignUp.FirstName.CapitalizeFirst()}{dTOUserSignUp.LastName.CapitalizeFirst()}",
                Email = dTOUserSignUp.Email,
                PhoneNumber = dTOUserSignUp.PhoneNumber
            };
            #endregion
            #region Password
            var hashPassword = _passwordHasher.HashPassword(user, dTOUserSignUp.Password);
            #endregion
        }
    }
}
