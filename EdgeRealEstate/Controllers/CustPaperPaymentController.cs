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
    public class CustPaperPaymentController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        // GET: PaperPayment
        public ActionResult Index(int? page)
        {
                var CustPaperPayments = db.CustPaperPayments.Where(x => x.isDeleted == false)
                .OrderByDescending(x => x.id).ToList().ToPagedList(page ?? 1, 1);
                return View(CustPaperPayments);
        }

        // GET: PaperPayment/Create
        public ActionResult Create()
        {
                ViewBag.Customers = new SelectList(db.Customers, "Id", "ARName");
                ViewBag.Employees = new SelectList(db.Employees, "Id", "ARName");
                ViewBag.SalesMans = new SelectList(db.Employees, "Id", "ARName");
                return View();
        }

        // POST: PaperPayment/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CustPaperPayment CustPaperPayments)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    ViewBag.Customers = new SelectList(db.Customers, "Id", "ARName", CustPaperPayments.customerId);
                    ViewBag.Employees = new SelectList(db.Employees, "Id", "ARName", CustPaperPayments.empId);
                    ViewBag.SalesMans = new SelectList(db.Employees, "Id", "ARName", CustPaperPayments.salesManId);
                    CustPaperPayments.isDeleted = false;
                    db.CustPaperPayments.Add(CustPaperPayments);
                    db.SaveChanges();
                    ViewBag.msgg = 1;
                    ModelState.Clear();
                    return View();
                }
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            ViewBag.Customers = new SelectList(db.Customers, "Id", "ARName", CustPaperPayments.customerId);
            ViewBag.Employees = new SelectList(db.Employees, "Id", "ARName", CustPaperPayments.empId);
            ViewBag.SalesMans = new SelectList(db.Employees, "Id", "ARName", CustPaperPayments.salesManId);
            ModelState.Clear();
            return View();
        }

        // GET: PaperPayment/Edit/5
        public ActionResult Edit(int? id)
        {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                CustPaperPayment CustPaperPayments = db.CustPaperPayments.Find(id);
                ViewBag.Customers = new SelectList(db.Customers, "Id", "ARName");
                ViewBag.Employees = new SelectList(db.Employees, "Id", "ARName");
                ViewBag.SalesMans = new SelectList(db.Employees, "Id", "ARName");
                if (CustPaperPayments == null)
                {
                    return HttpNotFound();
                }
                return View(CustPaperPayments);
        }

        // POST: PaperPayment/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CustPaperPayment CustPaperPayments)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    ViewBag.Customers = new SelectList(db.Customers, "Id", "ARName", CustPaperPayments.customerId);
                    ViewBag.Employees = new SelectList(db.Employees, "Id", "ARName", CustPaperPayments.empId);
                    ViewBag.SalesMans = new SelectList(db.Employees, "Id", "ARName", CustPaperPayments.salesManId);
                    CustPaperPayments.isDeleted = false;
                    db.Entry(CustPaperPayments).State = EntityState.Modified;
                    db.SaveChanges();
                    ViewBag.msgg = 1;
                    return View(CustPaperPayments);
                }
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            ViewBag.Customers = new SelectList(db.Customers, "Id", "ARName", CustPaperPayments.customerId);
            ViewBag.Employees = new SelectList(db.Employees, "Id", "ARName", CustPaperPayments.empId);
            ViewBag.SalesMans = new SelectList(db.Employees, "Id", "ARName", CustPaperPayments.salesManId);
            return View(CustPaperPayments);
        }

        // GET: PaperPayment/Delete/5
        public ActionResult Delete(int? id)
        {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                CustPaperPayment CustPaperPayments = db.CustPaperPayments.Find(id);
                if (CustPaperPayments == null)
                {
                    return HttpNotFound();
                }
                return View(CustPaperPayments);
        }

        // POST: PaperPayment/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CustPaperPayment CustPaperPayments = db.CustPaperPayments.Find(id);
            CustPaperPayments.isDeleted = true;
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