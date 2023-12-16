using CyberWebSystem.Dtos;
using System.ComponentModel.DataAnnotations;
namespace CyberWebSystem.Models
{
    public class Usuario
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string? Email { get; set; }
        [Required]
        public string? NombreCompleto { get; set; }
        [Required]
        public string? Password { get; set; }
        [Required]
        public RolEnum Rol { get; set; }

        //relaciones
        public virtual List<Flete>? Fletes { get; set; }
    }
}
