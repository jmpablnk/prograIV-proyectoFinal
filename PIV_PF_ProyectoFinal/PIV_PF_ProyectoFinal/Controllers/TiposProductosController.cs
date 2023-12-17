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
    public class TiposProductosController : Controller
    {
        private readonly FARMACIA_PROGRA4Context _context;

        public TiposProductosController(FARMACIA_PROGRA4Context context)
        {
            _context = context;
        }



        //Tipo de productos
        //[Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Index() //tampoco tiene
        {
              return _context.TiposProducto != null ? 
                          View(await _context.TiposProducto.ToListAsync()) :
                          Problem("Entity set 'FARMACIA_PROGRA4Context.TiposProducto'  is null.");
        }



        //Detalles
        //[Authorize(Roles = "Administrador")] // No contiene viewbag
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.TiposProducto == null)
            {
                return NotFound();
            }

            var tiposProducto = await _context.TiposProducto
                .FirstOrDefaultAsync(m => m.CodigoTipoProducto == id);
            if (tiposProducto == null)
            {
                return NotFound();
            }

            return View(tiposProducto);
        }




        // Crear
        public IActionResult Create()
        {
            return View();
        }
        // Crear
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CodigoTipoProducto,DescripcionTipoProducto")] TiposProducto tiposProducto)
        {
            try
            {
                _context.Add(tiposProducto);
                await _context.SaveChangesAsync();
                ViewBag.Mensaje = "El producto se agrego correctamente.";
            }
            catch
            {
                ViewBag.Error = "Se produjo un error al intentar crear el producto.";
            }
            
            return View(tiposProducto);
        }







        // Editar
        //[Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.TiposProducto == null)
            {
                return NotFound();
            }

            var tiposProducto = await _context.TiposProducto.FindAsync(id);
            if (tiposProducto == null)
            {
                return NotFound();
            }
            return View(tiposProducto);
        }
        // Editar
        [HttpPost]
        [ValidateAntiForgeryToken]
        //[Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Edit(string id, [Bind("CodigoTipoProducto,DescripcionTipoProducto")] TiposProducto tiposProducto)
        {
            if (id != tiposProducto.CodigoTipoProducto)
            {
                return NotFound();
            }

           
                try
                {
                    _context.Update(tiposProducto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TiposProductoExists(tiposProducto.CodigoTipoProducto))
                    {

                    ViewBag.Error = "Se produjo un error al intentar actualizar el producto.";
                }
                    else
                    {
                        throw;
                    }
                }
            ViewBag.Mensaje = "El producto se actualizo correctamente.";
            return View(tiposProducto);
        }









        // Eliminar
        //[Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.TiposProducto == null)
            {
                return NotFound();
            }

            var tiposProducto = await _context.TiposProducto
                .FirstOrDefaultAsync(m => m.CodigoTipoProducto == id);
            if (tiposProducto == null)
            {
                return NotFound();
            }

            return View(tiposProducto);
        }


        // Eliminar
        //[Authorize(Roles = "Administrador")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.TiposProducto == null)
            {
                return Problem("Entity set 'FARMACIA_PROGRA4Context.TiposProducto'  is null.");
            }
            var tiposProducto = await _context.TiposProducto.FindAsync(id);
            if (tiposProducto != null)
            {
                _context.TiposProducto.Remove(tiposProducto);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TiposProductoExists(string id)
        {
          return (_context.TiposProducto?.Any(e => e.CodigoTipoProducto == id)).GetValueOrDefault();
        }
    }
}
