namespace Prueba_Completa_NET.Interfaces.IServices
{
    using Prueba_Completa_NET.DTOs;
    public interface IOrdenService
    {
        Task<OrdenDTO> CrearOrden(OrdenCreateDTO orden);
    }
}
