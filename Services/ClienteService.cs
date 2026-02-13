
namespace Prueba_Completa_NET.Services
{
    using AutoMapper;
    using Prueba_Completa_NET.DTOs;
    using Prueba_Completa_NET.Interfaces.IRepository;
    using Prueba_Completa_NET.Interfaces.IServices;
    using Prueba_Completa_NET.Validators;
    using Prueba_Completa_NET.Exceptions;

    public class ClienteService : IClienteService
    {
        private readonly IClienteRepository _clienteRepository;
        private readonly IMapper _mapper;
        private readonly ClienteCreateValidator _clienteCreateValidator;
        private readonly ClienteUpdateValidator _clienteUpdateValidator;

        public ClienteService(IClienteRepository clienteRepository, IMapper mapper, ClienteCreateValidator clienteCreateValidator, ClienteUpdateValidator clienteUpdateValidator)
        {
            _clienteRepository = clienteRepository;
            _mapper = mapper;
            _clienteCreateValidator = clienteCreateValidator;
            _clienteUpdateValidator = clienteUpdateValidator;

        }

        public async Task<List<ClienteDTO>> ListarClientes()
        {
            var clientes = await _clienteRepository.ListarClientes();

            return _mapper.Map<List<ClienteDTO>>(clientes);

        }

        public async Task<ClienteDTO> ObtenerClientePorId(long clienteId)
        {
            var cliente = await _clienteRepository.ObtenerClientePorId(clienteId);

            if (cliente == null)
            {
                throw new NotFoundException("No existe un cliente con el ID especificado");
            }
            return _mapper.Map<ClienteDTO>(cliente);
        }

        public async Task<ClienteDTO> CrearCliente(ClienteCreateDTO cliente)
        {
            var validationResult = await _clienteCreateValidator.ValidateAsync(cliente);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(
                    validationResult.Errors.Select(e => e.ErrorMessage)
                );
            }

            var nuevoCliente = await _clienteRepository.CrearCliente(cliente);
            
            return _mapper.Map<ClienteDTO>(nuevoCliente);
        }

        public async Task<ClienteDTO> ActualizarCliente(long clienteId, ClienteUpdateDTO cliente)
        {

            cliente.ClienteId = clienteId;

            var validationResult = await _clienteUpdateValidator.ValidateAsync(cliente);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(
                    validationResult.Errors.Select(e => e.ErrorMessage)
                );
            }

            var clienteActualizado = await _clienteRepository.ActualizarCliente(clienteId, cliente);

             if (clienteActualizado == null)

                throw new NotFoundException("No existe un cliente con el ID especificado");

            return _mapper.Map<ClienteDTO>(clienteActualizado);
        }
    }
}
