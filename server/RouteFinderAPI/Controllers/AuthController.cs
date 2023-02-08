
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using RouteFinderAPI.Authentication;
using RouteFinderAPI.Common.Constants;
namespace RouteFinderAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : BaseController
    {
        private readonly IAuthService _authService;
        private readonly IAuthorizedAccountProvider _authorizedAccountProvider;
        public AuthController(IAuthService authService, IAuthorizedAccountProvider authorizedAccountProvider) {
            _authService = authService;
            _authorizedAccountProvider = authorizedAccountProvider;
        }
        
        [HttpPost]
        [Route("")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(TokenModel))]
        [AllowAnonymous]
        public async Task<ActionResult<TokenModel>> Authenticate(UserAuthModel model)
        {
            var user = await _authService.Authenticate(model.Email, model.Password);
            if (user == null) return Unauthorized();
            return Ok(new TokenModel()
            {
                Token = GenerateToken(user, 600, TokenConstants.TOKEN_TYPE.ACCESS),
                RefreshToken = GenerateToken(user, 60000, TokenConstants.TOKEN_TYPE.REFRESH)
            });
        }

        [HttpPost]
        [Route("refresh")]
        [AllowAnonymous]
        public async Task<ActionResult<TokenModel>> Refresh(TokenModel model)
        {
            var handler = new JwtSecurityTokenHandler();
            var jwtSecurityToken = handler.ReadJwtToken(model.RefreshToken);
            if (jwtSecurityToken.ValidTo >= DateTime.UtcNow)
            {
                var id = jwtSecurityToken.Claims
                    .Where(x => x.Type == "nameid")
                    .Select(x => x.Value)
                    .FirstOrDefault();

                var account = await _authorizedAccountProvider.GetLoggedInAccount(id);
                if (account is null) return Unauthorized();

                return new TokenModel
                {
                    Token = GenerateToken(account, 600, TokenConstants.TOKEN_TYPE.ACCESS),
                    RefreshToken = GenerateToken(account, 18000, TokenConstants.TOKEN_TYPE.REFRESH)
                };
            }
            else
            {
                return Unauthorized();
            }
        }

        private string GenerateToken(UserDto user, int expirationTimeInMinutes, TokenConstants.TOKEN_TYPE tokenType)
        {
            var secretKey = tokenType == TokenConstants.TOKEN_TYPE.ACCESS ? Encoding.UTF8.GetBytes("JWTIWASBORNINTHEUSA") : Encoding.UTF8.GetBytes("JWTIWASBORNINTHEUK");
            var securityKey = new SymmetricSecurityKey(secretKey);
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
            var expiryTime = DateTime.UtcNow.AddMinutes(expirationTimeInMinutes);

            var tokenDescriptor = new SecurityTokenDescriptor()
            {
             
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Role, user.Role.RoleName),
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
