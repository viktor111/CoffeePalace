using CoffeePalace.Models.Entities;

namespace CoffeePalace.Services.Auth;

public interface ITokenGeneratorService
{
    public string GenerateToken(User user);
}