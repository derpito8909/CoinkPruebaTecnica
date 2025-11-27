namespace Coink.Domain.Entities;

public class User : BaseEntity
{
    public string FullName { get; private set; } = default!;
    public string Phone { get; private set; } = default!;
    public string Address { get; private set; } = default!;

    public int CountryId { get; private set; }
    public int DepartmentId { get; private set; }
    public int MunicipalityId { get; private set; }

    public DateTime CreatedAt { get; private set; }

    public Country? Country { get; private set; }
    public Department? Department { get; private set; }
    public Municipality? Municipality { get; private set; }

    protected User() { }

    public User(
        string fullName,
        string phone,
        string address,
        int countryId,
        int departmentId,
        int municipalityId)
    {
        SetFullName(fullName);
        SetPhone(phone);
        SetAddress(address);
        SetLocation(countryId, departmentId, municipalityId);
        CreatedAt = DateTime.UtcNow;
    }

    public void SetFullName(string fullName)
    {
        if (string.IsNullOrWhiteSpace(fullName))
            throw new ArgumentException("El nombre completo es requerido", nameof(fullName));

        if (fullName.Length > 150)
            throw new ArgumentException("Este nombre esta muy largo", nameof(fullName));

        FullName = fullName.Trim();
    }

    public void SetPhone(string phone)
    {
        if (string.IsNullOrWhiteSpace(phone))
            throw new ArgumentException("El numero de telefono es requerido", nameof(phone));

        if (phone.Length > 20)
            throw new ArgumentException("El numero de telefono esta muy largo", nameof(phone));

        Phone = phone.Trim();
    }

    public void SetAddress(string address)
    {
        if (string.IsNullOrWhiteSpace(address))
            throw new ArgumentException("la direccion es requerida", nameof(address));

        if (address.Length > 250)
            throw new ArgumentException(" la direccion esta muy larga", nameof(address));

        Address = address.Trim();
    }

    public void SetLocation(int countryId, int departmentId, int municipalityId)
    {
        if (countryId <= 0) throw new ArgumentException("identificador de pais es invalido", nameof(countryId));
        if (departmentId <= 0) throw new ArgumentException("identificador del departamento es invalido", nameof(departmentId));
        if (municipalityId <= 0) throw new ArgumentException("identificacion de municipio invalido", nameof(municipalityId));

        CountryId = countryId;
        DepartmentId = departmentId;
        MunicipalityId = municipalityId;
    }
}