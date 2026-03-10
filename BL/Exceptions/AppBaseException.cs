using System;
using System.Collections.Generic;
using System.Text;

namespace BL.Exceptions
{
    public class AppBaseException:Exception
    {
        public List<Error>? Errors { get; set; }
        public AppBaseException(string message, List<Error>? errors) : base(message)
        {
            Errors = errors;
        }
    }
}
