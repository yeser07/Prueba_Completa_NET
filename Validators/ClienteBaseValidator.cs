using FluentValidation;
using Prueba_Completa_NET.DTOs;

public abstract class ClienteBaseValidator<T> : AbstractValidator<T>
    where T : ClienteBaseDTO
{
    protected ClienteBaseValidator()
    {
 
        RuleFor(c => c.Nombre)
            .NotEmpty().WithMessage("El nombre es requerido.")
            .Length(3, 100).WithMessage("El nombre debe tener entre 3 y 100 caracteres.");

        RuleFor(c => c.Identidad)
            .NotEmpty().WithMessage("La identidad es requerida.")
            .Matches(@"^\d{4}-\d{4}-\d{5}$")
            .WithMessage("Formato inválido. Ej: 0801-1988-55555");
    }
}
