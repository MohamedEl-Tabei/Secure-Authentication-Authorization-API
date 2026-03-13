using BL.DTO;
using BL.Exceptions;
using BL.Extensions;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace BL
{
    public class DTOValidators : IDTOValidators
    {
        private readonly IValidator<DTOUserSignUp> _dtoUserSignUp;
        private readonly IValidator<DTOOTPSendPhone> _dtoOTPSendPhone;
        private readonly IValidator<DTOOTPValidatePhone> _dtoOTPValidatePhone;

        public DTOValidators(IValidator<DTOUserSignUp> dtoUserSignUp, IValidator<DTOOTPSendPhone> dtoOTPSendPhone, IValidator<DTOOTPValidatePhone> dtoOTPValidatePhone)
        {

            _dtoUserSignUp = dtoUserSignUp;
            _dtoOTPSendPhone = dtoOTPSendPhone;
            _dtoOTPValidatePhone = dtoOTPValidatePhone;
        }

        public void ValidateAndThrowDTOOTPSendPhone(DTOOTPSendPhone dTOOTPSendPhone)
        {
            var result = _dtoOTPSendPhone.Validate(dTOOTPSendPhone);
            if (!result.IsValid) result.Errors.ThrowAppValidationException();
        }


        public void ValidateAndThrowDTOOTPValidatePhone(DTOOTPValidatePhone dTOOTPValidatePhone)
        {
            var result = _dtoOTPValidatePhone.Validate(dTOOTPValidatePhone);
            if (!result.IsValid) result.Errors.ThrowAppValidationException();
        }

        public void ValidateAndThrowDTOUserSignUp(DTOUserSignUp dTOUserSignUp)
        {
            var result = _dtoUserSignUp.Validate(dTOUserSignUp);
            if (!result.IsValid) result.Errors.ThrowAppValidationException();
        }

    }
}
