using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EdgeRealEstate.Entities;
using EdgeRealEstate.Models;

namespace EdgeRealEstate.Controllers
{
    [Authorize]

    public class EngenneringsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Engennerings
        public async Task<ActionResult> Index()
        {
            return View(await db.Engennerings.ToListAsync());
        }

        // GET: Engennerings/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Engennering engennering = await db.Engennerings.FindAsync(id);
            if (engennering == null)
            {
                return HttpNotFound();
            }
            return View(engennering);
        }

        // GET: Engennerings/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Engennerings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "id,Aname,Ename,Type,ResEmp,tele1,Tele2,Adress,LikeNo,EngCV,limitation,limitationTime,CrruntyCompletionRate,imgLogo,BankAccount1Name,BankAccount1,BankAccountName2,BankAccount2,BankAccountName3,BankAccount3,nots")] Engennering engennering)
        {
            if (ModelState.IsValid)
            {
                db.Engennerings.Add(engennering);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(engennering);
        }

        // GET: Engennerings/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Engennering engennering = await db.Engennerings.FindAsync(id);
            if (engennering == null)
            {
                return HttpNotFound();
            }
            return View(engennering);
        }

        // POST: Engennerings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "id,Aname,Ename,Type,ResEmp,tele1,Tele2,Adress,LikeNo,EngCV,limitation,limitationTime,CrruntyCompletionRate,imgLogo,BankAccount1Name,BankAccount1,BankAccountName2,BankAccount2,BankAccountName3,BankAccount3,nots")] Engennering engennering)
        {
            if (ModelState.IsValid)
            {
                db.Entry(engennering).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(engennering);
        }

        // GET: Engennerings/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Engennering engennering = await db.Engennerings.FindAsync(id);
            if (engennering == null)
            {
                return HttpNotFound();
            }
            return View(engennering);
        }

        // POST: Engennerings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Engennering engennering = await db.Engennerings.FindAsync(id);
            db.Engennerings.Remove(engennering);
            await db.SaveChangesAsync();
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
