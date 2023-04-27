using JR_API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Xml.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JR_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController : ControllerBase
    {
        

        [Route("Login")]
        [HttpPost]
        public JR_DB.Tokens LoginAPILogin(JR_DB.Tokens tokenRequest)
        {
            JR_DB.Tokens tokenResult = new JR_DB.Tokens();


            if (tokenRequest.token == "asdkhfalskdjfhas")
            {
                string applicationName = "JR_API";
                tokenResult.expirationTime = DateTime.Now.AddMinutes(30);
                tokenResult.token = CustomTokenJWT(applicationName, tokenResult.expirationTime);
            }

            return tokenResult;

        }

        private string CustomTokenJWT(string ApplicationName, DateTime token_expiration)
        {
            IConfiguration config = new ConfigurationBuilder()
                    .AddJsonFile("appsettings.json")
                    .AddEnvironmentVariables()
                    .Build();
            JR_DB.JWTResult jWTResult = config.GetRequiredSection("JWT").Get<JR_DB.JWTResult>();

            var _symmetricSecurityKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(jWTResult.SecretKey)
                );
            var _signingCredentials = new SigningCredentials(
                    _symmetricSecurityKey, SecurityAlgorithms.HmacSha256
                );
            var _Header = new JwtHeader(_signingCredentials);
            var _Claims = new[] {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.NameId, ApplicationName),
                new Claim("Name", "nombrepersona")
            };
            var _Payload = new JwtPayload(
                    issuer: jWTResult.Issuer,
                    audience: jWTResult.Audience,
                    claims: _Claims,
                    notBefore: DateTime.Now,
                    expires: token_expiration
                );
            var _Token = new JwtSecurityToken(
                    _Header,
                    _Payload
                );
            return new JwtSecurityTokenHandler().WriteToken(_Token);
        }
    }
}
