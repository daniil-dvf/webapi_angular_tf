using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using PetShop.Services.Abstractions.Jwt;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using ToolBox.Utils.Extensions;

namespace ToolBox.Security.Jwt
{
    public class JwtService : IJwtService
    {
        private readonly SymmetricSecurityKey _SecurityKey;
        private readonly JwtSecurityTokenHandler _Handler;
        private readonly string _Issuer;
        private readonly string _Audience;

        public JwtService(JwtConfig config)
        {
            _Handler = new JwtSecurityTokenHandler();
            _SecurityKey
                = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config.Signature));
            _Issuer = config.Issuer;
            _Audience = config.Audience;
        }

        public ClaimsPrincipal DecodeJwt(string jwt)
        {
            TokenValidationParameters validationParameters = GetValidationParameters();
            try
            {
                return _Handler.ValidateToken(jwt, validationParameters, out SecurityToken validatedToken);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public string EncodeJwt(IPayload payload)
        {
            return EncodeJwt(payload, DateTime.UtcNow.AddDays(7));
        }

        public string EncodeJwt(IPayload payload, DateTime exp)
        {
            SigningCredentials credentials = new SigningCredentials(_SecurityKey, SecurityAlgorithms.HmacSha512);

            JwtSecurityToken secToken = new JwtSecurityToken(
                _Issuer,
                _Audience,
                ToClaims(payload),
                DateTime.UtcNow,
                exp,
                credentials
            );
            return _Handler.WriteToken(secToken);
        }

        private TokenValidationParameters GetValidationParameters()
        {
            return new TokenValidationParameters()
            {
                ValidateIssuer = true,
                ValidateAudience = false,
                ValidIssuer = _Issuer,
                RequireSignedTokens = true,
                IssuerSigningKey = _SecurityKey
            };
        }

        private IEnumerable<Claim> ToClaims(IPayload payload)
        {
            yield return new Claim(ClaimTypes.Email, payload.Email);
            yield return new Claim(ClaimTypes.Role, payload.RoleName);
            yield return new Claim(ClaimTypes.PrimarySid, payload.Id.ToString());
            yield return new Claim(ClaimTypes.Name, payload.Name);

            foreach (PropertyInfo prop in payload.GetType().GetProperties())
            {
                if (prop.GetValue(payload) != null)
                    yield return CreatePropertyClaim(prop, payload);
            }

        }

        private Claim CreatePropertyClaim(PropertyInfo prop, object instance)
        {

            string type
                = IsNumericType(prop.PropertyType) ? ClaimValueTypes.Integer64
                : prop.PropertyType == typeof(DateTime) ? ClaimValueTypes.Date
                : prop.PropertyType == typeof(bool) ? ClaimValueTypes.Boolean
                : ClaimValueTypes.String;

            string value = prop.PropertyType.IsValueType || prop.PropertyType == typeof(string)
                ? prop.GetValue(instance)?.ToString()
                : JsonConvert.SerializeObject(prop.GetValue(instance));

            return new Claim(
                prop.Name.ToLowerCamelCase(),
                value,
                type
            );

        }

        public bool IsNumericType(Type t)
        {
            return t == typeof(byte)
                || t == typeof(sbyte)
                || t == typeof(ushort)
                || t == typeof(uint)
                || t == typeof(uint)
                || t == typeof(short)
                || t == typeof(int)
                || t == typeof(long)
                || t == typeof(decimal)
                || t == typeof(float)
                || t == typeof(double);
        }
    }
}
