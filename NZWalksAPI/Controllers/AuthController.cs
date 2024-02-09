using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NZWalksAPI.Models.DTO_s;
using NZWalksAPI.Repository;

namespace NZWalksAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase

    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly ITokenRepository tokenRepository;

        public AuthController(UserManager<IdentityUser> userManager,ITokenRepository tokenRepository)
        {
            this.userManager = userManager;
            this.tokenRepository = tokenRepository;
        }

       
        [HttpPost]
        [Route ("Register")]

        public async Task<IActionResult> Register([FromBody] RegistorRequestDto registorRequestDto)
        {

            var identityUser = new IdentityUser
            {
                UserName = registorRequestDto.Username,
                Email = registorRequestDto.Username

            };

          var identityResult =   await userManager.CreateAsync(identityUser, registorRequestDto.Password);

            if (identityResult.Succeeded)
            {
                //Add Roles to this user

                if (registorRequestDto.Roles != null && registorRequestDto.Roles.Any())
                {


                     identityResult = await userManager.AddToRoleAsync(identityUser, registorRequestDto.Roles);


                    if(identityResult.Succeeded)
                    {
                        return Ok("User was registered ! please login");
                    }
                }
            }

            return BadRequest("Something went wrong");
        }

        //PoST:/api 
        [HttpPost]

        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequestDto)
        {
            var user = await userManager.FindByEmailAsync(loginRequestDto.Username);

            if (user != null)
            {
               var checkPasswordResult =  await userManager.CheckPasswordAsync(user, loginRequestDto.Password);

                if (checkPasswordResult)
                {
                    //Get roles for this user
                    var roles = await userManager.GetRolesAsync(user);

                    if (roles != null)
                 { 
                    //Create Token

                    var jwtToken = tokenRepository.CreateJwtToken(user, roles.ToList());

                        var response = new LoginResponseDto
                        {
                            JwtToken = jwtToken
                        };

                        return Ok(response);

                }
                    
                }

                
                     
            }

            return BadRequest("Username or PAssword is Incorrect");

        }
    }
}
