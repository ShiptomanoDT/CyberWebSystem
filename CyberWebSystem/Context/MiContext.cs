using CyberWebSystem.Models;
using Microsoft.EntityFrameworkCore;
namespace CyberWebSystem.Context
{
    //A travez de esta clase se va a poder acceder a la base de datos
    //Los modelos se comunican con la base de datos a travez de esta clase
    public class MiContext : DbContext //Heredando de DbContext
    {
        //Constructor
        //Por qué es necesario el constructor?
        //Porque se necesita pasarle las opciones de configuración

        //Qué significa (DbContextOptions options) : base(options)?
        //Significa que se le va a pasar las opciones de configuración a la clase base
        public MiContext(DbContextOptions options) : base(options)
        {
        }
        //Aqui se van a poner las tablas que se van a crear en la base de datos
        //Se van a crear las tablas a partir de los modelos
        public DbSet<Usuario> Usuarios { get; set; }
        //Se van a crear las tablas a partir de los modelos
        public DbSet<Cliente> Clientes { get; set; }
        //Se van a crear las tablas a partir de los modelos
        public DbSet<Equipo> Equipos { get; set; }
    }
}
