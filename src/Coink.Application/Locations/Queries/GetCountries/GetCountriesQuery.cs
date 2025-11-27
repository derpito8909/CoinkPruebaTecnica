using MediatR;
using Coink.Application.Locations.Dtos;

namespace Coink.Application.Locations.Queries.GetCountries;

public record GetCountriesQuery() : IRequest<IReadOnlyList<CountryDto>>;