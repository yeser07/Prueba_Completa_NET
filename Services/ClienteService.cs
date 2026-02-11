
namespace Prueba_Completa_NET.Services
{
    using AutoMapper;
    using Prueba_Completa_NET.DTOs;
    using Prueba_Completa_NET.Interfaces.IRepository;
    using Prueba_Completa_NET.Interfaces.IServices;

    public class ClienteService : IClienteService
    {
        private readonly IClienteRepository _clienteRepository;
        private readonly IMapper _mapper;

        public ClienteService(IClienteRepository clienteRepository, IMapper mapper)
        {
            _clienteRepository = clienteRepository;
            _mapper = mapper;
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
