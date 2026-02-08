namespace Prueba_Completa_NET.DTOs
{
    public class DetalleOrdenCreateDTO
    {
        public long OrdenId { get; set; }
        public OrdenDTO Orden { get; set; }
        public long ProductoId { get; set; }
        public ProductoDTO Producto { get; set; }
        public int Cantidad { get; set; }
        public decimal Impuesto { get; set; }
        public decimal Subtotal { get; set; }
        public decimal Total { get; set; }
    }
}
