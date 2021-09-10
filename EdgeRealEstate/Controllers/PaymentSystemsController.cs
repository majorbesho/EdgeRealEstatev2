using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PagedList;
using EdgeRealEstate.Models;
using EdgeRealEstate.Entities;

namespace EdgeRealEstate.Controllers
{
    public class PaymentSystemsController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        // GET: PaymentSystems
        public ActionResult Index(int? page)
        {
            var PaymentSystems = db.PaymentSystems.Where(x => x.IsDeleted == false).OrderByDescending(x => x.id).ToList()
                .ToPagedList(page ?? 1, 1);
            return View(PaymentSystems);
        }
        // GET: PaymentSystems/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PaymentSystems/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PaymentSystems PaymentSystems)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    PaymentSystems.IsDeleted = false;
                    db.PaymentSystems.Add(PaymentSystems);
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

        // GET: PaymentSystems/Edit/5
        public ActionResult Edit(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PaymentSystems PaymentSystems = db.PaymentSystems.Find(id);
            if (PaymentSystems == null)
            {
                return HttpNotFound();
            }
            return View(PaymentSystems);
        }

        // POST: PaymentSystems/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(PaymentSystems PaymentSystems)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    PaymentSystems.IsDeleted = false;
                    db.Entry(PaymentSystems).State = EntityState.Modified;
                    db.SaveChanges();
                    ViewBag.msgg = 1;
                    return View(PaymentSystems);
                    //return RedirectToAction("Index");
                }
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            return View(PaymentSystems);
        }

        // GET: PaymentSystems/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PaymentSystems PaymentSystems = db.PaymentSystems.Find(id);
            if (PaymentSystems == null)
            {
                return HttpNotFound();
            }
            return View(PaymentSystems);
        }

        // POST: PaymentSystems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PaymentSystems PaymentSystems = db.PaymentSystems.Find(id);
            PaymentSystems.IsDeleted = true;
            //db.LKTitles.Remove(LKTitles);
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