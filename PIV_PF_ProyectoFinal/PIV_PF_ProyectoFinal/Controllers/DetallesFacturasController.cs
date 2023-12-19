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
            detallesFactura.Subtotal = detallesFactura.Total;
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
        public async Task<IActionResult> Create([Bind("IdDetallesFactura,Cantidad,CodigoFactura,CodigoProducto")] DetallesFactura detallesFactura)
        {
            try
            {
                // Recuperar las entidades Producto y Factura según los IDs proporcionados
                Producto producto = await _context.Producto.FindAsync(detallesFactura.CodigoProducto);
                Factura factura = await _context.Factura.FindAsync(detallesFactura.CodigoFactura);

                if (producto != null && factura != null)
                {
                    // Calcular Subtotal en función de Precio y Cantidad
                    detallesFactura.Subtotal = producto.Precio * factura.Cantidad;

                    // Calcular Total (suponiendo que hay cálculos adicionales)
                    detallesFactura.Total = detallesFactura.Subtotal;

                    // Agregar el registro al contexto
                    _context.Add(detallesFactura);

                    // Guardar cambios en la base de datos
                    await _context.SaveChangesAsync();

                    // Redirigir a la acción Index
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return RedirectToAction("Error");
                }
            }
            catch (Exception ex)
            {
                
                ViewData["CodigoFactura"] = new SelectList(_context.Factura, "CodigoFactura", "CodigoFactura", detallesFactura.CodigoFactura);
                ViewData["CodigoProducto"] = new SelectList(_context.Producto, "CodigoProducto", "CodigoProducto", detallesFactura.CodigoProducto);
            }
            return View(detallesFactura);
        }
    }
}





