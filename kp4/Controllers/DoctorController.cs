using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using kp4.Models;
using kp4.DAO;
using System.Data.Entity;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Security.Claims;

namespace kp4.Controllers
{
    public static class ExtensionMethod
    {

        public static List<SelectListItem> GetSizeOrganization(this List<SelectListItem> list, int iddoctor)
        {
            kp49Entities db = new kp49Entities();
            var elements = db.Schedule.Where(w => w.date > DateTime.Now && w.status == true && w.id_doctor == iddoctor);
            foreach (var item in elements)
            {
                list.Add(new SelectListItem { Text = item.date.ToString(), Value = item.id.ToString() });
            }
            return list;
        }
    }

    public class DoctorController : Controller
    {


        Doctor doctor = new Doctor();
        private kp49Entities db = new kp49Entities();
        // GET: Doctor
        public ActionResult Index()
        {
            return View(db.Doctor.ToList());
        }

        public ActionResult Search(string GName)
        {
            var doc = db.Doctor.Where(l => l.last_name.Contains(GName));
            return PartialView(doc);
        }



        public ActionResult Account(int id)
        {
            var identity = (ClaimsPrincipal)Thread.CurrentPrincipal;
            var email = HttpContext.User.Identity.Name;

            // проверка в таблице 
            Doctor doc = db.Doctor.Where(l => l.id == id).First();
            return View(doc);
        }
        // GET: Doctor/Details/5
        // GET: Doctor/Details/
        public ActionResult Details(int id)
        {
            Doctor doctor = db.Doctor.Find(id);
            if (doctor == null)
            {
                return HttpNotFound();
            }
            return View(doctor);
        }
        public ActionResult SendError()
        {
            return View();
        }
        public ActionResult AddEntry(int iddoctor)
        {
            List<SelectListItem> list = new List<SelectListItem>();
            var dd = db.Entry.Include(p => p.Schedule);
            var elements = db.Schedule.Where(w => w.date > DateTime.Now && w.status == true && w.id_doctor == iddoctor);
            foreach (var item in elements)
            {
                list.Add(new SelectListItem { Text = item.date.ToString(), Value = item.id.ToString() });
            }
            IEnumerable<Schedule> sc = db.Schedule.Where(w => w.date > DateTime.Now && w.status == true && w.id_doctor == iddoctor).ToList();

            SelectList schedule = new SelectList(db.Schedule.Where(w => w.date > DateTime.Now && w.status == true && w.id_doctor == iddoctor).ToList(), "id", "date");
            ViewBag.Schedule = schedule;
            return View();
        }


        // POST: Doctor/Create
        [HttpPost]
        public ActionResult AddEntry(int iddoctor, Entry entry)
        {
            Console.WriteLine(entry);
            if (ModelState.IsValid)
            {
                var identity = (ClaimsPrincipal)Thread.CurrentPrincipal;
                var email = HttpContext.User.Identity.Name;

                // проверка в таблице 
                Patient patient = db.Patient.Where(l => l.login == email).First();
                StatusEntry st = db.StatusEntry.Where(l => l.name == "На рассмотрении").First();
                int z = patient.id;
                entry.id_patient = patient.id;
                entry.id_doctor = iddoctor;
                entry.id_status = st.id;
                //  Schedule sch = db.Schedule.Where(l => l.id == entry.id_schedule).First();
                //   sch.status = false;
                if (patient.name == null || patient.last_name == null || patient.patronymic == null || patient.adress == null)
                //patient.date == null)
                {
                    return RedirectToAction("SendError");
                }
                else
                {
                    db.Entry.Add(entry);
                    db.SaveChanges();
                }
            }
            return View("~/Views/Home/Index.cshtml");
        }


        // GET: Doctor/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Doctor/Create
        [HttpPost]
        public ActionResult Create(Doctor doctor)
        {
            if (ModelState.IsValid)
            {
                db.Doctor.Add(doctor);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(doctor);
        }

        // GET: Doctor/Edit/5
        public ActionResult Edit(int id)
        {
            var new_id = id;
            var identity = (ClaimsPrincipal)Thread.CurrentPrincipal;
            Doctor doc = db.Doctor.Where(l => l.id == id).First();
            return View(doc);

        }

        // POST: Doctor/Edit/5
        [HttpPost]
        public ActionResult Edit(Doctor doctor)
        {
            Console.Write(doctor);

            if (ModelState.IsValid)
            {

                db.Entry(doctor).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Account", new { id = doctor.id });
            }
            return View(doctor);
        }

        public ActionResult Info()
        {
            var identity = (ClaimsPrincipal)Thread.CurrentPrincipal;
            var email = identity.Identity.Name;
            Doctor doc = db.Doctor.Where(l => l.login == email).First();
            return View(doc);
        }


        [HttpPost]
        public ActionResult Info(Doctor doctor)
        {
            Console.Write(doctor);
            if (db.Doctor.Where(l => l.login == doctor.login).Count() == 0) {
                if (ModelState.IsValid)
                {
                    db.Doctor.Add(doctor);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            } else
            {
                if (ModelState.IsValid)
                {


                    db.Entry(doctor).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Account", new { id = doctor.id });
                }
            }

            return View(doctor);

        }



        // GET: Doctor/Delete/5
        public ActionResult Delete(int id)
        {
            var identity = (ClaimsPrincipal)Thread.CurrentPrincipal;
            var email = HttpContext.User.Identity.Name;

            // проверка в таблице 
            Doctor doc = db.Doctor.Where(l => l.id == id).First();
            return View(doc);


        }

        // POST: Doctor/Delete/5
        [HttpPost]

        public ActionResult Delete(Doctor doctor)
        {
            db.Doctor.Remove(db.Doctor.Find(doctor.id));
            db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}
