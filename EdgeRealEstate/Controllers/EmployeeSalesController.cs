using PagedList;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EdgeRealEstate.Entities;
using EdgeRealEstate.Models;

namespace EdgeRealEstate.Controllers
{
    public class EmployeeSalesController : Controller
    {

        ApplicationDbContext db = new ApplicationDbContext();
        // GET: EmployeeSales
        public ActionResult Index(int? page)
        {
            var EmployeeSales = db.EmployeeSales.Where(x => x.IsDeleted == false).
            OrderByDescending(x => x.id).ToList().ToPagedList(page ?? 1, 3);
            return View(EmployeeSales);
        }
        // GET: EmployeeSales
        public ActionResult Details(int id)
        {
            EmployeeSales EmployeeSales = db.EmployeeSales.Find(id);
            return View(EmployeeSales);
        }
        // GET: EmployeeSales/Create
        public ActionResult Create()
        {
            ViewBag.Employees = new SelectList(db.Employees, "id", "ARName");
            ViewBag.Customers = new SelectList(db.Customers, "id", "ARName");
            ViewBag.Project = new SelectList(db.Projectes, "id", "AName");
            ViewBag.Flat = new SelectList(db.Flats, "id", "Aname");
            ViewBag.Villa = new SelectList(db.Villas, "id", "Aname");
            ViewBag.PaymentSystems = new SelectList(db.PaymentSystems, "id", "ARName");
            return View();
        }

        // POST: Customer/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(EmployeeSales EmployeeSales)
        {
            ViewBag.Employees = new SelectList(db.Employees, "id", "ARName", EmployeeSales.EmpId);
            ViewBag.Customers = new SelectList(db.Customers, "id", "ARName", EmployeeSales.CustId);
            //ViewBag.Project = new SelectList(db.Projectes, "id", "ARName", EmployeeSales.ProjectId);
            ViewBag.Flat = new SelectList(db.Flats, "id", "Aname", EmployeeSales.FlatId);
            ViewBag.Villa = new SelectList(db.Villas, "id", "Aname", EmployeeSales.VillaId);
            ViewBag.PaymentSystems = new SelectList(db.PaymentSystems, "id", "ARName", EmployeeSales.PaymentSystemId);
            EmployeeSales.IsDeleted = false;
            try
            {
                //if (ModelState.IsValid)
                //{
                db.EmployeeSales.Add(EmployeeSales);
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
            EmployeeSales EmployeeSales = db.EmployeeSales.Find(id);
            ViewBag.Employees = new SelectList(db.Employees, "id", "ARName");
            ViewBag.Customers = new SelectList(db.Customers, "id", "ARName");
            ViewBag.Project = new SelectList(db.Projectes, "id", "ARName");
            ViewBag.Flat = new SelectList(db.Flats, "id", "Aname");
            ViewBag.Villa = new SelectList(db.Villas, "id", "Aname");
            ViewBag.PaymentSystems = new SelectList(db.PaymentSystems, "id", "ARName");
            if (EmployeeSales == null)
            {
                return HttpNotFound();
            }
            return View(EmployeeSales);
        }

        // POST: Customer/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EmployeeSales EmployeeSales)
        {
            try
            {
                //if (ModelState.IsValid)
                //{
                ViewBag.Employees = new SelectList(db.Employees, "id", "ARName", EmployeeSales.EmpId);
                ViewBag.Customers = new SelectList(db.Customers, "id", "ARName", EmployeeSales.CustId);
               // ViewBag.Project = new SelectList(db.Projectes, "id", "ARName", EmployeeSales.ProjectId);
                ViewBag.Flat = new SelectList(db.Flats, "id", "Aname", EmployeeSales.FlatId);
                ViewBag.Villa = new SelectList(db.Villas, "id", "Aname", EmployeeSales.VillaId);
                ViewBag.PaymentSystems = new SelectList(db.PaymentSystems, "id", "ARName", EmployeeSales.PaymentSystemId);
                EmployeeSales.IsDeleted = false;
                db.Entry(EmployeeSales).State = EntityState.Modified;
                db.SaveChanges();
                ViewBag.msgg = 1;
                return View(EmployeeSales);
                //return RedirectToAction("Index");
                //}
            }
            catch (DataException)
            {
                //ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            ViewBag.Employees = new SelectList(db.Employees, "id", "ARName", EmployeeSales.EmpId);
            ViewBag.Customers = new SelectList(db.Customers, "id", "ARName", EmployeeSales.CustId);
           // ViewBag.Project = new SelectList(db.Projectes, "id", "ARName", EmployeeSales.ProjectId);
            ViewBag.Flat = new SelectList(db.Flats, "id", "Aname", EmployeeSales.FlatId);
            ViewBag.Villa = new SelectList(db.Villas, "id", "Aname", EmployeeSales.VillaId);
            ViewBag.PaymentSystems = new SelectList(db.PaymentSystems, "id", "ARName", EmployeeSales.PaymentSystemId);
            return View(EmployeeSales);
        }

        // GET: Customer/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmployeeSales EmployeeSales = db.EmployeeSales.Find(id);
            if (EmployeeSales == null)
            {
                return HttpNotFound();
            }
            return View(EmployeeSales);
        }

        // POST: Customer/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EmployeeSales EmployeeSales = db.EmployeeSales.Find(id);
            EmployeeSales.IsDeleted = true;
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