namespace Prueba_Completa_NET.Repositories
{
    using Microsoft.EntityFrameworkCore;
    using Prueba_Completa_NET.Data;
    using Prueba_Completa_NET.DTOs;
    using Prueba_Completa_NET.Models;

    public class ProductoRepository
    {
        private readonly AppDbContext _context;

        public ProductoRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<ProductoDTO>> ListarProductos()
        {
           var productos = await _context.Productos.ToListAsync();
            return productos.Select(p => new ProductoDTO
            {
                ProductoId = p.ProductoId,
                Nombre = p.Nombre,
                Descripcion = p.Descripcion,
                Precio = p.Precio,
                Existencia = p.Existencia
            }).ToList();
        }

        public async Task<ProductoDTO> ObtenerProductoPorId(long productoId)
        {
            var producto = await _context.Productos.FindAsync(productoId);
            if (producto == null)
            {
                return null;
            }
            return new ProductoDTO
            {
                ProductoId = producto.ProductoId,
                Nombre = producto.Nombre,
                Descripcion = producto.Descripcion,
                Precio = producto.Precio,
                Existencia = producto.Existencia
            };
        }

        public async Task<ProductoDTO> CrearProducto(ProductoCreateDTO producto)
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
            return new ProductoDTO
            {
                ProductoId = nuevoProducto.ProductoId,
                Nombre = nuevoProducto.Nombre,
                Descripcion = nuevoProducto.Descripcion,
                Precio = nuevoProducto.Precio,
                Existencia = nuevoProducto.Existencia
            };
        }

        public async Task<ProductoDTO> ActualizarProducto(long productoId, ProductoCreateDTO producto)
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
            return new ProductoDTO
            {
                ProductoId = productoExistente.ProductoId,
                Nombre = productoExistente.Nombre,
                Descripcion = productoExistente.Descripcion,
                Precio = productoExistente.Precio,
                Existencia = productoExistente.Existencia
            };
        }

    }
}
