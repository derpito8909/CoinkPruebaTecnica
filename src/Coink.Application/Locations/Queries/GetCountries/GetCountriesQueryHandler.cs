using AutoMapper;
using MediatR;
using Coink.Application.Locations.Dtos;
using Coink.Application.Repositories.Locations;

namespace Coink.Application.Locations.Queries.GetCountries;

public class GetCountriesQueryHandler :
    IRequestHandler<GetCountriesQuery, IReadOnlyList<CountryDto>>
{
    private readonly ILocationReadRepository _locationReadRepository;
    private readonly IMapper _mapper;

    public GetCountriesQueryHandler(
        ILocationReadRepository locationReadRepository,
        IMapper mapper)
    {
        _locationReadRepository = locationReadRepository;
        _mapper = mapper;
    }

    public async Task<IReadOnlyList<CountryDto>> Handle(
        GetCountriesQuery request,
        CancellationToken cancellationToken)
    {
        var countries = await _locationReadRepository.GetCountriesAsync(cancellationToken);
        return _mapper.Map<IReadOnlyList<CountryDto>>(countries);
    }
}
