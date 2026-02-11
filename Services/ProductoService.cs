namespace Prueba_Completa_NET.Services
{
    using AutoMapper;
    using Prueba_Completa_NET.DTOs;
    using Prueba_Completa_NET.Interfaces.IRepository;
    using Prueba_Completa_NET.Interfaces.IServices;

    public class ProductoService : IProductoService
    {
        private readonly IProductoRepository _productoRepository;
        private readonly IMapper _mapper;
        public ProductoService( IProductoRepository productoRepository, IMapper mapper)
        {
            _productoRepository = productoRepository;
            _mapper = mapper;
        }

        public async Task<List<ProductoDTO>> ListarProductos()
        {
            var productos = await _productoRepository.ListarProductos();
            return _mapper.Map<List<ProductoDTO>>(productos);
        }

        public async Task<ProductoDTO> ObtenerProductoPorId(long id)
        {
            var producto = await _productoRepository.ObtenerProductoPorId(id);
            return _mapper.Map<ProductoDTO>(producto);
        }

        public async Task<ProductoDTO> CrearProducto(ProductoCreateDTO producto)
        {
            var productoNuevo = await _productoRepository.CrearProducto(producto);
            return _mapper.Map<ProductoDTO>(productoNuevo);
        }

        public async Task<ProductoDTO> ActualizarProducto(long id, ProductoUpdateDTO producto)
        {
            var productoActualizado = await _productoRepository.ActualizarProducto(id, producto);
            return _mapper.Map<ProductoDTO>(productoActualizado);
        }
    }
}
