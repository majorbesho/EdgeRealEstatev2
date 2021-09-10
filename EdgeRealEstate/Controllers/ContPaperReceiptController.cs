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
    public class ContPaperReceiptController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        // GET: ContPaperReceipt
        public ActionResult Index(int? page)
        {
            var ContPaperReceipts = db.ContPaperReceipts.Where(x => x.isDeleted == false)
            .OrderByDescending(x => x.id).ToList().ToPagedList(page ?? 1, 10);
            return View(ContPaperReceipts);
        }

        // GET: ContPaperReceipt/Create
        public ActionResult Create()
        {
            ViewBag.Contributor = new SelectList(db.Contributor, "id", "ARName");
            ViewBag.Employees = new SelectList(db.Employees, "Id", "ARName");
            ViewBag.SalesMans = new SelectList(db.Employees, "Id", "ARName");
            ViewBag.Projects = new SelectList(db.Projectes, "Id", "Aname");
            ViewBag.paidMethod = new SelectList(db.PaymentMethods, "Id", "ARName");
            var list = new List<SelectListItem>
            {
                new SelectListItem{ Text=" استلام دفعات من المساهم", Value = "1" },
                new SelectListItem{ Text=" تخصيص مبلغ علي مساهمة", Value = "2" },
                new SelectListItem{ Text="  توزيع ربحية", Value = "3" },
                new SelectListItem{ Text="إغلاق المساهمة", Value = "4" },
            };

            ViewData["Types"] = list;
            return View();

          //  return View();
        }

        // POST: ContPaperReceipt/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ContPaperReceipt ContPaperReceipts)
        {
            ContPaperPayment ContPaperPayments = new ContPaperPayment();
           
            {
                if (ModelState.IsValid)
                {
                    ViewBag.Contributor = new SelectList(db.Contributor, "id", "ARName", ContPaperReceipts.ContributorId);
                    ViewBag.Employees = new SelectList(db.Employees, "Id", "ARName", ContPaperReceipts.empId);
                    ViewBag.SalesMans = new SelectList(db.Employees, "Id", "ARName", ContPaperReceipts.salesManId);
                    ViewBag.paidMethod = new SelectList(db.PaymentMethods, "Id", "ARName", ContPaperReceipts.paidMethod);
                    ViewBag.Projects = new SelectList(db.Projectes, "Id", "Aname");
                    //ViewBag.Contributor = new SelectList(db.Contributor, "id", "ARName" );
                    //ViewBag.Employees = new SelectList(db.Employees, "Id", "ARName");
                    //ViewBag.SalesMans = new SelectList(db.Employees, "Id", "ARName");
                    //ViewBag.Projects = new SelectList(db.Projectes, "Id", "Aname");
                    ContPaperReceipts.isDeleted = false;
                    var types = Request["Types"];
                    if (types=="1")
                    {
                        ContPaperReceipts.refType = "1";
                        //ContPaperReceipts.Project_id = 0;
                        db.ContPaperReceipts.Add(ContPaperReceipts);
                        db.SaveChanges();
                    }
                    else if(types=="2")
                    {
                        ContPaperReceipts.refType = "2";
                        db.ContPaperReceipts.Add(ContPaperReceipts);
                        try
                        {
                            db.SaveChanges();
                        }
                        catch (Exception eex)
                        { }
                        ContPaperPayments.ContributorId = ContPaperReceipts.ContributorId;
                        ContPaperPayments.paid = ContPaperReceipts.paid;
                        ContPaperPayments.refType = "3";
                        ContPaperPayments.indate = ContPaperReceipts.indate;
                        ContPaperPayments.empId = ContPaperReceipts.empId;
                        ContPaperPayments.salesManId = ContPaperReceipts.salesManId;
                        ContPaperPayments.isDeleted = ContPaperReceipts.isDeleted;

                        ContPaperPayments.ProjectId = ContPaperReceipts.ProjectId;
                        db.ContPaperPayments.Add(ContPaperPayments);
                        try
                        {
                            db.SaveChanges();
                        }
                        catch (Exception ex)
                        {

                        }
                    }
                    else if (types == "3")
                    {
                        ContPaperReceipts.refType = "3";
                       // ContPaperReceipts.Project_id = 0;
                        db.ContPaperReceipts.Add(ContPaperReceipts);
                        db.SaveChanges();
                        //ContPaperPayments.ContributorId = ContPaperReceipts.ContributorId;
                        //ContPaperPayments.paid = ContPaperReceipts.paid;
                        //ContPaperPayments.refType = "3";
                        //ContPaperPayments.indate = ContPaperReceipts.indate;
                        //ContPaperPayments.empId = ContPaperReceipts.empId;
                        //ContPaperPayments.salesManId = ContPaperReceipts.salesManId;
                        //ContPaperPayments.isDeleted = ContPaperReceipts.isDeleted;


                        //ContPaperPayments.Project_id = ContPaperReceipts.Project_id;
                    }
                    else if (types == "4")
                    {
                        ContPaperReceipts.refType = "4";
                        db.ContPaperReceipts.Add(ContPaperReceipts);
                        try
                        {
                            db.SaveChanges();
                        }
                        catch(Exception eex)
                        { }
                        ContPaperPayments.ContributorId = ContPaperReceipts.ContributorId;
                        ContPaperPayments.paid = ContPaperReceipts.paid;
                        ContPaperPayments.refType = "2";
                        ContPaperPayments.indate = ContPaperReceipts.indate;
                        ContPaperPayments.empId = ContPaperReceipts.empId;
                        ContPaperPayments.salesManId = ContPaperReceipts.salesManId;
                        ContPaperPayments.isDeleted = ContPaperReceipts.isDeleted;
                       // ContPaperPayments.Project_id = ContPaperReceipts.;

                        db.ContPaperPayments.Add(ContPaperPayments);
                        try 
                        { 
                        db.SaveChanges();
                        }
                        catch(Exception ex)
                        {

                        }
                    }

                    //db.ContPaperReceipts.Add(ContPaperReceipts);
                    //db.SaveChanges();
                    ViewBag.msgg = 1;
                    ViewBag.Contributor = new SelectList(db.Contributor, "id", "ARName");
                    ViewBag.Employees = new SelectList(db.Employees, "Id", "ARName");
                    ViewBag.SalesMans = new SelectList(db.Employees, "Id", "ARName");
                    ViewBag.Projects = new SelectList(db.Projectes, "Id", "Aname");
                    ViewBag.paidMethod = new SelectList(db.PaymentMethods, "Id", "ARName");
                    var list = new List<SelectListItem>
                    {
                        new SelectListItem{ Text=" استلام دفعات من المساهم", Value = "1" },
                        new SelectListItem{ Text=" تخصيص مبلغ علي مساهمة", Value = "2" },
                        new SelectListItem{ Text="  توزيع ربحية", Value = "3" },
                        new SelectListItem{ Text="إغلاق المساهمة", Value = "4" },
                    };

                    ViewData["Types"] = list;
                    ModelState.Clear();
                    return View();
                }
            }
            //catch (DataException)
            //{
            //    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            //}
            ViewBag.Customers = new SelectList(db.Customers, "Id", "ARName", ContPaperReceipts.ContributorId);
            ViewBag.Employees = new SelectList(db.Employees, "Id", "ARName", ContPaperReceipts.empId);
            ViewBag.SalesMans = new SelectList(db.Employees, "Id", "ARName", ContPaperReceipts.salesManId);
            ViewBag.paidMethod = new SelectList(db.PaymentMethods, "Id", "ARName", ContPaperReceipts.paidMethod);
            //if (ContPaperReceipts.Project_id > 0)
            //{
            //    ViewBag.Projects = new SelectList(db.Projectes, "Id", "Aname", ContPaperReceipts.Project_id);
            //}
            //else
            //{
            //    ViewBag.Projects = new SelectList(db.Projectes, "Id", "Aname");
            //}
            ModelState.Clear();
            return View();
        }

        // GET: ContPaperReceipt/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ContPaperReceipt ContPaperReceipts = db.ContPaperReceipts.Find(id);
            ViewBag.Contributor = new SelectList(db.Contributor, "id", "ARName");
            ViewBag.Employees = new SelectList(db.Employees, "Id", "ARName");
            ViewBag.SalesMans = new SelectList(db.Employees, "Id", "ARName");
            if (ContPaperReceipts == null)
            {
                return HttpNotFound();
            }
            return View(ContPaperReceipts);
        }

        // POST: ContPaperReceipt/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ContPaperReceipt ContPaperReceipts)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    ViewBag.Contributor = new SelectList(db.Contributor, "id", "ARName", ContPaperReceipts.ContributorId);
                    ViewBag.Employees = new SelectList(db.Employees, "Id", "ARName", ContPaperReceipts.empId);
                    ViewBag.SalesMans = new SelectList(db.Employees, "Id", "ARName", ContPaperReceipts.salesManId);
                    ContPaperReceipts.isDeleted = false;
                    db.Entry(ContPaperReceipts).State = EntityState.Modified;
                    db.SaveChanges();
                    ViewBag.msgg = 1;
                    return View(ContPaperReceipts);
                }
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            ViewBag.Contributor = new SelectList(db.Contributor, "Id", "ARName", ContPaperReceipts.ContributorId);
            ViewBag.Employees = new SelectList(db.Employees, "Id", "ARName", ContPaperReceipts.empId);
            ViewBag.SalesMans = new SelectList(db.Employees, "Id", "ARName", ContPaperReceipts.salesManId);
            return View(ContPaperReceipts);
        }

        // GET: ContPaperReceipt/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ContPaperReceipt ContPaperReceipts = db.ContPaperReceipts.Find(id);
            if (ContPaperReceipts == null)
            {
                return HttpNotFound();
            }
            return View(ContPaperReceipts);
        }

        // POST: ContPaperReceipt/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ContPaperReceipt ContPaperReceipts = db.ContPaperReceipts.Find(id);
            ContPaperReceipts.isDeleted = true;
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