using CoffeePalace.Data;
using CoffeePalace.Models.Entities;
using CoffeePalace.Services.Common;
using CoffeePalace.Services.Validators;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CoffeePalace.Services;

public class UserService : IUserService
{
    private readonly ApplicationDbContext dbContext;
    private readonly ILogger<UserService> logger;

    public UserService(ApplicationDbContext dbContext, ILogger<UserService> logger)
    {
        this.dbContext = dbContext;
        this.logger = logger;
    }

    public async Task<Result<User>> Save(User user)
    {
        try
        {
            var validation = UserValidator.Validate(user);
            if (!validation.Succeeded) return validation.Errors.First();
            
            var result = await this.dbContext.Users.AddAsync(user);
            
            await this.dbContext.SaveChangesAsync();

            return result.Entity;
        }
        catch (Exception e)
        {
            this.logger.LogError("{@e}", e);
            return ErrorMessageBuilder.Save(nameof(User));
        }
    }

    public async Task<Result<User>> Update(string id, User user)
    {
        try
        {
            var old = await this.dbContext.Users
                .FirstOrDefaultAsync(x => x.Id == id);

            if (old is null) return ErrorMessageBuilder.NotFound(nameof(old));

            old.Address = user.Address;
            old.City = user.City;
            old.Country = user.Country;
            old.Email = old.Email;
            old.Password = old.Password;
            old.Role = user.Role;
            old.BirthDate = user.BirthDate;
            old.FirstName = user.FirstName;
            old.LastName = user.LastName;
            old.PhoneNumber = user.PhoneNumber;

            await this.dbContext.SaveChangesAsync();

            return old;
        }
        catch (Exception e)
        {
            this.logger.LogError("{@e}", e);
            return ErrorMessageBuilder.Update(nameof(User));
        }
    }

    public async Task<Result> Delete(string id)
    {
        try
        {
            var user = await this.dbContext.Users
                .FirstOrDefaultAsync(x => x.Id == id);

            this.dbContext.Remove(user);


            await this.dbContext.SaveChangesAsync();

            return Result.Success;
        }
        catch (Exception e)
        {
            this.logger.LogError("{@e}", e);
            return ErrorMessageBuilder.Delete(nameof(User));
        }
    }

    public async Task<Result<IEnumerable<User>>> All()
    {
        try
        {
            var users = await this.dbContext.Users.ToArrayAsync();

            return users;
        }
        catch (Exception e)
        {
            this.logger.LogError("{@e}", e);
            return ErrorMessageBuilder.All(nameof(User));
        }
    }

    public async Task<Result<User>> GetById(string id)
    {
        try
        {
            var users = await this.dbContext.Users.FirstOrDefaultAsync(x => x.Id == id);

            if (users == null)
                return ErrorMessageBuilder.Get(nameof(User));

            return users;
        }
        catch (Exception e)
        {
            this.logger.LogError("{@e}", e);
            return ErrorMessageBuilder.Get(nameof(User));
        }
    }
}