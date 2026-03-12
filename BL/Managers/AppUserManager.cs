using BL.DTO;
using BL.Exceptions;
using BL.Extensions;
using BL.IManagers;
using DAL;
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
        private readonly IUnitOfWork _unitOfWork;

        public AppUserManager(IPasswordHasher<AppUser> passwordHasher, IDTOValidators dTOValidators, IUnitOfWork unitOfWork)
        {
            _passwordHasher = passwordHasher;
            _dTOValidators = dTOValidators;
            _unitOfWork = unitOfWork;
        }

        public async Task SignUp(DTOUserSignUp dTOUserSignUp)
        {
            #region Validation
            _dTOValidators.ValidateAndThrowDTOUserSignUp(dTOUserSignUp);
            #endregion
            var errors = new List<Error>();
            #region check if phone number or email exist
            var isEmailExist = await _unitOfWork.AppUserRepository.IsEmailExistAsync(dTOUserSignUp.Email);
            var isPhoneNumberExist = await _unitOfWork.AppUserRepository.IsPhoneNumberExistAsync(dTOUserSignUp.PhoneNumber);
            if (isEmailExist) errors.Add(new Error { PropertyName = "Email", Messages = new List<string> { "Email already exists" } });
            if (isPhoneNumberExist) errors.Add(new Error { PropertyName = "PhoneNumber", Messages = new List<string> { "Phone number already exists" } });
            if (errors.Any()) throw new AppConflictException("", errors);
            #endregion
            #region check if phone number and email are verified
            var isVerifiedEmail = await _unitOfWork.OTPRepository.IsVerifiedEmailAsync(dTOUserSignUp.Email);
            var isVerifiedPhoneNumber = await _unitOfWork.OTPRepository.IsVerifiedPhoneNumberAsync(dTOUserSignUp.PhoneNumber);
            if (!isVerifiedEmail) errors.Add(new Error { PropertyName = "Email", Messages = new List<string> { "Email is not verified" } });
            if (!isVerifiedPhoneNumber) errors.Add(new Error { PropertyName = "PhoneNumber", Messages = new List<string> { "Phone number is not verified" } });
            if (errors.Any()) throw new AppForbiddenException("", errors);
            #endregion
            #region Create user
            var user = new AppUser
            {
                UserName = $"{dTOUserSignUp.FirstName.CapitalizeFirst()}{dTOUserSignUp.LastName.CapitalizeFirst()}",
                Email = dTOUserSignUp.Email,
                PhoneNumber = dTOUserSignUp.PhoneNumber
            };
            #endregion
            #region Hash Password
            user.PasswordHash = _passwordHasher.HashPassword(user, dTOUserSignUp.Password);
            #endregion
            
            await _unitOfWork.BeginTransactionAsync();
            var otpPhone= await _unitOfWork.OTPRepository.GetByIdAsync(user.PhoneNumber);
            var otpEmail = await _unitOfWork.OTPRepository.GetByIdAsync(user.Email);
            _unitOfWork.OTPRepository.Delete(otpPhone);
            _unitOfWork.OTPRepository.Delete(otpEmail);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
