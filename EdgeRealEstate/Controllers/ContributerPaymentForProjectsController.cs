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
    public class ContributerPaymentForProjectsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ContributerPaymentForProjects
        public async Task<ActionResult> Index()
        {
            return View(await db.ContributerPaymentForProjects.ToListAsync());
        }

        // GET: ContributerPaymentForProjects/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ContributerPaymentForProject contributerPaymentForProject = await db.ContributerPaymentForProjects.FindAsync(id);
            if (contributerPaymentForProject == null)
            {
                return HttpNotFound();
            }
            return View(contributerPaymentForProject);
        }

        // GET: ContributerPaymentForProjects/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ContributerPaymentForProjects/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "id,ContributorId,ProjectId,paid,Actuallypaid,notes,salesManId,indate,empId,paidMethod,salesManName")] ContributerPaymentForProject contributerPaymentForProject)
        {
            if (ModelState.IsValid)
            {
                db.ContributerPaymentForProjects.Add(contributerPaymentForProject);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(contributerPaymentForProject);
        }

        // GET: ContributerPaymentForProjects/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ContributerPaymentForProject contributerPaymentForProject = await db.ContributerPaymentForProjects.FindAsync(id);
            if (contributerPaymentForProject == null)
            {
                return HttpNotFound();
            }
            return View(contributerPaymentForProject);
        }

        // POST: ContributerPaymentForProjects/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "id,ContributorId,ProjectId,paid,Actuallypaid,notes,salesManId,indate,empId,paidMethod,salesManName")] ContributerPaymentForProject contributerPaymentForProject)
        {
            if (ModelState.IsValid)
            {
                db.Entry(contributerPaymentForProject).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(contributerPaymentForProject);
        }

        // GET: ContributerPaymentForProjects/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ContributerPaymentForProject contributerPaymentForProject = await db.ContributerPaymentForProjects.FindAsync(id);
            if (contributerPaymentForProject == null)
            {
                return HttpNotFound();
            }
            return View(contributerPaymentForProject);
        }

        // POST: ContributerPaymentForProjects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            ContributerPaymentForProject contributerPaymentForProject = await db.ContributerPaymentForProjects.FindAsync(id);
            db.ContributerPaymentForProjects.Remove(contributerPaymentForProject);
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
