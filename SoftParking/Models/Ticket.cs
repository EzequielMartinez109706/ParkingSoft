using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SoftParking.Models
{
    public class Ticket
    {
        public long Id { get; set; }
        public string Dominio { get; set; }
        public DateTime Fecha { get; set; }
        public long IdCliente { get; set; }
        public string TipoVehiculo { get; set; }
        public long IdTipoVehiculo { get; set; }
        public string HoraIngreso { get; set; }
        public string HoraEgreso { get; set; }
        public string Tiempo { get; set; }
        public decimal Importe { get; set; }
        public long IdDetalleVenta { get; set; }
        public string Qr { get; set; }
        public string TipoEstadia { get; set; }
        public string Nombre { get; set; }
        public string Periodo { get; set; }
    }
}