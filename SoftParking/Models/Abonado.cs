using System;
using System.ComponentModel.DataAnnotations;

namespace SoftParking.Models
{
    public class Abonado
    {
        public int IdAbonado { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Dominio { get; set; }
        public string TipoVehiculo { get; set; }
        public long Telefono { get; set; }
        public string Dni { get; set; }
        public string TipoAbono { get; set; }
        public long IdTipoAbono { get; set; }
        public long IdTipoVehiculo { get; set; }
        public int IdPeriodo { get; set; }

        [DataType(DataType.Currency)]
        public float Importe { get; set; }
        public DateTime Fecha { get; set; }
        public int IdDetallePago { get; set; }
        public string FechaStr { get; set; }
        public string Periodo { get; set; }


    }
}