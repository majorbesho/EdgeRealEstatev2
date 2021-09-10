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
    public class CustPaperReceiptController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        // GET: PaperReceipt
        public ActionResult Index(int? page)
        {
                var CustPaperReceipts = db.CustPaperReceipts.Where(x => x.isDeleted == false)
                .OrderByDescending(x => x.id).ToList().ToPagedList(page ?? 1, 1);
                return View(CustPaperReceipts);
        }

        // GET: PaperReceipt/Create
        public ActionResult Create()
        {
                ViewBag.Customers = new SelectList(db.Customers, "Id", "ARName");
                ViewBag.Employees = new SelectList(db.Employees, "Id", "ARName");
                ViewBag.SalesMans = new SelectList(db.Employees, "Id", "ARName");
                return View();
        }

        // POST: PaperReceipt/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CustPaperReceipt CustPaperReceipts)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    ViewBag.Customers = new SelectList(db.Customers, "Id", "ARName", CustPaperReceipts.customerId);
                    ViewBag.Employees = new SelectList(db.Employees, "Id", "ARName", CustPaperReceipts.empId);
                    ViewBag.SalesMans = new SelectList(db.Employees, "Id", "ARName", CustPaperReceipts.salesManId);
                    CustPaperReceipts.isDeleted = false;
                    db.CustPaperReceipts.Add(CustPaperReceipts);
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
            ViewBag.Customers = new SelectList(db.Customers, "Id", "ARName", CustPaperReceipts.customerId);
            ViewBag.Employees = new SelectList(db.Employees, "Id", "ARName", CustPaperReceipts.empId);
            ViewBag.SalesMans = new SelectList(db.Employees, "Id", "ARName", CustPaperReceipts.salesManId);
            ModelState.Clear();
            return View();
        }

        // GET: PaperReceipt/Edit/5
        public ActionResult Edit(int? id)
        {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                CustPaperReceipt CustPaperReceipts = db.CustPaperReceipts.Find(id);
                ViewBag.Customers = new SelectList(db.Customers, "Id", "ARName");
                ViewBag.Employees = new SelectList(db.Employees, "Id", "ARName");
                ViewBag.SalesMans = new SelectList(db.Employees, "Id", "ARName");
                if (CustPaperReceipts == null)
                {
                    return HttpNotFound();
                }
                return View(CustPaperReceipts);
        }

        // POST: PaperReceipt/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CustPaperReceipt CustPaperReceipts)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    ViewBag.Customers = new SelectList(db.Customers, "Id", "ARName", CustPaperReceipts.customerId);
                    ViewBag.Employees = new SelectList(db.Employees, "Id", "ARName", CustPaperReceipts.empId);
                    ViewBag.SalesMans = new SelectList(db.Employees, "Id", "ARName", CustPaperReceipts.salesManId);
                    CustPaperReceipts.isDeleted = false;
                    db.Entry(CustPaperReceipts).State = EntityState.Modified;
                    db.SaveChanges();
                    ViewBag.msgg = 1;
                    return View(CustPaperReceipts);
                }
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            ViewBag.Customers = new SelectList(db.Customers, "Id", "ARName", CustPaperReceipts.customerId);
            ViewBag.Employees = new SelectList(db.Employees, "Id", "ARName", CustPaperReceipts.empId);
            ViewBag.SalesMans = new SelectList(db.Employees, "Id", "ARName", CustPaperReceipts.salesManId);
            return View(CustPaperReceipts);
        }

        // GET: PaperReceipt/Delete/5
        public ActionResult Delete(int? id)
        {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                CustPaperReceipt CustPaperReceipts = db.CustPaperReceipts.Find(id);
                if (CustPaperReceipts == null)
                {
                    return HttpNotFound();
                }
                return View(CustPaperReceipts);
        }

        // POST: PaperReceipt/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CustPaperReceipt CustPaperReceipts = db.CustPaperReceipts.Find(id);
            CustPaperReceipts.isDeleted = true;
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