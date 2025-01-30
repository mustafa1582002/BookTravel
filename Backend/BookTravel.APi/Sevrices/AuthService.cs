using BookTravel.APi.Dtos;
using BookTravel.APi.Models;
using BookTravel.APi.Settings;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BookTravel.APi.Sevrices
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly JWT jwt;
        public AuthService(UserManager<ApplicationUser> userManager, IOptions<JWT> options)
        {
            _userManager = userManager;
            jwt = options.Value;
        }
        public async Task<AuthModelDto> LoginAsync(ApplicationUser User)
        {
            User.Status = true;
            await _userManager.UpdateAsync(User);

            return new AuthModelDto
            {
                Id = User.Id,
                IsAuthenticated = true,
                Email = User.Email,
                UserName = User.UserName,
                Token = new JwtSecurityTokenHandler().WriteToken(await CreateTokenAsync(User)),
                ExpireOn = (await CreateTokenAsync(User)).ValidTo,
                Roles = (await _userManager.GetRolesAsync(User)).ToList()
            };
        }
        public async Task LogoutAsync(ApplicationUser User)
        {
            User.Status = false;
            await _userManager.UpdateAsync(User);
        }
        public async Task<ApplicationUser?> GetUserAsync(string Key)
        {
            return
                await _userManager.FindByEmailAsync(Key) ??
                await _userManager.FindByNameAsync(Key) ??
                await _userManager.FindByIdAsync(Key);
        }
        public async Task<ApplicationUser?> IsRegisterdAsync(string Email, string Password)
        {
            var User = await _userManager.FindByEmailAsync(Email);
            if (User == null || !await _userManager.CheckPasswordAsync(User, Password))
                return null;
            return User;
        }
        public  async Task<ApplicationUser> UpdatePassWordAsync(ApplicationUser User,string NewPassword)
        {
            await _userManager.RemovePasswordAsync(User);
            await _userManager.AddPasswordAsync(User,NewPassword);
            return User;
        }
        private async Task<JwtSecurityToken> CreateTokenAsync(ApplicationUser user)
        {
            var UserClaims = await _userManager.GetClaimsAsync(user);
            var Roles = await _userManager.GetRolesAsync(user);
            var RoleClaims = new List<Claim>();
            foreach (var role in Roles)
                RoleClaims.Add(new Claim("roles", role));

            var Claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub,user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email,user.Email),
                new Claim("uid",user.Id)

            }.Union(UserClaims).Union(RoleClaims);

            var SymmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.Key));
            var SigningCredentials = new SigningCredentials(SymmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            return new JwtSecurityToken(
                issuer: jwt.Issuer
                , claims: Claims
                , audience: jwt.Audience
                , expires: DateTime.Now.AddDays(jwt.DurationOnDays)
                , signingCredentials: SigningCredentials
                );
        }
        

    }
}
