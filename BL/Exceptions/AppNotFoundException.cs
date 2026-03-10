using System;
using System.Collections.Generic;
using System.Text;

namespace BL.Exceptions
{
    public class AppNotFoundException : AppBaseException
    {
        public AppNotFoundException(string message, List<Error> errors) : base(message, errors)
        {
        }
    }
}
