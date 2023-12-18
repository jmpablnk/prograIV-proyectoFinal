using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PIV_PF_ProyectoFinal.Models;

namespace PIV_PF_ProyectoFinal.Controllers
{
    public class ClientesController : Controller
    {
        private readonly FARMACIA_PROGRA4Context _context;

        public ClientesController(FARMACIA_PROGRA4Context context)
        {
            _context = context;
        }



        // Clientes 
        //[Authorize(Roles = "Administrador,Vendedor")]
        public async Task<IActionResult> Index() //no tiene
        {
              return _context.Cliente != null ? 
                          View(await _context.Cliente.ToListAsync()) :
                          Problem("Entity set 'FARMACIA_PROGRA4Context.Cliente'  is null.");
        }




        // Detalles
        //[Authorize(Roles = "Administrador,Vendedor")]
        public async Task<IActionResult> Details(int? id) // no tiene
        {
            if (id == null || _context.Cliente == null)
            {
                return NotFound();
            }

            var cliente = await _context.Cliente
                .FirstOrDefaultAsync(m => m.IdClientes == id);
            if (cliente == null)
            {
                return NotFound();
            }

            return View(cliente);
        }





        // Crear
        [Authorize(Roles = "Administrador,Vendedor")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador,Vendedor")]
        public async Task<IActionResult> Create([Bind("IdCliente,Identificacion,NombreCliente,Apellido,Correo,Estado")] Cliente tCliente) // si tiene
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(tCliente);
                    await _context.SaveChangesAsync();
                    // return RedirectToAction(nameof(Index)); -- No deja ver el VieBag

                    // Establecer el mensaje de éxito solo si la operación tiene éxito
                    ViewBag.Mensaje = "El usuario se agregó correctamente.";
                    return View(tCliente);
                }
                else
                {
                    // El modelo no es válido, probablemente debido a la validación personalizada
                    ViewBag.Error = "El modelo no es válido.";
                    return View(tCliente);
                }
            }
            catch
            {
                // Agregar un mensaje de error a ViewBag
                ViewBag.Error = "Se produjo un error al intentar crear el usuario.";
                return View(tCliente);
            }
        }

        public class VALIDACIONCLIENTE : ValidationAttribute
        {
            protected override ValidationResult IsValid(object value, ValidationContext validationContext)
            {
                var dbContext = validationContext.GetService(typeof(FARMACIA_PROGRA4Context)) as FARMACIA_PROGRA4Context;

                if (dbContext == null)
                {
                    return new ValidationResult("No se encontrado la base de datos.");
                }

                var identificacion = (string)value;

                if (dbContext.Set<Cliente>().Any(c => c.Identificacion == identificacion))
                {
                    return new ValidationResult(ErrorMessage);
                }

                return ValidationResult.Success;
            }
        }

        // Editar
        [Authorize(Roles = "Administrador,Vendedor")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Cliente == null)
            {
                return NotFound();
            }

            var cliente = await _context.Cliente.FindAsync(id);
            if (cliente == null)
            {
                return NotFound();
            }
            return View(cliente);
        }



        // Editar
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador,Vendedor")] // tiene
        public async Task<IActionResult> Edit(int id, [Bind("IdClientes,Identificacion,NombreCliente,Correo")] Cliente cliente)
        {
            if (id != cliente.IdClientes)
            {
                return NotFound();
            }
                try
                {
                    _context.Update(cliente);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClienteExists(cliente.IdClientes))
                    {
                            // Agregar un mensaje de error a ViewBag
                          ViewBag.Error = "Se produjo un error al intentar actualizar el usuario.";
                    }
                    else
                    {
                        throw;
                    }
                }
                ViewBag.Mensaje = "El usuario se actualizo correctamente.";

            return View(cliente);
        }







        //Eliminar 
        [Authorize(Roles = "Administrador,Vendedor")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Cliente == null)
            {
                return NotFound();
            }

            var cliente = await _context.Cliente
                .FirstOrDefaultAsync(m => m.IdClientes == id);
            if (cliente == null)
            {
                return NotFound();
            }

            return View(cliente);
        }
        //Tengo que modificar ESTA PARTE AGREGANDOLE EL viewbag

        // Eliminar 
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador,Vendedor")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Cliente == null)
            {
                return Problem("Entity set 'FARMACIA_PROGRA4Context.Cliente'  is null.");
            }
            var cliente = await _context.Cliente.FindAsync(id);
            if (cliente != null)
            {
                _context.Cliente.Remove(cliente);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index)); // ---
        }

        private bool ClienteExists(int id)
        {
          return (_context.Cliente?.Any(e => e.IdClientes == id)).GetValueOrDefault();
        }
    }
}
