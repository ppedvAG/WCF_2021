using System;
using System.Collections.Generic;
using System.Linq;

namespace WCF_REST_Flugzeuge
{
    public class FlugService : IFlugService
    {
        static List<Flugzeug> db = new List<Flugzeug>();

        static FlugService()
        {
            db.Add(new Flugzeug()
            {
                Id = 1,
                AnzSitzplätze = 274,
                Baujahr = DateTime.Now.AddDays(-7878),
                Hersteller = "Boing",
                Betreiber = "LH",
                Model = "777777777",
                MaxBeladung = 349834
            });

            db.Add(new Flugzeug()
            {
                Id = 2,
                AnzSitzplätze = 872,
                Baujahr = DateTime.Now.AddDays(-778),
                Hersteller = "BusAir",
                Betreiber = "OS",
                Model = "3434343434343",
                MaxBeladung = 2356
            });

            db.Add(new Flugzeug()
            {
                Id = 3,
                AnzSitzplätze = 6,
                Baujahr = DateTime.Now.AddDays(-8378),
                Hersteller = "Antonow",
                Betreiber = "OS",
                Model = "AN-124-100",
                MaxBeladung = 150_000
            });
        }

        public void AddFlugzeug(Flugzeug fz)
        {
            db.Add(fz);
        }


        public void Delete(int id)
        {
            var fz = GetById(id);
            if (fz != null)
                db.Remove(fz);
        }

        public IEnumerable<Flugzeug> GetAll()
        {
            return db;
        }

        public Flugzeug GetById(int id)
        {
            return db.FirstOrDefault(x => x.Id == id);
        }

        public void Update(Flugzeug fz)
        {
            var fzL = GetById(fz.Id);
            if (fzL != null)
            {
                Delete(fzL.Id);
                AddFlugzeug(fz);
            }
        }
    }
}
