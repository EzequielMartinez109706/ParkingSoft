namespace SoftParking.Models
{
    public class TipoAbonos
    {
        public long Id { get; set; }
        public string Tipo { get; set; }
        public string TipoAbono { get; set; }
        public decimal Monto { get; set; }
        public decimal NuevoMonto { get; set; }
    }
}