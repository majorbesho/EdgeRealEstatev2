using PagedList;
using EdgeRealEstate.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EdgeRealEstate.Models;

namespace EdgeRealEstate.Controllers
{
    public class ContPaperPaymentController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        // GET: ContPaperPayment
        public ActionResult Index(int? page)
        {
            var ContPaperPayments = db.ContPaperPayments.Where(x => x.isDeleted == false)
            .OrderByDescending(x => x.id).ToList().ToPagedList(page ?? 1, 10);
            return View(ContPaperPayments);
        }
        // GET: ContPaperPayment/Create
        public ActionResult Create()
        {
            ViewBag.Contributor = new SelectList(db.Contributor, "id", "ARName");
            ViewBag.Employees = new SelectList(db.Employees, "Id", "ARName");
            ViewBag.SalesMans = new SelectList(db.Employees, "Id", "ARName");
            ViewData["Types"] = new List<SelectListItem>
            {
                new SelectListItem{ Text=" مجنب ", Value = "1" },
                new SelectListItem{ Text="رصيد", Value = "2" },
                new SelectListItem{ Text="", Value = "3", Selected = true },
            };

            
            return View();
        }

        // POST: ContPaperPayment/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ContPaperPayment ContPaperPayments)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    ViewBag.Contributor = new SelectList(db.Contributor, "id", "ARName", ContPaperPayments.ContributorId);
                    ViewBag.Employees = new SelectList(db.Employees, "Id", "ARName", ContPaperPayments.empId);
                    ViewBag.SalesMans = new SelectList(db.Employees, "Id", "ARName", ContPaperPayments.salesManId);
                    ContPaperPayments.isDeleted = false;
                    db.ContPaperPayments.Add(ContPaperPayments);
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
            ViewBag.Contributor = new SelectList(db.Contributor, "Id", "ARName", ContPaperPayments.ContributorId);
            ViewBag.Employees = new SelectList(db.Employees, "Id", "ARName", ContPaperPayments.empId);
            ViewBag.SalesMans = new SelectList(db.Employees, "Id", "ARName", ContPaperPayments.salesManId);
            ModelState.Clear();
            return View();
        }

        // GET: ContPaperPayment/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ContPaperPayment ContPaperPayments = db.ContPaperPayments.Find(id);
            ViewBag.Contributor = new SelectList(db.Contributor, "id", "ARName");
            ViewBag.Employees = new SelectList(db.Employees, "Id", "ARName");
            ViewBag.SalesMans = new SelectList(db.Employees, "Id", "ARName");
            ViewData["Types"] = new List<SelectListItem>
            {
                new SelectListItem{ Text=" مجنب ", Value = "1" },
                new SelectListItem{ Text="رصيد", Value = "2" },
                new SelectListItem{ Text="", Value = "3", Selected = true },
            };

            if (ContPaperPayments == null)
            {
                return HttpNotFound();
            }
            return View(ContPaperPayments);
        }

        // POST: ContPaperPayment/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ContPaperPayment ContPaperPayments)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    ViewBag.Contributor = new SelectList(db.Contributor, "id", "ARName", ContPaperPayments.ContributorId);
                    ViewBag.Employees = new SelectList(db.Employees, "Id", "ARName", ContPaperPayments.empId);
                    ViewBag.SalesMans = new SelectList(db.Employees, "Id", "ARName", ContPaperPayments.salesManId);
                    ViewData["Types"] = new List<SelectListItem>
                    {
                        new SelectListItem{ Text=" مجنب ", Value = "1" },
                        new SelectListItem{ Text="رصيد", Value = "2" },
                        new SelectListItem{ Text="", Value = "3", Selected = true },
                    };

                    ContPaperPayments.isDeleted = false;
                    db.Entry(ContPaperPayments).State = EntityState.Modified;
                    db.SaveChanges();
                    ViewBag.msgg = 1;
                    return View(ContPaperPayments);
                }
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            ViewBag.Contributor = new SelectList(db.Contributor, "Id", "ARName", ContPaperPayments.Contributor);
            ViewBag.Employees = new SelectList(db.Employees, "Id", "ARName", ContPaperPayments.empId);
            ViewBag.SalesMans = new SelectList(db.Employees, "Id", "ARName", ContPaperPayments.salesManId);
            return View(ContPaperPayments);
        }

        // GET: ContPaperPayment/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ContPaperPayment ContPaperPayments = db.ContPaperPayments.Find(id);
            if (ContPaperPayments == null)
            {
                return HttpNotFound();
            }
            return View(ContPaperPayments);
        }

        // POST: ContPaperPayment/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ContPaperPayment ContPaperPayments = db.ContPaperPayments.Find(id);
            ContPaperPayments.isDeleted = true;
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