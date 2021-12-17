using SoftParking.Models;
using System;
using System.Web.Mvc;

namespace SoftParking.Controllers
{
    public class LoginController : Controller
    {
        private static MvcModel mvcModelStatic = new MvcModel();
        private AccesoDatos accesoDatos = new AccesoDatos();
        // GET: Login
        public ActionResult Login()
        {
            var logueadoSession = Session["logueado"];
            if (logueadoSession != null && (bool)logueadoSession)
            {
                return RedirectToAction("Home", "Home");
            }
            return View(mvcModelStatic);
        }

        [HttpGet]
        public ActionResult ValidarUsuario(MvcModel mvc)
        {
            try
            {
                var usr = accesoDatos.validarUsuario(mvc.usuario);
                if (usr != null && usr.Logueado)
                {
                    Session["logueado"] = true;
                    Session["usr"] = usr;
                    return RedirectToAction("Home", "Home");
                }
                Session["logueado"] = false;
                ModelState.Clear();
                mvcModelStatic = new MvcModel();
                return RedirectToAction("Login", "Login", mvcModelStatic);
            }
            catch (Exception)
            {

                throw;
            }

        }

        public ActionResult CerrarSesion()
        {
            mvcModelStatic = new MvcModel();
            Session.Clear();
            return RedirectToAction("Login", "Login");
        }

        
    }
}