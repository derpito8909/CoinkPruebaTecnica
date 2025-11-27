using AutoMapper;
using MediatR;
using Coink.Application.Users.Dtos;
using Coink.Application.Repositories;
using Coink.Domain.Entities;

namespace Coink.Application.commands.CreateUser;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, UserDto>
{
    private readonly IUserWriteRepository _userWriteRepository;
    private readonly IMapper _mapper;

    public CreateUserCommandHandler(
        IUserWriteRepository userWriteRepository,
        IMapper mapper)
    {
        _userWriteRepository = userWriteRepository;
        _mapper = mapper;
    }

    public async Task<UserDto> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var user = new User(
            request.FullName,
            request.Phone,
            request.Address,
            request.CountryId,
            request.DepartmentId,
            request.MunicipalityId);

        // Guardamos vía repositorio (que internamente llamará al SP/función)
        var createdUser = await _userWriteRepository.CreateUserAsync(user, cancellationToken);

        // Mapeamos entidad de dominio -> DTO de salida
        var userDto = _mapper.Map<UserDto>(createdUser);

        return userDto;
    }
}
