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
    public class TClientesController : Controller
    {
        private readonly FARMACIA_PROGRA4Context _context;

        public TClientesController(FARMACIA_PROGRA4Context context)
        {
            _context = context;
        }

        // GET: TClientes
        public async Task<IActionResult> Index()
        {
              return _context.TCliente != null ? 
                          View(await _context.TCliente.ToListAsync()) :
                          Problem("Entity set 'FARMACIA_PROGRA4Context.TCliente'  is null.");
        }

        // GET: TClientes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TCliente == null)
            {
                return NotFound();
            }

            var tCliente = await _context.TCliente
                .FirstOrDefaultAsync(m => m.IdCliente == id);
            if (tCliente == null)
            {
                return NotFound();
            }

            return View(tCliente);
        }

        // GET: TClientes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TClientes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdCliente,Identificacion,NombreCliente,Apellido,Correo,Estado")] TCliente tCliente)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tCliente);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tCliente);
        }

        // GET: TClientes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TCliente == null)
            {
                return NotFound();
            }

            var tCliente = await _context.TCliente.FindAsync(id);
            if (tCliente == null)
            {
                return NotFound();
            }
            return View(tCliente);
        }

        // POST: TClientes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdCliente,Identificacion,NombreCliente,Apellido,Correo,Estado")] TCliente tCliente)
        {
            if (id != tCliente.IdCliente)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tCliente);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TClienteExists(tCliente.IdCliente))
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
            return View(tCliente);
        }

        // GET: TClientes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TCliente == null)
            {
                return NotFound();
            }

            var tCliente = await _context.TCliente
                .FirstOrDefaultAsync(m => m.IdCliente == id);
            if (tCliente == null)
            {
                return NotFound();
            }

            return View(tCliente);
        }

        // POST: TClientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TCliente == null)
            {
                return Problem("Entity set 'FARMACIA_PROGRA4Context.TCliente'  is null.");
            }
            var tCliente = await _context.TCliente.FindAsync(id);
            if (tCliente != null)
            {
                _context.TCliente.Remove(tCliente);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TClienteExists(int id)
        {
          return (_context.TCliente?.Any(e => e.IdCliente == id)).GetValueOrDefault();
        }
    }
}
