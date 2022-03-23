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
    public class MedicamentController : Controller
    {
        MedicamentDAO medicamentdao = new MedicamentDAO();
        Medicament medicament = new Medicament();
        private kp49Entities db = new kp49Entities();
        
        // GET: Medicament
        
        public ActionResult Index()
        {
            return View(db.Medicament.ToList());
        }

        // GET: Medicament/Details/5
        public ActionResult Details(int id = 0)
        {
            Medicament medicament = db.Medicament.Find(id);
            if (medicament == null)
            {
                return HttpNotFound();
            }
            return View(medicament);
        }

        // GET: Medicament/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Medicament/Create
        [HttpPost]
        public ActionResult Create(Medicament medicament)
        {
            if (ModelState.IsValid)
            {
                db.Medicament.Add(medicament);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(medicament);
        }

        // GET: Medicament/Edit/5
        public ActionResult Edit(int id)
        {
            Medicament m = db.Medicament.Find(id);
            return View(m);
        }

        // POST: Medicament/Edit/5
        [HttpPost]
        public ActionResult Edit(Medicament medicament)
        {
            if (ModelState.IsValid)
            {
                db.Entry(medicament).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(medicament);
        }

        // GET: Medicament/Delete/5
        public ActionResult Delete(int id = 0)
        {
            Medicament m = db.Medicament.Find(id);
            if (m == null)
            {
                return HttpNotFound();
            }
            return View(m);
        }

        // POST: Medicament/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteOne(int id)
        {
            db.Medicament.Remove(db.Medicament.Find(id));
                db.SaveChanges();
                return RedirectToAction("Index");
            }
    }
}
