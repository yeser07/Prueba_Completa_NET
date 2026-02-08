using Prueba_Completa_NET.Models;
using System.Text.Json.Serialization;

namespace Prueba_Completa_NET.DTOs
{
    public class OrdenCreateDTO
    {
        public long OrdenId { get; set; } 
        public long ClienteId { get; set; }

        [JsonPropertyName("detalle")]
        public List<DetalleCreateDTO> Detalle { get; set; } = new List<DetalleCreateDTO>();
    }

    public class DetalleCreateDTO
    {
        public long ProductoId { get; set; }
        public int Cantidad { get; set; }
    }

}
