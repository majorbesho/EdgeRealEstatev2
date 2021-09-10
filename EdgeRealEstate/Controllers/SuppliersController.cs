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
    [Authorize]

    public class SupplieresController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Suppliers
        public ActionResult Index()
        {
            return View(db.Supplieres.ToList());
        }

        // GET: Suppliers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Supplieres supplieres = db.Supplieres.Find(id);
            if (supplieres == null)
            {
                return HttpNotFound();
            }
            return View(supplieres);
        }

        // GET: Suppliers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Suppliers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,Aname,Ename,Type,ResEmp,tele1,Tele2,Adress,LikeNo,CompanyCV,limitation,limitationTime,CrruntyCompletionRate,imgLogo,BankAccount1Name,BankAccount1,BankAccountName2,BankAccount2,BankAccountName3,BankAccount3,nots")] Supplieres supplieres)
        {
            if (ModelState.IsValid)
            {
                db.Supplieres.Add(supplieres);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(supplieres);
        }

        // GET: Suppliers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Supplieres supplieres = db.Supplieres.Find(id);
            if (supplieres == null)
            {
                return HttpNotFound();
            }
            return View(supplieres);
        }

        // POST: Suppliers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,Aname,Ename,Type,ResEmp,tele1,Tele2,Adress,LikeNo,CompanyCV,limitation,limitationTime,CrruntyCompletionRate,imgLogo,BankAccount1Name,BankAccount1,BankAccountName2,BankAccount2,BankAccountName3,BankAccount3,nots")] Supplieres suppliers)
        {
            if (ModelState.IsValid)
            {
                db.Entry(suppliers).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(suppliers);
        }

        // GET: Suppliers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Supplieres suppliers = db.Supplieres.Find(id);
            if (suppliers == null)
            {
                return HttpNotFound();
            }
            return View(suppliers);
        }

        // POST: Suppliers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Supplieres suppliers = db.Supplieres.Find(id);
            db.Supplieres.Remove(suppliers);
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
