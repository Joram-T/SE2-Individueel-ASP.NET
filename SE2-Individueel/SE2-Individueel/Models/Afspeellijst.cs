using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SE2_Individueel.Models
{
    public class Afspeellijst
    {
        public int ID { get; }
        public string Naam { get; }
        public TimeSpan Lengte { get; }
        public List<Lied> Liedjes { get; }

        public Afspeellijst(string naam, int ID)
        {
            Liedjes = new List<Lied>();
            this.Naam = naam;
            this.ID = ID;
        }

    }
}