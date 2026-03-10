using BL;
using BL.Extensions;
using FluentValidation.Results;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Text;

namespace BL.Extensions
{
    public static class ExtensionsString
    {
        extension(string str)
        {
            public string CapitalizeFirst()
            {
                return str.IsNullOrEmpty() ? str :
                    str.Length == 1 ? str.ToUpper() :
                    $"{char.ToUpper(str[0])}{str.Substring(1).ToLower()}";
            }

        }
        
    }
}
