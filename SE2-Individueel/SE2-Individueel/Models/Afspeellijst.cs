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
        public TimeSpan Lengte { get; set; }

        public Afspeellijst(int id, string naam)
        {
            ID = id;
            Naam = naam;
        }

    }
}