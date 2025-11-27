using AutoMapper;
using MediatR;
using Coink.Application.Locations.Dtos;
using Coink.Application.Repositories.Locations;

namespace Coink.Application.Locations.Queries.GetMunicipalitiesByDepartment;

public class GetMunicipalitiesByDepartmentQueryHandler :
    IRequestHandler<GetMunicipalitiesByDepartmentQuery, IReadOnlyList<MunicipalityDto>>
{
    private readonly ILocationReadRepository _locationReadRepository;
    private readonly IMapper _mapper;

    public GetMunicipalitiesByDepartmentQueryHandler(
        ILocationReadRepository locationReadRepository,
        IMapper mapper)
    {
        _locationReadRepository = locationReadRepository;
        _mapper = mapper;
    }

    public async Task<IReadOnlyList<MunicipalityDto>> Handle(
        GetMunicipalitiesByDepartmentQuery request,
        CancellationToken cancellationToken)
    {
        var municipalities = await _locationReadRepository
            .GetMunicipalitiesByDepartmentAsync(request.DepartmentId, cancellationToken);

        return _mapper.Map<IReadOnlyList<MunicipalityDto>>(municipalities);
    }
}
