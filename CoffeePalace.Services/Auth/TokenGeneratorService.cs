using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using CoffeePalace.Models.Entities;
using CoffeePalace.Models.Types;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace CoffeePalace.Services.Auth;

public class TokenGeneratorService : ITokenGeneratorService
{
    private readonly IConfiguration configuration;
    
    public TokenGeneratorService(IConfiguration configuration)
    {
        this.configuration = configuration;
    }
    
    public string GenerateToken(User user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Expires = DateTime.Now.AddDays(1),
            IssuedAt = DateTime.Now,
            SigningCredentials = GetSigningCredentials(),
            Subject = new ClaimsIdentity(GetClaims(user))
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        var serializedToken = tokenHandler.WriteToken(token);

        return serializedToken;
    }

    private SigningCredentials GetSigningCredentials()
    {
        var jwtKey = this.configuration.GetValue<string>("jwtKey");
        if (string.IsNullOrEmpty(jwtKey))
        {
            throw new Exception("jwtKey cannot be null while generating the token");
        }
        var key = Encoding.ASCII.GetBytes(jwtKey);

        var credentials = new SigningCredentials(
            new SymmetricSecurityKey(key),
            SecurityAlgorithms.HmacSha256Signature);

        return credentials;
    }

    private Claim[] GetClaims(User user)
    {
        var claims = new[] {

            new Claim(ClaimTypes.Upn, user.Id),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Role, GetUserRole(user.Role)),
            new Claim(ClaimTypes.MobilePhone, user.PhoneNumber),
            new Claim(ClaimTypes.StreetAddress, user.Address),
            new Claim(ClaimTypes.Name, GetUserFullName(user))
        };

        return claims;
    }

    private string GetUserRole(UserRoleType userRoleType)
    {
        var numberRole = (int)userRoleType;

        return userRoleType.ToString();
    }

    private string GetUserFullName(User user)
    {
        var fullName = $"{user.FirstName} {user.LastName}";

        return fullName;
    }
}