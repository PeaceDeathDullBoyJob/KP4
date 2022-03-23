using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using kp4.Models;
using kp4.DAO;
using System.Data.Entity;
using Microsoft.Ajax.Utilities;
using System.Threading;
using System.Security.Claims;

namespace kp4.Controllers
{
    public class EntryController : Controller
    {
        private kp49Entities db = new kp49Entities();
 
        public ActionResult IndexForDoctor()
        {
            var identity = (ClaimsPrincipal)Thread.CurrentPrincipal;
            var email = HttpContext.User.Identity.Name;

            // проверка в таблице 
            Doctor doctor = db.Doctor.Where(l => l.login == email).First();
            var dd = db.Entry.Include(p => p.Doctor);
            var dd1 = db.Entry.Include(p => p.Patient);
            var dd2 = db.Entry.Include(p => p.Schedule);
            var dd3 = db.Entry.Include(p => p.StatusEntry);
            IEnumerable<Entry> entry = db.Entry.Where(w => w.id_doctor == doctor.id).ToList();
            
            return View(entry);
        }
        public ActionResult IndexForPatient()
        {
            var identity = (ClaimsPrincipal)Thread.CurrentPrincipal;
            var email = HttpContext.User.Identity.Name;

            // проверка в таблице 
            Patient patient = db.Patient.Where(l => l.login == email).First();
            var dd = db.Entry.Include(p => p.Doctor);
            var dd1 = db.Entry.Include(p => p.Patient);
            var dd2 = db.Entry.Include(p => p.Schedule);
            var dd3 = db.Entry.Include(p => p.StatusEntry);
            IEnumerable<Entry> entry = db.Entry.Where(w => w.id_patient == patient.id).ToList();
            return View(entry);
        }

        public ActionResult Details(int id = 0)
        {
            Entry entry = db.Entry.Find(id);
            if (entry == null)
            {
                return HttpNotFound();
            }
            return View(entry);
        }

        public ActionResult AddVisit()
        {
            SelectList diag = new SelectList(db.Diagnosis, "id", "name");
            ViewBag.Diagnosis = diag;
            SelectList med = new SelectList(db.Medicament, "id", "name");
            ViewBag.Medicament = med;
            return View();

        }
        [HttpPost]
        public ActionResult AddVisit(int identry, Visit visit)
        {
            if (ModelState.IsValid)
            {

                var identity = (ClaimsPrincipal)Thread.CurrentPrincipal;
                var email = HttpContext.User.Identity.Name;

                // проверка в таблице 
                Doctor doctor = db.Doctor.Where(l => l.login == email).First();

                Entry entry = db.Entry.Where(l => l.id == identry).First();
                // 
                Schedule schedule = db.Schedule.Where(l => l.id == entry.id_schedule).First();
                
                StatusEntry st = db.StatusEntry.Where(l => l.id == entry.id_status).First();
                
                visit.id_patient = entry.id_patient;
                if (entry.id_doctor == doctor.id)
                {
                    visit.id_doctor = doctor.id;
                }
                visit.date = schedule.date;
                Patient patient = db.Patient.Where(l => l.id == entry.id_patient).First();
                patient.id_doctor = doctor.id;
                
                    db.Visit.Add(visit);
                db.SaveChanges();

                return RedirectToAction("IndexForDoctor");
            }

            return View(visit);
        }

        


        // GET: Entry/Edit/5
        public ActionResult Edit(int id = 0)
        {
            Entry v = db.Entry.Find(id);
            if (v == null)
            {

                return HttpNotFound();
            }
            SelectList entry1 = new SelectList(db.StatusEntry, "id", "name", v.id_status);
            ViewBag.StatusEntry = entry1;

            return View(v);
        }

        // POST: Entry/Edit/5
        [HttpPost]
        public ActionResult Edit(Entry entry)
        {
            
            if (ModelState.IsValid)
            {         
                StatusEntry s = db.StatusEntry.Where(l => l.id == entry.id_status).First();
                //Schedule sc = db.Schedule.Where(l => l.id == entry.id_schedule).First();
                //if (s.name == "Отклонена") { sc.status = true; }
                db.Entry(entry).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("IndexForDoctor");
            }
            return View(entry);
        }

        // GET: Entry/Delete/5
        public ActionResult Delete(int id)
        {
            db.Entry.Remove(db.Entry.Find(id));
            db.SaveChanges();
            return RedirectToAction("IndexForPatient");
        }

        // POST: Entry/Delete/5
        //[HttpPost]
        //public ActionResult DeleteOne(int id)
        //{
        //    db.Entry.Remove(db.Entry.Find(id));
        //    db.SaveChanges();
        //    return RedirectToAction("IndexForPatient");
        //}
    }
}
