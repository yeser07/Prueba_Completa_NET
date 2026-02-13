namespace Prueba_Completa_NET.Services
{
    using AutoMapper;
    using Prueba_Completa_NET.DTOs;
    using Prueba_Completa_NET.Exceptions;
    using Prueba_Completa_NET.Interfaces.IRepository;
    using Prueba_Completa_NET.Interfaces.IServices;
    using Prueba_Completa_NET.Validators;

    public class ProductoService : IProductoService
    {
        private readonly IProductoRepository _productoRepository;
        private readonly IMapper _mapper;
        private readonly ProductoCreateValidator _productoCreateValidator;
        private readonly ProductoUpdateValidator _productoUpdateValidator;
        public ProductoService( IProductoRepository productoRepository, IMapper mapper, ProductoCreateValidator productoCreateValidator, ProductoUpdateValidator productoUpdateValidator)
        {
            _productoRepository = productoRepository;
            _mapper = mapper;
            _productoCreateValidator = productoCreateValidator;
            _productoUpdateValidator = productoUpdateValidator;
        }

        public async Task<List<ProductoDTO>> ListarProductos()
        {
            var productos = await _productoRepository.ListarProductos();
            return _mapper.Map<List<ProductoDTO>>(productos);
        }

        public async Task<ProductoDTO> ObtenerProductoPorId(long id)
        {
            var producto = await _productoRepository.ObtenerProductoPorId(id);

            if (producto == null)
            {
                throw new NotFoundException($"Producto con ID {id} no encontrado.");
            }

            return _mapper.Map<ProductoDTO>(producto);
        }

        public async Task<ProductoDTO> CrearProducto(ProductoCreateDTO producto)
        {
            var validationResult = await _productoCreateValidator.ValidateAsync(producto);

            if (!validationResult.IsValid)
            {
                 var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                 throw new ValidationException(errors);
            }

            var productoNuevo = await _productoRepository.CrearProducto(producto);

            return _mapper.Map<ProductoDTO>(productoNuevo);
        }

        public async Task<ProductoDTO> ActualizarProducto(long id, ProductoUpdateDTO producto)
        {
            var validationResult = await _productoUpdateValidator.ValidateAsync(producto);

            if (!validationResult.IsValid)
            {
                 var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                 throw new ValidationException(errors);
            }

            var productoActualizado = await _productoRepository.ActualizarProducto(id, producto);
            return _mapper.Map<ProductoDTO>(productoActualizado);
        }
    }
}
