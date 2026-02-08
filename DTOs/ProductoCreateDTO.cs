namespace Prueba_Completa_NET.DTOs
{
    public class ProductoCreateDTO
    {
        public int ProductoId { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public decimal Precio { get; set; }
        public int Existencia { get; set; }

    }
}
