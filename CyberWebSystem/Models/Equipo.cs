using CyberWebSystem.Dtos;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CyberWebSystem.Models
{
    public class Equipo
    {
        [Key]
        public int Id{ get; set; }
        [Required]
        public string? Codigo { get; set; }
        [Required]
        public EstadoEnum Estado { get; set; }
        //Las propiedades que no son requeridas no necesitan la anotación [Required]
        public string? Detalle { get; set; }
        public string? Imagen { get; set; }

        //Agregamos la propiedad [NotMapped] para indicar que esta propiedad no se va a mapear a la base de datos
        //"no se va a mapear" quiere decir que no se va a crear una columna en la base de datos para esta propiedad
        [NotMapped]
        //Agregamos la propiedad [Display] para indicar que esta propiedad se va a mostrar en la vista
        //Y la forma en la que actua en el interfaz es que se va a mostrar el texto que se le pase como parametro
        [Display (Name = "Subir Imagen del equipo")]
        //Con esta propiedad se va a poder subir la imagen
        public IFormFile? ImagenFile { get; set; }//Para cargar la imagen de la UI  
    }
}
