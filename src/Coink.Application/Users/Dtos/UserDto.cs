namespace Coink.Application.Users.Dtos;

public class UserDto
{
    public int Id { get; set; }
    public string FullName { get; set; } = default!;
    public string Phone { get; set; } = default!;
    public string Address { get; set; } = default!;
    public int CountryId { get; set; }
    public int DepartmentId { get; set; }
    public int MunicipalityId { get; set; }
    public DateTime CreatedAt { get; set; }
}
