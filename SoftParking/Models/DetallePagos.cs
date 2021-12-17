using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SoftParking.Models
{
    public class DetallePagos
    {
        private int detalle_pago;
        private float total;

        public DetallePagos(int detalle_pago, float total)
        {
            this.Detalle_pago = detalle_pago;
            this.Total = total;
        }

        public int Detalle_pago { get => detalle_pago; set => detalle_pago = value; }
        public float Total { get => total; set => total = value; }

        public DetallePagos()
        {

        }
    }
}