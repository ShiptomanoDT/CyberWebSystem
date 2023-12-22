using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CyberWebSystem.Context;
using CyberWebSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;



//Toda la logica de programacion de la aplicacion se encuentra en esta clase
//que trabaja junto con la vista y el modelo
namespace CyberWebSystem.Controllers
{
    [Authorize(Roles = "Administrador")]//Solo los usuarios con el rol Administrador podran usar este controlador
    public class UsuariosController : Controller
    {
        private readonly MiContext _context;//Se crea una variable de tipo MiContext,readonly significa que el valor de la variable no puede cambiar
		public UsuariosController(MiContext context)//Se crea un constructor que recibe un parametro de tipo MiContext
        {
			_context = context;//Se asigna el valor del parametro al atributo de la clase
        }

        // GET: Usuarios
        //Esta clase es la que se encarga de mostrar la vista de la lista de usuarios
        public async Task<IActionResult> Index()//Se utiliza async para que el metodo se ejecute de forma asincrona
        {
              return _context.Usuarios != null ? //Aqui se verifica que el contexto no sea nulo
                          View(await _context.Usuarios.ToListAsync()) : //Si no es nulo se muestra la vista de la lista de usuarios
                          Problem("Entity set 'MiContext.Usuarios'  is null.");
        }

        // GET: Usuarios/Details/5
        //Esta clase es la que se encarga de mostrar la vista de los detalles de un usuario
        public async Task<IActionResult> Details(int? id)
        {
            //Aqui se verifica que el id no sea nulo y que el contexto no sea nulo
            if (id == null || _context.Usuarios == null)
            {
                return NotFound();
            }
            //Aqui se busca el usuario por el id
            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(m => m.Id == id);
            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }

        // GET: Usuarios/Create
        //Esta clase es la que se encarga de mostrar la vista de crear un usuario
        public IActionResult Create()
        {
            return View();
        }

        // POST: Usuarios/Create
        // (EN)To protect from overposting attacks, enable the specific properties you want to bind to.
        // (ES)Para protegerse de los ataques de vinculación, habilite las propiedades específicas que desea vincular.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]//El metodo es post sirve para enviar datos al servidor
        [ValidateAntiForgeryToken]//Aqui se verifica que el token sea valido lo que quiere dercir que el usuario esta autenticado
        
        //En esta clase se crea un usuario
        public async Task<IActionResult> Create([Bind("Id,Email,NombreCompleto,Password,Rol")] Usuario usuario)//El nuevo usuario se recibe como parametro
        {
            if (ModelState.IsValid)//Se verifica que el modelo sea valido
            {
                _context.Add(usuario);//Se agrega el usuario al contexto
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(usuario);
        }

        // GET: Usuarios/Edit/5
        //Esta clase es la que se encarga de mostrar la vista de editar un usuario
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Usuarios == null)//Se verifica que el id no sea nulo y que el contexto no sea nulo
            {
                return NotFound();
            }

            var usuario = await _context.Usuarios.FindAsync(id);//Se busca el usuario por el id
            if (usuario == null)
            {
                return NotFound();
            }
            return View(usuario);
        }

        // POST: Usuarios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //En esta clase se edita un usuario
        public async Task<IActionResult> Edit(int id, [Bind("Id,Email,NombreCompleto,Password,Rol")] Usuario usuario)
        {                                              //Bind() se utiliza para especificar las propiedades que se deben actualizar
            if (id != usuario.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)//Se verifica que el modelo sea valido
            {
                try
                {
                    _context.Update(usuario);//Se actualiza el usuario
                    await _context.SaveChangesAsync();//Se guardan los cambios
                }
                catch (DbUpdateConcurrencyException)//Se verifica que no haya errores de concurrencia
                {
                    if (!UsuarioExists(usuario.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));//Se redirecciona a la vista de la lista de usuarios
            }
            return View(usuario);//Se retorna la vista de editar usuario
        }

        // GET: Usuarios/Delete/5
        //Esta clase es la que se encarga de mostrar la vista de eliminar un usuario
        public async Task<IActionResult> Delete(int? id)//Se recibe el id del usuario a eliminar
        {
            if (id == null || _context.Usuarios == null)//Se verifica que el id no sea nulo y que el contexto no sea nulo
            {
                return NotFound();
            }

            var usuario = await _context.Usuarios   //Se busca el usuario por el id
                .FirstOrDefaultAsync(m => m.Id == id);
            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }

        // POST: Usuarios/Delete/5
        [HttpPost, ActionName("Delete")]//Se especifica que el metodo es post y que se llama Delete
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)//Se recibe el id del usuario a eliminar
        {
            if (_context.Usuarios == null)//Se verifica que el contexto no sea nulo
            {
                return Problem("Entity set 'MiContext.Usuarios'  is null.");//Si el contexto es nulo se muestra un mensaje de error
            }
            var usuario = await _context.Usuarios.FindAsync(id);//Se busca el usuario por el id
            if (usuario != null)
            {
                _context.Usuarios.Remove(usuario);
            }
            
            await _context.SaveChangesAsync();//Se guardan los cambios
            return RedirectToAction(nameof(Index));//Se redirecciona a la vista de la lista de usuarios
        }

        private bool UsuarioExists(int id)//Se verifica que el usuario exista
        {
          return (_context.Usuarios?.Any(e => e.Id == id)).GetValueOrDefault();//Se verifica que el contexto no sea nulo y que el usuario exista
        }
    }
}
