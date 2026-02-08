using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Prueba_Completa_NET.Data;
using Prueba_Completa_NET.DTOs;

namespace Prueba_Completa_NET.Validators
{
    
        public class ClienteUpdateValidator : AbstractValidator<ClienteCreateDTO> 
        {
            private readonly AppDbContext _context;

            public ClienteUpdateValidator(AppDbContext context)
            {
                _context = context;

                RuleFor(c => c.Nombre)
                    .NotEmpty().WithMessage("El nombre es requerido.")
                    .Length(3, 100).WithMessage("El nombre debe tener entre 3 y 100 caracteres.");

                RuleFor(c => c.Identidad)
                    .NotEmpty().WithMessage("La identidad es requerida.")
                    .Matches(@"^\d{4}-\d{4}-\d{5}$")
                    .WithMessage("El formato de identidad es inválido. Ej: 0801-1988-55555")
                    .MustAsync(async (cliente, identidad, cancellation) =>
                    {
                        // No debe existir otra identidad igual en la tabla, excepto la del cliente que se actualiza
                        return !await _context.Clientes
                            .AnyAsync(c => c.Identidad == identidad && c.ClienteId != cliente.ClienteId, cancellation);
                    })
                    .WithMessage((cliente, identidad) => $"Ya existe un cliente con la identidad {identidad}");
            }
        }
}
