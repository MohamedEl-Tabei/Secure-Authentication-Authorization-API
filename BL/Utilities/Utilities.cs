using BL.Exceptions;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Security;
using System.Security.Cryptography;
using System.Text;

namespace BL
{
    public static class Utilities
    {
        #region Exceptions
        private static List<Error> GetErrors(List<ValidationFailure> validationErrors)
        {
            var errors = new List<Error>();
            var propertyNames = validationErrors.Select(e => e.PropertyName).Distinct();
            foreach (var propertyName in propertyNames)
            {
                errors.Add(new Error
                {
                    PropertyName = propertyName,
                    Messages = validationErrors.Where(e => e.PropertyName == propertyName).Select(e => e.ErrorMessage).ToList()
                });
            }
            return errors;
        }
        public static void ThrowAppValidationException(List<ValidationFailure> validationErrors)
        {
            var errors = GetErrors(validationErrors);
            throw new AppValidationException("Data is not valid", errors);
        }
        #endregion
        #region OTP

        public static string GenerateCodeHash(string key, int length = 4)
        {
            var sha256 = SHA256.Create();
            var code = Guid.NewGuid().ToString().Substring(0, length);
            var bytes = Encoding.UTF8.GetBytes(code+key);
            var hashBytes = sha256.ComputeHash(bytes);
            var hashCode = Convert.ToBase64String(hashBytes).ToString();
            Console.WriteLine($"Generated OTP Code: {code} ");
            return hashCode;
        }
        public static bool VerifyCodeHash(string code, string key, string hash)
        {
            var hashCode = GenerateCodeHash(key);
            return hashCode == hash;
        }
        #endregion
    }
}
