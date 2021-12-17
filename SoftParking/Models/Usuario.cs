namespace SoftParking.Models
{
    public class Usuario
    {
        public long Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public bool EsAdmin { get; set; }
        public bool Logueado { get; set; }
    }
}