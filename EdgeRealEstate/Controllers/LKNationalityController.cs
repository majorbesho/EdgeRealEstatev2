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
    public class LKNationalityController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        // GET: LKNationality
        public ActionResult Index(int? page)
        {
                var LKNationalities = db.LKNationalities.Where(x => x.IsDeleted == false).
                OrderByDescending(x => x.Id).ToList().ToPagedList(page ?? 1, 1);
                return View(LKNationalities);
        }
        // GET: LKNationality/Create
        public ActionResult Create()
        {
                return View();
        }

        // POST: LKNationality/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(LKNationality LKNationalities)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    LKNationalities.IsDeleted = false;
                    db.LKNationalities.Add(LKNationalities);
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
            ModelState.Clear();
            return View();
        }

        // GET: LKNationality/Edit/5
        public ActionResult Edit(int? id)
        {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                LKNationality LKNationalities = db.LKNationalities.Find(id);
                if (LKNationalities == null)
                {
                    return HttpNotFound();
                }
                return View(LKNationalities);
        }

        // POST: LKNationality/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(LKNationality LKNationalities)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    LKNationalities.IsDeleted = false;
                    db.Entry(LKNationalities).State = EntityState.Modified;
                    db.SaveChanges();
                    ViewBag.msgg = 1;
                    return View(LKNationalities);
                    //return RedirectToAction("Index");
                }
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            return View(LKNationalities);
        }

        // GET: LKNationality/Delete/5
        public ActionResult Delete(int? id)
        {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                LKNationality LKNationalities = db.LKNationalities.Find(id);
                if (LKNationalities == null)
                {
                    return HttpNotFound();
                }
                return View(LKNationalities);
        }

        // POST: LKNationality/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            LKNationality LKNationalities = db.LKNationalities.Find(id);
            LKNationalities.IsDeleted = true;
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