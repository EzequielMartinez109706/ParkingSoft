using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SoftParking.Models
{
    public class PagoAbonos
    {
        private int id_pago;
        private int id_abonado;
        private DateTime fecha;
        private int id_detalle_pago;
        private int id_periodo;

        public PagoAbonos(int id_pago, int id_abonado, DateTime fecha, int id_detalle_pago, int id_periodo)
        {
            this.Id_pago = id_pago;
            this.Id_abonado = id_abonado;
            this.Fecha = fecha;
            this.Id_detalle_pago = id_detalle_pago;
            this.Id_periodo = id_periodo;
        }

        public int Id_pago { get => id_pago; set => id_pago = value; }
        public int Id_abonado { get => id_abonado; set => id_abonado = value; }
        public DateTime Fecha { get => fecha; set => fecha = value; }
        public int Id_detalle_pago { get => id_detalle_pago; set => id_detalle_pago = value; }
        public int Id_periodo { get => id_periodo; set => id_periodo = value; }

        public PagoAbonos()
        {

        }
    }
}