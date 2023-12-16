//Añadimos la libreria System.ComponentModel.DataAnnotations para
//agregarle anotaciones a las propiedades de la clase
using System.ComponentModel.DataAnnotations;
namespace CyberWebSystem.Models
{
    public class Cliente
    {
        //Añadimos la anotación [Key] para indicar que esta propiedad es la llave primaria
        [Key]
        public int Id { get; set; }
        //Añadimos la anotación [Required] para indicar que esta propiedad es requerida
        [Required]
        public string? NombreCompleto { get; set; }

        //relaciones
        public virtual List<Flete>? Fletes { get; set; }
    }
}
