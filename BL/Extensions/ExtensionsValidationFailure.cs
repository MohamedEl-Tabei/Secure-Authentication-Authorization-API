using BL.Exceptions;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace BL.Extensions
{
    public static class ExtensionsValidationFailure
    {
        extension(List<ValidationFailure> validationErrors)
        {
            public  void ThrowAppValidationException()
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
                throw new AppValidationException("Data is not valid", errors);
            }
        }
    }
}
