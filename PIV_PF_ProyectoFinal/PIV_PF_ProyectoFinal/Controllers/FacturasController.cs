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
    public class FacturasController : Controller
    {
        private readonly FARMACIA_PROGRA4Context _context;

        public FacturasController(FARMACIA_PROGRA4Context context)
        {
            _context = context;
        }



        // Facturas
        public async Task<IActionResult> Index()
        {
            var fARMACIA_PROGRA4Context = _context.Factura.Include(f => f.IdClientesNavigation);
            return View(await fARMACIA_PROGRA4Context.ToListAsync());
        }

        // Detalles
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.Factura == null)
            {
                return NotFound();
            }

            var factura = await _context.Factura
                .Include(f => f.IdClientesNavigation)
                .FirstOrDefaultAsync(m => m.CodigoFactura == id);
            if (factura == null)
            {
                return NotFound();
            }

            return View(factura);
        }




        // Crear
        public IActionResult Create()
        {
            ViewData["IdClientes"] = new SelectList(_context.Cliente, "IdClientes", "IdClientes");
            return View();
        }
        // Crear
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CodigoFactura,FechaCompra,Cantidad,MetodoPago,IdClientes")] Factura factura)
        {
            try
            {
                _context.Add(factura);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch 
            {
                throw;
            }
            ViewData["IdClientes"] = new SelectList(_context.Cliente, "IdClientes", "IdClientes", factura.IdClientes);
            return View(factura);
        }




        // Editar
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.Factura == null)
            {
                return NotFound();
            }

            var factura = await _context.Factura.FindAsync(id);
            if (factura == null)
            {
                return NotFound();
            }
            ViewData["IdClientes"] = new SelectList(_context.Cliente, "IdClientes", "IdClientes", factura.IdClientes);
            return View(factura);
        }

        // Editar
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("CodigoFactura,FechaCompra,Cantidad,MetodoPago,IdClientes")] Factura factura)
        {
            if (id != factura.CodigoFactura)
            {
                return NotFound();
            }

           
                try
                {
                    _context.Update(factura);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FacturaExists(factura.CodigoFactura))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            
            ViewData["IdClientes"] = new SelectList(_context.Cliente, "IdClientes", "IdClientes", factura.IdClientes);
            return View(factura);
        }




        //Eliminar
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.Factura == null)
            {
                return NotFound();
            }

            var factura = await _context.Factura
                .Include(f => f.IdClientesNavigation)
                .FirstOrDefaultAsync(m => m.CodigoFactura == id);
            if (factura == null)
            {
                return NotFound();
            }

            return View(factura);
        }

        // Eliminar
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.Factura == null)
            {
                return Problem("Entity set 'FARMACIA_PROGRA4Context.Factura'  is null.");
            }
            var factura = await _context.Factura.FindAsync(id);
            if (factura != null)
            {
                _context.Factura.Remove(factura);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FacturaExists(string id)
        {
          return (_context.Factura?.Any(e => e.CodigoFactura == id)).GetValueOrDefault();
        }
    }
}
