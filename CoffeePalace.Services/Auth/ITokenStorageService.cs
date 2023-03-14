using Microsoft.JSInterop;

namespace CoffeePalace.Services.Auth;

public interface ITokenStorageService
{
    public Task Save(string token);

    public Task<string> Get();

    public Task Delete();
}