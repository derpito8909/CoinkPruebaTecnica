using AutoMapper;
using MediatR;
using Coink.Application.Users.Dtos;
using Coink.Application.Repositories;

namespace Coink.Application.Users.Queries.GetUserById;

public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, UserDto?>
{
    private readonly IUserReadRepository _userReadRepository;
    private readonly IMapper _mapper;

    public GetUserByIdQueryHandler(
        IUserReadRepository userReadRepository,
        IMapper mapper)
    {
        _userReadRepository = userReadRepository;
        _mapper = mapper;
    }

    public async Task<UserDto?> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var user = await _userReadRepository.GetByIdAsync(request.Id, cancellationToken);

        if (user is null)
            return null;

        return _mapper.Map<UserDto>(user);
    }
}
