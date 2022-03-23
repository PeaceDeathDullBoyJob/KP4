using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using kp4.Models;

namespace kp4.DAO
{
    public class MedicamentDAO
    {
        private kp49Entities db = new kp49Entities();
        public void AddNew(Medicament medicament)
        {
            db.Medicament.Add(medicament);
            db.SaveChanges();
        }
        public void Edit(Medicament medicament)
        {
            db.Entry(medicament).State = EntityState.Modified;
            db.SaveChanges();
        }
        ////public void Delete(int id)
        ////{
        ////    db.Diagnosis.Remove(db.Diagnosis.Find(id));
        ////}
    }
}