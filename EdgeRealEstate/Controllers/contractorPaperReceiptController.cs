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
    public class contractorPaperReceiptController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        // GET: contractorPaperReceipt
        public ActionResult Index(int? page)
        {
            var contractorPaperReceipt = db.contractorPaperReceipts.Where(x => x.isDeleted == false)
            .OrderByDescending(x => x.id).ToList().ToPagedList(page ?? 1, 10);
            return View(contractorPaperReceipt);
        }

        // GET: contractorPaperReceipt/Create
        public ActionResult Create()
        {
            ViewBag.contractor = new SelectList(db.contractor, "Id", "ARName");
            ViewBag.Employees = new SelectList(db.Employees, "Id", "ARName");
            ViewBag.SalesMans = new SelectList(db.Employees, "Id", "ARName");
            return View();
        }

        // POST: contractorPaperReceipt/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(contractorPaperReceipt contractorPaperReceipt)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    ViewBag.contractor = new SelectList(db.contractor, "Id", "ARName", contractorPaperReceipt.contractorId);
                    ViewBag.Employees = new SelectList(db.Employees, "Id", "ARName", contractorPaperReceipt.empId);
                    ViewBag.SalesMans = new SelectList(db.Employees, "Id", "ARName", contractorPaperReceipt.salesManId);
                    contractorPaperReceipt.isDeleted = false;
                    db.contractorPaperReceipts.Add(contractorPaperReceipt);
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
            ViewBag.contractor = new SelectList(db.contractor, "Id", "ARName", contractorPaperReceipt.contractorId);
            ViewBag.Employees = new SelectList(db.Employees, "Id", "ARName", contractorPaperReceipt.empId);
            ViewBag.SalesMans = new SelectList(db.Employees, "Id", "ARName", contractorPaperReceipt.salesManId);
            ModelState.Clear();
            return View();
        }

        // GET: contractorPaperReceipt/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            contractorPaperReceipt contractorPaperReceipt = db.contractorPaperReceipts.Find(id);
            ViewBag.contractor = new SelectList(db.contractor, "Id", "ARName");
            ViewBag.Employees = new SelectList(db.Employees, "Id", "ARName");
            ViewBag.SalesMans = new SelectList(db.Employees, "Id", "ARName");
            if (contractorPaperReceipt == null)
            {
                return HttpNotFound();
            }
            return View(contractorPaperReceipt);
        }

        // POST: contractorPaperReceipt/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(contractorPaperReceipt contractorPaperReceipt)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    ViewBag.contractor = new SelectList(db.contractor, "Id", "ARName", contractorPaperReceipt.contractorId);
                    ViewBag.Employees = new SelectList(db.Employees, "Id", "ARName", contractorPaperReceipt.empId);
                    ViewBag.SalesMans = new SelectList(db.Employees, "Id", "ARName", contractorPaperReceipt.salesManId);
                    contractorPaperReceipt.isDeleted = false;
                    db.Entry(contractorPaperReceipt).State = EntityState.Modified;
                    db.SaveChanges();
                    ViewBag.msgg = 1;
                    return View(contractorPaperReceipt);
                }
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            ViewBag.contractor = new SelectList(db.contractor, "Id", "ARName", contractorPaperReceipt.contractorId);
            ViewBag.Employees = new SelectList(db.Employees, "Id", "ARName", contractorPaperReceipt.empId);
            ViewBag.SalesMans = new SelectList(db.Employees, "Id", "ARName", contractorPaperReceipt.salesManId);
            return View(contractorPaperReceipt);
        }

        // GET: contractorPaperReceipt/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            contractorPaperReceipt contractorPaperReceipt = db.contractorPaperReceipts.Find(id);
            if (contractorPaperReceipt == null)
            {
                return HttpNotFound();
            }
            return View(contractorPaperReceipt);
        }

        // POST: contractorPaperReceipt/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            contractorPaperReceipt contractorPaperReceipt = db.contractorPaperReceipts.Find(id);
            contractorPaperReceipt.isDeleted = true;
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