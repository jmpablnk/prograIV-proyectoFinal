using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace PIV_PF_ProyectoFinal.Controllers
{
    public class RolesController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public RolesController(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        //Roles
      [Authorize(Roles = "Administrador")]
        public IActionResult Index()
        {
            var roles = _roleManager.Roles;
            return View(roles);
        }



        // Detalles del Rol 
       [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var role = await _roleManager.FindByIdAsync(id);
            return role == null
                ? NotFound()
                : View(role);
        }




        //Crear Roles
      [Authorize(Roles = "Administrador")]
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
     [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Create(IdentityRole model)
        {
            // Validar el modelo antes de crear el rol.
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            try
            {
                // Verificar si el rol ya existe.
                var roleExists = await _roleManager.RoleExistsAsync(model.Name);

                if (!roleExists)
                {
                    // Si no existe, crearlo.
                    await _roleManager.CreateAsync(new IdentityRole(model.Name));
                }

                // Mostrar mensaje de exito o redirigir.
                TempData["SuccessMessage"] = "Rol creado exitosamente.";
                ViewBag.Mensaje = "El Rol se agrego correctamente.";
                return View(model);
            }
            catch (Exception ex)
            {
                // Manejar las excepciones.

                ViewBag.Error = "Se produjo un error al intentar crear el producto. El error fue: " + ex;
                return View(model);

            }
        }





        // Editar Roles
       [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Edit(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role == null)
            {

                return NotFound();
            }
            return View(role);
        }
        //Editar Roles
      [Authorize(Roles = "Administrador")]
        [HttpPost]
        public async Task<IActionResult> Edit(string id, IdentityRole model)
        {
            var rolexiste = await _roleManager.FindByIdAsync(id);

            if (rolexiste == null)
            {
                ViewBag.Error = "Hubo error, vuelve a intentarlo";
            }

            if (ModelState.IsValid)
            {
                if (!string.IsNullOrEmpty(model.Name))
                {
                    rolexiste.Name = model.Name;
                }

                var result = await _roleManager.UpdateAsync(rolexiste);

                if (result.Succeeded)
                {
                    ViewBag.Mensaje = "Se edito el Rol con exito"; 
                }
            }
            return View(rolexiste);
        }




        // Eliminar Roles 
      [Authorize(Roles = "Administrador")]
        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);

            if (role == null)
            {
                return NotFound();
            }

            return View("Delete", role);
        }

        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);

            if (role == null)
            {
                return NotFound();
            }

            var eliminar = await _roleManager.DeleteAsync(role);

            if (eliminar.Succeeded)
            {
                return RedirectToAction("Index");
            }

            ModelState.AddModelError(string.Empty, "Error al eliminar el rol.");

            return View("Delete", role);
        }

        private async Task<bool> RolExistsAsync(string id) //Verifica si el rol con un identificador especifico existe
        {
            return await _roleManager.RoleExistsAsync(id);
        }
    }

}


