using BAL.IService;
using Microsoft.AspNetCore.Mvc;
using MODEL.CommonConfig;
using MODEL.DTO;

namespace SocialConnectAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly ILoginService _loginService;

        public AuthController(ILoginService loginService)
        {
            _loginService = loginService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDTO dto)
        {
            try
            {
                var result = await _loginService.Register(dto);
                return Ok(new Response { Message = result.Message, Status = APIStatus.Successful, Data = result });
            }
            catch (Exception ex)
            {
                return Ok(new Response { Message = ex.InnerException?.Message ?? ex.Message, Status = APIStatus.SystemError });
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDTO dto)
        {
            if (!ModelState.IsValid) return BadRequest(new Response { Message = "Invalid data", Status = APIStatus.Error, Data = ModelState });
            try
            {
                var result = await _loginService.UserLogin(dto);
                return Ok(new Response { Message = result.Message, Status = result.Success ? APIStatus.Successful : APIStatus.Error, Data = result });
            }
            catch (Exception ex)
            {
                return Ok(new Response { Message = ex.InnerException?.Message ?? ex.Message, Status = APIStatus.SystemError });
            }
        }
        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            try
            {
                var authHeader = Request.Headers["Authorization"].ToString();

                if (string.IsNullOrEmpty(authHeader) || !authHeader.StartsWith("Bearer "))
                {
                    return Ok(new Response
                    {
                        Message = "Authorization header is missing or invalid",
                        Status = APIStatus.Error
                    });
                }

                var result = await _loginService.UserLogout(authHeader);

                return Ok(new Response
                {
                    Message = result.Message,
                    Status = result.Success ? APIStatus.Successful : APIStatus.Error,
                    Data = result
                });
            }
            catch (Exception ex)
            {
                return Ok(new Response
                {
                    Message = ex.InnerException?.Message ?? ex.Message,
                    Status = APIStatus.SystemError
                });
            }
        }

    }

}
