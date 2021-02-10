using System;

namespace WCF_REST_Flugzeuge
{
    public class Flugzeug
    {
        public int Id { get; set; }

        public string Model { get; set; }
        public string Hersteller { get; set; }
        public int AnzSitzplätze { get; set; }
        public int MaxBeladung { get; set; }
        public DateTime Baujahr { get; set; }
        public string Betreiber { get; set; }
    }



}
