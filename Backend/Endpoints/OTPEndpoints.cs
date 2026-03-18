using BL.DTO;
using BL.IManagers;
using Microsoft.AspNetCore.Mvc;

namespace Backend.MinimalAPIs
{
    public static class OTPEndpoints
    {
        extension(WebApplication app)
        {
            public void MapOTPEndpoints()
            {

                app.MapPost("/otp/send-phone", async (DTOOTPSendPhone dTOOTPSendPhone, IOTPManager _oTPManager) => await _oTPManager.SendToPhoneAsync(dTOOTPSendPhone));

                app.MapPost("/otp/validate-phone", async (DTOOTPValidatePhone dTOOTPValidatePhone, IOTPManager _oTPManager) => await _oTPManager.ValidatePhoneOTPAsync(dTOOTPValidatePhone));

            }
        }
    }
}
