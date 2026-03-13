using BL.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BL.IManagers
{
    public interface IOTPManager
    {
        Task SendToPhoneAsync(DTOOTPSendPhone dTOOTPSendPhone);
        Task<string> ValidatePhoneOTPAsync(DTOOTPValidatePhone dTOOTPValidatePhone);
    }
}
