using System.ComponentModel.DataAnnotations;

namespace Prueba_Completa_NET.Models
{
    public class Cliente
    {
        [Key]
        public int ClienteId { get; set; }
        public string Nombre { get; set; }
        public string Identidad { get; set; }
        public DateTime CreatedAt { get; set; }

        public ICollection<Orden> Ordenes { get; set; }

    }
}
