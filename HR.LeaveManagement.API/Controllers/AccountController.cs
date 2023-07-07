using HR.LeaveManagement.Application.Contracts.Identity;
using HR.LeaveManagement.Application.Models.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAuthService _authenticationService;
        private readonly IjwtService _jwtService;
        private readonly IUserService _userService;

        public AccountController(IAuthService authenticationService,IjwtService jwtService,IUserService userService)
        {
            _authenticationService = authenticationService;
            _jwtService = jwtService;
            _userService = userService;
        }

        [HttpPost("login")]
        public async Task<ActionResult<AuthResponse>> Login(AuthRequest request)
        {
            return Ok(await _authenticationService.Login(request));
        }

        [HttpPost("register")]
        public async Task<ActionResult<RegistrationResponse>> Register(RegistrationRequest request)
        {
            return Ok(await _authenticationService.Register(request));
        }

        [HttpPost]
        [Route("refresh")]
        public IActionResult Refresh(Tokens token)
        {
            var newJwtToken = _authenticationService.Refresh(token);
            if(newJwtToken != null)
            {
                return Ok(newJwtToken);
            }
            return Unauthorized("Invalid attempt!");

        }
    }
}
