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
    public class TFacturasController : Controller
    {
        private readonly FARMACIA_PROGRA4Context _context;

        public TFacturasController(FARMACIA_PROGRA4Context context)
        {
            _context = context;
        }

        // GET: TFacturas
        public async Task<IActionResult> Index()
        {
            var fARMACIA_PROGRA4Context = _context.TFactura.Include(t => t.IdClienteNavigation).Include(t => t.IdProductoNavigation).Include(t => t.IdUsuarioNavigation);
            return View(await fARMACIA_PROGRA4Context.ToListAsync());
        }

        // GET: TFacturas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TFactura == null)
            {
                return NotFound();
            }

            var tFactura = await _context.TFactura
                .Include(t => t.IdClienteNavigation)
                .Include(t => t.IdProductoNavigation)
                .Include(t => t.IdUsuarioNavigation)
                .FirstOrDefaultAsync(m => m.IdFactura == id);
            if (tFactura == null)
            {
                return NotFound();
            }

            return View(tFactura);
        }

        // GET: TFacturas/Create
        public IActionResult Create()
        {
            ViewData["IdCliente"] = new SelectList(_context.TCliente, "IdCliente", "IdCliente");
            ViewData["IdProducto"] = new SelectList(_context.TProducto, "IdProducto", "IdProducto");
            ViewData["IdUsuario"] = new SelectList(_context.TUsuario, "IdUsuario", "IdUsuario");
            return View();
        }

        // POST: TFacturas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdFactura,FechaCompra,MontoTotal,Estadoactivo,IdCliente,IdUsuario,IdProducto")] TFactura tFactura)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tFactura);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdCliente"] = new SelectList(_context.TCliente, "IdCliente", "IdCliente", tFactura.IdCliente);
            ViewData["IdProducto"] = new SelectList(_context.TProducto, "IdProducto", "IdProducto", tFactura.IdProducto);
            ViewData["IdUsuario"] = new SelectList(_context.TUsuario, "IdUsuario", "IdUsuario", tFactura.IdUsuario);
            return View(tFactura);
        }

        // GET: TFacturas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TFactura == null)
            {
                return NotFound();
            }

            var tFactura = await _context.TFactura.FindAsync(id);
            if (tFactura == null)
            {
                return NotFound();
            }
            ViewData["IdCliente"] = new SelectList(_context.TCliente, "IdCliente", "IdCliente", tFactura.IdCliente);
            ViewData["IdProducto"] = new SelectList(_context.TProducto, "IdProducto", "IdProducto", tFactura.IdProducto);
            ViewData["IdUsuario"] = new SelectList(_context.TUsuario, "IdUsuario", "IdUsuario", tFactura.IdUsuario);
            return View(tFactura);
        }

        // POST: TFacturas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdFactura,FechaCompra,MontoTotal,Estadoactivo,IdCliente,IdUsuario,IdProducto")] TFactura tFactura)
        {
            if (id != tFactura.IdFactura)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tFactura);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TFacturaExists(tFactura.IdFactura))
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
            ViewData["IdCliente"] = new SelectList(_context.TCliente, "IdCliente", "IdCliente", tFactura.IdCliente);
            ViewData["IdProducto"] = new SelectList(_context.TProducto, "IdProducto", "IdProducto", tFactura.IdProducto);
            ViewData["IdUsuario"] = new SelectList(_context.TUsuario, "IdUsuario", "IdUsuario", tFactura.IdUsuario);
            return View(tFactura);
        }

        // GET: TFacturas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TFactura == null)
            {
                return NotFound();
            }

            var tFactura = await _context.TFactura
                .Include(t => t.IdClienteNavigation)
                .Include(t => t.IdProductoNavigation)
                .Include(t => t.IdUsuarioNavigation)
                .FirstOrDefaultAsync(m => m.IdFactura == id);
            if (tFactura == null)
            {
                return NotFound();
            }

            return View(tFactura);
        }

        // POST: TFacturas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TFactura == null)
            {
                return Problem("Entity set 'FARMACIA_PROGRA4Context.TFactura'  is null.");
            }
            var tFactura = await _context.TFactura.FindAsync(id);
            if (tFactura != null)
            {
                _context.TFactura.Remove(tFactura);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TFacturaExists(int id)
        {
          return (_context.TFactura?.Any(e => e.IdFactura == id)).GetValueOrDefault();
        }
    }
}
