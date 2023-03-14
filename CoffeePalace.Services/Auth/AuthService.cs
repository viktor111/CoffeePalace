using CoffeePalace.Models.Dtos;
using CoffeePalace.Models.Entities;

namespace CoffeePalace.Services.Auth;

public class AuthService : IAuthService
{
    private readonly IUserService userService;
    
    public AuthService(IUserService userService)
    {
        this.userService = userService;
    }
    
    public Task<User> Register(RegisterDto registerDto)
    {
        // Check match passwords
        
        // Validate Email
        
        // Validate PhoneNumber
        
        // Validate Country exists
        
        // Save the user
        
        throw new NotImplementedException();
    }

    public Task<string> Login(LoginDto loginDto)
    {
        throw new NotImplementedException();
    }
}