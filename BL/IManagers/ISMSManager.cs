using System;
using System.Collections.Generic;
using System.Text;

namespace BL.IManagers
{
    public interface ISMSManager
    {
        Task<string> SendVerificationCode(string phoneNumber);

    }
}
