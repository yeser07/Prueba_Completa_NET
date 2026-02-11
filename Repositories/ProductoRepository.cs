namespace Prueba_Completa_NET.Repositories
{
    using Microsoft.EntityFrameworkCore;
    using Prueba_Completa_NET.Data;
    using Prueba_Completa_NET.DTOs;
    using Prueba_Completa_NET.Models;
    using Prueba_Completa_NET.Interfaces.IRepository;

    public class ProductoRepository : IProductoRepository
    {
        private readonly AppDbContext _context;

        public ProductoRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Producto>> ListarProductos()
        {
           var productos = await _context.Productos.ToListAsync();
            return productos;
        }

        public async Task<Producto> ObtenerProductoPorId(long productoId)
        {
            var producto = await _context.Productos.FindAsync(productoId);
            if (producto == null)
            {
                return null;
            }
            
            return producto;
        }

        public async Task<Producto> CrearProducto(ProductoCreateDTO producto)
        {
            var nuevoProducto = new Producto
            {
                Nombre = producto.Nombre,
                Descripcion = producto.Descripcion,
                Precio = producto.Precio,
                Existencia = producto.Existencia,

            };
            _context.Productos.Add(nuevoProducto);
            await _context.SaveChangesAsync();
            return nuevoProducto;
        }

        public async Task<Producto> ActualizarProducto(long productoId, ProductoUpdateDTO producto)
        {
            var productoExistente = await _context.Productos.FindAsync(productoId);
            if (productoExistente == null)
            {
                return null;
            }

            productoExistente.Nombre = producto.Nombre;
            productoExistente.Precio = producto.Precio;
            productoExistente.Descripcion = producto.Descripcion;
            productoExistente.Existencia = producto.Existencia;

            _context.Productos.Update(productoExistente);
            await _context.SaveChangesAsync();
            return productoExistente;
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
