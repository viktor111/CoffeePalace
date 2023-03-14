using System.Text;
using CoffeePalace.Models.Entities;
using CoffeePalace.Models.Types;

namespace CoffeePalace.Models.Factories;

public class UserFactory : IUserFactory
{
    private string firstName = default!;
    private string lastName = default!;
    private string address = default!;
    private string country = default!;
    private string city = default!;
    private string password = default!;
    private string email = default!;
    private string phoneNumber = default!;
    private UserRoleType role = default!;
    private DateTime birthDate = default!;

    private bool isFirstNameSet = false;
    private bool isLastNameSet = false;
    private bool isAddressSet = false;
    private bool isCountrySet = false;
    private bool isCitySet = false;
    private bool isPassword = false;
    private bool isEmailSet = false;
    private bool isPhoneNumberSet = false;
    private bool isRoleSet = false;
    private bool isBirthDateSet = false;
    
    public IUserFactory SetFirstName(string firstName)
    {
        this.firstName = firstName;
        this.isFirstNameSet = true;

        return this;
    }

    public IUserFactory SetLastName(string lastName)
    {
        this.lastName = lastName;
        this.isLastNameSet = false;

        return this;
    }

    public IUserFactory SetAddress(string address)
    {
        this.address = address;
        this.isAddressSet = false;

        return this;
    }

    public IUserFactory SetCountry(string country)
    {
        this.country = country;
        this.isCountrySet = true;

        return this;
    }

    public IUserFactory SetCity(string city)
    {
        this.city = city;
        this.isCitySet = true;

        return this;
    }

    public IUserFactory SetPassword(string password)
    {
        this.password = password;
        this.isPassword = true;

        return this;
    }

    public IUserFactory SetEmail(string email)
    {
        this.email = email;
        isEmailSet = true;

        return this;
    }

    public IUserFactory SetPhoneNumber(string phoneNumber)
    {
        this.phoneNumber = phoneNumber;
        this.isPhoneNumberSet = true;

        return this;
    }

    public IUserFactory SetRole(UserRoleType role)
    {
        this.role = role;
        this.isRoleSet = true;

        return this;
    }

    public IUserFactory SetBirthDate(DateTime birthDate)
    {
        this.birthDate = birthDate;
        this.isBirthDateSet = true;

        return this;
    }
    
    public User Create()
    {
        if (!this.isFirstNameSet) throw new Exception("FirsName must be set");
        if (!this.isLastNameSet) throw new Exception("LastName must be set");
        if (!this.isAddressSet) throw new Exception("Address must be set");
        if (!this.isCountrySet) throw new Exception("Country must be set");
        if (!this.isCitySet) throw new Exception("City must be set");
        if (!this.isPassword) throw new Exception("Password must be set");
        if (!this.isEmailSet) throw new Exception("Email address must be set");
        if (!this.isPhoneNumberSet) throw new Exception("PhoneNumber must be set");
        if (!this.isRoleSet) throw new Exception("Role must be set");
        if (!this.isBirthDateSet) throw new Exception("BirthDate must be set");

        return new User
        {
            FirstName = this.firstName,
            LastName = this.lastName,
            Address = this.address,
            Country = this.country,
            City = this.city,
            PhoneNumber = this.phoneNumber,
            Password = this.password,
            Email = this.email,
            Role = this.role,
            BirthDate = this.birthDate,
        };
    }
}