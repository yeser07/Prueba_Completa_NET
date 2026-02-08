using Prueba_Completa_NET.Models;

namespace Prueba_Completa_NET.DTOs
{
    public class OrdenCreateDTO
    {

        public ClienteDTO Cliente { get; set; }
        public decimal Impuesto { get; set; }
        public decimal Subtotal { get; set; }
        public decimal Total { get; set; }
        public DateTime FechaCreacion { get; set; }
        public List<DetalleOrdenDTO> Detalles { get; set; }
    }
}
