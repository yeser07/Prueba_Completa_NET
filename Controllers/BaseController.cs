using Microsoft.AspNetCore.Mvc;
using Prueba_Completa_NET.DTOs;

namespace Prueba_Completa_NET.Controllers
{
    [ApiController]
    public class BaseController : ControllerBase
    {
        protected IActionResult SuccessResponse<T>(T data, string message =  "")
        {
            var response = new ApiResponse<T>
            {
                Success = true,
                Message = message,
                Errors = new List<string>(),
                Data = data
            };
            return Ok(response);
        }
        
    }
}
