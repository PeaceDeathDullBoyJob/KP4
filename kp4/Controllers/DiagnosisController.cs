using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using kp4.Models;
using kp4.DAO;

using kp4.Models;
using System.Data;
using System.Linq;
using System.Web.Mvc;
using System.Data.Entity;

namespace kp4.Controllers
{
    public class DiagnosisController : Controller
    {
        DiagnosisDAO diagnosisdao = new DiagnosisDAO();
        Diagnosis diagnosis = new Diagnosis();
        private kp49Entities db = new kp49Entities();

        // GET: Diagnosis
        public ActionResult Index()
        {
            return View(db.Diagnosis.ToList());
        }

        // GET: Diagnosis/Details/5
        public ActionResult Details(int id=0)
        {
            diagnosis = db.Diagnosis.Find(id);
            if (diagnosis == null)
            {
                return HttpNotFound();
            }
            return View(diagnosis);
        }

        // GET: Diagnosis/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Diagnosis/Create
        [HttpPost]
        public ActionResult Create(Diagnosis diagnosis)
        {
            if (ModelState.IsValid)
            {
                db.Diagnosis.Add(diagnosis);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(diagnosis);
        }
        // GET: Diagnosis/Edit/5
        public ActionResult Edit(int id)
        {
            Diagnosis diag = db.Diagnosis.Find(id);
            return View(diag);
        }

        // POST: Diagnosis/Edit/5
        [HttpPost]
        public ActionResult Edit(Diagnosis diagnosis)
        {
            if (ModelState.IsValid)
            {
                db.Entry(diagnosis).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(diagnosis);
        }

        //GET: Diagnosis/Delete/5
        public ActionResult Delete(int id)
        {
            Diagnosis d = db.Diagnosis.Find(id);
            if (d == null)
            {
                return HttpNotFound();
            }
            return View(d);
        }

        //POST: Diagnosis/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteOne(int id)
        {
            db.Diagnosis.Remove(db.Diagnosis.Find(id));
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
