namespace Prueba_Completa_NET.Services
{
    using Prueba_Completa_NET.Data;
    using Prueba_Completa_NET.Models;

    public class ProductoService
    {
        private readonly AppDbContext _context;
        public ProductoService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<Producto?> ObtenerProductoPorIdAsync(long productoId)
        {
            return await _context.Productos.FindAsync(productoId);
        }

        public async Task ActualizarExistenciaAsync(Producto producto, int cantidadVendida)
        {
            if (producto.Existencia < cantidadVendida)
                throw new Exception($"Producto {producto.ProductoId} no tiene suficiente stock.");

            producto.Existencia -= cantidadVendida;
            _context.Productos.Update(producto);
        }




    }
}
