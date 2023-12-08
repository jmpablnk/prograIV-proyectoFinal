using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PIV_PF_PROYECTOFINAL.Models;

namespace PIV_PF_PROYECTOFINAL.Controllers
{
    public class TUsuariosController : Controller
    {
        private readonly FARMACIA_PROGRA4Context _context;

        public TUsuariosController(FARMACIA_PROGRA4Context context)
        {
            _context = context;
        }

        // GET: TUsuarios
        public async Task<IActionResult> Index()
        {
            var fARMACIA_PROGRA4Context = _context.TUsuario.Include(t => t.IdRoleNavigation);
            return View(await fARMACIA_PROGRA4Context.ToListAsync());
        }

        // GET: TUsuarios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TUsuario == null)
            {
                return NotFound();
            }

            var tUsuario = await _context.TUsuario
                .Include(t => t.IdRoleNavigation)
                .FirstOrDefaultAsync(m => m.IdUsuario == id);
            if (tUsuario == null)
            {
                return NotFound();
            }

            return View(tUsuario);
        }

        // GET: TUsuarios/Create
        public IActionResult Create()
        {
            ViewData["IdRole"] = new SelectList(_context.TRole, "IdRole", "IdRole");
            return View();
        }

        // POST: TUsuarios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdUsuario,Identificacion,Nombre,Apellido,Correo,Clave,Estado,IdRole")] TUsuario tUsuario)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tUsuario);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdRole"] = new SelectList(_context.TRole, "IdRole", "IdRole", tUsuario.IdRole);
            return View(tUsuario);
        }

        // GET: TUsuarios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TUsuario == null)
            {
                return NotFound();
            }

            var tUsuario = await _context.TUsuario.FindAsync(id);
            if (tUsuario == null)
            {
                return NotFound();
            }
            ViewData["IdRole"] = new SelectList(_context.TRole, "IdRole", "IdRole", tUsuario.IdRole);
            return View(tUsuario);
        }

        // POST: TUsuarios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdUsuario,Identificacion,Nombre,Apellido,Correo,Clave,Estado,IdRole")] TUsuario tUsuario)
        {
            if (id != tUsuario.IdUsuario)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tUsuario);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TUsuarioExists(tUsuario.IdUsuario))
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
            ViewData["IdRole"] = new SelectList(_context.TRole, "IdRole", "IdRole", tUsuario.IdRole);
            return View(tUsuario);
        }

        // GET: TUsuarios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TUsuario == null)
            {
                return NotFound();
            }

            var tUsuario = await _context.TUsuario
                .Include(t => t.IdRoleNavigation)
                .FirstOrDefaultAsync(m => m.IdUsuario == id);
            if (tUsuario == null)
            {
                return NotFound();
            }

            return View(tUsuario);
        }

        // POST: TUsuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TUsuario == null)
            {
                return Problem("Entity set 'FARMACIA_PROGRA4Context.TUsuario'  is null.");
            }
            var tUsuario = await _context.TUsuario.FindAsync(id);
            if (tUsuario != null)
            {
                _context.TUsuario.Remove(tUsuario);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TUsuarioExists(int id)
        {
          return (_context.TUsuario?.Any(e => e.IdUsuario == id)).GetValueOrDefault();
        }
    }
}
