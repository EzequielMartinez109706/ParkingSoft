using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SoftParking.Models
{
    public class VentaEstadias
    {
        private int id_venta_estadia;
        private int id_tipo_estadia;
        private string dominio;
        public DateTime FechaIngreso { get; set; }
        public DateTime FechaEgreso { get; set; }

        public VentaEstadias(int id_venta_estadia, int id_tipo_estadia, string dominio)
        {
            this.Id_venta_estadia = id_venta_estadia;
            this.Id_tipo_estadia = id_tipo_estadia;
            this.Dominio = dominio;
        }

        public int Id_venta_estadia { get => id_venta_estadia; set => id_venta_estadia = value; }
        public int Id_tipo_estadia { get => id_tipo_estadia; set => id_tipo_estadia = value; }
        public string Dominio { get => dominio; set => dominio = value; }

        public VentaEstadias()
        {

        }
    }
}