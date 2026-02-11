using Microsoft.AspNetCore.Mvc;
using Prueba_Completa_NET.DTOs;
using Prueba_Completa_NET.Interfaces;
using Prueba_Completa_NET.Validators;

namespace Prueba_Completa_NET.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClienteController : ControllerBase
    {

        private readonly IClienteRepository _clienteRepository;
        private readonly ClienteCreateValidator _clienteCreateValidator;
        private readonly ClienteUpdateValidator _clienteUpdateValidator;

        public ClienteController(IClienteRepository clienteRepository, ClienteCreateValidator clienteCreateValidator, ClienteUpdateValidator clienteUpdateValidator)
        {
            _clienteRepository = clienteRepository;
            _clienteCreateValidator = clienteCreateValidator;
            _clienteUpdateValidator = clienteUpdateValidator;
        }

        [HttpGet]
        public async Task<IActionResult> GetClientes()
        {
            var clientes = await _clienteRepository.ListarClientes();
            var response = new DTOs.ApiResponse<List<ClienteDTO>>
            {
                Success = true,
                Message = "",
                Errors = new List<string>(),
                Data = clientes
            };

            return Ok(response);
        }

        [HttpGet("{id}")]

        public async Task<IActionResult> GetClientePorId(int id)
        {
            long idCliente = id;

            var cliente = await _clienteRepository.ObtenerClientePorId(idCliente);
            if (cliente == null)
            {
                var notFoundResponse = new ApiResponse<ClienteDTO>
                {
                    Success = false,
                    Message = "Cliente no encontrado",
                    Errors = new List<string> { "No existe un cliente con el ID especificado" },
                    Data = null
                };
                return NotFound(notFoundResponse);
            }
            var response = new ApiResponse<ClienteDTO>
            {
                Success = true,
                Message = "",
                Errors = new List<string>(),
                Data = cliente
            };
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> CrearCliente([FromBody] ClienteCreateDTO clienteCreateDTO)
        {

            var validationResult = await _clienteCreateValidator.ValidateAsync(clienteCreateDTO);

            if (!validationResult.IsValid)
            {
                var errorResponse = new ApiResponse<ClienteDTO>
                {
                    Success = false,
                    Message = "Error de validación",
                    Errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList(),
                    Data = null
                };
                return BadRequest(errorResponse);
            }

            var nuevoCliente = await _clienteRepository.CrearCliente(clienteCreateDTO);

            var response = new ApiResponse<ClienteDTO>
            {
                Success = true,
                Message = "Cliente creado exitosamente",
                Errors = new List<string>(),
                Data = nuevoCliente
            };

            return Ok(response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> ActualizarCliente(long id, [FromBody] ClienteUpdateDTO clienteUpdateDTO)
        {
            var dtoValidacion = new ClienteDTO
            {
                ClienteId = id,
                Nombre = clienteUpdateDTO.Nombre,
                Identidad = clienteUpdateDTO.Identidad
            };

            var validationResult = await _clienteUpdateValidator.ValidateAsync(dtoValidacion);
            if (!validationResult.IsValid)
            {
                var errorResponse = new ApiResponse<ClienteDTO>
                {
                    Success = false,
                    Message = "Error de validación",
                    Errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList(),
                    Data = null
                };
                return BadRequest(errorResponse);
            }

            // Actualizar
            var clienteActualizado = await _clienteRepository.ActualizarCliente(id, clienteUpdateDTO);

            if (clienteActualizado == null)
            {
                var notFoundResponse = new ApiResponse<ClienteDTO>
                {
                    Success = false,
                    Message = "Cliente no encontrado",
                    Errors = new List<string> { "No existe un cliente con el ID especificado" },
                    Data = null
                };
                return NotFound(notFoundResponse);
            }

            var response = new ApiResponse<ClienteDTO>
            {
                Success = true,
                Message = "Cliente actualizado exitosamente",
                Errors = new List<string>(),
                Data = clienteActualizado
            };

            return Ok(response);
        }

    }
}
