using PagedList;
using EdgeRealEstate.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EdgeRealEstate.Entities;

namespace EdgeRealEstate.Controllers
{
    public class EmployeeAttachController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        // GET: EmployeeAttach
        public ActionResult Index(int? page)
        {
                var EmployeeAttaches = db.EmployeeAttaches.Where(x => x.isDeleted == false)
                .OrderByDescending(x => x.id).ToList().ToPagedList(page ?? 1, 1);
                return View(EmployeeAttaches);
        }

        // GET: EmployeeAttach/Create
        public ActionResult Create()
        {
                ViewBag.Employees = new SelectList(db.Employees.Where(x => x.IsDeleted == false), "Id", "ARName");
                return View();
        }

        // POST: EmployeeAttach/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(EmployeeAttach EmployeeAttaches, HttpPostedFileBase File)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    ViewBag.Employees = new SelectList(db.Employees.Where(x => x.IsDeleted == false)
                        , "Id", "ARName", EmployeeAttaches.EmpId);
                    EmployeeAttaches.isDeleted = false;

                    EmployeeAttaches.attachFile = new byte[File.ContentLength];
                    File.InputStream.Read(EmployeeAttaches.attachFile, 0, File.ContentLength);
                    if (File != null && File.ContentLength > 0)
                    {
                        var fileName = Path.GetFileName(File.FileName);
                        var AttachPath = Path.Combine(Server.MapPath("~/App_Data/uploads"), fileName);
                        File.SaveAs(AttachPath);
                        EmployeeAttaches.AttachPath = AttachPath;
                        EmployeeAttaches.FileName = fileName;
                    }
                    db.EmployeeAttaches.Add(EmployeeAttaches);
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
            ViewBag.Employees = new SelectList(db.Employees.Where(x => x.IsDeleted == false)
                                , "Id", "ARName", EmployeeAttaches.EmpId);
            ModelState.Clear();
            return View();
        }

        // GET: EmployeeAttach/Edit/5
        public ActionResult Edit(int? id)
        {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                EmployeeAttach EmployeeAttaches = db.EmployeeAttaches.Find(id);
                ViewBag.Employees = new SelectList(db.Employees.Where(x => x.IsDeleted == false), "Id", "ARName");
                if (EmployeeAttaches == null)
                {
                    return HttpNotFound();
                }
                return View(EmployeeAttaches);
        }

        // POST: EmployeeAttach/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EmployeeAttach EmployeeAttaches, HttpPostedFileBase File)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    ViewBag.Employees = new SelectList(db.Employees.Where(x => x.IsDeleted == false)
                        , "Id", "ARName", EmployeeAttaches.EmpId);
                    EmployeeAttaches.isDeleted = false;

                    EmployeeAttaches.attachFile = new byte[File.ContentLength];
                    File.InputStream.Read(EmployeeAttaches.attachFile, 0, File.ContentLength);
                    if (File != null && File.ContentLength > 0)
                    {
                        var fileName = Path.GetFileName(File.FileName);
                        var AttachPath = Path.Combine(Server.MapPath("~/App_Data/uploads"), fileName);
                        System.IO.File.Delete(Path.Combine(Server.MapPath("~/App_Data/uploads"), EmployeeAttaches.FileName));
                        File.SaveAs(AttachPath);
                        EmployeeAttaches.AttachPath = AttachPath;
                        EmployeeAttaches.FileName = fileName;
                    }
                    db.Entry(EmployeeAttaches).State = EntityState.Modified;
                    db.SaveChanges();
                    ViewBag.msgg = 1;
                    return View(EmployeeAttaches);
                    //return RedirectToAction("Index");
                }
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            ViewBag.Employees = new SelectList(db.Employees.Where(x => x.IsDeleted == false)
                        , "Id", "ARName", EmployeeAttaches.EmpId);
            return View(EmployeeAttaches);
        }

        // GET: EmployeeAttach/Delete/5
        public ActionResult Delete(int? id)
        {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                EmployeeAttach EmployeeAttaches = db.EmployeeAttaches.Find(id);
                if (EmployeeAttaches == null)
                {
                    return HttpNotFound();
                }
                return View(EmployeeAttaches);
        }

        // POST: EmployeeAttach/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EmployeeAttach EmployeeAttaches = db.EmployeeAttaches.Find(id);
            System.IO.File.Delete(Path.Combine(Server.MapPath("~/App_Data/uploads"), EmployeeAttaches.FileName));
            EmployeeAttaches.isDeleted = true;
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