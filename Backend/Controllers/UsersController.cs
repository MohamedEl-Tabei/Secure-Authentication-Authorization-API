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
    public class UsersController : ControllerBase
    {
        private readonly IAppUserManager _appUserManager;

        public UsersController(IAppUserManager appUserManager)
        {
            _appUserManager = appUserManager;
        }
        [HttpPost("signup")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> SignUp(DTOUserSignUp dTOUserSignUp)
        {
            await _appUserManager.SignUp(dTOUserSignUp);
            return Ok();
        }
    }
}
