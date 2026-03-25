using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repopattern.Model;
using Repopattern.Repository.Interface;
using Repopattern.Service;

namespace Repopattern.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly JWTService _jwtService;
        public UserController(IUserRepository userRepository,JWTService jWTService) {
            _userRepository = userRepository;
            _jwtService = jWTService;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(Credentials credentials)
        {
            var user= await _userRepository.GetUser(credentials.Username, credentials.Password);
            if (user == null)
            {
                return Unauthorized();
            }
            var token=_jwtService.GenerateToken(user);

            return Ok(new { token });

        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(User user)
        {
            await _userRepository.AddUser(user);
            return Ok("Created");

        }

    }
}
