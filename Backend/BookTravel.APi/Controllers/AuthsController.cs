using AutoMapper;
using BookTravel.APi.Dtos;
using BookTravel.APi.Models;
using BookTravel.APi.Sevrices;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;

namespace BookTravel.APi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthsController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IMapper _mapper;
        public AuthsController(IAuthService authService, IMapper mapper)
        {
            _authService = authService;
            _mapper = mapper;
        }
        [HttpPost("Login")]
        public async Task<IActionResult> LoginAsync([FromBody] LoginDto Dto)
        {
            var User = await _authService.IsRegisterdAsync(Dto.Email, Dto.Password);
            if (User is null)
                return NotFound(Dto);

            var AuthModel = await _authService.LoginAsync(User);

            return Ok(AuthModel);
        }

        [HttpPost("Logout")]
        public async Task<IActionResult> LogoutAsync(string UserId)
        {
            var User = await _authService.GetUserAsync(UserId);
            if (User is null)
                return NotFound($"Not Found {UserId}");

            await _authService.LogoutAsync(User);
            return Ok();
        }
        [HttpPut("ForgetPassword")]
        [Authorize(Roles =AppRole.Admin)]
        public async Task<IActionResult> UpdatePasswordAsync(UpdatePasswordDto Dto)
        {
            var User = await _authService.GetUserAsync(Dto.Email);
            if (User is null)
                return NotFound();

            return Ok(await _authService.UpdatePassWordAsync(User, Dto.NewPassword));
        }
    }
}
