using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjetoLogin.API.DTOs;
using ProjetoLogin.API.Models;
using ProjetoLogin.API.Services;

namespace ProjetoLogin.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserService _service;

        public UserController(UserService service)
        {
            _service = service;
        }
        
        [AllowAnonymous]
        [HttpPost("login")]
        public IActionResult Login([FromBody] UserLoginDTO loginDto)
        {
            User? user = _service.Authenticate(loginDto.Username, loginDto.Password);
            if (user == null)
                return Unauthorized("Credenciais inválidas");

            string token = _service.GenerateJwtToken(user);

            return Ok(new { token });
        }

        [Authorize]
        [HttpGet("list")]
        public IActionResult GetUsers([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            List<User> users = _service.GetUsersPaged(page, pageSize);
            int total = _service.GetTotalCount();
            return Ok(new { total, users });
        }
    }
}