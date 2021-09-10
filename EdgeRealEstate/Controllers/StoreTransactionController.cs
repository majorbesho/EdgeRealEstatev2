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
    public class StoreTransactionController : Controller
    {
        // GET: StoreTransaction
        ApplicationDbContext db = new ApplicationDbContext();

        // GET: StoreTransaction
        public ActionResult Index(int? page)
        {
                var StoreTransactions = db.StoreTransactions.Where(x => x.isDeleted == false)
                .OrderByDescending(x => x.id).ToList().ToPagedList(page ?? 1, 3);
                return View(StoreTransactions);  
        }

        // GET: StoreTransaction/Create
        public ActionResult Create()
        {
                ViewBag.ConstructionMaterials = new SelectList(db.ConstructionMaterials, "ID", "MaterialName");
                ViewBag.LKStores = new SelectList(db.LKStores, "Id", "ARName");
                return View();
        }

        // POST: StoreTransaction/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(StoreTransaction StoreTransactions)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    ViewBag.ConstructionMaterials = new SelectList(db.ConstructionMaterials, "ID", "MaterialName", StoreTransactions.ConstructionMaterialId);
                    ViewBag.LKStores = new SelectList(db.LKStores, "Id", "ARName", StoreTransactions.storied);
                    StoreTransactions.isDeleted = false;
                    db.StoreTransactions.Add(StoreTransactions);
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
            ViewBag.ConstructionMaterials = new SelectList(db.ConstructionMaterials, "ID", "MaterialName", StoreTransactions.ConstructionMaterialId);
            ViewBag.LKStores = new SelectList(db.LKStores, "Id", "ARName", StoreTransactions.storied);
            ModelState.Clear();
            return View();
        }

        // GET: StoreTransaction/Edit/5
        public ActionResult Edit(int? id)
        {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                StoreTransaction StoreTransactions = db.StoreTransactions.Find(id);
                ViewBag.ConstructionMaterials = new SelectList(db.ConstructionMaterials, "ID", "MaterialName");
                ViewBag.LKStores = new SelectList(db.LKStores, "Id", "ARName");
                if (StoreTransactions == null)
                {
                    return HttpNotFound();
                }
                return View(StoreTransactions);
        }

        // POST: StoreTransaction/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(StoreTransaction StoreTransactions)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    ViewBag.ConstructionMaterials = new SelectList(db.ConstructionMaterials, "ID", "MaterialName", StoreTransactions.ConstructionMaterialId);
                    ViewBag.LKStores = new SelectList(db.LKStores, "Id", "ARName", StoreTransactions.storied);
                    StoreTransactions.isDeleted = false;
                    db.Entry(StoreTransactions).State = EntityState.Modified;
                    db.SaveChanges();
                    ViewBag.msgg = 1;
                    return View(StoreTransactions);
                }
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            ViewBag.ConstructionMaterials = new SelectList(db.ConstructionMaterials, "ID", "MaterialName", StoreTransactions.ConstructionMaterialId);
            ViewBag.LKStores = new SelectList(db.LKStores, "Id", "ARName", StoreTransactions.storied);
            return View(StoreTransactions);
        }

        // GET: StoreTransaction/Delete/5
        public ActionResult Delete(int? id)
        {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                StoreTransaction StoreTransactions = db.StoreTransactions.Find(id);
                if (StoreTransactions == null)
                {
                    return HttpNotFound();
                }
                return View(StoreTransactions);
        }

        // POST: StoreTransaction/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            StoreTransaction StoreTransactions = db.StoreTransactions.Find(id);
            StoreTransactions.isDeleted = true;
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