namespace Coink.Domain.Entities;

public class Department : BaseEntity
{
    public string Name { get; private set; } = default!;
    public int CountryId { get; private set; }
    public Country? Country { get; private set; }

    protected Department() { }

    public Department(string name, int countryId)
    {
        SetName(name);
        SetCountry(countryId);
    }

    public void SetName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("El departamento es requerido", nameof(name));

        Name = name.Trim();
    }

    public void SetCountry(int countryId)
    {
        if (countryId <= 0)
            throw new ArgumentException("identificador de pais invalido", nameof(countryId));

        CountryId = countryId;
    }
}
