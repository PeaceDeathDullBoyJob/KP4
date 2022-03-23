using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using kp4.Models;


namespace kp4.DAO
{
    public class DiagnosisDAO
    {
        private kp49Entities db = new kp49Entities();
        public void AddNew(Diagnosis diagnosis)
        {
            db.Diagnosis.Add(diagnosis);
            db.SaveChanges();
        }
        public void Edit(Diagnosis diagnosis)
        {
            db.Entry(diagnosis).State = EntityState.Modified;
            db.SaveChanges();
        }
        ////public void Delete(int id)
        ////{
        ////    db.Diagnosis.Remove(db.Diagnosis.Find(id));
        ////}
    }
}