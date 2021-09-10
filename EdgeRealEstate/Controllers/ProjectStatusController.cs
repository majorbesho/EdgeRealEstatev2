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

    public class ProjectStatusController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ProjectStatus
        public async Task<ActionResult> Index()
        {
            return View(await db.ProjectStatus.ToListAsync());
        }

        // GET: ProjectStatus/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProjectStatu projectStatu = await db.ProjectStatus.FindAsync(id);
            if (projectStatu == null)
            {
                return HttpNotFound();
            }
            return View(projectStatu);
        }

        // GET: ProjectStatus/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProjectStatus/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Aname,Ename")] ProjectStatu projectStatu)
        {
            if (ModelState.IsValid)
            {
                db.ProjectStatus.Add(projectStatu);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(projectStatu);
        }

        // GET: ProjectStatus/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProjectStatu projectStatu = await db.ProjectStatus.FindAsync(id);
            if (projectStatu == null)
            {
                return HttpNotFound();
            }
            return View(projectStatu);
        }

        // POST: ProjectStatus/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Aname,Ename")] ProjectStatu projectStatu)
        {
            if (ModelState.IsValid)
            {
                db.Entry(projectStatu).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(projectStatu);
        }

        // GET: ProjectStatus/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProjectStatu projectStatu = await db.ProjectStatus.FindAsync(id);
            if (projectStatu == null)
            {
                return HttpNotFound();
            }
            return View(projectStatu);
        }

        // POST: ProjectStatus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            ProjectStatu projectStatu = await db.ProjectStatus.FindAsync(id);
            db.ProjectStatus.Remove(projectStatu);
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
