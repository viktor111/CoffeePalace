using CoffeePalace.Data;
using CoffeePalace.Models.Entities;
using CoffeePalace.Services;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;

namespace CoffeePalace.Tests.Services;

public class ImageDataServiceTests
{
    private readonly DbContextOptions<ApplicationDbContext> options;

    public ImageDataServiceTests()
    {
        options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;
    }

    [Fact]
    public async Task ShouldSaveImageData()
    {
        // Arrange
        using var dbContext = new ApplicationDbContext(options);
        var mockLogger = new Mock<ILogger<ImageDataService>>();
        var imageProcessingService = new ImageProcessingService();
        var imageDataService = new ImageDataService(dbContext, mockLogger.Object, imageProcessingService);
        var imageName = Path.Combine(Directory.GetCurrentDirectory(), "Assets/seed-image.jpeg");
        var bytes = await File.ReadAllBytesAsync(imageName);
        var imageData = new ImageData
        {
            Name = imageName,
            Data = bytes,
            ExternalId = "externalIdTest"
        };
        
        // Act
        var saved = await imageDataService.Save(imageData);
        var images = await dbContext.ImageDatas.ToListAsync();

        // Assert
        saved.Succeeded
            .Should()
            .BeTrue();
        
        saved.Data
            .Should()
            .BeEquivalentTo(imageData);
        
        images
            .Should()
            .Contain(saved.Data)
            .And
            .HaveCountGreaterOrEqualTo(1);
    }
    
    [Fact]
    public async Task ShouldUpdateImageData()
    {
        // Arrange
        using var dbContext = new ApplicationDbContext(options);
        var mockLogger = new Mock<ILogger<ImageDataService>>();
        var imageProcessingService = new ImageProcessingService();
        var imageDataService = new ImageDataService(dbContext, mockLogger.Object, imageProcessingService);
        var imageName = Path.Combine(Directory.GetCurrentDirectory(), "Assets/seed-image.jpeg");
        var bytes = await File.ReadAllBytesAsync(imageName);
        var imageData = new ImageData
        {
            Name = imageName,
            Data = bytes,
            ExternalId = "externalIdTest"
        };
        var imageNameForUpdate = Path.Combine(Directory.GetCurrentDirectory(), "Assets/seed-image.jpeg");
        var bytesForUpdate = await File.ReadAllBytesAsync(imageName);
        var imageDataForUpdate = new ImageData
        {
            Name = imageNameForUpdate,
            Data = bytesForUpdate,
            ExternalId = "externalIdTest"
        };
        
        // Act
        var saved = await imageDataService.Save(imageData);
        
        
        // Assert
    }
    
    [Fact]
    public async Task ShouldDeleteImageData()
    {
        // Arrange
        using var dbContext = new ApplicationDbContext(options);
        var mockLogger = new Mock<ILogger<ImageDataService>>();
        var imageProcessingService = new ImageProcessingService();
        var imageDataService = new ImageDataService(dbContext, mockLogger.Object, imageProcessingService);
        var imageName = Path.Combine(Directory.GetCurrentDirectory(), "Assets/seed-image.jpeg");
        var bytes = await File.ReadAllBytesAsync(imageName);
        var imageData = new ImageData
        {
            Name = imageName,
            Data = bytes,
            ExternalId = "externalIdTest"
        };
        
        // Act
        var saved = await imageDataService.Save(imageData);
        var imagesBeforeDelete = await dbContext.ImageDatas.ToListAsync(); 
        await imageDataService.Delete(saved.Data.Id);
        var imagesAfterDelete = await dbContext.ImageDatas.ToListAsync();

        // Assert
        imagesBeforeDelete
            .Should()
            .Contain(saved.Data)
            .And
            .HaveCountGreaterOrEqualTo(1);
        
        imagesAfterDelete
            .Should()
            .HaveCountLessThan(imagesBeforeDelete.Count);
    }
}