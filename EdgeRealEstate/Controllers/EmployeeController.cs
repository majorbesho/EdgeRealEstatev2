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
    public class EmployeeController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        // GET: Employee
        public ActionResult Index(int? page)
        {
                var Employees = db.Employees.Where(x => x.IsDeleted == false)
                .OrderByDescending(x => x.Id).ToList().ToPagedList(page ?? 1, 8);
                return View(Employees);
        }
        // GET: Employee/Details
        public ActionResult Details(int id)
        {
            Employee Employees = db.Employees.Find(id);
            return View(Employees);
        }
        // GET: Employee/Create
        public ActionResult Create()
        {
                ViewBag.Branchs = new SelectList(db.LKBranchs.OrderBy(x=>x.Id), "Id", "ARName");
                ViewBag.Nationalities = new SelectList(db.LKNationalities, "Id", "ARName");
                ViewBag.Titles = new SelectList(db.LKTitles, "Id", "ARName");
                return View();
        }

        // POST: Employee/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Employee Employees)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    ViewBag.Branchs = new SelectList(db.LKBranchs.OrderBy(x => x.Id), "Id", "ARName", Employees.LKBranchId);
                    ViewBag.Nationalities = new SelectList(db.LKNationalities, "Id", "ARName", Employees.LKNationalityId);
                    ViewBag.Titles = new SelectList(db.LKTitles, "Id", "ARName", Employees.TitleId);
                    Employees.IsDeleted = false;
                    db.Employees.Add(Employees);
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
            ViewBag.Branchs = new SelectList(db.LKBranchs, "Id", "ARName", Employees.LKBranchId);
            ViewBag.Nationalities = new SelectList(db.LKNationalities, "Id", "ARName", Employees.LKNationalityId);
            ViewBag.Titles = new SelectList(db.LKTitles, "Id", "ARName", Employees.TitleId);
            //ModelState.Clear();
            return View();
        }

        // GET: Employee/Edit/5
        public ActionResult Edit(int? id)
        {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Employee Employees = db.Employees.Find(id);
                ViewBag.Branchs = new SelectList(db.LKBranchs, "Id", "ARName");
                ViewBag.Nationalities = new SelectList(db.LKNationalities, "Id", "ARName");
                ViewBag.Titles = new SelectList(db.LKTitles, "Id", "ARName");
                if (Employees == null)
                {
                    return HttpNotFound();
                }
                return View(Employees);
        }

        // POST: Employee/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Employee Employees)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    ViewBag.Branchs = new SelectList(db.LKBranchs, "Id", "ARName", Employees.LKBranchId);
                    ViewBag.Nationalities = new SelectList(db.LKNationalities, "Id", "ARName", Employees.LKNationalityId);
                    ViewBag.Titles = new SelectList(db.LKTitles, "Id", "ARName", Employees.TitleId);
                    Employees.IsDeleted = false;
                    db.Entry(Employees).State = EntityState.Modified;
                    db.SaveChanges();
                    ViewBag.msgg = 1;
                    return View(Employees);
                    //return RedirectToAction("Index");
                }
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            ViewBag.Branchs = new SelectList(db.LKBranchs, "Id", "ARName", Employees.LKBranchId);
            ViewBag.Nationalities = new SelectList(db.LKNationalities, "Id", "ARName", Employees.LKNationalityId);
            ViewBag.Titles = new SelectList(db.LKTitles, "Id", "ARName", Employees.TitleId);
            return View(Employees);
        }

        // GET: Employee/Delete/5
        public ActionResult Delete(int? id)
        {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Employee Employees = db.Employees.Find(id);
                if (Employees == null)
                {
                    return HttpNotFound();
                }
                return View(Employees);
        }

        // POST: Employee/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Employee Employees = db.Employees.Find(id);
            Employees.IsDeleted = true;
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