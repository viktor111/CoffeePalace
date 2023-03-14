using CoffeePalace.Data;
using CoffeePalace.Models.Entities;
using CoffeePalace.Services;
using CoffeePalace.Web;
using FakeItEasy;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;

namespace CoffeePalace.Tests.Services;

public class BasketServiceTests
{
    private readonly DbContextOptions<ApplicationDbContext> options;
    
    public BasketServiceTests()
    {
        options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;

        using var dbContext = new ApplicationDbContext(options);
        var seeder = new Seeder(dbContext);

        seeder.Seed().GetAwaiter().GetResult();
    }

    [Fact]
    public async Task ShouldAddProduct()
    {
        // Arrange
        using var dbContext = new ApplicationDbContext(options);
        var mockLogger = new Mock<ILogger<BasketService>>();
        var basketService = new BasketService(dbContext, mockLogger.Object);
        var product = A.Dummy<CoffeeProduct>();
        var user = A.Dummy<User>();
        var basket = new Basket
        {
            UserId = user.Id,
        };
        
        // Act
        await dbContext.CoffeeProducts.AddAsync(product);
        await dbContext.Baskets.AddAsync(basket);
        await dbContext.SaveChangesAsync();

        await basketService.AddProductToBasket(user.Id, product.Id);
        
        var basketItem = await dbContext.BasketItems
            .FirstOrDefaultAsync(x => x.CoffeeProductId == product.Id && x.BasketId == basket.Id);
        
        // Assert
        basketItem.Should().NotBeNull();
        basketItem.Quantity.Should().Be(1);
    }
    
    [Fact]
    public async Task ShouldAddQuantity()
    {
        // Arrange
        using var dbContext = new ApplicationDbContext(options);
        var mockLogger = new Mock<ILogger<BasketService>>();
        var basketService = new BasketService(dbContext, mockLogger.Object);
        var product = A.Dummy<CoffeeProduct>();
        var user = A.Dummy<User>();
        var basket = new Basket
        {
            UserId = user.Id,
        };
        
        // Act
        await dbContext.CoffeeProducts.AddAsync(product);
        await dbContext.Baskets.AddAsync(basket);
        await dbContext.SaveChangesAsync();

        await basketService.AddProductToBasket(user.Id, product.Id);
        await basketService.AddProductToBasket(user.Id, product.Id);
        
        var basketItem = await dbContext.BasketItems
            .FirstOrDefaultAsync(x => x.CoffeeProductId == product.Id && x.BasketId == basket.Id);
        
        // Assert
        basketItem.Should().NotBeNull();
        basketItem.Quantity.Should().Be(2);
    }
    
    [Fact]
    public async Task ShouldLoweQuantity()
    {
        // Arrange
        using var dbContext = new ApplicationDbContext(options);
        var mockLogger = new Mock<ILogger<BasketService>>();
        var basketService = new BasketService(dbContext, mockLogger.Object);
        var product = A.Dummy<CoffeeProduct>();
        var user = A.Dummy<User>();
        var basket = new Basket
        {
            UserId = user.Id,
        };
        
        // Act
        await dbContext.CoffeeProducts.AddAsync(product);
        await dbContext.Baskets.AddAsync(basket);
        await dbContext.SaveChangesAsync();

        await basketService.AddProductToBasket(user.Id, product.Id);
        await basketService.AddProductToBasket(user.Id, product.Id);
        await basketService.RemoveProductFromBasket(user.Id, product.Id);
        
        var basketItem = await dbContext.BasketItems
            .FirstOrDefaultAsync(x => x.CoffeeProductId == product.Id && x.BasketId == basket.Id);
        
        // Assert
        basketItem.Should().NotBeNull();
        basketItem.Quantity.Should().Be(1);
    }
    
    [Fact]
    public async Task ShouldRemoveProduct()
    {
        // Arrange
        using var dbContext = new ApplicationDbContext(options);
        var mockLogger = new Mock<ILogger<BasketService>>();
        var basketService = new BasketService(dbContext, mockLogger.Object);
        var product = A.Dummy<CoffeeProduct>();
        var user = A.Dummy<User>();
        var basket = new Basket
        {
            UserId = user.Id,
        };
        
        // Act
        await dbContext.CoffeeProducts.AddAsync(product);
        await dbContext.Baskets.AddAsync(basket);
        await dbContext.SaveChangesAsync();

        await basketService.AddProductToBasket(user.Id, product.Id);
        await basketService.RemoveProductFromBasket(user.Id, product.Id);
        
        var basketItem = await dbContext.BasketItems
            .FirstOrDefaultAsync(x => x.CoffeeProductId == product.Id && x.BasketId == basket.Id);
        
        // Assert
        basketItem.Should().BeNull();
    }
    
    [Fact]
    public async Task ShouldClearCart()
    {
        // Arrange
        using var dbContext = new ApplicationDbContext(options);
        var mockLogger = new Mock<ILogger<BasketService>>();
        var basketService = new BasketService(dbContext, mockLogger.Object);
        var product = A.Dummy<CoffeeProduct>();
        var user = A.Dummy<User>();
        var basket = new Basket
        {
            UserId = user.Id,
        };
        
        // Act
        await dbContext.CoffeeProducts.AddAsync(product);
        await dbContext.Baskets.AddAsync(basket);
        await dbContext.SaveChangesAsync();

        await basketService.AddProductToBasket(user.Id, product.Id);
        await basketService.AddProductToBasket(user.Id, product.Id);
        await basketService.ClearBasket(user.Id);   
        
        var basketItem = await dbContext.BasketItems
            .Where(x => x.BasketId == basket.Id)
            .ToListAsync();
        
        // Assert
        basketItem.Should().HaveCountLessThan(1);
    }
    
    [Fact]
    public async Task ShouldNotClearEmptyCart()
    {
        // Arrange
        using var dbContext = new ApplicationDbContext(options);
        var mockLogger = new Mock<ILogger<BasketService>>();
        var basketService = new BasketService(dbContext, mockLogger.Object);
        var product = A.Dummy<CoffeeProduct>();
        var user = A.Dummy<User>();
        var basket = new Basket
        {
            UserId = user.Id,
        };
        
        // Act
        await dbContext.CoffeeProducts.AddAsync(product);
        await dbContext.Baskets.AddAsync(basket);
        await dbContext.SaveChangesAsync();
        
        var result = await basketService.ClearBasket(user.Id);   
        
        var basketItem = await dbContext.BasketItems
            .Where(x => x.BasketId == basket.Id)
            .ToListAsync();
        
        // Assert
        result.Succeeded.Should().BeFalse();
        basketItem.Should().HaveCount(0);
    }
}