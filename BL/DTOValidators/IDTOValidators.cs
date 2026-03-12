using BL.DTO;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace BL
{
    public interface IDTOValidators
    {
        void ValidateAndThrowDTOUserSignUp(DTOUserSignUp dTOUserSignUp);
        void ValidateAndThrowDTOOTPSendPhone(DTOOTPSendPhone dTOOTPSendPhone);
        void ValidateAndThrowDTOOTPValidatePhone(DTOOTPValidatePhone dTOOTPValidatePhone);

    }
}
