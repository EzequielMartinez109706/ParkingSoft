using IronPdf;
using QRCoder;
using SoftParking.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web.Mvc;

namespace SoftParking.Controllers
{
    public class HomeController : Controller
    {
        private static AccesoDatos accesoDatos = new AccesoDatos();
        private static MvcModel mvcModelStatic = new MvcModel();

        private MvcModel getListas()
        {
            mvcModelStatic.lstTipoVehiculos = accesoDatos.getTipoVehiculos();
            mvcModelStatic.lstTiposEstadias = accesoDatos.getTipoEstadias();
            mvcModelStatic.lstReporteIngresosVehiculos = accesoDatos.lstReporteIngresosVehiculos();
            mvcModelStatic.lstTarifas = accesoDatos.getTarifas();
            //mvcModelStatic.lstGastos = accesoDatos.getGastos();
            mvcModelStatic.lstAbonados = accesoDatos.getAbonados();
            mvcModelStatic.lstAbonadosDelDia = accesoDatos.getAbonadosDelDia();
            mvcModelStatic.lstPeriodos = accesoDatos.getPeriodos();
            mvcModelStatic.lstEstadias = accesoDatos.lstReporteEstadias(true);
            mvcModelStatic.montoCierreCaja = accesoDatos.getCierreCaja();
            mvcModelStatic.montoCierreCajaSubtotalEstadias = accesoDatos.getCierreCajaSubtotalEstadias();
            mvcModelStatic.montoCierreCajaSubtotalAbonos = accesoDatos.getCierreCajaSubtotalAbonos();
            mvcModelStatic.montoCierreCajaTotal = mvcModelStatic.montoCierreCaja + mvcModelStatic.montoCierreCajaSubtotalEstadias + mvcModelStatic.montoCierreCajaSubtotalAbonos;
            return mvcModelStatic;
        }
        public ActionResult Home()
        {
            if (Session["logueado"] == null)
            {
                Session.Clear();
                return RedirectToAction("Login", "Login");
            }
            var usr = (Usuario)Session["usr"];
            return View(getListas());
        }

        [HttpPost]
        public ActionResult RegistrarIngreso(MvcModel mvcModel)
        {
            try
            {
                accesoDatos.registrarIngreso(mvcModel.cliente);
                mvcModelStatic.mostrarAlertSuccess = true;
                QRCodeGenerator qrGenerator = new QRCodeGenerator();
                QRCodeData qrCodeData = qrGenerator.CreateQrCode(mvcModel.cliente.Dominio, QRCodeGenerator.ECCLevel.Q);
                QRCode qrCode = new QRCode(qrCodeData);
                var data = "";
                using (Bitmap bitMap = qrCode.GetGraphic(20))
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        bitMap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                        data = "data:image/png;base64," + Convert.ToBase64String(ms.ToArray());
                    }
                }
                var ticket = new Ticket();
                ticket.Dominio = mvcModel.cliente.Dominio;
                ticket.HoraIngreso = mvcModel.cliente.FechaHora.ToShortTimeString();
                ticket.TipoVehiculo = getTipoVehiculo(mvcModel.cliente.IdTipoVehiculo);
                ticket.Qr = data;

                Session["ticketIngreso"] = ticket;
                Thread.Sleep(5000);
                //GenerarPdfIngreso(ticket);
                return RedirectToAction("Home");
            }
            catch (Exception)
            {

                throw;
            }
        }

        private string getTipoVehiculo(long idTipoVehiculo)
        {
            switch (idTipoVehiculo)
            {
                case 1:
                    return "Auto";
                case 2:
                    return "Pick-up";
                case 3:
                    return "Moto";
                default:
                    return "";
            }
        }

        [HttpGet]
        public ActionResult GetDominioIngreso(string dominio)
        {
            try
            {
                if (!string.IsNullOrEmpty(dominio))
                {
                    var values = accesoDatos.getFechaIngreso(dominio);
                    DateTime? fecha = new DateTime();
                    if (values != null)
                    {
                        fecha = values.FechaHora;
                        Session["IdCliente"] = values.Id;
                    }

                    if (fecha == DateTime.Parse("01/01/0001 0:00:00"))
                    {
                        fecha = null;
                    }

                    if (values != null)
                    {
                        values.FechaNulleable = fecha;
                    }

                    return Json(values, JsonRequestBehavior.AllowGet);
                }
                return Json(null, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        [HttpPost]
        public ActionResult RegistrarEgreso(MvcModel mvcModel)
        {
            try
            {
                var venta = mvcModel.venta;
                venta.Dominio = mvcModel.cliente.Dominio;
                venta.IdCliente = Convert.ToInt32(Session["IdCliente"]);
                var cargadaDetalle = accesoDatos.registrarDetalleVenta(venta);

                if (cargadaDetalle > 0)
                {
                    venta.IdDetalleVenta = cargadaDetalle;
                    venta.HoraEgresoDate = Convert.ToDateTime(venta.HoraEgreso);
                    var cargado = accesoDatos.registrarVenta(venta);

                    if (cargado)
                    {
                        var cliente = accesoDatos.getDatosCliente(venta.IdCliente);
                        var t = new Ticket();
                        t.Dominio = venta.Dominio;
                        t.IdCliente = venta.IdCliente;
                        t.TipoVehiculo = cliente.TipoVehiculo;
                        t.HoraEgreso = DateTime.Parse(venta.HoraEgreso).ToShortTimeString();
                        t.HoraIngreso = cliente.FechaHora.ToShortTimeString();
                        t.Tiempo = tiempoAMinutos(t.HoraEgreso, t.HoraIngreso);
                        t.Importe = venta.Monto;
                        t.IdDetalleVenta = cargadaDetalle;
                        t.Fecha = DateTime.Now;
                        accesoDatos.registrarTicket(t);

                        Session["ticketEgreso"] = t;
                        Thread.Sleep(5000);
                        ModelState.Clear();
                        mvcModelStatic.mostrarAlertSuccess = true;
                        return RedirectToAction("Home");
                    }

                }

                return View("Home", getListas());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }

        }

        public ActionResult GenerarPdfEgreso()
        {
            var t = Session["ticketEgreso"] as Ticket;
            // Create a PDF from an existing HTML using C#
            var path = Server.MapPath("~/Content/plantillas/");

            //var html = System.IO.File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), path + "plantilla.html"));
            var html = plantilla(t);

            var htmlToPdf = new HtmlToPdf();
            var pdf = htmlToPdf.RenderHtmlAsPdf(html);

            Response.Clear();
            MemoryStream ms = new MemoryStream(pdf.BinaryData);
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "attachment;filename=egreso_" + t.Dominio + "_" + t.Fecha + ".pdf");
            Response.Buffer = true;
            ms.WriteTo(Response.OutputStream);
            Response.End();

            return new FileStreamResult(ms, "application/pdf");
        }

        [HttpPost]
        public ActionResult RegistrarEstadia(MvcModel mvcModel)
        {
            try
            {
                var lstTiposEstadias = accesoDatos.getTipoEstadias();
                Session["lstTiposEstadias"] = lstTiposEstadias;
                var fechaActual = DateTime.Now;
                var cantidadHoras = lstTiposEstadias.Single(x => x.IdTipoEstadia == mvcModel.ventaEstadias.Id_tipo_estadia).Horas;
                mvcModel.ventaEstadias.FechaIngreso = fechaActual;
                mvcModel.ventaEstadias.FechaEgreso = fechaActual.AddHours(cantidadHoras);
                var cargado = accesoDatos.registrarEstadia(mvcModel.ventaEstadias);
                if (cargado)
                {
                    Session["estadiaInserted"] = mvcModel.ventaEstadias;
                }
                Thread.Sleep(5000);
                ModelState.Clear();
                return View("Home", getListas());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public ActionResult GenerarPdfEstadia()
        {

            var ventaEstadia = Session["estadiaInserted"] as VentaEstadias;
            var lstTiposEstadias = Session["lstTiposEstadias"] as List<TipoEstadias>;
            var t = new Ticket();
            t.Dominio = ventaEstadia.Dominio.ToUpper();
            t.HoraEgreso = ventaEstadia.FechaEgreso.ToShortTimeString();
            t.HoraIngreso = ventaEstadia.FechaIngreso.ToShortTimeString();
            t.Importe = lstTiposEstadias.Single(x => x.IdTipoEstadia == ventaEstadia.Id_tipo_estadia).Monto;
            t.TipoEstadia = lstTiposEstadias.Single(x => x.IdTipoEstadia == ventaEstadia.Id_tipo_estadia).Tipo.ToUpper();

            // Create a PDF from an existing HTML using C#
            var path = Server.MapPath("~/Content/plantillas/");

            //var html = System.IO.File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), path + "plantilla.html"));
            var html = plantillaEstadia(t);

            var htmlToPdf = new HtmlToPdf();
            var pdf = htmlToPdf.RenderHtmlAsPdf(html);
            var fileName = "ticket_" + t.Dominio + "_" + ventaEstadia.FechaIngreso;

            Response.Clear();
            MemoryStream ms = new MemoryStream(pdf.BinaryData);
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "attachment;filename=" + fileName + ".pdf");
            Response.Buffer = true;
            ms.WriteTo(Response.OutputStream);
            Response.End();

            return new FileStreamResult(ms, "application/pdf");

        }

        private string plantillaEstadia(Ticket t = null)
        {
            return @"
                <!DOCTYPE html>
                <html>
                <head>  

                <style type=""text/css"">
    
            .contenedor {
            height: 390px;
            width: 300px;
            top: 60px;
            position: relative;
            margin: 0 auto;
            padding: 10px;
            }

            .border {
                border: 1px solid rgba(128, 128, 128, 0.3);
               }

            .text-center {
                text-align: center;
            }

            .text-left {
                text-align: left;
            }

            .title {
                margin-bottom: 0;
                margin-top: 30px;
                font-size: 20px;
            }

            .subtitle {
                margin: 0;
                font-size: 13px;
            }

            ul {
                list-style-type: none;
                text-align: left;
                padding-left: 5px;
            }

            .p-footer {
                font-size: 13px;
                margin: 5px;
            }
        
            .content-direccion {
                margin: 15px;
            }

            .p-direccion {
                margin: 5px;
                font-size: 13px;
            }
        </style>
        </head>

        <body>
            <div class=""contenedor border"">
        <div class=""title-content text-center"">
            <p class=""title""><strong>- PARKING NET -</strong></p>
            <p class=""subtitle"">de Martínez Pablo G.</p>
        </div>
        <hr>
        <div class=""content-result"">
            <ul>
                <li>
                    DOMINIO: ..................................... " + t.Dominio +
                @"</li>
                <li>
                    TIPO DE ESTADÍA: ................... " + t.TipoEstadia +
                @"</li>
                <li>
                    HORA DE INGRESO: ................... " + t.HoraIngreso +
                @"</li>
                <li>
                    HORA DE EGRESO: ..................... " + t.HoraEgreso +
                @"</li>
                <li>
                    MONTO: ......................................... $" + t.Importe +
                @"</li>
            </ul>
        </div>
        <hr>
        <div class=""content-footer text-center"">
            <p><strong> GRACIAS POR SU VISITA</strong></p>
        </div>
        <div class=""text-left"">
            <p class=""p-footer"">Horario de atención: lunes a viernes 7:30hs.a 19:00hs.</p>
            <p class=""p-footer"">Teléfonos: 351-6748817 / 4236783</p>
        </div>
        <div class=""content-direccion text-center"">
            <p class=""p-direccion"">9 de julio - 825 - Bº Alberdi - Córdoba, Argentina</p>
        </div>
            </div>

            </body>

        </html>";
        }

        public ActionResult Generar()
        {
            return View();
        }

        public void GenerarPdf(Ticket t)
        {
            // Create a PDF from an existing HTML using C#
            var path = Server.MapPath("~/Content/plantillas/");

            //var html = System.IO.File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), path + "plantilla.html"));
            var html = plantilla(t);

            var htmlToPdf = new HtmlToPdf();
            var pdf = htmlToPdf.RenderHtmlAsPdf(html);

            Response.Clear();
            MemoryStream ms = new MemoryStream(pdf.BinaryData);
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "attachment;filename=labtest.pdf");
            Response.Buffer = true;
            ms.WriteTo(Response.OutputStream);
            Response.End();

            new FileStreamResult(ms, "application/pdf");

        }


        private string plantilla(Ticket t = null)
        {
            return @"
                <!DOCTYPE html>
                <html>
                <head>  

                <style type=""text/css"">
    
            .contenedor {
            height: 390px;
            width: 300px;
            top: 60px;
            position: relative;
            margin: 0 auto;
            padding: 10px;
            }

            .border {
                border: 1px solid rgba(128, 128, 128, 0.3);
               }

            .text-center {
                text-align: center;
            }

            .text-left {
                text-align: left;
            }

            .title {
                margin-bottom: 0;
                margin-top: 30px;
                font-size: 20px;
            }

            .subtitle {
                margin: 0;
                font-size: 13px;
            }

            ul {
                list-style-type: none;
                text-align: left;
                padding-left: 5px;
            }

            .p-footer {
                font-size: 13px;
                margin: 5px;
            }
        
            .content-direccion {
                margin: 15px;
            }

            .p-direccion {
                margin: 5px;
                font-size: 13px;
            }
        </style>
        </head>

        <body>
            <div class=""contenedor border"">
        <div class=""title-content text-center"">
            <p class=""title""><strong>- PARKING NET -</strong></p>
            <p class=""subtitle"">de Martínez Pablo G.</p>
        </div>
        <hr>
        <div class=""content-result"">
            <ul>
                <li>
                    DOMINIO: ..................................... " + t.Dominio +
                @"</li>
                <li>
                    TIPO DE VEHICULO: ................... " + t.TipoVehiculo.ToUpper() +
                @"</li>
                <li>
                    HORA DE INGRESO: ................... " + t.HoraIngreso +
                @"</li>
                <li>
                    HORA DE EGRESO: ..................... " + t.HoraEgreso +
                @"</li>
                <li>
                    TIEMPO: ....................................... " + t.Tiempo +
                @"</li>
                <li>
                    MONTO: ......................................... $" + t.Importe +
                @"</li>
            </ul>
        </div>
        <hr>
        <div class=""content-footer text-center"">
            <p><strong> GRACIAS POR SU VISITA</strong></p>
        </div>
        <div class=""text-left"">
            <p class=""p-footer"">Horario de atención: lunes a viernes 7:30hs.a 19:00hs.</p>
            <p class=""p-footer"">Teléfonos: 351-6748817 / 4236783</p>
        </div>
        <div class=""content-direccion text-center"">
            <p class=""p-direccion"">9 de julio - 825 - Bº Alberdi - Córdoba, Argentina</p>
        </div>
            </div>

            </body>

        </html>";
        }

        public string tiempoAMinutos(string horaEgreso, string horaIngreso)
        {
            TimeSpan tiempo = DateTime.Parse(horaEgreso).Subtract(DateTime.Parse(horaIngreso));
            var splitTiempo = tiempo.ToString().Split(':');
            var result = splitTiempo[1] + "min"; // MINUTOS

            if (splitTiempo[0] != "00")
            {
                result = splitTiempo[0] + ":" + splitTiempo[1] + "hs";
            }
            return result;
        }

        [HttpPost]
        public ActionResult RegistrarGasto(MvcModel mvcModel)
        {
            try
            {
                mvcModel.gasto.Fecha = DateTime.Now;
                var cargado = accesoDatos.registrarGasto(mvcModel.gasto);
                if (cargado)
                {
                    return RedirectToAction("Home");
                }
                return View("Home", getListas());
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost]
        public ActionResult ConfirmarAbono(MvcModel mvcModel)
        {
            try
            {
                var lstAbonados = accesoDatos.getAbonados();
                var idDetallePago = accesoDatos.registrarDetallePago(mvcModel.abonado.Importe);
                if (idDetallePago > 0)
                {
                    var abonado = lstAbonados.Single(x => x.IdAbonado == mvcModel.abonado.IdAbonado);
                    abonado.IdPeriodo = mvcModel.abonado.IdPeriodo;
                    abonado.Importe = mvcModel.abonado.Importe;
                    abonado.IdDetallePago = idDetallePago;
                    abonado.Fecha = DateTime.Now;
                    var cargado = accesoDatos.registrarPagoAbonado(abonado);

                    if (cargado)
                    {
                        mvcModelStatic.mostrarAlertSuccess = true;
                        Session["confirmacionAbono"] = abonado;
                        Session["lstPeriodos"] = accesoDatos.getPeriodos();

                        Thread.Sleep(5000);
                        ModelState.Clear();
                        return RedirectToAction("Home");
                    }
                }
                return View("Home", getListas());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public ActionResult GenerarPdfConfirmacionAbono()
        {
            var confirmacionAbono = Session["confirmacionAbono"] as Abonado;
            var lstPeriodos = Session["lstPeriodos"] as List<Periodo>;
            var t = new Ticket();
            t.Dominio = confirmacionAbono.Dominio.ToUpper();
            t.Nombre = confirmacionAbono.Nombre + " " + confirmacionAbono.Apellido;
            t.Periodo = lstPeriodos.Single(x => x.Id_periodo == confirmacionAbono.IdPeriodo).Mes_periodo;
            t.Importe = Convert.ToDecimal(confirmacionAbono.Importe);

            // Create a PDF from an existing HTML using C#
            var path = Server.MapPath("~/Content/plantillas/");
            
            var html = plantillaAbono(t);

            var htmlToPdf = new HtmlToPdf();
            var pdf = htmlToPdf.RenderHtmlAsPdf(html);
            var fileName = "ticket_" + t.Dominio + "_" + confirmacionAbono.Fecha;

            Response.Clear();
            MemoryStream ms = new MemoryStream(pdf.BinaryData);
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "attachment;filename=" + fileName + ".pdf");
            Response.Buffer = true;
            ms.WriteTo(Response.OutputStream);
            Response.End();

            return new FileStreamResult(ms, "application/pdf");
        }

        private string plantillaAbono(Ticket t = null)
        {
            return @"
                <!DOCTYPE html>
                <html>
                <head>  

                <style type=""text/css"">
    
            .contenedor {
            height: 390px;
            width: 300px;
            top: 60px;
            position: relative;
            margin: 0 auto;
            padding: 10px;
            }

            .border {
                border: 1px solid rgba(128, 128, 128, 0.3);
               }

            .text-center {
                text-align: center;
            }

            .text-left {
                text-align: left;
            }

            .title {
                margin-bottom: 0;
                margin-top: 30px;
                font-size: 20px;
            }

            .subtitle {
                margin: 0;
                font-size: 13px;
            }

            ul {
                list-style-type: none;
                text-align: left;
                padding-left: 5px;
            }

            .p-footer {
                font-size: 13px;
                margin: 5px;
            }
        
            .content-direccion {
                margin: 15px;
            }

            .p-direccion {
                margin: 5px;
                font-size: 13px;
            }
        </style>
        </head>

        <body>
            <div class=""contenedor border"">
        <div class=""title-content text-center"">
            <p class=""title""><strong>- PARKING NET -</strong></p>
            <p class=""subtitle"">de Martínez Pablo G.</p>
        </div>
        <hr>
        <div class=""content-result"">
            <ul>
                <li>
                    DOMINIO: ..................................... " + t.Dominio +
                @"</li>
                <li>
                    NOMBRE: ................... " + t.Nombre.ToUpper() +
                @"</li>
                <li>
                    PERIODO: ..................................... " + t.Periodo.ToUpper() +
                @"</li>
                <li>
                    IMPORTE: ..................................... $" + t.Importe +
                @"
            </ul>
        </div>
        <hr>
        <div class=""content-footer text-center"">
            <p><strong> GRACIAS POR SU VISITA</strong></p>
        </div>
        <div class=""text-left"">
            <p class=""p-footer"">Horario de atención: lunes a viernes 7:30hs.a 19:00hs.</p>
            <p class=""p-footer"">Teléfonos: 351-6748817 / 4236783</p>
        </div>
        <div class=""content-direccion text-center"">
            <p class=""p-direccion"">9 de julio - 825 - Bº Alberdi - Córdoba, Argentina</p>
        </div>
            </div>

            </body>

        </html>";
        }

        public void GenerarPdfIngreso()
        {
            var t = Session["ticketIngreso"] as Ticket;
            // Create a PDF from an existing HTML using C#
            var path = Server.MapPath("~/Content/plantillas/");

            //var html = System.IO.File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), path + "plantilla.html"));
            var html = plantillaIngreso(t);

            var htmlToPdf = new HtmlToPdf();
            var pdf = htmlToPdf.RenderHtmlAsPdf(html);

            Response.Clear();
            MemoryStream ms = new MemoryStream(pdf.BinaryData);
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "attachment;filename=ingreso_"+ t.Dominio + "_" + t.Fecha +".pdf");
            Response.Buffer = true;
            ms.WriteTo(Response.OutputStream);
            Response.End();

            new FileStreamResult(ms, "application/pdf");

        }

        private string plantillaIngreso(Ticket t = null)
        {
            return @"
                <!DOCTYPE html>
                <html>
                <head>  

                <style type=""text/css"">
    
            .contenedor {
            height: 390px;
            width: 300px;
            top: 60px;
            position: relative;
            margin: 0 auto;
            padding: 10px;
            }

            .border {
                border: 1px solid rgba(128, 128, 128, 0.3);
               }

            .text-center {
                text-align: center;
            }

            .text-left {
                text-align: left;
            }

            .title {
                margin-bottom: 0;
                margin-top: 30px;
                font-size: 20px;
            }

            .subtitle {
                margin: 0;
                font-size: 13px;
            }

            ul {
                list-style-type: none;
                text-align: left;
                padding-left: 5px;
            }

            .p-footer {
                font-size: 13px;
                margin: 5px;
            }
        
            .content-direccion {
                margin: 15px;
            }

            .p-direccion {
                margin: 5px;
                font-size: 13px;
            }
        </style>
        </head>

        <body>
            <div class=""contenedor border"">
        <div class=""title-content text-center"">
            <p class=""title""><strong>- PARKING NET -</strong></p>
            <p class=""subtitle"">de Martínez Pablo G.</p>
        </div>
        <hr>
        <div class=""content-result"">
            <ul>
                <li>
                    DOMINIO: ..................................... " + t.Dominio +
                @"</li>
                <li>
                    TIPO DE VEHICULO: ................... " + t.TipoVehiculo.ToUpper() +
                @"</li>
                <li>
                    HORA DE INGRESO: ................... " + t.HoraIngreso +
                @"</li>
                <li> <img style=""display: block; margin: 0 auto"" src=""" +
                    t.Qr +
                @""" width=""100"" height=""100""/></li>
            </ul>
        </div>
        <hr>
        <div class=""content-footer text-center"">
            <p><strong> GRACIAS POR SU VISITA</strong></p>
        </div>
        <div class=""text-left"">
            <p class=""p-footer"">Horario de atención: lunes a viernes 7:30hs.a 19:00hs.</p>
            <p class=""p-footer"">Teléfonos: 351-6748817 / 4236783</p>
        </div>
        <div class=""content-direccion text-center"">
            <p class=""p-direccion"">9 de julio - 825 - Bº Alberdi - Córdoba, Argentina</p>
        </div>
            </div>

            </body>

        </html>";
        }

        public ActionResult CerrarSesion()
        {
            ModelState.Clear();
            Session.Clear();
            return View("Login", "Login");
        }


        public ActionResult Terminos()
        {
            if (Session["logueado"] == null)
            {
                Session.Clear();
                return RedirectToAction("Login", "Login");
            }
            var usr = (Usuario)Session["usr"];
            return View(getListas());
        }

        public ActionResult Preguntas()
        {
            if (Session["logueado"] == null)
            {
                Session.Clear();
                return RedirectToAction("Login", "Login");
            }
            var usr = (Usuario)Session["usr"];
            return View(getListas());
        }
    }


}