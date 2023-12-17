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
    public class ProductosController : Controller
    {
        private readonly FARMACIA_PROGRA4Context _context;

        public ProductosController(FARMACIA_PROGRA4Context context)
        {
            _context = context;
        }



        // Productos
        //[Authorize(Roles = "Administrador")] // No contiene viewbag
        public async Task<IActionResult> Index()
        {
            var fARMACIA_PROGRA4Context = _context.Producto.Include(p => p.CodigoTipoProductoNavigation);
            return View(await fARMACIA_PROGRA4Context.ToListAsync());
        }




        // Detalles
        //[Authorize(Roles = "Administrador")] // No contieen view bag
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.Producto == null)
            {
                return NotFound();
            }

            var producto = await _context.Producto
                .Include(p => p.CodigoTipoProductoNavigation)
                .FirstOrDefaultAsync(m => m.CodigoProducto == id);
            if (producto == null)
            {
                return NotFound();
            }

            return View(producto);
        }





        // Crear
        //[Authorize(Roles = "Administrador")]
        public IActionResult Create()
        {
            ViewData["CodigoTipoProducto"] = new SelectList(_context.TiposProducto, "CodigoTipoProducto", "CodigoTipoProducto");
            return View();
        }

        // Crear
        //[Authorize(Roles = "Administrador")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CodigoProducto,DescripcionProducto,Precio,Estado,CantidadStock,CodigoTipoProducto")] Producto producto) //si tiene
        {
            try
            {
                _context.Add(producto);
                await _context.SaveChangesAsync();
                ViewBag.Mensaje = "El producto se agrego correctamente.";
            }
            catch
            {
                ViewBag.Error = "Se produjo un error al intentar crear el producto.";
            }
            ViewData["CodigoTipoProducto"] = new SelectList(_context.TiposProducto, "CodigoTipoProducto", "CodigoTipoProducto", producto.CodigoTipoProducto);
            return View(producto);
        }





        // Editar
        //[Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.Producto == null)
            {
                return NotFound();
            }

            var producto = await _context.Producto.FindAsync(id);
            if (producto == null)
            {
                return NotFound();
            }
            ViewData["CodigoTipoProducto"] = new SelectList(_context.TiposProducto, "CodigoTipoProducto", "CodigoTipoProducto", producto.CodigoTipoProducto);
            return View(producto);
        }

        // Editar
        [HttpPost]
        [ValidateAntiForgeryToken]
        //[Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Edit(string id, [Bind("CodigoProducto,DescripcionProducto,Precio,Estado,CantidadStock,CodigoTipoProducto")] Producto producto)
        {
            if (id != producto.CodigoProducto)
            {
                return NotFound();
            }
                try
                {
                    _context.Update(producto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductoExists(producto.CodigoProducto))
                    {
                    // Agregar un mensaje de error a ViewBag
                    ViewBag.Error = "Se produjo un error al intentar actualizar el producto.";
                    }   
                    else
                    {
                        throw;
                    }
            }
            ViewBag.Mensaje = "El producto se actualizo correctamente.";
            ViewData["CodigoTipoProducto"] = new SelectList(_context.TiposProducto, "CodigoTipoProducto", "CodigoTipoProducto", producto.CodigoTipoProducto);
            return View(producto);
        }




        // Eliminar
        //[Authorize(Roles = "Administrador")] /// Me fatla
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.Producto == null)
            {
                return NotFound();
            }

            var producto = await _context.Producto
                .Include(p => p.CodigoTipoProductoNavigation)
                .FirstOrDefaultAsync(m => m.CodigoProducto == id);
            if (producto == null)
            {
                return NotFound();
            }

            return View(producto);
        }

        // Eliminar
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        //[Authorize(Roles = "Administrador")]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.Producto == null)
            {
                return Problem("Entity set 'FARMACIA_PROGRA4Context.Producto'  is null.");
            }
            var producto = await _context.Producto.FindAsync(id);
            if (producto != null)
            {
                _context.Producto.Remove(producto);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductoExists(string id)
        {
          return (_context.Producto?.Any(e => e.CodigoProducto == id)).GetValueOrDefault();
        }
    }
}
