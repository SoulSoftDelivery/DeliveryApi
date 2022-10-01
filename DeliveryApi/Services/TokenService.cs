using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using DeliveryApi.Models;

namespace DeliveryApi.Services
{
    public static class TokenService
    {
        public static string GenerateToken(UsuarioModel usuario, string secretKey)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secretKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, usuario.Email),
                }),
                Expires = DateTime.UtcNow.AddHours(8),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public static string GenerateToken(IEnumerable<Claim> claims, string secretKey)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secretKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(8),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public static string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }

        public static ClaimsPrincipal GetPrincipalFromExpiredToken(string token, string secretKey)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)),
                ValidateLifetime = false
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out var securityToken);
            if (securityToken is not JwtSecurityToken jwtSecurityToken ||
                !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256,
                    StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Token invalido");

            return principal;
        }

        private static List<(string, string)> _refreshTokens = new();

        public static void SaveRefreshToken(string usuarioEmail, string refreshToken)
        {
            _refreshTokens.Add(new(usuarioEmail, refreshToken));
        }

        public static string GetRefreshToken(string usuarioEmail)
        {
            return _refreshTokens.FirstOrDefault(x => x.Item1 == usuarioEmail).Item2;
        }

        //public static void DeleteRefreshToken(string usuarioEmail, string refreshToken)
        //{
        //    var item = _refreshTokens.FirstOrDefault(x => x.Item1 == usuarioEmail && x.Item2 == refreshToken);
        //    _refreshTokens.Remove(item);
        //}

        public static void DeleteRefreshToken(string usuarioEmail)
        {
            var itens = _refreshTokens.Where(x => x.Item1 == usuarioEmail).ToList();

            foreach (var item in itens)
            {
                _refreshTokens.Remove(item);
            }
        }
    }
}