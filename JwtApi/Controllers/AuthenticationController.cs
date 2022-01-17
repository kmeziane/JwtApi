using JwtApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace JwtApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IJwtAuthenticationService _jwtAuthenticationService;
        private readonly IConfiguration _config;

        public AuthenticationController(IJwtAuthenticationService JwtAuthenticationService, IConfiguration config)
        {
            _jwtAuthenticationService = JwtAuthenticationService;
            _config = config;
        }


        [HttpPost]
        [Route("login")]
        public IActionResult Login([FromBody] LoginModel model)
        {
            var user = _jwtAuthenticationService.Authenticate(model.Email, model.Password);
            if (user != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Email, user.Email),
                };
                var token = _jwtAuthenticationService.GenerateToken(_config["Jwt:Key"], claims);
                return Ok(token);
            }
            return Unauthorized();
        }
    }
}
