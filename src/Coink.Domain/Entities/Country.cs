namespace Coink.Domain.Entities;

public class Country : BaseEntity
{
    public string Name { get; private set; } = default!;
    public string IsoCode { get; private set; } = default!;

    protected Country() { }

    public Country(string name, string isoCode)
    {
        SetName(name);
        SetIsoCode(isoCode);
    }

    public void SetName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("El nombre del pais es requerido", nameof(name));

        Name = name.Trim();
    }

    public void SetIsoCode(string isoCode)
    {
        if (string.IsNullOrWhiteSpace(isoCode))
            throw new ArgumentException("El codigo ISO es requerido", nameof(isoCode));

        if (isoCode.Length > 5)
            throw new ArgumentException("El codigo ISO debe tener mas de 5 caracteres", nameof(isoCode));

        IsoCode = isoCode.Trim().ToUpperInvariant();
    }
}
