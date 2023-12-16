using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CyberWebSystem.Context;
using CyberWebSystem.Models;

namespace CyberWebSystem.Controllers
{
    public class FletesController : Controller
    {
        private readonly MiContext _context;

        public FletesController(MiContext context)
        {
            _context = context;
        }

        // GET: Fletes
        public async Task<IActionResult> Index()
        {
            var miContext = _context.Flete.Include(f => f.Cliente).Include(f => f.Equipo).Include(f => f.Usuario);
            return View(await miContext.ToListAsync());
        }

        // GET: Fletes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Flete == null)
            {
                return NotFound();
            }

            var flete = await _context.Flete
                .Include(f => f.Cliente)
                .Include(f => f.Equipo)
                .Include(f => f.Usuario)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (flete == null)
            {
                return NotFound();
            }

            return View(flete);
        }

        // GET: Fletes/Create
        public IActionResult Create()
        {
            ViewData["ClienteId"] = new SelectList(_context.Clientes, "Id", "NombreCompleto");
            ViewData["EquipoId"] = new SelectList(_context.Equipos, "Id", "Codigo");
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "Id", "Email");
            return View();
        }

        // POST: Fletes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Fecha,UsuarioId,EquipoId,ClienteId")] Flete flete)
        {
            if (ModelState.IsValid)
            {
                _context.Add(flete);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClienteId"] = new SelectList(_context.Clientes, "Id", "NombreCompleto", flete.ClienteId);
            ViewData["EquipoId"] = new SelectList(_context.Equipos, "Id", "Codigo", flete.EquipoId);
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "Id", "Email", flete.UsuarioId);
            return View(flete);
        }

        // GET: Fletes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Flete == null)
            {
                return NotFound();
            }

            var flete = await _context.Flete.FindAsync(id);
            if (flete == null)
            {
                return NotFound();
            }
            ViewData["ClienteId"] = new SelectList(_context.Clientes, "Id", "NombreCompleto", flete.ClienteId);
            ViewData["EquipoId"] = new SelectList(_context.Equipos, "Id", "Codigo", flete.EquipoId);
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "Id", "Email", flete.UsuarioId);
            return View(flete);
        }

        // POST: Fletes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Fecha,UsuarioId,EquipoId,ClienteId")] Flete flete)
        {
            if (id != flete.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(flete);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FleteExists(flete.Id))
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
            ViewData["ClienteId"] = new SelectList(_context.Clientes, "Id", "NombreCompleto", flete.ClienteId);
            ViewData["EquipoId"] = new SelectList(_context.Equipos, "Id", "Codigo", flete.EquipoId);
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "Id", "Email", flete.UsuarioId);
            return View(flete);
        }

        // GET: Fletes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Flete == null)
            {
                return NotFound();
            }

            var flete = await _context.Flete
                .Include(f => f.Cliente)
                .Include(f => f.Equipo)
                .Include(f => f.Usuario)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (flete == null)
            {
                return NotFound();
            }

            return View(flete);
        }

        // POST: Fletes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Flete == null)
            {
                return Problem("Entity set 'MiContext.Flete'  is null.");
            }
            var flete = await _context.Flete.FindAsync(id);
            if (flete != null)
            {
                _context.Flete.Remove(flete);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FleteExists(int id)
        {
          return (_context.Flete?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
