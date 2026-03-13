using BL;
using BL.Extensions;
using FluentValidation.Results;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace BL.Extensions
{
    public static class ExtensionsString
    {
        extension(string str)
        {
            public string CapitalizeFirst()
            {
                return string.IsNullOrEmpty(str) ? str :
                    str.Length == 1 ? str.ToUpper() :
                    $"{char.ToUpper(str[0])}{str.Substring(1).ToLower()}";
            }
            public string Hash(string key,string secretKey)
            {
                var sha256 = SHA256.Create();
                var bytes = Encoding.UTF8.GetBytes(str + key + secretKey);
                var hashBytes = sha256.ComputeHash(bytes);
                var hashCode = Convert.ToBase64String(hashBytes).ToString();
                Console.WriteLine($"Generated OTP Code: {str} ");
                return hashCode;
            }

        }

    }
}
