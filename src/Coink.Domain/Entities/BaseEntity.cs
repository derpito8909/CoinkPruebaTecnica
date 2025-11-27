namespace Coink.Domain.Entities;

public abstract class BaseEntity
{
    public int Id { get; private set; }

    public void SetId(int id)
    {
        if (id <= 0)
            throw new ArgumentException("El Id debe ser mayor a 0", nameof(id));

        Id = id;
    }
}
