using CoffeePalace.Data;
using CoffeePalace.Models.Entities;
using CoffeePalace.Services;
using FakeItEasy;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;

namespace CoffeePalace.Tests.Services;

public class UserServiceTests
{
    private readonly DbContextOptions<ApplicationDbContext> options;

    public UserServiceTests()
    {
        options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;
    }

    [Fact]
    public async Task ShouldAddUser()
    {
        // Arrange
        using var dbContext = new ApplicationDbContext(options);
        var mockLogger = new Mock<ILogger<UserService>>();
        var userService = new UserService(dbContext, mockLogger.Object);
        var user = A.Dummy<User>();
        
        // Act
        var saved = await userService.Save(user);
        var users = await dbContext.Users.ToListAsync();
        
        // Assert
        saved.Succeeded
            .Should()
            .BeTrue();
        
        users.Should()
            .Contain(saved.Data)
            .And
            .HaveCountGreaterOrEqualTo(1);
    }
}