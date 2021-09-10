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
    public class LKTitleController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        // GET: LKTitle
        public ActionResult Index(int? page)
        {
                var LKTitles = db.LKTitles.Where(x => x.IsDeleted == false).OrderByDescending(x => x.Id).ToList().ToPagedList(page ?? 1, 1);
                return View(LKTitles);
        }
        // GET: LKTitle/Create
        public ActionResult Create()
        {
                return View();
        }

        // POST: LKTitle/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(LKTitle LKTitles)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    LKTitles.IsDeleted = false;
                    db.LKTitles.Add(LKTitles);
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

        // GET: LKTitle/Edit/5
        public ActionResult Edit(int? id)
        {

                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                LKTitle LKTitles = db.LKTitles.Find(id);
                if (LKTitles == null)
                {
                    return HttpNotFound();
                }
                return View(LKTitles);
        }

        // POST: LKTitle/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(LKTitle LKTitles)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    LKTitles.IsDeleted = false;
                    db.Entry(LKTitles).State = EntityState.Modified;
                    db.SaveChanges();
                    ViewBag.msgg = 1;
                    return View(LKTitles);
                    //return RedirectToAction("Index");
                }
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            return View(LKTitles);
        }

        // GET: LKTitle/Delete/5
        public ActionResult Delete(int? id)
        {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                LKTitle LKTitles = db.LKTitles.Find(id);
                if (LKTitles == null)
                {
                    return HttpNotFound();
                }
                return View(LKTitles);
        }

        // POST: LKTitle/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            LKTitle LKTitles = db.LKTitles.Find(id);
            LKTitles.IsDeleted = true;
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