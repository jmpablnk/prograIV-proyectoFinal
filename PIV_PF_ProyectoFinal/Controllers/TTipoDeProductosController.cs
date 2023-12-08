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
    public class TTipoDeProductosController : Controller
    {
        private readonly FARMACIA_PROGRA4Context _context;

        public TTipoDeProductosController(FARMACIA_PROGRA4Context context)
        {
            _context = context;
        }

        // GET: TTipoDeProductos
        public async Task<IActionResult> Index()
        {
              return _context.TTipoDeProducto != null ? 
                          View(await _context.TTipoDeProducto.ToListAsync()) :
                          Problem("Entity set 'FARMACIA_PROGRA4Context.TTipoDeProducto'  is null.");
        }

        // GET: TTipoDeProductos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TTipoDeProducto == null)
            {
                return NotFound();
            }

            var tTipoDeProducto = await _context.TTipoDeProducto
                .FirstOrDefaultAsync(m => m.IdTipoProducto == id);
            if (tTipoDeProducto == null)
            {
                return NotFound();
            }

            return View(tTipoDeProducto);
        }

        // GET: TTipoDeProductos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TTipoDeProductos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdTipoProducto,NombreTipoProducto,Descripcion")] TTipoDeProducto tTipoDeProducto)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tTipoDeProducto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tTipoDeProducto);
        }

        // GET: TTipoDeProductos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TTipoDeProducto == null)
            {
                return NotFound();
            }

            var tTipoDeProducto = await _context.TTipoDeProducto.FindAsync(id);
            if (tTipoDeProducto == null)
            {
                return NotFound();
            }
            return View(tTipoDeProducto);
        }

        // POST: TTipoDeProductos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdTipoProducto,NombreTipoProducto,Descripcion")] TTipoDeProducto tTipoDeProducto)
        {
            if (id != tTipoDeProducto.IdTipoProducto)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tTipoDeProducto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TTipoDeProductoExists(tTipoDeProducto.IdTipoProducto))
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
            return View(tTipoDeProducto);
        }

        // GET: TTipoDeProductos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TTipoDeProducto == null)
            {
                return NotFound();
            }

            var tTipoDeProducto = await _context.TTipoDeProducto
                .FirstOrDefaultAsync(m => m.IdTipoProducto == id);
            if (tTipoDeProducto == null)
            {
                return NotFound();
            }

            return View(tTipoDeProducto);
        }

        // POST: TTipoDeProductos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TTipoDeProducto == null)
            {
                return Problem("Entity set 'FARMACIA_PROGRA4Context.TTipoDeProducto'  is null.");
            }
            var tTipoDeProducto = await _context.TTipoDeProducto.FindAsync(id);
            if (tTipoDeProducto != null)
            {
                _context.TTipoDeProducto.Remove(tTipoDeProducto);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TTipoDeProductoExists(int id)
        {
          return (_context.TTipoDeProducto?.Any(e => e.IdTipoProducto == id)).GetValueOrDefault();
        }
    }
}
