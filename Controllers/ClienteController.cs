using Microsoft.AspNetCore.Mvc;
using Prueba_Completa_NET.DTOs;
using Prueba_Completa_NET.Interfaces.IRepository;
using Prueba_Completa_NET.Interfaces.IServices;
using Prueba_Completa_NET.Validators;

namespace Prueba_Completa_NET.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClienteController : BaseController
    {

        private readonly IClienteService _clienteService;

        public ClienteController(IClienteService clienteService)
        {
            _clienteService = clienteService;
        }

        [HttpGet]
        public async Task<IActionResult> GetClientes()
        {
            return Ok(await _clienteService.ListarClientes());
        }

        [HttpGet("{id}")]

        public async Task<IActionResult> GetClientePorId(int id)
        {
            return SuccessResponse(await _clienteService.ObtenerClientePorId(Convert.ToInt64(id)));
        }

        [HttpPost]
        public async Task<IActionResult> CrearCliente([FromBody] ClienteCreateDTO clienteCreateDTO)
        {
            return SuccessResponse(await _clienteService.CrearCliente(clienteCreateDTO),"Cliente creado exítosamente!");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> ActualizarCliente(long id, [FromBody] ClienteUpdateDTO clienteUpdateDTO)
        {

            return SuccessResponse(await _clienteService.ActualizarCliente(id, clienteUpdateDTO),"Cliente Actulizado con exito!");
        }

    }
}
