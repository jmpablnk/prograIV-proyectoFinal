using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize(Roles = "Administrador,Vendedor,Contador")]
        public async Task<IActionResult> Index()
        {
            var fARMACIA_PROGRA4Context = _context.DetallesFactura.Include(d => d.CodigoFacturaNavigation).Include(d => d.CodigoProductoNavigation);
            return View(await fARMACIA_PROGRA4Context.ToListAsync());
        }

        // Detalles

        [Authorize(Roles = "Administrador,Vendedor,Contador")]
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
            detallesFactura.Subtotal = _context.DetallesFactura.Sum(df => df.Subtotal);
            detallesFactura.Total = detallesFactura.Subtotal * 1.13m;

            return View(detallesFactura);
        }


        // Crear

        [Authorize(Roles = "Administrador,Vendedor")]
        public IActionResult Create()
        {
            ViewData["CodigoFactura"] = new SelectList(_context.Factura, "CodigoFactura", "CodigoFactura");
            ViewData["CodigoProducto"] = new SelectList(_context.Producto, "CodigoProducto", "CodigoProducto");
            return View();
        }



        // Crear 
        [HttpPost]
        [ValidateAntiForgeryToken]

        [Authorize(Roles = "Administrador,Vendedor")]
        public async Task<IActionResult> Create([Bind("IdDetallesFactura,Subtotal,Total,CodigoFactura,CodigoProducto")] DetallesFactura detallesFactura)
        {
            try
            {
                _context.Add(detallesFactura);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                throw;
            }
            ViewData["CodigoFactura"] = new SelectList(_context.Factura, "CodigoFactura", "CodigoFactura", detallesFactura.CodigoFactura);
            ViewData["CodigoProducto"] = new SelectList(_context.Producto, "CodigoProducto", "CodigoProducto", detallesFactura.CodigoProducto);
            return View(detallesFactura);
        }
    }
}





