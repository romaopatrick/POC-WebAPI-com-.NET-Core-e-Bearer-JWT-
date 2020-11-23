using CrudApi.Domain.Models;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CrudApi.Services
{
    public class TokenService
    {
        private readonly JwtSecurityTokenHandler _jwtSecurityTokenHandler;
        public TokenService(JwtSecurityTokenHandler jwtSecurityTokenHandler)
        {
            _jwtSecurityTokenHandler = jwtSecurityTokenHandler ?? throw new ArgumentNullException(nameof(jwtSecurityTokenHandler));
        }
        public TokenModel GenerateToken(ClientModel client)
        {
            var token = _jwtSecurityTokenHandler.WriteToken(_jwtSecurityTokenHandler.CreateToken(new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, client.Login.ToString())
                }),
                Expires = DateTime.UtcNow.AddMinutes(20),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("fedaf7d8863b48e197b9287d492b708e")), SecurityAlgorithms.HmacSha256Signature)
            })); 
            return new TokenModel { Token = token, CreatedTime = DateTime.Now, ExpirationDate = DateTime.Now.AddMinutes(20) };
        }
    }
}
