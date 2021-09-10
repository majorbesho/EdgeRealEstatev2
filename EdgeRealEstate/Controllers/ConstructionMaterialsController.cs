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
using System.IO;

namespace EdgeRealEstate.Controllers
{
    public class ConstructionMaterialsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ConstructionMaterials
        public async Task<ActionResult> Index()
        {
            return View(await db.ConstructionMaterials.ToListAsync());
        }

        // GET: ConstructionMaterials/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ConstructionMaterial constructionMaterial = await db.ConstructionMaterials.FindAsync(id);
            if (constructionMaterial == null)
            {
                return HttpNotFound();
            }
            return View(constructionMaterial);
        }

        // GET: ConstructionMaterials/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ConstructionMaterials/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,MaterialName,MasuringUnit,Length,Width,Price,Brand,Type,effective,ProcessingTime,Specifications,Notes,Image")] ConstructionMaterial constructionMaterial, IEnumerable<HttpPostedFileBase> Files)
        {
            if (ModelState.IsValid)
            {
                //ConstructionMaterial Con = new ConstructionMaterial();
                db.ConstructionMaterials.Add(constructionMaterial);
                db.SaveChanges();
                List<Attachment> Attachments = new List<Attachment>();
                if (Files != null)
                {
                    foreach (var file in Files)
                    {
                        var rondom = Guid.NewGuid() + Path.GetFileName(file.FileName);
                        var path = Path.Combine(Server.MapPath("~/Uploads"), rondom);

                        file.SaveAs(path);

                        Attachment att = new Attachment() {
                            Path = path,
                            ConstructionMaterialId = constructionMaterial.ID
                        };
                        Attachments.Add(att);
                        db.Attachments.Add(att);
                    }


                }
                return RedirectToAction("Index");
            }

            return View(constructionMaterial);
        }

        // GET: ConstructionMaterials/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ConstructionMaterial constructionMaterial = await db.ConstructionMaterials.FindAsync(id);
            if (constructionMaterial == null)
            {
                return HttpNotFound();
            }
            return View(constructionMaterial);
        }

        // POST: ConstructionMaterials/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,MaterialName,MasuringUnit,Length,Width,Price,Brand,Type,effective,ProcessingTime,Specifications,Notes,Image")] ConstructionMaterial constructionMaterial)
        {
            if (ModelState.IsValid)
            {
                db.Entry(constructionMaterial).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(constructionMaterial);
        }

        // GET: ConstructionMaterials/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ConstructionMaterial constructionMaterial = await db.ConstructionMaterials.FindAsync(id);
            if (constructionMaterial == null)
            {
                return HttpNotFound();
            }
            return View(constructionMaterial);
        }

        // POST: ConstructionMaterials/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            ConstructionMaterial constructionMaterial = await db.ConstructionMaterials.FindAsync(id);
            db.ConstructionMaterials.Remove(constructionMaterial);
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
