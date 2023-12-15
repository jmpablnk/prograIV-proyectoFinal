using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PIV_PF_ProyectoFinal.Models;

namespace PIV_PF_ProyectoFinal.Controllers
{
    public class DetallesFacturasController : Controller
    {
        private readonly FARMACIA_PROGRA4Context _context;

        public DetallesFacturasController(FARMACIA_PROGRA4Context context)
        {
            _context = context;
        }




        // Detalles Facturas
        public async Task<IActionResult> Index()
        {
            var fARMACIA_PROGRA4Context = _context.DetallesFactura.Include(d => d.CodigoFacturaNavigation).Include(d => d.CodigoProductoNavigation);
            return View(await fARMACIA_PROGRA4Context.ToListAsync());
        }





        // Detalles
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.DetallesFactura == null)
            {
                return NotFound();
            }

            var detallesFactura = await _context.DetallesFactura
                .Include(d => d.CodigoFacturaNavigation)
                .Include(d => d.CodigoProductoNavigation)
                .FirstOrDefaultAsync(m => m.IdDetallesFactura == id);
            if (detallesFactura == null)
            {
                return NotFound();
            }

            return View(detallesFactura);
        }





        // Crear
        public IActionResult Create()
        {
            ViewData["CodigoFactura"] = new SelectList(_context.Factura, "CodigoFactura", "CodigoFactura");
            ViewData["CodigoProducto"] = new SelectList(_context.Producto, "CodigoProducto", "CodigoProducto");
            return View();
        }



        // Crear 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdDetallesFactura,Subtotal,Total,CodigoFactura,CodigoProducto")] DetallesFactura detallesFactura)
        {
            if (ModelState.IsValid)
            {
                _context.Add(detallesFactura);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CodigoFactura"] = new SelectList(_context.Factura, "CodigoFactura", "CodigoFactura", detallesFactura.CodigoFactura);
            ViewData["CodigoProducto"] = new SelectList(_context.Producto, "CodigoProducto", "CodigoProducto", detallesFactura.CodigoProducto);
            return View(detallesFactura);
        }




        // Editar
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.DetallesFactura == null)
            {
                return NotFound();
            }

            var detallesFactura = await _context.DetallesFactura.FindAsync(id);
            if (detallesFactura == null)
            {
                return NotFound();
            }
            ViewData["CodigoFactura"] = new SelectList(_context.Factura, "CodigoFactura", "CodigoFactura", detallesFactura.CodigoFactura);
            ViewData["CodigoProducto"] = new SelectList(_context.Producto, "CodigoProducto", "CodigoProducto", detallesFactura.CodigoProducto);
            return View(detallesFactura);
        }




        // Editar
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdDetallesFactura,Subtotal,Total,CodigoFactura,CodigoProducto")] DetallesFactura detallesFactura)
        {
            if (id != detallesFactura.IdDetallesFactura)
            {
                return NotFound();
            }

           
                try
                {
                    _context.Update(detallesFactura);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DetallesFacturaExists(detallesFactura.IdDetallesFactura))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            
            ViewData["CodigoFactura"] = new SelectList(_context.Factura, "CodigoFactura", "CodigoFactura", detallesFactura.CodigoFactura);
            ViewData["CodigoProducto"] = new SelectList(_context.Producto, "CodigoProducto", "CodigoProducto", detallesFactura.CodigoProducto);
            return View(detallesFactura);
        }



        // Eliminar
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.DetallesFactura == null)
            {
                return NotFound();
            }

            var detallesFactura = await _context.DetallesFactura
                .Include(d => d.CodigoFacturaNavigation)
                .Include(d => d.CodigoProductoNavigation)
                .FirstOrDefaultAsync(m => m.IdDetallesFactura == id);
            if (detallesFactura == null)
            {
                return NotFound();
            }

            return View(detallesFactura);
        }



        // Eliminar 
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.DetallesFactura == null)
            {
                return Problem("Entity set 'FARMACIA_PROGRA4Context.DetallesFactura'  is null.");
            }
            var detallesFactura = await _context.DetallesFactura.FindAsync(id);
            if (detallesFactura != null)
            {
                _context.DetallesFactura.Remove(detallesFactura);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DetallesFacturaExists(int id)
        {
          return (_context.DetallesFactura?.Any(e => e.IdDetallesFactura == id)).GetValueOrDefault();
        }
    }
}
