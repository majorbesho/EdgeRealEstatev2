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
    public class LKBranchController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        // GET: LKBranch
        public ActionResult Index(int? page)
        {
                var LKBranchs = db.LKBranchs.Where(x => x.IsDeleted == false).
                OrderByDescending(x => x.Id).ToList().ToPagedList(page ?? 1, 1);
                return View(LKBranchs);
        }
        // GET: LKBranch/Create
        public ActionResult Create()
        {
                return View();
        }

        // POST: LKBranch/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(LKBranch LKBranchs)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    LKBranchs.IsDeleted = false;
                    db.LKBranchs.Add(LKBranchs);
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

        // GET: LKBranch/Edit/5
        public ActionResult Edit(int? id)
        {

                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                LKBranch LKBranchs = db.LKBranchs.Find(id);
                if (LKBranchs == null)
                {
                    return HttpNotFound();
                }
                return View(LKBranchs);
        }

        // POST: LKBranch/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(LKBranch LKBranchs)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    LKBranchs.IsDeleted = false;
                    db.Entry(LKBranchs).State = EntityState.Modified;
                    db.SaveChanges();
                    ViewBag.msgg = 1;
                    return View(LKBranchs);
                    //return RedirectToAction("Index");
                }
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            return View(LKBranchs);
        }

        // GET: LKBranch/Delete/5
        public ActionResult Delete(int? id)
        {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                LKBranch LKBranchs = db.LKBranchs.Find(id);
                if (LKBranchs == null)
                {
                    return HttpNotFound();
                }
                return View(LKBranchs);
        }

        // POST: LKBranch/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            LKBranch LKBranchs = db.LKBranchs.Find(id);
            LKBranchs.IsDeleted = true;
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