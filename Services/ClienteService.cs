
namespace Prueba_Completa_NET.Services
{
    using Prueba_Completa_NET.Data;
    using Prueba_Completa_NET.Models;

    public class ClienteService
    {
        private readonly AppDbContext _context;

        public ClienteService(AppDbContext context) 
        { 
            _context = context;
        }

        public async Task<Cliente?> ObtenerClientePorIdAsync(long clienteId)
        {
            return await _context.Clientes.FindAsync(clienteId);
        }

    }
}
