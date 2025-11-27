using MediatR;
using Coink.Application.Users.Dtos;

namespace Coink.Application.Users.Queries.GetUserById;

public record GetUserByIdQuery(int Id) : IRequest<UserDto?>;
