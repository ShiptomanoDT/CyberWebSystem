using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CyberWebSystem.Context;
using CyberWebSystem.Models;
using CyberWebSystem.Dtos;


namespace CyberWebSystem.Controllers
{
	public class ClientesController : Controller
	{
		private readonly MiContext _context;

		public ClientesController(MiContext context)
		{
			_context = context;
		}

		// GET: Clientes
		public async Task<IActionResult> Index()
		{
			return _context.Clientes != null ?
						View(await _context.Clientes.ToListAsync()) :
						Problem("Entity set 'MiContext.Clientes'  is null.");
		}

		// GET: Clientes/Details/
		public async Task<IActionResult> Details(int? id)
		{
			if (id == null || _context.Clientes == null)
			{
				return NotFound();
			}

			var cliente = await _context.Clientes
				.FirstOrDefaultAsync(m => m.Id == id);
			if (cliente == null)
			{
				return NotFound();
			}
			//Cargando datos de equipos sin alterar el contexto
			var equipos = await _context.Equipos.ToListAsync();
			//Agregando los Equipos a Viewdata
			ViewData["Equipos"] = equipos;

			var fletes = await _context.Flete.Include(f => f.Cliente)
										.ToListAsync();
			ViewData["Fletes"] = fletes;

			return View(cliente);
		}

		// GET: Clientes/VerFlete/
		public IActionResult ClienteFletes(int id)
		{
			//Esta linea busca el cliente por el id y carga los datos de los fletes
			//registrados para ese cliente
			var cliente = _context.Clientes.Include(c => c.Fletes)
								   .ThenInclude(f => f.Equipo)//Cargando datos de equipos sin alterar el contexto
								   .SingleOrDefault(c => c.Id == id);//Buscando el cliente por el id
			if (cliente == null)
			{
				return NotFound();
			}
			return View(cliente);
		}

		// GET: Clientes/Create
		public IActionResult Create()
		{
			return View();
		}

		// POST: Clientes/Create
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create([Bind("Id,NombreCompleto,Ci")] Cliente cliente)
		{
			if (ModelState.IsValid)
			{
				_context.Add(cliente);
				await _context.SaveChangesAsync();
				return RedirectToAction(nameof(Index));
			}
			return View(cliente);
		}

		// GET: Clientes/Edit/5
		public async Task<IActionResult> Edit(int? id)
		{
			if (id == null || _context.Clientes == null)
			{
				return NotFound();
			}

			var cliente = await _context.Clientes.FindAsync(id);
			if (cliente == null)
			{
				return NotFound();
			}
			return View(cliente);
		}

		// POST: Clientes/Edit/5
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, [Bind("Id,NombreCompleto,Ci")] Cliente cliente)
		{
			if (id != cliente.Id)
			{
				return NotFound();
			}

			if (ModelState.IsValid)
			{
				try
				{
					_context.Update(cliente);
					await _context.SaveChangesAsync();
				}
				catch (DbUpdateConcurrencyException)
				{
					if (!ClienteExists(cliente.Id))
					{
						return NotFound();
					}
					else
					{
						throw;
					}
				}
				return RedirectToAction(nameof(Index));
			}
			return View(cliente);
		}

		// GET: Clientes/Delete/5
		public async Task<IActionResult> Delete(int? id)
		{
			if (id == null || _context.Clientes == null)
			{
				return NotFound();
			}

			var cliente = await _context.Clientes
				.FirstOrDefaultAsync(m => m.Id == id);
			if (cliente == null)
			{
				return NotFound();
			}

			return View(cliente);
		}

		// POST: Clientes/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			if (_context.Clientes == null)
			{
				return Problem("Entity set 'MiContext.Clientes'  is null.");
			}
			var cliente = await _context.Clientes.FindAsync(id);
			if (cliente != null)
			{
				_context.Clientes.Remove(cliente);
			}

			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
		}

		//Metodo para crear un nuevo flete para un cliente
		//El boton que llama a este metodo esl de Stop que detendra el contador de tiempo
		//Y registrara el flete, al mismo tiempo cambia el estado del equipo a libre y se deshabilita hasta que 
		//el boton de play sea presionado
		public async Task<IActionResult> CrearFlete(int clienteId, int equipoId)
		{
			if (_context.Clientes == null || _context.Equipos == null)
			{
				return Problem("Entity set 'MiContext.Clientes' or 'MiContext.Equipos' is null.");
			}
			var cliente = await _context.Clientes.FindAsync(clienteId);
			var equipo = await _context.Equipos.FindAsync(equipoId);
			if (cliente == null || equipo == null)
			{
				return NotFound();
			}
			//Con esta linea editamos el estado del equipo a Libre
			equipo.Estado = EstadoEnum.Libre;
			var flete = new Flete
			{
				ClienteId = cliente.Id,
				EquipoId = equipo.Id,
				UsuarioId = 1,
				Fecha = DateTime.Now
			};
			_context.Flete.Add(flete);
			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Details), new { id = clienteId });
		}

		private bool ClienteExists(int id)
		{
			return (_context.Clientes?.Any(e => e.Id == id)).GetValueOrDefault();
		}
	}
}
