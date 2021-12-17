using System;

namespace SoftParking.Models
{
    public class Venta
    {
        public string HoraEgreso { get; set; }
        public long IdDetalleVenta { get; set; }
        public long IdCliente { get; set; }
        public decimal Monto { get; set; }
        public string Dominio { get; set; }
        public string TipoVehiculo { get; set; }
        public DateTime HoraEgresoDate { get; set; }

    }
}