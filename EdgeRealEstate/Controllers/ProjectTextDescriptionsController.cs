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

    public class ProjectTextDescriptionsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ProjectTextDescriptions
        public async Task<ActionResult> Index()
        {
            return View(await db.ProjectTextDescriptions.ToListAsync());
        }

        // GET: ProjectTextDescriptions/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProjectTextDescription projectTextDescription = await db.ProjectTextDescriptions.FindAsync(id);
            if (projectTextDescription == null)
            {
                return HttpNotFound();
            }
            return View(projectTextDescription);
        }

        // GET: ProjectTextDescriptions/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProjectTextDescriptions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Aname,Ename")] ProjectTextDescription projectTextDescription)
        {
            if (ModelState.IsValid)
            {
                db.ProjectTextDescriptions.Add(projectTextDescription);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(projectTextDescription);
        }

        // GET: ProjectTextDescriptions/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProjectTextDescription projectTextDescription = await db.ProjectTextDescriptions.FindAsync(id);
            if (projectTextDescription == null)
            {
                return HttpNotFound();
            }
            return View(projectTextDescription);
        }

        // POST: ProjectTextDescriptions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Aname,Ename")] ProjectTextDescription projectTextDescription)
        {
            if (ModelState.IsValid)
            {
                db.Entry(projectTextDescription).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(projectTextDescription);
        }

        // GET: ProjectTextDescriptions/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProjectTextDescription projectTextDescription = await db.ProjectTextDescriptions.FindAsync(id);
            if (projectTextDescription == null)
            {
                return HttpNotFound();
            }
            return View(projectTextDescription);
        }

        // POST: ProjectTextDescriptions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            ProjectTextDescription projectTextDescription = await db.ProjectTextDescriptions.FindAsync(id);
            db.ProjectTextDescriptions.Remove(projectTextDescription);
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
