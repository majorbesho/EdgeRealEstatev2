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
    public class contractorController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        // GET: contractor
        public ActionResult Index(int? page)
        {
            var contractor = db.contractor.Where(x => x.IsDeleted == false).
            OrderByDescending(x => x.Id).ToList().ToPagedList(page ?? 1, 3);
            return View(contractor);
        }
        // GET: contractor
        public ActionResult Details(int id)
        {
            contractor contractor = db.contractor.Find(id);
            return View(contractor);
        }
        // GET: contractor/Create
        public ActionResult Create()
        {
            ViewBag.Countries = new SelectList(db.LKCountries, "Id", "ARName");
            ViewBag.Cities = new SelectList(db.Cities, "Id", "Aname");
            ViewBag.Accounts = new SelectList(db.LKAccounts, "Id", "ARName");
            return View();
        }

        // POST: contractor/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(contractor contractor)
        {
            ViewBag.Countries = new SelectList(db.LKCountries, "Id", "ARName", contractor.LKCountryId);
            ViewBag.Cities = new SelectList(db.Cities, "Id", "Aname", contractor.LKCityId);
            ViewBag.Accounts = new SelectList(db.LKAccounts, "Id", "ARName", contractor.AccountId);
            contractor.IsDeleted = false;
            try
            {
                //if (ModelState.IsValid)
                //{
                db.contractor.Add(contractor);
                db.SaveChanges();
                ViewBag.msgg = 1;
                ModelState.Clear();
                return View();
                //}
            }
            catch (DataException)
            {
                //ModelState.AddModelError("", "");
            }
            return View();
        }

        // GET: contractor/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            contractor contractor = db.contractor.Find(id);
            ViewBag.Countries = new SelectList(db.LKCountries, "Id", "ARName");
            ViewBag.Cities = new SelectList(db.Cities, "Id", "Aname");
            ViewBag.Accounts = new SelectList(db.LKAccounts, "Id", "ARName");
            if (contractor == null)
            {
                return HttpNotFound();
            }
            return View(contractor);
        }

        // POST: contractor/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(contractor contractor)
        {
            try
            {
                //if (ModelState.IsValid)
                //{
                ViewBag.Countries = new SelectList(db.LKCountries, "Id", "ARName", contractor.LKCountryId);
                ViewBag.Cities = new SelectList(db.Cities, "Id", "Aname", contractor.LKCityId);
                ViewBag.Accounts = new SelectList(db.LKAccounts, "Id", "ARName", contractor.AccountId);
                contractor.IsDeleted = false;
                db.Entry(contractor).State = EntityState.Modified;
                db.SaveChanges();
                ViewBag.msgg = 1;
                return View(contractor);
                //return RedirectToAction("Index");
                //}
            }
            catch (DataException)
            {
                //ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            ViewBag.Countries = new SelectList(db.LKCountries, "Id", "ARName", contractor.LKCountryId);
            ViewBag.Cities = new SelectList(db.Cities, "Id", "Aname", contractor.LKCityId);
            ViewBag.Accounts = new SelectList(db.LKAccounts, "Id", "ARName", contractor.AccountId);
            return View(contractor);
        }

        // GET: contractor/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            contractor contractor = db.contractor.Find(id);
            if (contractor == null)
            {
                return HttpNotFound();
            }
            return View(contractor);
        }

        // POST: contractor/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            contractor contractor = db.contractor.Find(id);
            contractor.IsDeleted = true;
            //db.LKBranchs.Remove(LKBranchs);
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