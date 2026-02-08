namespace Prueba_Completa_NET.Repositories
{
    using Prueba_Completa_NET.Data;
    using Prueba_Completa_NET.Models;
    using Prueba_Completa_NET.DTOs;
    using Microsoft.EntityFrameworkCore;

    public class ClienteRepository
    {
        private readonly AppDbContext _context;


        public ClienteRepository(AppDbContext context)
        {
            _context = context;
        }



        public async Task<List<ClienteDTO>> ListarClientes()
        {
            {
                var clientes = await _context.Clientes.ToListAsync();

                return clientes.Select(c => new ClienteDTO
                {
                    ClienteId = c.ClienteId,
                    Nombre = c.Nombre,
                    Identidad = c.Identidad
                }).ToList();


            }
        }

        public async Task<ClienteDTO> ObtenerClientePorId(long clienteId)
        {
            var cliente = await _context.Clientes.FindAsync(clienteId);
            if (cliente == null)
            {
                return null;
            }
            return new ClienteDTO
            {
                ClienteId = cliente.ClienteId,
                Nombre = cliente.Nombre,
                Identidad = cliente.Identidad
            };

        }

        public async Task<ClienteDTO> CrearCliente(ClienteCreateDTO cliente)
        {
           var nuevoCliente = new Cliente
            {
                Nombre = cliente.Nombre,
                Identidad = cliente.Identidad
            };
            _context.Clientes.Add(nuevoCliente);
            await _context.SaveChangesAsync();
            cliente.ClienteId = nuevoCliente.ClienteId;
            
            return new ClienteDTO
            {
                ClienteId = nuevoCliente.ClienteId,
                Nombre = nuevoCliente.Nombre,
                Identidad = nuevoCliente.Identidad
            };
        }

        public async Task<ClienteDTO> ActualizarCliente(long clienteId, ClienteCreateDTO clienteUpdateDTO)
        {
            var clienteExistente = await _context.Clientes.FindAsync(clienteId);

            if (clienteExistente == null)
                return null;

            clienteExistente.Nombre = clienteUpdateDTO.Nombre;
            clienteExistente.Identidad = clienteUpdateDTO.Identidad;

            _context.Clientes.Update(clienteExistente);
            await _context.SaveChangesAsync();

            return new ClienteDTO
            {
                ClienteId = clienteExistente.ClienteId,
                Nombre = clienteExistente.Nombre,
                Identidad = clienteExistente.Identidad
            };
        }


    }
}
