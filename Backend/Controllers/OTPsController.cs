using Backend.Responses;
using BL.DTO;
using BL.IManagers;
using BL.Managers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OTPsController : ControllerBase
    {
        private readonly IOTPManager _oTPManager;

        public OTPsController(IOTPManager oTPManager)
        {
            _oTPManager = oTPManager;
        }
        [HttpPost("send-phone")]
        public async Task<IActionResult> SendToPhoneAsync(DTOOTPSendPhone dTOOTPSendPhone)
        {
            await _oTPManager.SendToPhoneAsync(dTOOTPSendPhone);
            return Ok();
        }
        [HttpPost("validate-phone")]
        public async Task<ActionResult<Response<string>>> ValidatePhoneAsync(DTOOTPValidatePhone dTOOTPValidatePhone)
        {
            var data = await _oTPManager.ValidatePhoneOTPAsync(dTOOTPValidatePhone);
            var response = new Response<string>
            {
                Data = data
            };
            return Ok(response);
        }
    }
}
