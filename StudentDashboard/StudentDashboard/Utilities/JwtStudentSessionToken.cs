using Microsoft.IdentityModel.Tokens;
using StudentDashboard.Models.Session;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Web;

namespace StudentDashboard.Utilities
{
    public static class JwtStudentSessionToken
    {
        public static string GenerateJSONWebToken(StudentSession studentSession)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.
                GetBytes(MvcApplication._strStudentSessionJwtTokenKey));
            var credentials = new SigningCredentials(
                securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[] {
                new Claim(ClaimTypes.Email, studentSession.EmailId),
                new Claim("UserId", studentSession.StudentId.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(MvcApplication._strStudentSessionJwtTokenKey,
              MvcApplication._strStudentSessionJwtTokenKey,
              claims,
              expires: studentSession.ExpiryTime,
              signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        public static long ValidateToken(string authToken)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var validationParameters = GetValidationParameters();
            SecurityToken validatedToken;
            IPrincipal principal = tokenHandler.ValidateToken(authToken, validationParameters, out validatedToken);
            var jwtToken = (JwtSecurityToken)validatedToken;
            long userId = -1;
            long.TryParse(jwtToken.Claims.First(x => x.Type == "UserId").Value, out userId);
            return userId;
        }
        private static TokenValidationParameters GetValidationParameters()
        {
            return new TokenValidationParameters()
            {
                ValidateLifetime = false, // Because there is no expiration in the generated token
                ValidateAudience = false, // Because there is no audiance in the generated token
                ValidateIssuer = false,   // Because there is no issuer in the generated token
                ValidIssuer = "Sample",
                ValidAudience = "Sample",
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(MvcApplication._strStudentSessionJwtTokenKey)) // The same key as the one that generate the token
            };
        }
    }
}