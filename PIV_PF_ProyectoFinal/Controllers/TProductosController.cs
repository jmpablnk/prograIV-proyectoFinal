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
    public class TProductosController : Controller
    {
        private readonly FARMACIA_PROGRA4Context _context;

        public TProductosController(FARMACIA_PROGRA4Context context)
        {
            _context = context;
        }

        // GET: TProductos
        public async Task<IActionResult> Index()
        {
            var fARMACIA_PROGRA4Context = _context.TProducto.Include(t => t.IdTipoProductoNavigation);
            return View(await fARMACIA_PROGRA4Context.ToListAsync());
        }

        // GET: TProductos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TProducto == null)
            {
                return NotFound();
            }

            var tProducto = await _context.TProducto
                .Include(t => t.IdTipoProductoNavigation)
                .FirstOrDefaultAsync(m => m.IdProducto == id);
            if (tProducto == null)
            {
                return NotFound();
            }

            return View(tProducto);
        }

        // GET: TProductos/Create
        public IActionResult Create()
        {
            ViewData["IdTipoProducto"] = new SelectList(_context.TTipoDeProducto, "IdTipoProducto", "IdTipoProducto");
            return View();
        }

        // POST: TProductos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdProducto,Nombre,Descripcion,Precio,Estado,Cantidad,IdTipoProducto")] TProducto tProducto)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tProducto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdTipoProducto"] = new SelectList(_context.TTipoDeProducto, "IdTipoProducto", "IdTipoProducto", tProducto.IdTipoProducto);
            return View(tProducto);
        }

        // GET: TProductos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TProducto == null)
            {
                return NotFound();
            }

            var tProducto = await _context.TProducto.FindAsync(id);
            if (tProducto == null)
            {
                return NotFound();
            }
            ViewData["IdTipoProducto"] = new SelectList(_context.TTipoDeProducto, "IdTipoProducto", "IdTipoProducto", tProducto.IdTipoProducto);
            return View(tProducto);
        }

        // POST: TProductos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdProducto,Nombre,Descripcion,Precio,Estado,Cantidad,IdTipoProducto")] TProducto tProducto)
        {
            if (id != tProducto.IdProducto)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tProducto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TProductoExists(tProducto.IdProducto))
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
            ViewData["IdTipoProducto"] = new SelectList(_context.TTipoDeProducto, "IdTipoProducto", "IdTipoProducto", tProducto.IdTipoProducto);
            return View(tProducto);
        }

        // GET: TProductos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TProducto == null)
            {
                return NotFound();
            }

            var tProducto = await _context.TProducto
                .Include(t => t.IdTipoProductoNavigation)
                .FirstOrDefaultAsync(m => m.IdProducto == id);
            if (tProducto == null)
            {
                return NotFound();
            }

            return View(tProducto);
        }

        // POST: TProductos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TProducto == null)
            {
                return Problem("Entity set 'FARMACIA_PROGRA4Context.TProducto'  is null.");
            }
            var tProducto = await _context.TProducto.FindAsync(id);
            if (tProducto != null)
            {
                _context.TProducto.Remove(tProducto);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TProductoExists(int id)
        {
          return (_context.TProducto?.Any(e => e.IdProducto == id)).GetValueOrDefault();
        }
    }
}
