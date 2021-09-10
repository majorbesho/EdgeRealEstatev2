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
    public class CustomerController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        // GET: Customer
        public ActionResult Index(int? page)
        {
                var Customers = db.Customers.Where(x => x.IsDeleted == false).
                OrderByDescending(x => x.Id).ToList().ToPagedList(page ?? 1, 3);
                return View(Customers);
        }
        // GET: Customer
        public ActionResult Details(int id)
        {
            Customer Customers = db.Customers.Find(id);
            return View(Customers);
        }
        // GET: Customer/Create
        public ActionResult Create()
        {
                ViewBag.Countries = new SelectList(db.LKCountries, "Id", "ARName");
                ViewBag.Cities = new SelectList(db.Cities, "Id", "Aname");
                ViewBag.Accounts = new SelectList(db.LKAccounts, "Id", "ARName");
                return View();
        }

        // POST: Customer/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Customer Customers)
        {
            ViewBag.Countries = new SelectList(db.LKCountries, "Id", "ARName", Customers.LKCountryId);
            ViewBag.Cities = new SelectList(db.Cities, "Id", "Aname", Customers.LKCityId);
            ViewBag.Accounts = new SelectList(db.LKAccounts, "Id", "ARName", Customers.AccountId);
            Customers.IsDeleted = false;
            try
            {
                //if (ModelState.IsValid)
                //{
                db.Customers.Add(Customers);
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

        // GET: Customer/Edit/5
        public ActionResult Edit(int? id)
        {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Customer Customers = db.Customers.Find(id);
                ViewBag.Countries = new SelectList(db.LKCountries, "Id", "ARName");
                ViewBag.Cities = new SelectList(db.Cities, "Id", "Aname");
                ViewBag.Accounts = new SelectList(db.LKAccounts, "Id", "ARName");
                if (Customers == null)
                {
                    return HttpNotFound();
                }
                return View(Customers);
        }

        // POST: Customer/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Customer Customers)
        {
            try
            {
                //if (ModelState.IsValid)
                //{
                ViewBag.Countries = new SelectList(db.LKCountries, "Id", "ARName", Customers.LKCountryId);
                ViewBag.Cities = new SelectList(db.Cities, "Id", "Aname", Customers.LKCityId);
                ViewBag.Accounts = new SelectList(db.LKAccounts, "Id", "ARName", Customers.AccountId);
                Customers.IsDeleted = false;
                db.Entry(Customers).State = EntityState.Modified;
                db.SaveChanges();
                ViewBag.msgg = 1;
                return View(Customers);
                //return RedirectToAction("Index");
                //}
            }
            catch (DataException)
            {
                //ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            ViewBag.Countries = new SelectList(db.LKCountries, "Id", "ARName", Customers.LKCountryId);
            ViewBag.Cities = new SelectList(db.Cities, "Id", "Aname", Customers.LKCityId);
            ViewBag.Accounts = new SelectList(db.LKAccounts, "Id", "ARName", Customers.AccountId);
            return View(Customers);
        }

        // GET: Customer/Delete/5
        public ActionResult Delete(int? id)
        {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Customer Customers = db.Customers.Find(id);
                if (Customers == null)
                {
                    return HttpNotFound();
                }
                return View(Customers);
        }

        // POST: Customer/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Customer Customers = db.Customers.Find(id);
            Customers.IsDeleted = true;
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