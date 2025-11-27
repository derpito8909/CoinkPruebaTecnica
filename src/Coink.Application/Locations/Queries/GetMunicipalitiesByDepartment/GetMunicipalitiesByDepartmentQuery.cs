using MediatR;
using Coink.Application.Locations.Dtos;

namespace Coink.Application.Locations.Queries.GetMunicipalitiesByDepartment;

public record GetMunicipalitiesByDepartmentQuery(int DepartmentId)
    : IRequest<IReadOnlyList<MunicipalityDto>>;
