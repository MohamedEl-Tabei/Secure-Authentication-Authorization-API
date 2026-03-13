using BL.DTO;
using BL.Exceptions;
using BL.Extensions;
using BL.IManagers;
using DAL;
using DAL.Enums;
using DAL.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using Vonage;

namespace BL.Managers
{
    public class OTPManager : IOTPManager
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDTOValidators _dTOValidators;
        private readonly ISMSManager _sMSManager;
        private readonly IConfiguration _configuration;

        public OTPManager(IUnitOfWork unitOfWork, IDTOValidators dTOValidators, ISMSManager sMSManager, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _dTOValidators = dTOValidators;
            _sMSManager = sMSManager;
            _configuration = configuration;
        }
        public async Task SendToPhoneAsync(DTOOTPSendPhone dTOOTPSendPhone)
        {
            _dTOValidators.ValidateAndThrowDTOOTPSendPhone(dTOOTPSendPhone);
            #region Get the existing OTP if it exists. 
            var otp = await _unitOfWork.OTPRepository.GetByTargetAsync(dTOOTPSendPhone.PhoneNumber);
            #endregion
            #region If OTP doesn't exist, create a new one.
            if (otp == null)
            {
                otp = new OTP
                {
                    Target = dTOOTPSendPhone.PhoneNumber,
                    ExpirationTime = DateTime.UtcNow.AddMinutes(5),
                    TargetType = OTPTargetType.PhoneNumber
                };
                await _unitOfWork.OTPRepository.AddAsync(otp);
            }
            #endregion
            #region If OTP exists 
            else
            {
                var minutesLeft = Math.Floor((otp.ExpirationTime - DateTime.UtcNow).TotalMinutes);
                #region If OTP is still valid, throw an exception to prevent spamming.
                if (minutesLeft > 0)
                {
                    var errors = new List<Error>
                    {
                        new Error
                        {
                            PropertyName = "phoneNumber",
                            Messages = new List<string> { $"OTP already sent. Please wait {minutesLeft} minutes before requesting a new one." }
                        }
                    };
                    throw new AppTooManyRequestsException("", errors);

                }
                #endregion
                #region If OTP is expired, update the expiration time.
                otp.ExpirationTime = DateTime.UtcNow.AddMinutes(5);
                #endregion
            }
            #endregion
            otp.CodeHash = await _sMSManager.SendVerificationCode(dTOOTPSendPhone.PhoneNumber);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<string> ValidatePhoneOTPAsync(DTOOTPValidatePhone dTOOTPValidatePhone)
        {
            _dTOValidators.ValidateAndThrowDTOOTPValidatePhone(dTOOTPValidatePhone);
            var otp = await _unitOfWork.OTPRepository.GetByTargetAsync(dTOOTPValidatePhone.PhoneNumber);
            var errors = new List<Error>();
            #region Check if OTP exists 

            if (otp == null)
            {
                errors.Add(new Error
                {
                    PropertyName = "phoneNumber",
                    Messages = new List<string> { "No OTP found for this phone number." }
                });
                throw new AppNotFoundException("", errors);
            }
            #endregion
            #region Check if OTP is not expired
            if (otp.ExpirationTime < DateTime.UtcNow)
            {
                errors.Add(new Error
                {
                    PropertyName = "phoneNumber",
                    Messages = new List<string> { "OTP has expired. Please request a new one." }
                });
                throw new AppValidationException("", errors);

            }
            #endregion
            var isValid = otp.CodeHash == dTOOTPValidatePhone.Code.Hash(dTOOTPValidatePhone.PhoneNumber, _configuration["Hash:Key"]);
            #region Check if OTP code is not valid 
            if (!isValid)
            {
                errors.Add(new Error
                {
                    PropertyName = "code",
                    Messages = new List<string> { "Invalid OTP code." }
                });
                throw new AppValidationException("", errors);
            }
            #endregion
            #region If OTP is valid Delete the OTP
            _unitOfWork.OTPRepository.Delete(otp);
            #endregion
            await _unitOfWork.SaveChangesAsync();
            return "Token can be generated here and returned to the user for further authentication steps."; 
        }
    }
}
