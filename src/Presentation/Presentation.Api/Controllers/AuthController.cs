using Domain.Interfaces.Identity;
using Microsoft.AspNetCore.Mvc;
using Domain.DataTransferObjects.Identity;
using Infrastructure.Providers.Services;

namespace Presentation.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthProvider _authProvider;

        public AuthController(IAuthProvider authProvider, IDataProtectionService dataProtectionService)
        {
            _authProvider = authProvider;
        }

        [HttpPost("login")]
        public async Task<ActionResult<AuthResponse>> Login([FromBody] string credentials) =>
            Ok(await _authProvider.Login(credentials));

        [HttpPost("register")]
        public async Task<ActionResult<RegistrationResponse>> Register([FromBody] string credentials) => 
            Ok(await _authProvider.Register(credentials));
    }
}
