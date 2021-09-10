using EdgeRealEstate.Entities;
using EdgeRealEstate.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace EdgeRealEstate.Controllers
{
    public class LKStoreController : Controller
    {
        // GET: LKStore
        ApplicationDbContext db = new ApplicationDbContext();
        // GET: LKStore
        public ActionResult Index(int? page)
        {
                var LKStores = db.LKStores.Where(x => x.IsDeleted == false).OrderByDescending(x => x.Id).ToList().ToPagedList(page ?? 1, 1);
                return View(LKStores);
        }

        // GET: LKStore/Create
        public ActionResult Create()
        {
                return View();
        }

        // POST: LKStore/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(LKStore LKStores)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    LKStores.IsDeleted = false;
                    db.LKStores.Add(LKStores);
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

        // GET: LKStore/Edit/5
        public ActionResult Edit(int? id)
        {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                LKStore LKStores = db.LKStores.Find(id);
                if (LKStores == null)
                {
                    return HttpNotFound();
                }
                return View(LKStores);
        }

        // POST: LKStore/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(LKStore LKStores)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    LKStores.IsDeleted = false;
                    db.Entry(LKStores).State = EntityState.Modified;
                    db.SaveChanges();
                    ViewBag.msgg = 1;
                    return View(LKStores);
                    //return RedirectToAction("Index");
                }
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            return View(LKStores);
        }

        // GET: LKStore/Delete/5
        public ActionResult Delete(int? id)
        {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                LKStore LKStores = db.LKStores.Find(id);
                if (LKStores == null)
                {
                    return HttpNotFound();
                }
                return View(LKStores);
        }

        // POST: LKStore/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            LKStore LKStores = db.LKStores.Find(id);
            LKStores.IsDeleted = true;
            //db.LKStores.Remove(LKStores);
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