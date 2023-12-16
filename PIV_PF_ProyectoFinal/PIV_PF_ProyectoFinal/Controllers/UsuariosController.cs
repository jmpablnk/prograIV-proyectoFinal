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
    public class UsuariosController : Controller
    {
        private readonly FARMACIA_PROGRA4Context _context;

        public UsuariosController(FARMACIA_PROGRA4Context context)
        {
            _context = context;
        }

        // Usuarios
     //   [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Index()
        {
              return _context.Usuario != null ? 
                          View(await _context.Usuario.ToListAsync()) :
                          Problem("Entity set 'FARMACIA_PROGRA4Context.Usuario'  is null.");
        }

        // Detalles
     //   [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Usuario == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuario
                .FirstOrDefaultAsync(m => m.IdUsuario == id);
            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }




        // Crear
    //    [Authorize(Roles = "Administrador")]
        public IActionResult Create()
        {
            return View();
        }

        //Crear 
        [HttpPost]
        [ValidateAntiForgeryToken]
   //     [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Create([Bind("IdUsuario,IdentificacionUsuario,NombreCompletoUsuario,CorreoUsuario,TipoUsuario,EstadoUsuario,ContrasenaUsuario")] Usuario usuario)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(usuario);
                    await _context.SaveChangesAsync();
                    // return RedirectToAction(nameof(Index)); -- No deja ver el VieBag

                    // Establecer el mensaje de éxito solo si la operación tiene éxito
                    ViewBag.Mensaje = "El usuario se agregó correctamente.";
                    return View(usuario);
                }
                else
                {
                    // El modelo no es válido, probablemente debido a la validación personalizada
                    ViewBag.Error = "El modelo no es válido.";
                    return View(usuario);
                }
            }
            catch
            {
                // Agregar un mensaje de error a ViewBag
                ViewBag.Error = "Se produjo un error al intentar crear el usuario.";
                return View(usuario);
            }
        }

        public class VALIDACION_USUARIO : ValidationAttribute
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
  //      [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Usuario == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuario.FindAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }
            return View(usuario);
        }
        // Editar
        [HttpPost]
        [ValidateAntiForgeryToken]
   //     [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Edit(int id, [Bind("IdUsuario,IdentificacionUsuario,NombreCompletoUsuario,CorreoUsuario,TipoUsuario,EstadoUsuario,ContrasenaUsuario")] Usuario usuario)
        {
            if (id != usuario.IdUsuario)
            {
                return NotFound();
            }

           
                try
                {
                    _context.Update(usuario);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UsuarioExists(usuario.IdUsuario))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            
            return View(usuario);
        }

        //Eliminar 
   //     [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Usuario == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuario
                .FirstOrDefaultAsync(m => m.IdUsuario == id);
            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }

        // Eliminar
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
   //     [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Usuario == null)
            {
                return Problem("Entity set 'FARMACIA_PROGRA4Context.Usuario'  is null.");
            }
            var usuario = await _context.Usuario.FindAsync(id);
            if (usuario != null)
            {
                _context.Usuario.Remove(usuario);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UsuarioExists(int id)
        {
          return (_context.Usuario?.Any(e => e.IdUsuario == id)).GetValueOrDefault();
        }
    }
}
