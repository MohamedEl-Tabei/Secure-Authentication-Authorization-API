using System;
using System.Collections.Generic;
using System.Text;

namespace BL.Exceptions
{
    public class AppValidationException : AppBaseException
    {
        public AppValidationException(string message,List<Error> errors) : base(message,errors)
        {
        }
    }
}

