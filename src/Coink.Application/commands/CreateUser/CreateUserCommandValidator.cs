using FluentValidation;

namespace Coink.Application.commands.CreateUser;

public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator()
    {
        RuleFor(x => x.FullName)
            .NotEmpty().WithMessage("El nombre completo es necesario")
            .MaximumLength(150).WithMessage("El nombre debe contener una longitud maxima de 150 caracteres");

        RuleFor(x => x.Phone)
            .NotEmpty().WithMessage("El telefono es necesario")
            .MaximumLength(10).WithMessage("El telefono debe tener una longiutd maxima de 10");

        RuleFor(x => x.Address)
            .NotEmpty().WithMessage("La direccion es necesaria")
            .MaximumLength(250).WithMessage("La direccion debe tener una longitud de 250 caracteres");

        RuleFor(x => x.CountryId)
            .GreaterThan(0).WithMessage("El Id del pais debe ser diferente de cero");

        RuleFor(x => x.DepartmentId)
            .GreaterThan(0).WithMessage("El Id del departamento debe ser diferente de cero");

        RuleFor(x => x.MunicipalityId)
            .GreaterThan(0).WithMessage("El Id del municipio debe ser diferente de cero");
    }
}
