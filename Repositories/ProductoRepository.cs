namespace Prueba_Completa_NET.Repositories
{
    using AutoMapper;

    using Microsoft.EntityFrameworkCore;
    using Prueba_Completa_NET.Data;
    using Prueba_Completa_NET.DTOs;
using Prueba_Completa_NET.Exceptions;
    using Prueba_Completa_NET.Interfaces.IRepository;
    using Prueba_Completa_NET.Models;

    public class ProductoRepository : IProductoRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public ProductoRepository(AppDbContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<Producto>> ListarProductos()
        {
          return await _context.Productos.ToListAsync();
        }

        public async Task<Producto> ObtenerProductoPorId(long productoId)
        {
            return await _context.Productos.FindAsync(productoId);
        }

        public async Task<Producto> CrearProducto(ProductoCreateDTO producto)
        {
            var nuevoProducto = _mapper.Map<Producto>(producto);
            nuevoProducto.CreatedAt = DateTime.Now;

            _context.Productos.Add(nuevoProducto);
            await _context.SaveChangesAsync();
            return nuevoProducto;
        }

        public async Task<Producto> ActualizarProducto(long productoId, ProductoUpdateDTO producto)
        {
            var productoExistente = await _context.Productos.FindAsync(productoId);
            if (productoExistente == null)
            {
                throw new NotFoundException ($"Producto con ID {productoId} no encontrado.");
            }

            var productoActualizado = _mapper.Map(producto, productoExistente);

            _context.Productos.Update(productoActualizado);
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
