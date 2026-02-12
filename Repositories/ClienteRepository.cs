namespace Prueba_Completa_NET.Repositories
{
    using Microsoft.EntityFrameworkCore;
    using Prueba_Completa_NET.Data;
    using Prueba_Completa_NET.DTOs;
    using Prueba_Completa_NET.Exceptions;
    using Prueba_Completa_NET.Interfaces.IRepository;
    using Prueba_Completa_NET.Models;
    using AutoMapper;

    public class ClienteRepository : IClienteRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;


        public ClienteRepository(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }



        public async Task<List<Cliente>> ListarClientes()
        {
                return await _context.Clientes.ToListAsync();
        }

        public async Task<Cliente> ObtenerClientePorId(long clienteId)
        {
            return await _context.Clientes.FindAsync(clienteId);
       
        }

        public async Task<Cliente> CrearCliente(ClienteCreateDTO cliente)
        {
            var nuevoCliente = _mapper.Map<Cliente>(cliente);
            nuevoCliente.CreatedAt = DateTime.Now;
            
            _context.Clientes.Add(nuevoCliente);
            await _context.SaveChangesAsync();

            return nuevoCliente;
        }

        public async Task<Cliente> ActualizarCliente(long clienteId, ClienteUpdateDTO clienteUpdateDTO)
        {
            var clienteExistente = await _context.Clientes.FindAsync(clienteId);

            if (clienteExistente == null)
                throw new NotFoundException("No existe un cliente con el ID especificado");

            clienteExistente.Nombre = clienteUpdateDTO.Nombre;
            clienteExistente.Identidad = clienteUpdateDTO.Identidad;

            _context.Clientes.Update(clienteExistente);
            await _context.SaveChangesAsync();

            return clienteExistente;
        }

    }
}
