namespace Prueba_Completa_NET.Interfaces
{
    using Prueba_Completa_NET.DTOs;
    using Prueba_Completa_NET.Models;

    public interface IProductoRepository
    {
            Task<List<Producto>> ListarProductos();
            Task<Producto> ObtenerProductoPorId(long productoId);
            Task<Producto> CrearProducto(ProductoCreateDTO producto);
            Task<Producto> ActualizarProducto(long productoId, ProductoUpdateDTO producto);
            Task ActualizarExistenciaAsync(Producto producto, int cantidadVendida);
    }
}
