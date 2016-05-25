using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SE2_IndividueleOpdracht.Models
{
    public class Spotifoe
    {
        public List<Afspeellijst> afspeellijsten = new List<Afspeellijst>();

        public Spotifoe()
        {
            afspeellijsten.Add(new Afspeellijst(1, "TestAP"));
            afspeellijsten.Add(new Afspeellijst(2, "TestAP2"));
        }
    }
}