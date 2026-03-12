using BL.DTO;
using BL.IManagers;
using DAL.Enums;
using DAL.Models;
using DAL;
using System;
using System.Collections.Generic;
using System.Text;
using BL.Exceptions;

namespace BL.Managers
{
    public class OTPManager : IOTPManager
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDTOValidators _dTOValidators;

        public OTPManager(IUnitOfWork unitOfWork, IDTOValidators dTOValidators)
        {
            _unitOfWork = unitOfWork;
            _dTOValidators = dTOValidators;
        }
        public async Task SendToPhoneAsync(DTOOTPSendPhone dTOOTPSendPhone)
        {
            _dTOValidators.ValidateAndThrowDTOOTPSendPhone(dTOOTPSendPhone);
            #region Check if OTP already exists for the phone number
            var otp = await _unitOfWork.OTPRepository.GetByIdAsync(dTOOTPSendPhone.PhoneNumber);
            #endregion
            #region If OTP doesn't exist, create a new one.
            if (otp == null)
            {
                otp = new OTP
                {
                    Target = dTOOTPSendPhone.PhoneNumber,
                    CodeHash = Utilities.GenerateCodeHash(dTOOTPSendPhone.PhoneNumber),
                    ExpirationTime = DateTime.UtcNow.AddMinutes(5),
                    TargetType = OTPTargetType.PhoneNumber
                };
                await _unitOfWork.OTPRepository.AddAsync(otp);
            }
            #endregion
            #region If OTP exists, check if it's expired. If not expired, throw an exception. If expired, generate a new code and update the expiration time.
            else
            {
                var minutesLeft = Math.Floor((otp.ExpirationTime - DateTime.UtcNow).TotalMinutes);
                if (minutesLeft > 0) throw new AppTooManyRequestsException($"OTP already sent. Please wait {minutesLeft} minutes before requesting a new one.", null);
                otp.CodeHash = Utilities.GenerateCodeHash(dTOOTPSendPhone.PhoneNumber);
                otp.ExpirationTime = DateTime.UtcNow.AddMinutes(5);
            }
            #endregion
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task ValidatePhoneOTPAsync(DTOOTPValidatePhone dTOOTPValidatePhone)
        {
            _dTOValidators.ValidateAndThrowDTOOTPValidatePhone(dTOOTPValidatePhone);
            var otp = await _unitOfWork.OTPRepository.GetByIdAsync(dTOOTPValidatePhone.PhoneNumber);
            var errors = new List<Error>();
            #region Check if OTP exists 

            if (otp == null)
                throw new AppNotFoundException("Phone number not found or OTP not generated for this phone number.", null);
            #endregion
            #region Check if OTP is not expired
            if (otp.ExpirationTime < DateTime.UtcNow)
                throw new AppValidationException("OTP code has expired.", null);
            #endregion
            var isValid = Utilities.VerifyCodeHash(dTOOTPValidatePhone.Code, dTOOTPValidatePhone.PhoneNumber, otp.CodeHash);
            if (!isValid) throw new AppValidationException("Invalid OTP code.", null);
            #region If OTP is valid, mark it as verified and save changes.
            otp.IsVerified = true;
            otp.ExpirationTime = DateTime.UtcNow.AddHours(1);
            otp.CodeHash = null;
            await _unitOfWork.SaveChangesAsync();
            #endregion
        }
    }
}
