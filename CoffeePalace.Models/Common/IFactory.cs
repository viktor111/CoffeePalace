namespace CoffeePalace.Models.Common;

public interface IFactory<out TEntity>
    where TEntity : Entity
{
    TEntity Create();
}