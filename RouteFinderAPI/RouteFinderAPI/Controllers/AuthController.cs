
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;

namespace RouteFinderAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : BaseController
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService) => _authService = authService;
        
        [HttpPost]
        [Route("/auth")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(TokenModel))]
        [AllowAnonymous]
        public async Task<ActionResult<TokenModel>> Authenticate(UserAuthModel model)
        {
            var user = await _authService.Authenticate(model.Email, model.Password);
            if (user == null) return Unauthorized();
            return Ok(new TokenModel()
            {
                Token = GenerateToken(user, 600)
            });
        }
        
        private string GenerateToken(UserDto user, int expirationTimeInMinutes)
        {
            var secretKey = Encoding.UTF8.GetBytes("SomethingReallyRandom");
            var securityKey = new SymmetricSecurityKey(secretKey);
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
            var expiryTime = DateTime.UtcNow.AddMinutes(expirationTimeInMinutes);

            var tokenDescriptor = new SecurityTokenDescriptor()
            {
             
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Role, user.Role)
                }),
                Expires = expiryTime,
                SigningCredentials = credentials
            };
            
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = tokenHandler.CreateJwtSecurityToken(tokenDescriptor);
         
            return tokenHandler.WriteToken(jwtToken);
        }
    }
}
