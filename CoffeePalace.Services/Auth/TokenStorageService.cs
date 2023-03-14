using Microsoft.JSInterop;

namespace CoffeePalace.Services.Auth;

public class TokenStorageService : ITokenStorageService
{
    private const string TOKEN_PREFIX = "token";   
    
    private readonly IJSRuntime jsRuntime;
    
    public TokenStorageService(IJSRuntime jsRuntime)
    {
        this.jsRuntime = jsRuntime;
    }
    
    public async Task Save(string token)
    {
        await jsRuntime.InvokeVoidAsync("localStorage.setItem", TOKEN_PREFIX, token);
    }

    public async Task<string> Get()
    {
        return await jsRuntime.InvokeAsync<string>("localStorage.getItem", TOKEN_PREFIX);
    }

    public async Task Delete()
    {
        await jsRuntime.InvokeVoidAsync("localStorage.removeItem", TOKEN_PREFIX);
    }
}