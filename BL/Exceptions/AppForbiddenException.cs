using System;
using System.Collections.Generic;
using System.Text;

namespace BL.Exceptions
{
    public class AppForbiddenException:AppBaseException
    {
        public AppForbiddenException(string message, List<Error> errors) : base(message, errors)
        {
        }
    }
}
