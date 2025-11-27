using AutoMapper;
using MediatR;
using Coink.Application.Locations.Dtos;
using Coink.Application.Repositories.Locations;

namespace Coink.Application.Locations.Queries.GetDepartmentsByCountry;

public class GetDepartmentsByCountryQueryHandler :
    IRequestHandler<GetDepartmentsByCountryQuery, IReadOnlyList<DepartmentDto>>
{
    private readonly ILocationReadRepository _locationReadRepository;
    private readonly IMapper _mapper;

    public GetDepartmentsByCountryQueryHandler(
        ILocationReadRepository locationReadRepository,
        IMapper mapper)
    {
        _locationReadRepository = locationReadRepository;
        _mapper = mapper;
    }

    public async Task<IReadOnlyList<DepartmentDto>> Handle(
        GetDepartmentsByCountryQuery request,
        CancellationToken cancellationToken)
    {
        var departments = await _locationReadRepository
            .GetDepartmentsByCountryAsync(request.CountryId, cancellationToken);

        return _mapper.Map<IReadOnlyList<DepartmentDto>>(departments);
    }
}
