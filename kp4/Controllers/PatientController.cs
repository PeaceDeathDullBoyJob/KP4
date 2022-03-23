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

using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System.Web.Security;

namespace kp4.Controllers
{
    public class PatientController : Controller
    {
        private kp49Entities db = new kp49Entities();
        // GET: Patient
        public ActionResult Index()
        {
            var identity = (ClaimsPrincipal)Thread.CurrentPrincipal;
            var email = HttpContext.User.Identity.Name;
            //    var role = Roles.GetRolesForUser(User.Identity.Name).First();

            // проверка в таблице 
            //Doctor doctor = db.Doctor.Where(l => l.login == email).First();

            //IEnumerable<Patient> pat = db.Patient.Where(w => w.id_doctor == doctor.id).ToList(); 
            IEnumerable<Patient> pat = db.Patient.ToList();
            return View(pat);

        }


        public ActionResult Account()
        {
            var identity = (ClaimsPrincipal)Thread.CurrentPrincipal;
            //var email = HttpContext.User.Identity.Name;
            var email = identity.Identity.Name;
            // проверка в таблице 
            Patient pat;
            try
            {
                pat = db.Patient.Where(l => l.login == email).First();

            }
            catch
            {
                return RedirectToAction("Create");
            }



            //var dd = db.Patient.Include(p => p.Doctor);
            
            return View(pat);
        }
        // GET: Patient/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Patient/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Patient/Create
        [HttpPost]
        public ActionResult Create(Patient patient)
        {
            try
            {
                // TODO: Add insert logic here
                var identity = (ClaimsPrincipal)Thread.CurrentPrincipal;
                var email = identity.Identity.Name;
                patient.login = email;
                db.Patient.Add(patient);
                db.SaveChanges();
                return RedirectToAction("Account");
            }
            catch
            {
                return View();
            }
        }

        // GET: Patient/Edit/5
        public ActionResult Edit(int id)
        {
            var identity = (ClaimsPrincipal)Thread.CurrentPrincipal;
            var email = HttpContext.User.Identity.Name;
            Patient pat = db.Patient.Where(l => l.login == email).First();
            return View(pat);
        }

        // POST: Patient/Edit/5
        [HttpPost]
        public ActionResult Edit(Patient patient)
        {

            if (ModelState.IsValid)
            {
                db.Entry(patient).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Account");
            }
            return View(patient);
        }

        // GET: Patient/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Patient/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            db.Entry.Remove(db.Entry.Find(id));
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
