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

    public class DegreeOfExcellencesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: DegreeOfExcellences
        public async Task<ActionResult> Index()
        {
            return View(await db.DegreeOfExcellences.ToListAsync());
        }

        // GET: DegreeOfExcellences/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DegreeOfExcellence degreeOfExcellence = await db.DegreeOfExcellences.FindAsync(id);
            if (degreeOfExcellence == null)
            {
                return HttpNotFound();
            }
            return View(degreeOfExcellence);
        }

        // GET: DegreeOfExcellences/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DegreeOfExcellences/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Aname,Ename")] DegreeOfExcellence degreeOfExcellence)
        {
            if (ModelState.IsValid)
            {
                db.DegreeOfExcellences.Add(degreeOfExcellence);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(degreeOfExcellence);
        }

        // GET: DegreeOfExcellences/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DegreeOfExcellence degreeOfExcellence = await db.DegreeOfExcellences.FindAsync(id);
            if (degreeOfExcellence == null)
            {
                return HttpNotFound();
            }
            return View(degreeOfExcellence);
        }

        // POST: DegreeOfExcellences/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Aname,Ename")] DegreeOfExcellence degreeOfExcellence)
        {
            if (ModelState.IsValid)
            {
                db.Entry(degreeOfExcellence).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(degreeOfExcellence);
        }

        // GET: DegreeOfExcellences/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DegreeOfExcellence degreeOfExcellence = await db.DegreeOfExcellences.FindAsync(id);
            if (degreeOfExcellence == null)
            {
                return HttpNotFound();
            }
            return View(degreeOfExcellence);
        }

        // POST: DegreeOfExcellences/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            DegreeOfExcellence degreeOfExcellence = await db.DegreeOfExcellences.FindAsync(id);
            db.DegreeOfExcellences.Remove(degreeOfExcellence);
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
