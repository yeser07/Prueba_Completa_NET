
namespace Prueba_Completa_NET.Services
{
    using Prueba_Completa_NET.Data;
    using Prueba_Completa_NET.Models;
    using Prueba_Completa_NET.Interfaces;
    using AutoMapper;
    using Prueba_Completa_NET.DTOs;

    public class ClienteService : IClienteService
    {
        private readonly AppDbContext _context;
        private readonly IClienteRepository _clienteRepository;
        private readonly IMapper _mapper;

        public ClienteService(AppDbContext context, IClienteRepository clienteRepository, IMapper mapper)
        {
            _context = context;
            _clienteRepository = clienteRepository;
            _mapper = mapper;
        }

        public async Task<Cliente?> ObtenerClientePorIdAsync(long clienteId)
        {
            return await _context.Clientes.FindAsync(clienteId);
        }



        public async Task<List<ClienteDTO>> ListarClientes()
        {
            var clientes = await _clienteRepository.ListarClientes();

            return _mapper.Map<List<ClienteDTO>>(clientes);

        }

        public async Task<ClienteDTO> ObtenerClientePorId(long clienteId)
        {
            var cliente = await _clienteRepository.ObtenerClientePorId(clienteId);
            return _mapper.Map<ClienteDTO>(cliente);
        }

        public async Task<ClienteDTO> CrearCliente(ClienteCreateDTO cliente)
        {
            var nuevoCliente = await _clienteRepository.CrearCliente(cliente);
            
            return _mapper.Map<ClienteDTO>(nuevoCliente);
        }

        public async Task<ClienteDTO> ActualizarCliente(long clienteId, ClienteUpdateDTO cliente)
        {
            var clienteActualizado = await _clienteRepository.ActualizarCliente(clienteId, cliente);
            return _mapper.Map<ClienteDTO>(clienteActualizado);
        }
    }
}
