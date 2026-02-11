namespace Prueba_Completa_NET.Interfaces
{
    using Prueba_Completa_NET.DTOs;
    public interface IOrdenService
    {
        Task<OrdenDTO> CrearOrden(OrdenCreateDTO orden);
    }
}
