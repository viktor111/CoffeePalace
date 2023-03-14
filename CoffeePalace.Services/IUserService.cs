using CoffeePalace.Models.Entities;
using CoffeePalace.Services.Common;

namespace CoffeePalace.Services;

public interface IUserService
{
    public Task<Result<User>> Save(User user);

    public Task<Result<User>> Update(string id, User user);
    
    public Task<Result> Delete(string id);
    
    public Task<Result<IEnumerable<User>>> All();
}