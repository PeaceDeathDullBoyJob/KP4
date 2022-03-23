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
    public class VisitController : Controller

    {
        private kp49Entities db = new kp49Entities();
        // GET: Visit
        public ActionResult IndexForDoctor()
        {
            var identity = (ClaimsPrincipal)Thread.CurrentPrincipal;
            var email = HttpContext.User.Identity.Name;

            // проверка в таблице 
            Doctor doctor = db.Doctor.Where(l => l.login == email).First();
            var dd = db.Visit.Include(p => p.Doctor);
            var dd1 = db.Visit.Include(p => p.Patient);
            var dd2 = db.Visit.Include(p => p.Diagnosis);
            var dd3 = db.Visit.Include(p => p.Medicament);
            IEnumerable<Visit> visit = db.Visit.Where(w => w.id_doctor == doctor.id).ToList();

            return View(visit);
        }
        public ActionResult IndexForPatient()
        {
            var identity = (ClaimsPrincipal)Thread.CurrentPrincipal;
            var email = HttpContext.User.Identity.Name;

            // проверка в таблице 
            Patient pat = db.Patient.Where(l => l.login == email).First();
            var dd = db.Visit.Include(p => p.Doctor);
            var dd1 = db.Visit.Include(p => p.Patient);
            var dd2 = db.Visit.Include(p => p.Diagnosis);
            var dd3 = db.Visit.Include(p => p.Medicament);
            IEnumerable<Visit> visit = db.Visit.Where(w => w.id_patient == pat.id).ToList();

            return View(visit);
        }
        // GET: Visit/Details/5
        public ActionResult Details(int id = 0)
        {
            Visit v = db.Visit.Find(id);
            if (v == null)
            {
                return HttpNotFound();
            }
            return View(v);
        }

        // GET: Visit/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Visit/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Visit/Edit/5
        public ActionResult Edit(int id = 0)
        {
            Visit v = db.Visit.Find(id);
            if (v == null)
            {
                
                return HttpNotFound();
            }
            SelectList d = new SelectList(db.Diagnosis, "id", "name", v.id_diagnosis);
            ViewBag.Diagnosis = d;
            SelectList m = new SelectList(db.Medicament, "id", "name", v.id_medicament);
            ViewBag.Medicament = m;
            return View(v);
        }

        // POST: Visit/Edit/5
        [HttpPost]
        public ActionResult Edit(Visit v)
        {
            if (ModelState.IsValid)
            {
                db.Entry(v).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("IndexForDoctor");
            }
            return View(v);
        }
        // GET: Visit/Delete/5
        public ActionResult Delete(int id)
        {
            Visit v = db.Visit.Find(id);
            if (v == null)
            {
                return HttpNotFound();
            }
            return View(v);
        }

        // POST: Visit/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteOne(int id)
        {
            db.Visit.Remove(db.Visit.Find(id));
            db.SaveChanges();
            return RedirectToAction("IndexForDoctor");
        }
    }
}
