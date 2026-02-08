using Microsoft.EntityFrameworkCore.Storage;

namespace Prueba_Completa_NET.Models
{
    public class Orden
    {
        public long OrdenId { get; set; }
        public long ClienteId { get; set; }
        public Cliente Cliente { get; set; }
        public decimal Impuesto { get; set; }
        public decimal Subtotal { get; set; }
        public decimal Total { get; set; }
        public DateTime FechaCreacion { get; set; }
        public ICollection<DetalleOrden> Detalles { get; set; }


    }
}
