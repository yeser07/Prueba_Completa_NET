namespace Prueba_Completa_NET.Models
{
    public class DetalleOrden
    {
        public long DetalleOrdenId { get; set; }
        public long OrdenId { get; set; }
        public Orden Orden { get; set; }
        public long ProductoId { get; set; }

        public Producto Producto { get; set; }

        public int Cantidad { get; set; }
        public decimal Impuesto { get; set; }
        public decimal Subtotal { get; set; }
        public decimal Total { get; set; }
    }
}
