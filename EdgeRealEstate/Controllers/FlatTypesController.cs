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
    public class FlatTypesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: FlatTypes
        public async Task<ActionResult> Index()
        {
            return View(await db.FlatTypes.ToListAsync());
        }

        // GET: FlatTypes/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FlatType flatType = await db.FlatTypes.FindAsync(id);
            if (flatType == null)
            {
                return HttpNotFound();
            }
            return View(flatType);
        }

        // GET: FlatTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: FlatTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Aname,Ename,Description,IsDeleted")] FlatType flatType)
        {
            if (ModelState.IsValid)
            {
                db.FlatTypes.Add(flatType);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(flatType);
        }

        // GET: FlatTypes/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FlatType flatType = await db.FlatTypes.FindAsync(id);
            if (flatType == null)
            {
                return HttpNotFound();
            }
            return View(flatType);
        }

        // POST: FlatTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Aname,Ename,Description,IsDeleted")] FlatType flatType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(flatType).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(flatType);
        }

        // GET: FlatTypes/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FlatType flatType = await db.FlatTypes.FindAsync(id);
            if (flatType == null)
            {
                return HttpNotFound();
            }
            return View(flatType);
        }

        // POST: FlatTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            FlatType flatType = await db.FlatTypes.FindAsync(id);
            db.FlatTypes.Remove(flatType);
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
