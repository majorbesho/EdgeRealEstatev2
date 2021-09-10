using PagedList;
using EdgeRealEstate.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EdgeRealEstate.Entities;

namespace EdgeRealEstate.Controllers
{
    public class EmloyeeTargetController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();

        // GET: EmloyeeTarget
        public ActionResult Index(int? page)
        {
                var EmloyeeTargets = db.EmloyeeTargets.Where(x => x.isDeleted == false)
                .OrderByDescending(x => x.id).ToList().ToPagedList(page ?? 1, 1);
                return View(EmloyeeTargets);
        }

        // GET: EmloyeeTarget/Create
        public ActionResult Create()
        {
                ViewBag.Employees = new SelectList(db.Employees.Where(x => x.IsDeleted == false), "Id", "ARName");
                return View();
        }

        // POST: EmloyeeTarget/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(EmloyeeTarget EmloyeeTargets)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    ViewBag.Employees = new SelectList(db.Employees.Where(x => x.IsDeleted == false), "Id", "ARName", EmloyeeTargets.salesManId);
                    EmloyeeTargets.isDeleted = false;
                    db.EmloyeeTargets.Add(EmloyeeTargets);
                    db.SaveChanges();
                    ViewBag.msgg = 1;
                    ModelState.Clear();
                    return View();
                    //return RedirectToAction("Index");
                }
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            ViewBag.Employees = new SelectList(db.Employees.Where(x => x.IsDeleted == false), "Id", "ARName", EmloyeeTargets.salesManId);
            ModelState.Clear();
            return View(EmloyeeTargets);
        }

        // GET: EmloyeeTarget/Edit/5
        public ActionResult Edit(int? id)
        {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                EmloyeeTarget EmloyeeTargets = db.EmloyeeTargets.Find(id);
                ViewBag.Employees = new SelectList(db.Employees.Where(x => x.IsDeleted == false), "Id", "ARName");
                if (EmloyeeTargets == null)
                {
                    return HttpNotFound();
                }
                return View(EmloyeeTargets);
        }

        // POST: EmloyeeTarget/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EmloyeeTarget EmloyeeTargets)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    ViewBag.Employees = new SelectList(db.Employees.Where(x => x.IsDeleted == false), "Id", "ARName", EmloyeeTargets.salesManId);
                    EmloyeeTargets.isDeleted = false;
                    db.Entry(EmloyeeTargets).State = EntityState.Modified;
                    db.SaveChanges();
                    ViewBag.msgg = 1;
                    return View(EmloyeeTargets);
                    //return RedirectToAction("Index");
                }
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            ViewBag.Employees = new SelectList(db.Employees.Where(x => x.IsDeleted == false), "Id", "ARName", EmloyeeTargets.salesManId);
            return View(EmloyeeTargets);
        }

        // GET: EmloyeeTarget/Delete/5
        public ActionResult Delete(int? id)
        {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                EmloyeeTarget EmloyeeTargets = db.EmloyeeTargets.Find(id);
                if (EmloyeeTargets == null)
                {
                    return HttpNotFound();
                }
                return View(EmloyeeTargets);
        }

        // POST: EmloyeeTarget/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EmloyeeTarget EmloyeeTargets = db.EmloyeeTargets.Find(id);
            EmloyeeTargets.isDeleted = true;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }       
    }
}