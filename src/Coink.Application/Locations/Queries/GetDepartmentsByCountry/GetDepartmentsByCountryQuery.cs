using MediatR;
using Coink.Application.Locations.Dtos;

namespace Coink.Application.Locations.Queries.GetDepartmentsByCountry;

public record GetDepartmentsByCountryQuery(int CountryId)
    : IRequest<IReadOnlyList<DepartmentDto>>;
