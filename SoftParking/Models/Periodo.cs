using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SoftParking.Models
{
    public class Periodo
    {
        private int id_periodo;
        private string mes_periodo;

        public Periodo()
        {

        }

        public Periodo(int id_periodo, string mes_periodo)
        {
            this.Id_periodo = id_periodo;
            this.Mes_periodo = mes_periodo;
        }

        public int Id_periodo { get => id_periodo; set => id_periodo = value; }
        public string Mes_periodo { get => mes_periodo; set => mes_periodo = value; }
    }
}