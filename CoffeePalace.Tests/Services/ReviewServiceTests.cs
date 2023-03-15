using CoffeePalace.Data;
using CoffeePalace.Models.Entities;
using CoffeePalace.Models.Types;
using CoffeePalace.Services;
using FakeItEasy;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;

namespace CoffeePalace.Tests.Services;

public class ReviewServiceTests
{
    private readonly DbContextOptions<ApplicationDbContext> options;

    public ReviewServiceTests()
    {
        options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;
    }
    
    [Fact]
    public async Task ShouldAddReview()
    {
        // Arrange
        using var dbContext = new ApplicationDbContext(options);
        var mockLogger = new Mock<ILogger<ReviewService>>();
        var reviewService = new ReviewService(dbContext, mockLogger.Object);
        var review = A.Dummy<Review>();

        // Act
        var saved = await reviewService.Save(review);
        var reviews = await dbContext.Reviews.ToListAsync();

        // Assert
        saved.Succeeded
            .Should()
            .BeTrue();

        saved.Data
            .Should()
            .BeEquivalentTo(review);

        reviews
            .Should()
            .Contain(saved.Data)
            .And
            .HaveCountGreaterOrEqualTo(1);
    }
}