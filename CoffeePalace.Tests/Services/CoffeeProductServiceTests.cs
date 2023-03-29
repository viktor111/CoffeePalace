using CoffeePalace.Data;
using CoffeePalace.Models.Entities;
using CoffeePalace.Models.Types;
using CoffeePalace.Services;
using CoffeePalace.Services.Common;
using CoffeePalace.Web;
using FakeItEasy;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;

namespace CoffeePalace.Tests.Services;

public class CoffeeProductServiceTests
{
    private readonly DbContextOptions<ApplicationDbContext> options;
    
    public CoffeeProductServiceTests()
    {
        options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;
    }

    [Fact]
    public async Task ShouldSaveProduct()
    {
        // Arrange
        using var dbContext = new ApplicationDbContext(options);
        var mockLogger = new Mock<ILogger<CoffeeProductService>>();
        var mockValidator = new Mock<IValidator<CoffeeProduct>>();

        var coffeeProductService = new CoffeeProductService(dbContext, mockLogger.Object, mockValidator.Object);
        var product = A.Dummy<CoffeeProduct>();
        mockValidator.Setup(x => x.Validate(product)).Returns(Result.Success);

        // Act
        var saved = await coffeeProductService.Save(product);
        var products = await dbContext.CoffeeProducts.ToListAsync();
        
        //Assert
        saved.Succeeded
            .Should()
            .BeTrue();
        
        saved.Data
            .Should()
            .BeEquivalentTo(product);
        
        products
            .Should()
            .Contain(saved.Data)
            .And
            .HaveCountGreaterOrEqualTo(1);

    }

    [Fact]
    public async Task ShouldUpdateProduct()
    {
        // Arrange
        using var dbContext = new ApplicationDbContext(options);
        var mockLogger = new Mock<ILogger<CoffeeProductService>>();
        var mockValidator = new Mock<IValidator<CoffeeProduct>>();

        var coffeeProductService = new CoffeeProductService(dbContext, mockLogger.Object, mockValidator.Object);
        var product = A.Dummy<CoffeeProduct>();
        mockValidator.Setup(x => x.Validate(product)).Returns(Result.Success);

        // Act
        var saved = await coffeeProductService.Save(product);
        var newProductData = new CoffeeProduct
        {
            Name = "TEST P NAME",
            Price = 10,
            CountryOfOrigin = product.CountryOfOrigin,
            Description = product.Description,
            IsInStock = product.IsInStock,
            RoastLevel = product.RoastLevel,
            CaffeineContent = product.CaffeineContent,
            BeanType = product.BeanType,
            GrindType = product.GrindType,
        };
        await coffeeProductService.Update(saved.Data.Id, newProductData);
        var updated = await dbContext.CoffeeProducts.FirstOrDefaultAsync(x => x.Id == product.Id);
        
        //Assert
        saved.Succeeded
            .Should()
            .BeTrue();
        
        saved
            .Should()
            .NotBeEquivalentTo(updated);
    }

    [Fact]
    public async Task ShouldDeleteProduct()
    {
        // Arrange
        using var dbContext = new ApplicationDbContext(options);
        var mockLogger = new Mock<ILogger<CoffeeProductService>>();
        var mockValidator = new Mock<IValidator<CoffeeProduct>>();
        var coffeeProductService = new CoffeeProductService(dbContext, mockLogger.Object, mockValidator.Object);
        var product = A.Dummy<CoffeeProduct>();
        mockValidator.Setup(x => x.Validate(product)).Returns(Result.Success);

        // Act
        var saved = await coffeeProductService.Save(product);
        var productsBeforeDelete = await dbContext.CoffeeProducts.ToListAsync();

        await coffeeProductService.Delete(saved.Data.Id);
        var productsAfterDelete = await dbContext.CoffeeProducts.ToListAsync();
        
        //Assert
        saved.Succeeded
            .Should()
            .BeTrue();
    }
}