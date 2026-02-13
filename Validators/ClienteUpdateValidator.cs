using Prueba_Completa_NET.Data;
using Prueba_Completa_NET.DTOs;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

public class ClienteUpdateValidator
    : ClienteBaseValidator<ClienteUpdateDTO>
{
    private readonly AppDbContext _context;

    public ClienteUpdateValidator(AppDbContext context)
        : base()
    {
        _context = context;

        RuleFor(c => c.Identidad)
            .MustAsync(async (cliente, identidad, cancellation) =>
                !await _context.Clientes.AnyAsync(c =>
                    c.Identidad == identidad &&
                    c.ClienteId != cliente.ClienteId,
                    cancellation))
            .WithMessage(c => $"Ya existe un cliente con la identidad {c.Identidad}");
    }
}