using Microsoft.AspNetCore.Identity;
using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace NZWalksAPI.Repository
{
    public class TokenRepository : ITokenRepository
    {
        private readonly IConfiguration configuration;

        public TokenRepository(IConfiguration configuration) 
        {
            this.configuration = configuration;
        }
        public String CreateJwtToken(IdentityUser user,List<String> roles)
        {
            //Create Claims
            var Claims = new List<Claim>();

            Claims.Add(new Claim(ClaimTypes.Email, user.Email));

            foreach(var role in roles)
            {
                Claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));

            var credentials = new SigningCredentials(key,SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                configuration["Jwt:Issuer"],
                configuration["Jwt:Audeience"],
                Claims,
                expires:DateTime.Now.AddMinutes(15),
                signingCredentials:credentials);

            return new JwtSecurityTokenHandler().WriteToken(token); 
        }

        public void CreateJwtToken()
        {
            throw new NotImplementedException();
        }
    }
}
