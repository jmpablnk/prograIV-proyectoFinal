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

        [Authorize(Roles = "Administrador,Vendedor,Contador")]
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

        [Authorize(Roles = "Administrador,Vendedor")]
        public IActionResult Create()
        {
            ViewData["IdClientes"] = new SelectList(_context.Cliente, "IdClientes", "NombreCliente");
            return View();
        }
        // Crear
        [HttpPost]
        [ValidateAntiForgeryToken]

        [Authorize(Roles = "Administrador,Vendedor")]
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
    }
}
