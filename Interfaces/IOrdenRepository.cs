namespace Prueba_Completa_NET.Interfaces
{
    using Prueba_Completa_NET.DTOs;
    public interface IOrdenRepository
    {
        Task<OrdenDTO> CrearOrden(OrdenCreateDTO orden);

    }
}
