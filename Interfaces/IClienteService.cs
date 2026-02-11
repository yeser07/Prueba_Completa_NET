namespace Prueba_Completa_NET.Interfaces
{
    using Prueba_Completa_NET.DTOs;
    public interface IClienteService
    {
        Task<List<ClienteDTO>> ListarClientes();
        Task<ClienteDTO> ObtenerClientePorId(long clienteId);
        Task<ClienteDTO> CrearCliente(ClienteCreateDTO cliente);
        Task<ClienteDTO> ActualizarCliente(long clienteId, ClienteUpdateDTO cliente);
    }
}
