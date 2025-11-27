namespace Coink.Domain.Entities;

public class Municipality : BaseEntity
{
    public string Name { get; private set; } = default!;
    public int DepartmentId { get; private set; }

    public Department? Department { get; private set; }

    protected Municipality() { }

    public Municipality(string name, int departmentId)
    {
        SetName(name);
        SetDepartment(departmentId);
    }

    public void SetName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("El nombre del municipio es requerido", nameof(name));

        Name = name.Trim();
    }

    public void SetDepartment(int departmentId)
    {
        if (departmentId <= 0)
            throw new ArgumentException("Identificador del departamento es invalido", nameof(departmentId));

        DepartmentId = departmentId;
    }
}
