using Prueba_Completa_NET.Data;
using Prueba_Completa_NET.DTOs;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Prueba_Completa_NET.Validators
{
    public class ClienteCreateValidator
        : ClienteBaseValidator<ClienteCreateDTO> 
    {
        private readonly AppDbContext _context;

        public ClienteCreateValidator(AppDbContext context)
        {
            _context = context;


            RuleFor(c => c.ClienteId)
               .Equal(0).WithMessage("El clienteId debe ser 0.");

            RuleFor(c => c.Identidad)
                .MustAsync(async (identidad, cancellation) =>
                    !await _context.Clientes
                        .AnyAsync(c => c.Identidad == identidad, cancellation))
                .WithMessage(c => $"Ya existe un cliente con la identidad {c.Identidad}");
        }
    }
}
