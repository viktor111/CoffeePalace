using CoffeePalace.Data;
using CoffeePalace.Models.Entities;
using CoffeePalace.Models.Types;
using Microsoft.EntityFrameworkCore;
using BC = BCrypt.Net.BCrypt;


namespace CoffeePalace.Web;

public class Seeder
{
    private readonly ApplicationDbContext dbContext;
    
    public Seeder(ApplicationDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task Seed()
    {
        // NOTE: Cannot use Task.WhenAll because DbContext cannot be accessed concurrently from multiple threads
        await SeedProduct();
        await SeedCustomer();
        await SeedModerator();
        await SeedAdmin();
    }

    private async Task SeedProduct()
    {
        var product = new CoffeeProduct
        {
            Name = "Red nut coffee",
            Price = 10.00m,
            CountryOfOrigin = "Brazil",
            Description = "Very good taste in this coffee",
            IsInStock = true,
            RoastLevel = RoastLevelType.Dark,
            CaffeineContent = CaffeineContentType.High,
            BeanType = BeanType.Robusta,
            GrindType = GrindType.ExtraFine,
        };
        
        if (!await this.dbContext.CoffeeProducts.AnyAsync(p => p.Name == product.Name))
        {
            await this.dbContext.CoffeeProducts.AddAsync(product);

            var imageName = Path.Combine(Directory.GetCurrentDirectory(), "Assets/seed-image.jpeg");
            
            var bytes = await File.ReadAllBytesAsync(imageName);

            var image = new ImageData
            {
                Name = imageName,
                Data = bytes,
                ExternalId = product.Id
            };

            await this.dbContext.ImageDatas.AddAsync(image);

            var customer = new User
            {
                FirstName = "Bobby",
                LastName = "Kenedy",
                Address = "yanko ot somlia 99",
                Country = "USA",
                City = "New York",
                PhoneNumber = "+1-895313156",
                Password = HashPassword("goober"),
                Email = "viktor.draganov1@outlook.com",
                Role = UserRoleType.Customer,
                BirthDate = new DateTime(1988, 4, 5),
            };

            await this.dbContext.Users.AddAsync(customer);
            
            var basket = new Basket
            {
                UserId = customer.Id,
            };

            await this.dbContext.Baskets.AddAsync(basket);
            
            await this.dbContext.BasketItems.AddAsync(new BasketItem
            {
                BasketId = basket.Id,
                Basket = basket,
                CoffeeProduct = product,
                CoffeeProductId = product.Id,
                Quantity = 3
            });

            var review = new Review
            {
                Text = "Very good coffe from brazil I drink it 10 times a day and I like the effects.",
                Rating = RatingType.Excellent,
                UserId = customer.Id,
                CoffeeProductId = product.Id,
            };

            await this.dbContext.Reviews.AddAsync(review);

            await this.dbContext.SaveChangesAsync();
        }
    }

    private async Task SeedAdmin()
    {
        var admin = new User
        {
            FirstName = "Viktor",
            LastName = "Draganov",
            Address = "mladost 14",
            Country = "Bulgaria",
            City = "Stara Zagora",
            PhoneNumber = "+359-895313156",
            Password = HashPassword("viktor11"),
            Email = "swifrorlilko@gmail.com",
            Role = UserRoleType.Administrator,
            BirthDate = new DateTime(1999, 12, 11),
        };

        if (!await this.dbContext.Users.AnyAsync(u => u.FirstName == admin.FirstName))
        {
            await this.dbContext.Users.AddAsync(admin);

            var basket = new Basket
            {
                UserId = admin.Id,
            };

            await this.dbContext.Baskets.AddAsync(basket);

            await this.dbContext.SaveChangesAsync();
        }
    }

    private async Task SeedModerator()
    {
        var moderator = new User
        {
            FirstName = "Erol",
            LastName = "Yusuf",
            Address = "arba 89",
            Country = "Turkey",
            City = "Istanbul",
            PhoneNumber = "+90-895313156",
            Password = HashPassword("erolbezoko123"),
            Email = "azohop@gmail.com",
            Role = UserRoleType.Moderator,
            BirthDate = new DateTime(1977, 9, 10),
        };

        if (!await this.dbContext.Users.AnyAsync(u => u.FirstName == moderator.FirstName))
        {
            await this.dbContext.Users.AddAsync(moderator);

            var basket = new Basket
            {
                UserId = moderator.Id,
            };

            await this.dbContext.Baskets.AddAsync(basket);
            
            await this.dbContext.SaveChangesAsync();
        }

    }

    private async Task SeedCustomer()
    {
        var customer = new User
        {
            FirstName = "Isvan",
            LastName = "Benze",
            Address = "kurabia suma 100",
            Country = "Uganda",
            City = "Kampala",
            PhoneNumber = "+256-895313156",
            Password = HashPassword("knuckle"),
            Email = "draganovop@gmail.com",
            Role = UserRoleType.Customer,
            BirthDate = new DateTime(1933, 1, 1),
        };
        
        if (!await this.dbContext.Users.AnyAsync(u => u.FirstName == customer.FirstName))
        {
            await this.dbContext.Users.AddAsync(customer);

            var basket = new Basket
            {
                UserId = customer.Id,
            };

            await this.dbContext.Baskets.AddAsync(basket);

            await this.dbContext.SaveChangesAsync();
        }
    }

    private static string HashPassword(string password)
    {
        var salt = BC.GenerateSalt();

        var hash = BC.HashPassword(password, salt);

        return hash;
    }

}