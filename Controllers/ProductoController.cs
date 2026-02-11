using Microsoft.AspNetCore.Mvc;
using Prueba_Completa_NET.Validators;
using Prueba_Completa_NET.DTOs;
using Prueba_Completa_NET.Interfaces.IServices;

namespace Prueba_Completa_NET.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductoController : ControllerBase
    {
        private readonly IProductoService _productoService;
        private readonly ProductoCreateValidator _productoCreateValidator;
        private readonly ProductoUpdateValidator _productoUpdateValidator;

        public ProductoController(IProductoService productoService, ProductoCreateValidator productoCreateValidator, ProductoUpdateValidator productoUpdateValidator)
        {
            _productoService = productoService;
            _productoCreateValidator = productoCreateValidator;
            _productoUpdateValidator = productoUpdateValidator;
        }

        [HttpGet]

        public async Task<IActionResult> GetProductos()
        {
            var productos = await _productoService.ListarProductos();
            var response = new DTOs.ApiResponse<List<DTOs.ProductoDTO>>
            {
                Success = true,
                Message = "",
                Errors = new List<string>(),
                Data = productos
            };

            return Ok(response);
        }

        [HttpGet("{id}")]

        public async Task<IActionResult> GetProductoPorId(int id)
        {
            long idProducto = id;
            var producto = await _productoService.ObtenerProductoPorId(idProducto);
            if (producto == null)
            {
                var notFoundResponse = new ApiResponse<ProductoDTO>
                {
                    Success = false,
                    Message = "Producto no encontrado",
                    Errors = new List<string> { "No existe un producto con el ID especificado" },
                    Data = null
                };
                return NotFound(notFoundResponse);
            }
            var response = new ApiResponse<DTOs.ProductoDTO>
            {
                Success = true,
                Message = "",
                Errors = new List<string>(),
                Data = producto
            };
            return Ok(response);
        }

        [HttpPost]

        public async Task<IActionResult> CrearProducto([FromBody] ProductoCreateDTO productoCreateDTO)
        {
            var validationResult = _productoCreateValidator.Validate(productoCreateDTO);
            if (!validationResult.IsValid)
            {
                var errorResponse = new ApiResponse<ProductoDTO>
                {
                    Success = false,
                    Message = "Error de validación",
                    Errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList(),
                    Data = null
                };
                return BadRequest(errorResponse);
            }
            var nuevoProducto = await _productoService.CrearProducto(productoCreateDTO);
            var response = new ApiResponse<ProductoDTO>
            {
                Success = true,
                Message = "Producto creado exitosamente",
                Errors = new List<string>(),
                Data = nuevoProducto
            };
            return Ok(response);
        }

        [HttpPut("{id}")]

        public async Task<IActionResult> ActualizarProducto(int id, [FromBody] ProductoUpdateDTO productoUpdateDTO)
        {
            long idProducto = id;
            var validationResult = _productoUpdateValidator.Validate(productoUpdateDTO);
            if (!validationResult.IsValid)
            {
                var errorResponse = new ApiResponse<ProductoDTO>
                {
                    Success = false,
                    Message = "Error de validación",
                    Errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList(),
                    Data = null
                };
                return BadRequest(errorResponse);
            }

            var productoActualizado = await _productoService.ActualizarProducto(idProducto, productoUpdateDTO);
            if (productoActualizado == null)
            {
                var notFoundResponse = new ApiResponse<ProductoDTO>
                {
                    Success = false,
                    Message = "Producto no encontrado",
                    Errors = new List<string> { "No existe un producto con el ID especificado" },
                    Data = null
                };
                return NotFound(notFoundResponse);
            }


            var response = new ApiResponse<ProductoDTO>
            {
                Success = true,
                Message = "Producto actualizado exitosamente",
                Errors = new List<string>(),
                Data = productoActualizado
            };
            return Ok(response);
        }
    }
}
