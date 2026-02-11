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
        private readonly ClienteService _clienteService;
        private readonly IOrdenRepository _ordenRepository;

        public OrdenController(ClienteService clienteService, IOrdenRepository ordenRepository)
        {
            _clienteService = clienteService;
            _ordenRepository = ordenRepository;
        }



        [HttpPost]
        public async Task<IActionResult> CrearOrden([FromBody] OrdenCreateDTO ordenCreateDTO)
        {
            // Validar cliente
            var cliente = await _clienteService.ObtenerClientePorIdAsync(ordenCreateDTO.ClienteId);
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
                var ordenCreada = await _ordenRepository.CrearOrden(ordenCreateDTO);

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
