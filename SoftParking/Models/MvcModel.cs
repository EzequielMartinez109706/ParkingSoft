using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SoftParking.Models
{
    public class MvcModel
    {
        public Abonado abonado { get; set; }
        public Cliente cliente { get; set; }
        public DetallePagos detallePagos { get; set; }
        public DetalleVentas detalleVentas { get; set; }
        public Gasto gasto { get; set; }
        public PagoAbonos pagoAbonos { get; set; }
        public Periodo periodo { get; set; }
        public TipoAbonos tipoAbonos { get; set; }
        public TipoEstadias tipoEstadias { get; set; }
        public TipoVehiculos tipoVehiculos { get; set; }
        public Tarifa tarifa { get; set; }
        public Usuario usuario { get; set; }
        public Venta venta { get; set; }
        public VentaEstadias ventaEstadias { get; set; }
        public List<TipoVehiculos> lstTipoVehiculos { get; set; }
        public List<TipoAbonos> lstTiposAbonos { get; set; }
        public List<TipoEstadias> lstTiposEstadias { get; set; }
        public bool mostrarAlertSuccess { get; set; }
        public List<ReporteIngresosVehiculos> lstReporteIngresosVehiculos { get; set; }
        public List<ReporteEstadias> lstEstadias { get; set; }
        public List<PeriodosEstadias> lstPeriodoEstadias { get; set; }
        public List<Abonado> lstAbonados { get; set; }
        public List<Abonado> lstAbonadosDelDia { get; set; }
        public List<Gasto> lstGastos { get; set; }
        public List<Tarifa> lstTarifas { get; set; }
        public PeriodosEstadias periodosEstadias { get; set; }
        public bool mostrarModalEstadias { get; set; }
        public List<Periodo> lstPeriodos { get; set; }
        public List<Usuario> lstUsuarios { get; set; }
        public decimal totalCaja { get; set; }
        public decimal? montoCierreCaja { get; set; }
        public decimal? montoCierreCajaSubtotalEstadias { get; set; }
        public decimal? montoCierreCajaSubtotalAbonos { get; set; }
        public decimal? montoCierreCajaTotal { get; set; }
        public List<Caja> lstReporteCaja { get; set; }

        public MvcModel() { }
        

    }
}