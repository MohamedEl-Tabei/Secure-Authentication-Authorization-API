using BL.DTO;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace BL.Validators
{
    public class DTOOTPValidatePhoneValidator : AbstractValidator<DTOOTPValidatePhone>
    {
        public DTOOTPValidatePhoneValidator()
        {
            RuleFor(x => x.PhoneNumber).PhoneNumber();
            RuleFor(x => x.Code).Code();
        }
    }
}
