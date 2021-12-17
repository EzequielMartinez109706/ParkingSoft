using IronPdf;
using SoftParking.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web.Mvc;

namespace SoftParking.Controllers
{
    public class ReporteController : Controller
    {
        private static AccesoDatos accesoDatos = new AccesoDatos();
        private static MvcModel mvcModelStatic = new MvcModel();
        // GET: Reporte

        private void CargarListas()
        {
            mvcModelStatic.lstEstadias = accesoDatos.lstReporteEstadias();
            mvcModelStatic.lstPeriodoEstadias = lstPeriodosEstadias();
            mvcModelStatic.lstAbonados = accesoDatos.lstReporteAbonado();
            mvcModelStatic.lstReporteCaja = accesoDatos.getReporteCaja();
            mvcModelStatic.lstGastos = accesoDatos.getGastos();
        }

        private List<PeriodosEstadias> lstPeriodosEstadias()
        {
            var lst = new List<PeriodosEstadias>();

            var periodo = new PeriodosEstadias();
            //periodo.Id = 1;
            //periodo.Detalle = "Semanal";
            //lst.Add(periodo);

            periodo = new PeriodosEstadias();
            periodo.Id = 2;
            periodo.Detalle = "Mensual";
            lst.Add(periodo);

            periodo = new PeriodosEstadias();
            periodo.Id = 3;
            periodo.Detalle = "Anual";
            lst.Add(periodo);

            return lst;
        }
        public ActionResult PanelReportes()
        {
            CargarListas();
            foreach (var item in mvcModelStatic.lstAbonados)
            {
                item.FechaStr = item.Fecha.ToString() != "01/01/0001 0:00:00" ? item.Fecha.ToString() : "";
            }
            return View(mvcModelStatic);
        }

        public ActionResult FiltrarEstadias(MvcModel mvc)
        {
            CargarListas();
            mvcModelStatic.mostrarModalEstadias = true;
            ViewBag.loadModalEstadias = true;
            mvcModelStatic.lstEstadias = accesoDatos.lstReporteEstadiasFiltrado(mvc.periodosEstadias.Id);
            return View("PanelReportes", mvcModelStatic);
        }

        public ActionResult FiltrarAbonos(MvcModel mvc)
        {
            CargarListas();
            ViewBag.loadModalAbonos = true;
            mvcModelStatic.lstAbonados = accesoDatos.lstReporteAbonosFiltrado(mvc.periodosEstadias.Id);
            return View("PanelReportes", mvcModelStatic);
        }

        public void GenerarPdf(string tipo)
        {
            // Create a PDF from an existing HTML using C#
            var path = Server.MapPath("~/Content/plantillas/");

            //var html = System.IO.File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), path + "plantilla.html"));
            var html = "";
            switch (tipo)
            {
                case "estadias":
                    html = plantillaEstadias();
                    break;
                case "abonados":
                    html = plantillaAbonados();
                    break;
                case "gastos":
                    html = plantillaGastos();
                    break;
                default:
                    break;
            }

            var htmlToPdf = new HtmlToPdf();
            var pdf = htmlToPdf.RenderHtmlAsPdf(html);
            //var pdf = htmlToPdf.RenderHtmlAsPdf(html);

            Response.Clear();
            MemoryStream ms = new MemoryStream(pdf.BinaryData);
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "attachment;filename=reporte_" + tipo + ".pdf");
            Response.Buffer = true;
            ms.WriteTo(Response.OutputStream);
            Response.End();

            new FileStreamResult(ms, "application/pdf");

        }

        private string plantillaEstadias()
        {
            var results = "";
            foreach (var item in mvcModelStatic.lstEstadias)
            {
                results += @"
                                            <tr>
                                                <th scope=""row"">" + item.Dominio + @"</th>
 
                                                <td>" + item.TipoEstadia + @"</td>
 
                                                <td>$" + item.Importe + @"</td>
   
                                            <td>" + item.Fecha + @"</td>
   
                                       </tr> ";
            }
            return @"
                <!DOCTYPE html>
                <html>
                <head>  

                <style type=""text/css"">
    
            .contenedor {
            height: 390px;
            width: 500px;
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

            .table {
                width: 100%;
                max-width: 100%;
                margin-bottom: 20px;
                border-spacing: 0;
                border-collapse: collapse;
            }

            .table>thead>tr>th, .table>tbody>tr>th, .table>tfoot>tr>th, .table>thead>tr>td, .table>tbody>tr>td, .table>tfoot>tr>td {
                padding: 8px;
                line-height: 1.42857143;
                vertical-align: top;
                border-top: 1px solid #ddd;
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
           <table class=""table"">
                        <thead>
                            <tr>
                                <th scope =""col""> Dominio </th>
 
                                 <th scope=""col""> Tipo Estadia </th>
      
                                      <th scope=""col""> Importe </th>
       
                                       <th scope=""col""> Fecha </th>
        
                                    </tr>
        
                                </thead>
        
                                <tbody>" + 
                                results + @" 
                                </tbody>
                            </table>
                </div>
                <hr>
            </div>

            </body>

        </html>";
        }

        private string plantillaAbonados()
        {
            
            var results = "";
            foreach (var item in mvcModelStatic.lstAbonados)
            {
                results += @"
                                            <tr>
                                                <th scope=""row"">" + item.Nombre + @"</th>
 
                                                <td>" + item.Apellido + @"</td>
 
                                                <td>" + item.Dominio + @"</td>
   
                                            <td>" + item.TipoAbono + @"</td>

                                            <td>" + item.TipoVehiculo + @"</td>
   
                                       </tr> ";
            }
            return @"
                <!DOCTYPE html>
                <html>
                <head>  

                <style type=""text/css"">
    
            .contenedor {
            height: 390px;
            width: 500px;
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

            .table {
                width: 100%;
                max-width: 100%;
                margin-bottom: 20px;
                border-spacing: 0;
                border-collapse: collapse;
            }

            .table>thead>tr>th, .table>tbody>tr>th, .table>tfoot>tr>th, .table>thead>tr>td, .table>tbody>tr>td, .table>tfoot>tr>td {
                padding: 8px;
                line-height: 1.42857143;
                vertical-align: top;
                border-top: 1px solid #ddd;
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
        <h3>Reporte abonados<h3>
        <div class=""content-result"">
           <table class=""table"">
                        <thead>
                            <tr>
                                <th scope =""col""> Nombre </th>
 
                                 <th scope=""col""> Apellido </th>
      
                                      <th scope=""col""> Dominio </th>
       
                                       <th scope=""col""> Tipo abono </th>

                                        <th scope=""col""> Tipo vehiculo </th>
        
                                    </tr>
        
                                </thead>
        
                                <tbody>" +
                                results + @" 
                                </tbody>
                            </table>
                </div>
                <hr>
            </div>

            </body>

        </html>";
        }

        [HttpGet]
        public ActionResult getGastos(string tipo)
        {
            try
            {
                CargarListas();
                mvcModelStatic.lstGastos = accesoDatos.getGastos(tipo);
                ViewBag.loadGastosModal = true;
                ViewBag.tipoPeriodoGasto = tipo;
                return View("PanelReportes", mvcModelStatic);
            }
            catch (Exception)
            {

                throw;
            }
        }

        private string plantillaGastos()
        {

            var results = "";
            foreach (var item in mvcModelStatic.lstGastos)
            {
                results += @"
                                            <tr>
                                                <th scope=""row"">" + item.Periodo + @"</th>
 
                                                <td>$" + item.Monto + @"</td>
 
                                                <td>" + item.Fecha + @"</td>
   
                                            <td>" + item.Detalle + @"</td>
   
                                       </tr> ";
            }
            return @"
                <!DOCTYPE html>
                <html>
                <head>  

                <style type=""text/css"">
    
            .contenedor {
            height: 390px;
            width: 500px;
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

            .table {
                width: 100%;
                max-width: 100%;
                margin-bottom: 20px;
                border-spacing: 0;
                border-collapse: collapse;
            }

            .table>thead>tr>th, .table>tbody>tr>th, .table>tfoot>tr>th, .table>thead>tr>td, .table>tbody>tr>td, .table>tfoot>tr>td {
                padding: 8px;
                line-height: 1.42857143;
                vertical-align: top;
                border-top: 1px solid #ddd;
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
        <h3>Reporte abonados<h3>
        <div class=""content-result"">
           <table class=""table"">
                        <thead>
                            <tr>
                                <th scope =""col""> Período </th>
 
                                 <th scope=""col""> Monto </th>
      
                                      <th scope=""col""> Fecha </th>
       
                                       <th scope=""col""> Detalle </th>
        
                                    </tr>
        
                                </thead>
        
                                <tbody>" +
                                results + @" 
                                </tbody>
                            </table>
                </div>
                <hr>
            </div>

            </body>

        </html>";
        }

        [HttpGet]
        public ActionResult getFiltroCaja(int periodo, int year)
        {
            try
            {
                CargarListas();
                mvcModelStatic.lstReporteCaja = accesoDatos.getFiltroCaja(periodo, year);
                ViewBag.loadModalCaja = true;
                return View("PanelReportes", mvcModelStatic);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}