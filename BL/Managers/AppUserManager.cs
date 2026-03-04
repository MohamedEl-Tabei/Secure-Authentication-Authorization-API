using BL.DTO;
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
        private readonly UserManager<AppUser> _userManager;

        public AppUserManager(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task SignUp(DTOUserSignUp dTOUserSignUp)
        {
            var user = new AppUser
            {
                UserName = $"{dTOUserSignUp.FirstName}_{dTOUserSignUp.LastName}",
                Email = dTOUserSignUp.Email,
                PhoneNumber = dTOUserSignUp.PhoneNumber
            };
            var result = await _userManager.CreateAsync(user, dTOUserSignUp.Password);
            if (!result.Succeeded)
            {
                throw new ValidationException(result.Errors.FirstOrDefault()?.Description);
            }
        }
    }
}
