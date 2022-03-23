using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using kp4.Models;
using kp4.DAO;
using System.Data.Entity;

namespace kp4.Controllers
{
    public class StatusEntryController : Controller
    {
        StatusEntry statusEntry = new StatusEntry();
        private kp49Entities db = new kp49Entities();
        // GET: StatusEntry
        public ActionResult Index()
        {
            return View(db.StatusEntry.ToList());
        }

        // GET: StatusEntry/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: StatusEntry/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: StatusEntry/Create
        [HttpPost]
        public ActionResult Create(StatusEntry statusEntry)
        {
            if (ModelState.IsValid)
            {
                db.StatusEntry.Add(statusEntry);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(statusEntry);
        }

        // GET: StatusEntry/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: StatusEntry/Edit/5
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

        // GET: StatusEntry/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: StatusEntry/Delete/5
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
