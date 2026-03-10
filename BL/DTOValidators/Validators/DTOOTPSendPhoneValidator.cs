using BL;
using BL.DTO;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace BL.Validators
{
    public class DTOOTPSendPhoneValidator : AbstractValidator<DTOOTPSendPhone>
    {
        public DTOOTPSendPhoneValidator()
        {
            RuleFor(u => u.PhoneNumber).PhoneNumber();
                
        }
    }
}
