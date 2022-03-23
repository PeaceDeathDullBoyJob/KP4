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
    public class ScheduleController : Controller
    {
        private kp49Entities db = new kp49Entities();

        // GET: Schedule
        public ActionResult Index()
        {
            var identity = (ClaimsPrincipal)Thread.CurrentPrincipal;
            var email = HttpContext.User.Identity.Name;

            // проверка в таблице 
            Doctor doctor = db.Doctor.Where(l => l.login == email).First();
            IEnumerable<Schedule> sc = db.Schedule.Where(w => w.id_doctor == doctor.id).ToList();
            return View(sc);

        }

        public ActionResult AddSchedule()
        {
            return View();

        }
        [HttpPost]
        public ActionResult AddSchedule(Schedule schedule)
        {
            if (ModelState.IsValid)
            {

                var identity = (ClaimsPrincipal)Thread.CurrentPrincipal;
                var email = HttpContext.User.Identity.Name;

                // проверка в таблице 
                Doctor doctor = db.Doctor.Where(l => l.login == email).First();
                schedule.id_doctor = doctor.id;
                db.Schedule.Add(schedule);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
             return View(schedule);
        }

        // GET: Schedule/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Schedule/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Schedule/Create
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

        // GET: Schedule/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Schedule/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Schedule/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Schedule/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
