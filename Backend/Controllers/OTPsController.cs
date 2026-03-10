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
    }
}
