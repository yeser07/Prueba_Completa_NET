namespace Prueba_Completa_NET.Interfaces
{
    using Prueba_Completa_NET.Models;
    using Prueba_Completa_NET.DTOs;

    public interface IClienteRepository
    {
        Task<List<Cliente>> ListarClientes();
        Task<Cliente> ObtenerClientePorId(long clienteId);
        Task<Cliente> CrearCliente(ClienteCreateDTO cliente);
        Task<Cliente> ActualizarCliente(long clienteId, ClienteUpdateDTO cliente);

    }
}
