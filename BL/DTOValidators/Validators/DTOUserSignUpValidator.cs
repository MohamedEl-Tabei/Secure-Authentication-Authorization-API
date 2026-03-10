using BL;
using BL.DTO;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace BL.Validators
{
    public class DTOUserSignUpValidator : AbstractValidator<DTOUserSignUp>
    {
        public DTOUserSignUpValidator()
        {
            RuleFor(u => u.PhoneNumber).PhoneNumber();
                
        }
    }
}
