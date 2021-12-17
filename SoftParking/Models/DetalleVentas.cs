using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SoftParking.Models
{
    public class DetalleVentas
    {
        private int id_detalle_venta;
        private float importe;

        public DetalleVentas(int id_detalle_venta, float importe)
        {
            this.Id_detalle_venta = id_detalle_venta;
            this.Importe = importe;
        }

        public int Id_detalle_venta { get => id_detalle_venta; set => id_detalle_venta = value; }
        public float Importe { get => importe; set => importe = value; }

        public DetalleVentas()
        {

        }
    }
}