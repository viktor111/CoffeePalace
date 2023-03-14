using Bogus;
using CoffeePalace.Models.Entities;
using CoffeePalace.Models.Types;
using FakeItEasy;

namespace CoffeePalace.Tests.Fakes;

public class UserFakes
{
    public class UserDummyFactory : IDummyFactory
    {
        public bool CanCreate(Type type) => type == typeof(User);

        public object? Create(Type type) => UserData.GetUser();

        public Priority Priority => Priority.Default;
    }

    public static class UserData
    {
        public static User GetUser()
        {
            var fake = new Faker<User>()
                .CustomInstantiator(x => new User
                {
                    FirstName = x.Name.FirstName(),
                    LastName = x.Name.LastName(),
                    Address = x.Address.StreetName(),
                    Country = x.Address.Country(),
                    City = x.Address.City(),
                    PhoneNumber = x.Phone.PhoneNumber(),
                    Email = x.Internet.Email(),
                    Password = x.Internet.Password(),
                    Role = x.Random.Enum<UserRoleType>(),
                    BirthDate = DateTime.UtcNow,
                });

            return fake;
        }
    }
}
    