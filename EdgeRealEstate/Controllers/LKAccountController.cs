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
    public class LKAccountController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        // GET: LKAccount
        public ActionResult Index(int? page)
        {
                var LKAccounts = db.LKAccounts.Where(x => x.IsDeleted == false)
                .OrderByDescending(x => x.Id).ToList().ToPagedList(page ?? 1, 1);
                return View(LKAccounts);
        }

        // GET: LKAccount/Create
        public ActionResult Create()
        { 
                return View();
        }

        // POST: LKAccount/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(LKAccount LKAccounts)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    LKAccounts.IsDeleted = false;
                    db.LKAccounts.Add(LKAccounts);
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

        // GET: LKAccount/Edit/5
        public ActionResult Edit(int? id)
        {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                LKAccount LKAccounts = db.LKAccounts.Find(id);
                if (LKAccounts == null)
                {
                    return HttpNotFound();
                }
                return View(LKAccounts);
        }

        // POST: LKAccount/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(LKAccount LKAccounts)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    LKAccounts.IsDeleted = false;
                    db.Entry(LKAccounts).State = EntityState.Modified;
                    db.SaveChanges();
                    ViewBag.msgg = 1;
                    return View(LKAccounts);
                    //return RedirectToAction("Index");
                }
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            return View(LKAccounts);
        }

        // GET: LKAccount/Delete/5
        public ActionResult Delete(int? id)
        {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                LKAccount LKAccounts = db.LKAccounts.Find(id);
                if (LKAccounts == null)
                {
                    return HttpNotFound();
                }
                return View(LKAccounts);
        }

        // POST: LKAccount/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            LKAccount LKAccounts = db.LKAccounts.Find(id);
            LKAccounts.IsDeleted = true;
            //db.LKBrands.Remove(LKBrands);
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