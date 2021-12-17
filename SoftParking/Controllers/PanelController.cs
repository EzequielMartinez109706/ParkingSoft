using SoftParking.Models;
using System;
using System.Web.Mvc;

namespace SoftParking.Controllers
{
    public class PanelController : Controller
    {
        private static AccesoDatos accesoDatos = new AccesoDatos();
        private static MvcModel mvcModel = new MvcModel();

        private MvcModel getListas()
        {
            mvcModel.lstTipoVehiculos = accesoDatos.getTipoVehiculos();
            mvcModel.lstTiposAbonos = accesoDatos.getTiposAbonos();
            mvcModel.lstTiposEstadias = accesoDatos.getTipoEstadias();
            mvcModel.lstAbonados = accesoDatos.lstReporteAbonado();
            mvcModel.lstTarifas = accesoDatos.getTarifas();
            mvcModel.lstUsuarios = accesoDatos.getUsuarios();
            return mvcModel;
        }
        // GET: Panel
        public ActionResult PanelControl()
        {
            if (Session["logueado"] == null)
            {
                Session.Clear();
                return RedirectToAction("Login", "Login");
            }
            return View(getListas());
        }

        [HttpPost]
        public ActionResult RegistrarAbonado(MvcModel mvc)
        {
            try
            {
                if (accesoDatos.registrarAbonado(mvc.abonado))
                {
                    mvcModel.abonado = new Abonado();
                    mvcModel.mostrarAlertSuccess = true;
                    return RedirectToAction("PanelControl");
                }
            }
            catch (Exception)
            {

                throw;
            }
            return View();
        }

        [HttpPost]
        public ActionResult ActualizarTipoEstadia(MvcModel mvc)
        {
            try
            {
                mvcModel.mostrarAlertSuccess = accesoDatos.actualizarTipoEstadias(mvc.tipoEstadias);
                return RedirectToAction("PanelControl");
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost]
        public ActionResult ActualizarTarifa(MvcModel mvc)
        {
            try
            {
                mvcModel.mostrarAlertSuccess = accesoDatos.actualizarTarifa(mvc.tarifa);
                return RedirectToAction("PanelControl");
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost]
        public ActionResult ActualizarAbonado(MvcModel mvc)
        {
            try
            {
                var cargado = accesoDatos.actualizarAbonado(mvc.abonado);
                if (cargado)
                {
                    mvcModel.mostrarAlertSuccess = true;
                }
                return RedirectToAction("PanelControl");
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost]
        public ActionResult GuardarUsuario(MvcModel mvc)
        {
            try
            {
                var cargado = accesoDatos.guardarUsuario(mvc.usuario);
                if (cargado)
                {
                    mvcModel.mostrarAlertSuccess = true;
                }
                return RedirectToAction("PanelControl");
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost]
        public ActionResult ActualizarUsuario(MvcModel mvc)
        {
            try
            {
                var cargado = accesoDatos.actualizarUsuario(mvc.usuario);
                if (cargado)
                {
                    mvcModel.mostrarAlertSuccess = true;
                }
                return RedirectToAction("PanelControl");
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost]
        public ActionResult ActualizarAbono(MvcModel mvc)
        {
            try
            {

                if (mvc.tipoAbonos.Id > 0)
                {
                    accesoDatos.actualizarTipoAbono(mvc.tipoAbonos);                    
                }
                else
                {
                    accesoDatos.nuevoTipoAbono(mvc.tipoAbonos);
                }
                return RedirectToAction("PanelControl");
            }
            catch (Exception)
            {

                throw;
            }
        }
       
    }
}