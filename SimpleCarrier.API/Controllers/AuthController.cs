using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using SimpleCarrier.API.Options;
using SimpleCarrier.API.ViewModels.Auth;
using SimpleCarrier.Domain.Entities.Users;
using SimpleCarrier.Domain.RepositoryInterfaces.Users;

namespace SimpleCarrier.API.Controllers
{
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly JwtSecurityTokenHandler _jwtSecurityTokenHandler = new JwtSecurityTokenHandler();

        public AuthController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpPost]
        [Route("Token")]
        public async Task<ActionResult> Token([FromBody] LoginModel loginModel)
        {
            if (!ModelState.IsValid) return BadRequest(loginModel);

            User findedUser = await _userRepository.FindByUserNameAsync(loginModel.UserName);

            if (findedUser == null)
            {
                return BadRequest(JsonConvert.SerializeObject(new
                {
                    Error = "invalid_grant",
                    Description = $"Can`t find user with login: {loginModel.UserName}."
                }));
            }

            if (!(await _userRepository.CheckPasswordAsync(findedUser, loginModel.Password)))
            {
                return BadRequest(JsonConvert.SerializeObject(new
                {
                    Error = "invalid_grant",
                    Description = $"Wrong login or password."
                }));
            }

            (string accessToken, string refreshToken) generatedTokens = await _GenerateTokens(loginModel.UserName);  

            return Ok(new
            {
                AccessToken = generatedTokens.accessToken,
                RefreshToken = generatedTokens.refreshToken
            });
        }

        [HttpPost]
        [Route("RefreshToken")]
        public async Task<ActionResult> RefreshToken([FromForm] RefreshTokenModel refreshTokenModel)
        {
            if (!ModelState.IsValid) return BadRequest(refreshTokenModel);

            SecurityToken securityToken = new JwtSecurityToken();
            
            ClaimsPrincipal claimsPrincipal = _jwtSecurityTokenHandler.ValidateToken(
                refreshTokenModel.RefreshToken,
                new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = AuthOptions.Issuer,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey()
                }, 
                out securityToken);

            if (claimsPrincipal == null)
            {
                return BadRequest(JsonConvert.SerializeObject(new
                {
                    Error = "invalid_grant",
                    Description = "Refresh token not valid"
                }));
            }

            var jwtSecurityToken = (JwtSecurityToken) securityToken;
            Claim userNameClaim = jwtSecurityToken.Claims.FirstOrDefault(c => c.Type == "UserName");
            
            if(userNameClaim == null) 
            {
                return BadRequest(JsonConvert.SerializeObject(new
                {
                    Error = "invalid_grant",
                    Description = "Refresh token not valid"
                }));
            }

            User user = await _userRepository.FindByUserNameAsync(userNameClaim.Value);
            
            if (user == null)
            {
                return BadRequest(JsonConvert.SerializeObject(new
                {
                    Error = "invalid_grant",
                    Description = "Refresh token not valid"
                }));
            }
            
            (string accessToken, string refreshToken) generatedTokens = await _GenerateTokens(userNameClaim.Value);  

            return Ok(new
            {
                AaccessToken = generatedTokens.accessToken,
                RefreshToken = generatedTokens.refreshToken
            });
        }

        private async Task<(string accessToken, string RefreshToken)> _GenerateTokens(string userName)
        {
            ClaimsIdentity identity = await _GetIdentity(userName);
            DateTime now = DateTime.Now;

            var accessToken = new JwtSecurityToken(
                issuer: AuthOptions.Issuer,
                notBefore: now,
                claims: identity.Claims,
                expires: now.Add(TimeSpan.FromMinutes(20)),
                signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(),
                    SecurityAlgorithms.HmacSha256));

            string accessTokenEncoded = _jwtSecurityTokenHandler.WriteToken(accessToken);

            var refreshToken = new JwtSecurityToken(
                issuer: AuthOptions.Issuer,
                notBefore: now,
                claims: new List<Claim> { new Claim("UserName", userName) },
                expires: now.Add(TimeSpan.FromMinutes(60)),
                signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(),
                    SecurityAlgorithms.HmacSha256));
            
            string refreshTokenEncoded = _jwtSecurityTokenHandler.WriteToken(refreshToken);
            
            return (accessTokenEncoded, refreshTokenEncoded);
        }
        
        private async Task<ClaimsIdentity> _GetIdentity(string userName)
        {
            User user = await _userRepository.FindByUserNameAsync(userName);

            if (user == null) return null;

            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.UserName)
            };


            foreach(Role role in user.Roles)
            {
                claims.Add(new Claim(ClaimsIdentity.DefaultRoleClaimType, role.Name));
            }

            var claimsIdentity = new ClaimsIdentity(
                claims,
                "Token",
                ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType);

            return claimsIdentity;
        }
    }
}