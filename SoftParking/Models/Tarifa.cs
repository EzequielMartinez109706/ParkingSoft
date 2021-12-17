using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SoftParking.Models
{
    public class Tarifa
    {
        public int IdTarifa { get; set; }
        public string TipoVehiculo { get; set; }
        public float Precio { get; set; }
        public float NuevoPrecio { get; set; }
        public int IdTipoVehiculo { get; set; }
    }
}