using CoffeePalace.Data;
using CoffeePalace.Models.Entities;
using CoffeePalace.Models.Types;
using CoffeePalace.Services;
using CoffeePalace.Services.Common;
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
        var mockValidator = new Mock<IValidator<Review>>();
        var reviewService = new ReviewService(dbContext, mockLogger.Object, mockValidator.Object);
        var review = A.Dummy<Review>();
        review.CoffeeProductId = "test";
        review.Text = "Good Review from user on product...";
        review.UserId = "test1";
        mockValidator.Setup(x => x.Validate(review)).Returns(Result.Success);

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