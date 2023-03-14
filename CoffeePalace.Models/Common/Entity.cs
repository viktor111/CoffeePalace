namespace CoffeePalace.Models.Common;

public abstract class Entity
{
    public string Id { get; } = Guid.NewGuid().ToString();

    public DateTime CreatedAt { get; } = DateTime.UtcNow;
}