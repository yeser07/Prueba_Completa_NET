using Microsoft.AspNetCore.Mvc;
using Prueba_Completa_NET.DTOs;
using Prueba_Completa_NET.Services;
using Prueba_Completa_NET.Interfaces;


namespace Prueba_Completa_NET.Controllers
{
    

    [ApiController]
    [Route("api/[controller]")]
    public class OrdenController : ControllerBase
    {
        //private readonly OrdenRepository _ordenRepository;
        private readonly IClienteService _clienteService;
        private readonly IOrdenService _ordenService;

        public OrdenController( IOrdenService ordenService,IClienteService clienteService)
        {
            _clienteService = clienteService;
            _ordenService = ordenService;
        }



        [HttpPost]
        public async Task<IActionResult> CrearOrden([FromBody] OrdenCreateDTO ordenCreateDTO)
        {
            // Validar cliente
            var cliente = await _clienteService.ObtenerClientePorId(ordenCreateDTO.ClienteId);
            if (cliente == null)
            {
                var notFoundResponse = new ApiResponse<OrdenDTO>
                {
                    Success = false,
                    Message = "Error al crear la orden",
                    Errors = new List<string> { "El cliente especificado no existe" },
                    Data = null
                };
                return BadRequest(notFoundResponse);
            }

            try
            {
                // Llamamos al repositorio para crear la orden
                var ordenCreada = await _ordenService.CrearOrden(ordenCreateDTO);

                var response = new ApiResponse<OrdenDTO>
                {
                    Success = true,
                    Message = "Orden creada exitosamente",
                    Errors = null,
                    Data = ordenCreada
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                var errorResponse = new ApiResponse<OrdenDTO>
                {
                    Success = false,
                    Message = "Error al crear la orden",
                    Errors = new List<string> { ex.Message },
                    Data = null
                };
                return BadRequest(errorResponse);
            }
        }

    }
}
