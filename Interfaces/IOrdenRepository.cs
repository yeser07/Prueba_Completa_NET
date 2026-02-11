namespace Prueba_Completa_NET.Interfaces
{
    using Prueba_Completa_NET.DTOs;
    using Prueba_Completa_NET.Models;

    public interface IOrdenRepository
    {
        Task<Orden> CrearOrden(OrdenCreateDTO orden);

    }
}
