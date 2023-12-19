using System.ComponentModel.DataAnnotations;

namespace CyberWebSystem.Models
{
    public class Flete
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public decimal Costo { get; set; }
        [Required]
        public DateTime Fecha { get; set; }
        [Required]
        public TimeSpan Hora { get; set; }
        [Required]
        public int Numero { get; set; }




        //foreing key
        public int UsuarioId { get; set; }
        public Usuario? Usuario { get; set; }

        public int EquipoId { get; set; }
        public Equipo? Equipo { get; set; }

        public int ClienteId { get; set; }
        public Cliente? Cliente { get; set; }
    }
}
