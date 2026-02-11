namespace Prueba_Completa_NET.Interfaces
{
    using Prueba_Completa_NET.DTOs;
    public interface IProductoService
    {
        Task<List<ProductoDTO>> ListarProductos();
        Task<ProductoDTO> ObtenerProductoPorId(long productoId);
        Task<ProductoDTO> CrearProducto(ProductoCreateDTO producto);
        Task<ProductoDTO> ActualizarProducto(long productoId, ProductoUpdateDTO producto);
    }
}
