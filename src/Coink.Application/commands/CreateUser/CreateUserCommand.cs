using MediatR;
using Coink.Application.Users.Dtos;

namespace Coink.Application.commands.CreateUser;


public record CreateUserCommand(
    string FullName,
    string Phone,
    string Address,
    int CountryId,
    int DepartmentId,
    int MunicipalityId
) : IRequest<UserDto>;
