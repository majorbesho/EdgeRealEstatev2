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
    public class contractorPaperPaymentController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        // GET: contractorPaperPayment
        public ActionResult Index(int? page)
        {
            var contractorPaperPayment = db.contractorPaperPayments.Where(x => x.isDeleted == false)
            .OrderByDescending(x => x.id).ToList().ToPagedList(page ?? 1, 10);
            return View(contractorPaperPayment);
        }

        // GET: contractorPaperPayment/Create
        public ActionResult Create()
        {
            ViewBag.contractor = new SelectList(db.contractor, "Id", "ARName");
            ViewBag.Employees = new SelectList(db.Employees, "Id", "ARName");
            ViewBag.SalesMans = new SelectList(db.Employees, "Id", "ARName");
            return View();
        }

        // POST: contractorPaperPayment/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(contractorPaperPayment contractorPaperPayment)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    ViewBag.contractor = new SelectList(db.contractor, "Id", "ARName", contractorPaperPayment.contractorId);
                    ViewBag.Employees = new SelectList(db.Employees, "Id", "ARName", contractorPaperPayment.empId);
                    ViewBag.SalesMans = new SelectList(db.Employees, "Id", "ARName", contractorPaperPayment.salesManId);
                    contractorPaperPayment.isDeleted = false;
                    db.contractorPaperPayments.Add(contractorPaperPayment);
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
            ViewBag.contractor = new SelectList(db.contractor, "Id", "ARName", contractorPaperPayment.contractorId);
            ViewBag.Employees = new SelectList(db.Employees, "Id", "ARName", contractorPaperPayment.empId);
            ViewBag.SalesMans = new SelectList(db.Employees, "Id", "ARName", contractorPaperPayment.salesManId);
            ModelState.Clear();
            return View();
        }

        // GET: contractorPaperPayment/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            contractorPaperPayment contractorPaperPayment = db.contractorPaperPayments.Find(id);
            ViewBag.contractor = new SelectList(db.contractor, "Id", "ARName");
            ViewBag.Employees = new SelectList(db.Employees, "Id", "ARName");
            ViewBag.SalesMans = new SelectList(db.Employees, "Id", "ARName");
            if (contractorPaperPayment == null)
            {
                return HttpNotFound();
            }
            return View(contractorPaperPayment);
        }

        // POST: contractorPaperPayment/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(contractorPaperPayment contractorPaperPayment)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    ViewBag.contractor = new SelectList(db.Customers, "Id", "ARName", contractorPaperPayment.contractorId);
                    ViewBag.Employees = new SelectList(db.Employees, "Id", "ARName", contractorPaperPayment.empId);
                    ViewBag.SalesMans = new SelectList(db.Employees, "Id", "ARName", contractorPaperPayment.salesManId);
                    contractorPaperPayment.isDeleted = false;
                    db.Entry(contractorPaperPayment).State = EntityState.Modified;
                    db.SaveChanges();
                    ViewBag.msgg = 1;
                    return View(contractorPaperPayment);
                }
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            ViewBag.contractor = new SelectList(db.contractor, "Id", "ARName", contractorPaperPayment.contractorId);
            ViewBag.Employees = new SelectList(db.Employees, "Id", "ARName", contractorPaperPayment.empId);
            ViewBag.SalesMans = new SelectList(db.Employees, "Id", "ARName", contractorPaperPayment.salesManId);
            return View(contractorPaperPayment);
        }

        // GET: contractorPaperPayment/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            contractorPaperPayment contractorPaperPayment = db.contractorPaperPayments.Find(id);
            if (contractorPaperPayment == null)
            {
                return HttpNotFound();
            }
            return View(contractorPaperPayment);
        }

        // POST: contractorPaperPayment/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            contractorPaperPayment contractorPaperPayment = db.contractorPaperPayments.Find(id);
            contractorPaperPayment.isDeleted = true;
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