using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace BL
{
    public static class ExtensionsValidator
    {
        extension<T>(IRuleBuilderInitial<T, string> rule)
        {
            public IRuleBuilderOptions<T, string> PhoneNumber()
            {
                return rule
                    .NotEmpty().WithMessage("Phone number is required.")
                    .Length(11).WithMessage("Phone number must be exactly 11 characters long.")
                    .Matches(@"^01[0-2,5]{1}[0-9]{8}$").WithMessage("Invalid Egyptian phone number."); 
            }
            public IRuleBuilderOptions<T, string> Code()
            {
                return rule
                    .NotEmpty().WithMessage("Code is required.")
                    .Length(4).WithMessage("Code must be exactly 4 characters long.");
            }
        }
    }
}
