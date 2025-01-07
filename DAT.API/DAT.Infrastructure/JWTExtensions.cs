using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace DAT.Infrastructure
{
    public static class JWTExtensions
    {
        private static readonly string Issuer = "DaoAnhTu@020996060404";
        private static readonly string Audience = "AdminWebsiteShopFood";
        private static readonly string Expires = "30";
        private static readonly string SecretKey = "DaoAnhTu~!@#$%DaoAnhTu~!@#$%DaoAnhTu~!@#$%DaoAnhTu~!@#$%";

        public static string GenerateToken(string userId, string? userEmail, string? username, out DateTime datetimeExpired)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            datetimeExpired = DateTime.Now.AddDays(double.Parse(Expires));
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, userId),
                new Claim(JwtRegisteredClaimNames.Email, userEmail ?? "default"),
                new Claim(JwtRegisteredClaimNames.Name, username ?? "default"),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(
                issuer: Issuer,
                audience: Audience,
                claims: claims,
                expires: datetimeExpired,
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public static Dictionary<string, object>? DecodeToken(string token)
        {
            var jwtDictionary = new Dictionary<string, object>();

            var handler = new JwtSecurityTokenHandler();

            if (handler.CanReadToken(token))
            {
                var jwtToken = handler.ReadJwtToken(token);
                foreach (var header in jwtToken.Header)
                {
                    Console.WriteLine($"{header.Key}: {header.Value}");
                    jwtDictionary.Add(header.Key, header.Value);
                }
                foreach (var claim in jwtToken.Claims)
                {
                    Console.WriteLine($"{claim.Type}: {claim.Value}");
                    jwtDictionary.Add(claim.Type, claim.Value);
                }
                return jwtDictionary;
            }

            return null;
        }

        public static bool ValidateToken(string token, ref string messageError)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(SecretKey);

            try
            {
                var validationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),

                    ValidateIssuer = true,
                    ValidIssuer = Issuer,

                    ValidateAudience = true,
                    ValidAudience = Audience,

                    ValidateLifetime = true, // Ensure the token hasn't expired
                    ClockSkew = TimeSpan.Zero // Optional: reduce clock skew (default is 5 minutes)
                };

                ClaimsPrincipal principal = tokenHandler.ValidateToken(token, validationParameters, out SecurityToken validatedToken);
            }
            catch (SecurityTokenException)
            {
                messageError = "Token invalid";
                return false;
            }
            return true;
        }
    }
}