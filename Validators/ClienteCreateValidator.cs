using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Prueba_Completa_NET.Data;
using Prueba_Completa_NET.DTOs;

namespace Prueba_Completa_NET.Validators
{
    public class ClienteCreateValidator : AbstractValidator<ClienteCreateDTO>
    {
        private readonly AppDbContext _context;
        public ClienteCreateValidator( AppDbContext context )
        {
            /*
             ●
                `clienteId` debe ser 0
                ●
                `nombre` requerido (3-100 caracteres)
                ●
                `identidad` requerida, única, formato válido formato "identidad": "0801-1988-55555"*/

            _context = context;

            RuleFor(c => c.ClienteId)
                .Equal(0).WithMessage("El clienteId debe ser 0.");

            RuleFor(c => c.Nombre)
                .NotEmpty().WithMessage("El nombre es requerido.")
                .Length(3, 100).WithMessage("El nombre debe tener entre 3 y 100 caracteres.");

            RuleFor(c => c.Identidad)
                .NotEmpty().WithMessage("La identidad es requerida.")
                .Matches(@"^\d{4}-\d{4}-\d{5}$")
                .WithMessage("El formato de identidad es inválido. Ej: 0801-1988-55555")
                .MustAsync(async (identidad, cancellation) =>
                {
                    // Verifica que no exista otra identidad igual en la base de datos
                    return !await _context.Clientes.AnyAsync(c => c.Identidad == identidad, cancellation);
                })
                .WithMessage((cliente, identidad) => $"Ya existe un cliente con la identidad {identidad}");

        }

    }
}
