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
    public class LKCountryController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        // GET: LKCountry
        public ActionResult Index(int? page)
        {

            var LKCountries = db.LKCountries.Where(x => x.IsDeleted == false).OrderByDescending(x => x.Id).ToList();//.ToPagedList(page ?? 1, 1);
                return View(LKCountries);
        }

        // GET: LKCountry/Create
        public ActionResult Create()
        {
                return View();
        }

        // POST: LKCountry/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(LKCountry LKCountries)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    LKCountries.IsDeleted = false;
                    db.LKCountries.Add(LKCountries);
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

        // GET: LKCountry/Edit/5
        public ActionResult Edit(int? id)
        {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                LKCountry LKCountries = db.LKCountries.Find(id);
                if (LKCountries == null)
                {
                    return HttpNotFound();
                }
                return View(LKCountries);
        }

        // POST: LKCountry/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(LKCountry LKCountries)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    LKCountries.IsDeleted = false;
                    db.Entry(LKCountries).State = EntityState.Modified;
                    db.SaveChanges();
                    ViewBag.msgg = 1;
                    return View(LKCountries);
                    //return RedirectToAction("Index");
                }
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            return View(LKCountries);
        }

        // GET: LKCountry/Delete/5
        public ActionResult Delete(int? id)
        {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                LKCountry LKCountries = db.LKCountries.Find(id);
                if (LKCountries == null)
                {
                    return HttpNotFound();
                }
                return View(LKCountries);
        }

        // POST: LKCountry/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            LKCountry LKCountries = db.LKCountries.Find(id);
            LKCountries.IsDeleted = true;
            //db.LKCountries.Remove(LKCountries);
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
