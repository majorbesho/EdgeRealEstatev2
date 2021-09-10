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
    [Authorize]

    public class FlatsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Flats
        public async Task<ActionResult> Index()
        {
            return View(await db.Flats.ToListAsync());
        }

        // GET: Flats/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Flat flat = await db.Flats.FindAsync(id);
            if (flat == null)
            {
                return HttpNotFound();
            }
            return View(flat);
        }

        // GET: Flats/Create
        public ActionResult Create()
        {
            ViewBag.Projects = new SelectList(db.Projectes, "id", "Aname");
            ViewBag.DegreeOfExcellence = new SelectList(db.DegreeOfExcellences, "Id", "Aname");
            ViewBag.Buildings = new SelectList(db.Buildingses, "Id", "Code");
            ViewBag.FlatType = new SelectList(db.FlatTypes, "Id", "Aname");

            return View();
        }

        // POST: Flats/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Flat flat, IEnumerable<HttpPostedFileBase> Files, HttpPostedFileBase Image)
        {
            if (ModelState.IsValid)
            {
                if (Image != null)
                {
                    flat.ImgUrl = SaveFile(Image, "Flats");
                }
                db.Flats.Add(flat);
                await db.SaveChangesAsync();

                List<FlatAttachment> Attachments = new List<FlatAttachment>();
                if (Files != null)
                {
                    foreach (var file in Files)
                    {
                        var path = SaveFile(file, "Flats");
                        FlatAttachment att = new FlatAttachment()
                        {
                            Path = path,
                            FlatId = flat.id
                        };
                        Attachments.Add(att);
                        db.FlatAttachments.Add(att);
                        db.SaveChanges();

                    }


                }

                return RedirectToAction("Index");
            }
            ViewBag.Projects = new SelectList(db.Projectes, "id", "Aname");
            ViewBag.DegreeOfExcellence = new SelectList(db.DegreeOfExcellences, "Id", "Aname");

            
            return View(flat);
        }

        // GET: Flats/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            ViewBag.Projects = new SelectList(db.Projectes, "id", "Aname");
            ViewBag.DegreeOfExcellence = new SelectList(db.DegreeOfExcellences, "Id", "Aname");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Flat flat = await db.Flats.FindAsync(id);
            if (flat == null)
            {
                return HttpNotFound();
            }
            return View(flat);
        }

        // POST: Flats/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Flat flat, IEnumerable<HttpPostedFileBase> Files, HttpPostedFileBase Image)
        {
            ViewBag.Projects = new SelectList(db.Projectes, "id", "Aname");
            ViewBag.DegreeOfExcellence = new SelectList(db.DegreeOfExcellences, "Id", "Aname");
            if (ModelState.IsValid)
            {
                if (Image != null)
                {
                    System.IO.File.Delete(flat.ImgUrl);
                    flat.ImgUrl = SaveFile(Image, "Flats");
                }

                db.Entry(flat).State = EntityState.Modified;
                await db.SaveChangesAsync();

                List<FlatAttachment> Attachments = new List<FlatAttachment>();
                if (Files != null)
                {
                    var List = db.FlatAttachments.Where(x => x.FlatId == flat.id);
                    foreach (var item in List)
                    {
                        System.IO.File.Delete(item.Path);
                    }
                    foreach (var file in Files)
                    {
                        var path = SaveFile(file, "Flats");
                        FlatAttachment att = new FlatAttachment()
                        {
                            Path = path,
                            FlatId = flat.id
                        };
                        Attachments.Add(att);
                        db.FlatAttachments.Add(att);
                        db.SaveChanges();

                    }


                }

                return RedirectToAction("Index");
            }
            return View(flat);
        }

        // GET: Flats/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Flat flat = await db.Flats.FindAsync(id);
            if (flat == null)
            {
                return HttpNotFound();
            }
            return View(flat);
        }

        // POST: Flats/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Flat flat = await db.Flats.FindAsync(id);
            db.Flats.Remove(flat);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public string SaveFile(HttpPostedFileBase File, string Folder)
        {

            var rondom = Guid.NewGuid() + Path.GetFileName(File.FileName);
            var path = Path.Combine(Server.MapPath("~/Uploads/" + Folder), rondom);
            File.SaveAs(path);
            return path;

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
