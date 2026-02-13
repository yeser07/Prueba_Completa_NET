using Microsoft.AspNetCore.Mvc;
using Prueba_Completa_NET.Validators;
using Prueba_Completa_NET.DTOs;
using Prueba_Completa_NET.Interfaces.IServices;

namespace Prueba_Completa_NET.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductoController : BaseController
    {
        private readonly IProductoService _productoService;
        public ProductoController(IProductoService productoService)
        {
            _productoService = productoService;
        }

        [HttpGet]

        public async Task<IActionResult> GetProductos()
        {

             return SuccessResponse(await _productoService.ListarProductos());
        }

        [HttpGet("{id}")]

        public async Task<IActionResult> GetProductoPorId(int id)
        {

            return SuccessResponse(await _productoService.ObtenerProductoPorId(Convert.ToInt64(id)));
        }

        [HttpPost]

        public async Task<IActionResult> CrearProducto([FromBody] ProductoCreateDTO productoCreateDTO)
        {

            return SuccessResponse(await _productoService.CrearProducto(productoCreateDTO) );
        }

        [HttpPut("{id}")]

        public async Task<IActionResult> ActualizarProducto(int id, [FromBody] ProductoUpdateDTO productoUpdateDTO)
        {
            
            return SuccessResponse( await _productoService.ActualizarProducto(Convert.ToInt64(id), productoUpdateDTO));
            
        }
    }
}
