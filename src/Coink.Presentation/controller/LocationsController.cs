using MediatR;
using Microsoft.AspNetCore.Mvc;
using Coink.Application.Locations.Dtos;
using Coink.Application.Locations.Queries.GetCountries;
using Coink.Application.Locations.Queries.GetDepartmentsByCountry;
using Coink.Application.Locations.Queries.GetMunicipalitiesByDepartment;

namespace Coink.Presentation.controller;

[ApiController]
[Route("api/[controller]")]
public class LocationsController : ControllerBase
{
    private readonly IMediator _mediator;

    public LocationsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Obtiene la lista de países.
    /// </summary>
    [HttpGet("countries")]
    public async Task<ActionResult<IReadOnlyList<CountryDto>>> GetCountries()
    {
        var result = await _mediator.Send(new GetCountriesQuery());
        return Ok(result);
    }

    /// <summary>
    /// Obtiene la lista de departamentos para un país.
    /// </summary>
    [HttpGet("departments")]
    public async Task<ActionResult<IReadOnlyList<DepartmentDto>>> GetDepartments([FromQuery] int countryId)
    {
        if (countryId <= 0)
            return BadRequest("El Id de country debe ser mayor a 0");

        var result = await _mediator.Send(new GetDepartmentsByCountryQuery(countryId));
        return Ok(result);
    }

    /// <summary>
    /// Obtiene la lista de municipios para un departamento.
    /// </summary>
    [HttpGet("municipalities")]
    public async Task<ActionResult<IReadOnlyList<MunicipalityDto>>> GetMunicipalities([FromQuery] int departmentId)
    {
        if (departmentId <= 0)
            return BadRequest("El ID del departamento debe ser mayor a 0");

        var result = await _mediator.Send(new GetMunicipalitiesByDepartmentQuery(departmentId));
        return Ok(result);
    }
}
