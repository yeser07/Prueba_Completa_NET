namespace Prueba_Completa_NET.Repositories
{
    using Prueba_Completa_NET.Data;
    using Prueba_Completa_NET.Models;
    using Prueba_Completa_NET.DTOs;
    using Microsoft.EntityFrameworkCore;
    using Prueba_Completa_NET.Interfaces.IRepository;

    public class ClienteRepository : IClienteRepository
    {
        private readonly AppDbContext _context;


        public ClienteRepository(AppDbContext context)
        {
            _context = context;
        }



        public async Task<List<Cliente>> ListarClientes()
        {
                return await _context.Clientes.ToListAsync();
        }

        public async Task<Cliente> ObtenerClientePorId(long clienteId)
        {
            var cliente = await _context.Clientes.FindAsync(clienteId);
            
            if (cliente == null)
                throw new KeyNotFoundException("No existe un cliente con el ID especificado");
            
            return cliente;

        }

        public async Task<Cliente> CrearCliente(ClienteCreateDTO cliente)
        {
           var nuevoCliente = new Cliente
            {
                Nombre = cliente.Nombre,
                Identidad = cliente.Identidad,
                CreatedAt = DateTime.Now
           };
            _context.Clientes.Add(nuevoCliente);
            await _context.SaveChangesAsync();

            return nuevoCliente;
        }

        public async Task<Cliente> ActualizarCliente(long clienteId, ClienteUpdateDTO clienteUpdateDTO)
        {
            var clienteExistente = await _context.Clientes.FindAsync(clienteId);

            if (clienteExistente == null)
                throw new KeyNotFoundException("No existe un cliente con el ID especificado");

            clienteExistente.Nombre = clienteUpdateDTO.Nombre;
            clienteExistente.Identidad = clienteUpdateDTO.Identidad;

            _context.Clientes.Update(clienteExistente);
            await _context.SaveChangesAsync();

            return clienteExistente;
        }

    }
}
