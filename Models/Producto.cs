using System.ComponentModel.DataAnnotations;

namespace Prueba_Completa_NET.Models
{
    public class Producto
    {
        [Key]
        public long ProductoId { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public decimal Precio { get; set; }
        public int Existencia { get; set; }
        public DateTime CreatedAt { get; set; }

        public ICollection<DetalleOrden> DetallesOrden { get; set; }
    }
}
