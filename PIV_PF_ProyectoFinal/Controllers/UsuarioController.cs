using Microsoft.Ajax.Utilities;
using PIV_PF_ProyectoFinal.Models;
using PIV_PF_ProyectoFinal.Models.viewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace PIV_PF_ProyectoFinal.Controllers
{
    public class UsuarioController : Controller
    {
        // Este método maneja las solicitudes GET para mostrar el formulario de registro
        [HttpGet]
        public ActionResult RegistroUsuario()
        {
            return View();
        }

        // Este método maneja las solicitudes POST cuando se envía el formulario
        [HttpPost]
        public ActionResult RegistroUsuario(cAgregarUsuarios Registro)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(Registro);
                }

                using (Models.FARMACIA_PROGRA_FINALEntities db = new Models.FARMACIA_PROGRA_FINALEntities())
                {
                    T_USUARIOS usuario = new T_USUARIOS();
                    usuario.IDENTIFICACION = Registro.IDENTIFICACION;
                    usuario.NOMBRE = Registro.NOMBRE;
                    usuario.APELLIDO = Registro.APELLIDOS;
                    usuario.CORREO = Registro.CORREO;
                    usuario.CLAVE = Registro.CLAVE;
                    usuario.ESTADO = Registro.ESTADO;
                    //usuario.T_ROLES = Registro.IDROLE;      //aqui tengo error
                    Registro.IDROLE = 3; // contador

                    db.T_USUARIOS.Add(usuario);
                    db.SaveChanges();

                    ViewBag.Productos = 1;
                    ViewBag.MensajeProceso = "Se agregó el usuario correctamente";
                }

                return View(Registro);
            }
            catch (Exception ex)
            {
                ViewBag.ValorMensaje = 0;
                ViewBag.MensajeProceso = "Fallo al agregar el usuario: " + ex.Message;
                return View(Registro);
            }
        }

        //Editar usuarios

        [HttpGet]
        public ActionResult Editar(int id)
        {
            ActualizarUsuario A_Usuario = new ActualizarUsuario();
            using (Models.FARMACIA_PROGRA_FINALEntities db = new Models.FARMACIA_PROGRA_FINALEntities()) 
            {
                T_USUARIOS usuario = db.T_USUARIOS.FirstOrDefault(x => x.ID_USUARIO == id);
                A_Usuario = new ActualizarUsuario
                {
                    IDUSUARIO = A_Usuario.IDUSUARIO,
                    CLAVE = A_Usuario.CLAVE,
                    CORREO = A_Usuario.CORREO,
                    IDROLE = A_Usuario.IDROLE,
                    NOMBRE = A_Usuario.NOMBRE,
                    APELLIDOS = A_Usuario.APELLIDOS,
                    ESTADO = A_Usuario.ESTADO,
                };
            }
            return View(A_Usuario);
        }

        [HttpGet]
        public ActionResult Editar(ActualizarUsuario usuario)
        {
            {
                try
                {
                    if (!ModelState.IsValid)
                    {
                        return View(usuario);
                    }
                    using (Models.FARMACIA_PROGRA_FINALEntities db = new Models.FARMACIA_PROGRA_FINALEntities())
                    {
                        var Actualizar = db.T_USUARIOS.Find(usuario.IDUSUARIO);


                        Actualizar.NOMBRE = usuario.NOMBRE;
                        Actualizar.APELLIDO = usuario.APELLIDOS;
                        Actualizar.IDENTIFICACION = usuario.IDENTIFICACION;
                        Actualizar.CORREO = usuario.CORREO;
                        Actualizar.ESTADO = usuario.ESTADO;

                        db.Entry(Actualizar).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                        ViewBag.ValorMensaje = 1;
                        ViewBag.MensajeProceso = "Usuario actualizada correctamente";
                        List<RolesUsuarios> ROLES = new List<RolesUsuarios>();
                        List<SelectListItem> Lista = new List<SelectListItem>();
                        foreach (var role in ROLES)
                        {
                            Lista.Add(new SelectListItem
                            {
                                Text = role.Nombre.ToString(),
                                Value = role.IDRole.ToString()

                            }); ViewBag.listaRoles = Lista;
                        }
                    }
                }
                catch (Exception ex)
                {
                    ViewBag.ValorMensaje = 0;
                    ViewBag.MensajeProceso = "Fallo al actualizar la persona" + ex;
                    return View(usuario);
                }
            }
            return View(usuario);
        }

        //eliminar usuarios // 

        //Consultar usuarios//

        public ActionResult cConsultarUsuarios()
        {
            List<cListaUsuario> ConsultarLista = null;
            using (Models.FARMACIA_PROGRA_FINALEntities db = new Models.FARMACIA_PROGRA_FINALEntities())
            {
                ConsultarLista = (from Lista in db.T_USUARIOS
                                  select new cListaUsuario
                                  {
                                      ID_Usuario = Lista.ID_USUARIO,
                                      Identificacion = Lista.IDENTIFICACION,
                                      Nombre_Usuario = Lista.NOMBRE,
                                      Apellidos_Usuario = Lista.APELLIDO,
                                      ESTADO = Lista.ESTADO,
                                  }).ToList();
            }
            return View(ConsultarLista);

        }

    }
}
