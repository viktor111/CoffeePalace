using CoffeePalace.Models.Common;
using CoffeePalace.Models.Entities;
using CoffeePalace.Models.Types;

namespace CoffeePalace.Models.Factories;

public interface IUserFactory : IFactory<User>
{
    public IUserFactory SetFirstName(string firstName);
    
    public IUserFactory SetLastName(string lastName);

    public IUserFactory SetAddress(string address);

    public IUserFactory SetCountry(string country);

    public IUserFactory SetCity(string city);
    
    public IUserFactory SetPassword(string password);
    
    public IUserFactory SetEmail(string email);

    public IUserFactory SetPhoneNumber(string phoneNumber);

    public IUserFactory SetRole(UserRoleType role);

    public IUserFactory SetBirthDate(DateTime birthDate);
}