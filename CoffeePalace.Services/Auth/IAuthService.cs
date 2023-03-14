using CoffeePalace.Models.Dtos;
using CoffeePalace.Models.Entities;

namespace CoffeePalace.Services.Auth;

public interface IAuthService
{
    public Task<User> Register(RegisterDto registerDto);
    public Task<string> Login(LoginDto loginDto);
}