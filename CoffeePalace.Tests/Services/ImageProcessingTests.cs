using CoffeePalace.Models.Entities;
using CoffeePalace.Services;
using FluentAssertions;

namespace CoffeePalace.Tests.Services;

public class ImageProcessingTests
{
    [Fact]
    public async Task ShouldProcessImage()
    {
        // Arrange
        var imageName = Path.Combine(Directory.GetCurrentDirectory(), "Assets/seed-image.jpeg");
        var bytes = await File.ReadAllBytesAsync(imageName);
        var imageData = new ImageData
        {
            Name = imageName,
            Data = bytes,
            ExternalId = "testId"
        };

        var imageService = new ImageProcessingService();
        
        // Act
        var processedImage = await imageService.Process(imageData);

        // Assert
        processedImage.Succeeded
            .Should()
            .BeTrue();
        
        processedImage.Data.Data
            .Should()
            .NotBeNull()
            .And
            .NotBeEmpty()
            .And
            .HaveSameCount(bytes);
        
        processedImage.Data.Thumbnail
            .Should()
            .NotBeNull()
            .And
            .NotBeEmpty()
            .And
            .HaveCountLessThan(processedImage.Data.Data.Length);
        
        processedImage.Data.FullScreen
            .Should()
            .NotBeNull()
            .And
            .NotBeEmpty()
            .And
            .HaveCountLessThan(processedImage.Data.Data.Length);
    }
}