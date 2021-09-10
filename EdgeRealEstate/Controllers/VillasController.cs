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

    public class VillasController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Villas
        public async Task<ActionResult> Index()
        {
            return View(await db.Villas.ToListAsync());
        }

        // GET: Villas/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Villa villa = await db.Villas.FindAsync(id);
            if (villa == null)
            {
                return HttpNotFound();
            }
            return View(villa);
        }

        // GET: Villas/Create
        public ActionResult Create()
        {
            ViewBag.Projects = new SelectList(db.Projectes, "id", "Aname");
            ViewBag.DegreeOfExcellence = new SelectList(db.DegreeOfExcellences, "Id", "Aname");
            return View();
        }

        // POST: Villas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Villa villa, IEnumerable<HttpPostedFileBase> Files, HttpPostedFileBase Image)
        {
            ViewBag.Projects = new SelectList(db.Projectes, "id", "Aname");
            ViewBag.DegreeOfExcellence = new SelectList(db.DegreeOfExcellences, "Id", "Aname");
            if (ModelState.IsValid)
            {
                if (Image != null)
                {
                    villa.ImgUrl = SaveFile(Image, "Villas");
                }
                db.Villas.Add(villa);
                await db.SaveChangesAsync();

                List<VillaAttachment> Attachments = new List<VillaAttachment>();
                if (Files != null)
                {
                    foreach (var file in Files)
                    {
                        var path = SaveFile(file, "Villas");
                        VillaAttachment att = new VillaAttachment()
                        {
                            Path = path,
                            VillaId = villa.id
                        };
                        Attachments.Add(att);
                        db.VillaAttachments.Add(att);
                        db.SaveChanges();

                    }


                }

                return RedirectToAction("Index");
            }

            return View(villa);
        }

        // GET: Villas/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            ViewBag.Projects = new SelectList(db.Projectes, "id", "Aname");
            ViewBag.DegreeOfExcellence = new SelectList(db.DegreeOfExcellences, "Id", "Aname");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Villa villa = await db.Villas.FindAsync(id);
            if (villa == null)
            {
                return HttpNotFound();
            }
            return View(villa);
        }

        // POST: Villas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Villa villa, IEnumerable<HttpPostedFileBase> Files, HttpPostedFileBase Image)
        {
            ViewBag.Projects = new SelectList(db.Projectes, "id", "Aname");
            ViewBag.DegreeOfExcellence = new SelectList(db.DegreeOfExcellences, "Id", "Aname");
            if (ModelState.IsValid)
            {
                if (Image != null)
                {
                    System.IO.File.Delete(villa.ImgUrl);
                    villa.ImgUrl = SaveFile(Image, "Villas");
                }
                db.Entry(villa).State = EntityState.Modified;
                await db.SaveChangesAsync();

                List<VillaAttachment> Attachments = new List<VillaAttachment>();
                if (Files != null)
                {
                    var List = db.FlatAttachments.Where(x => x.FlatId == villa.id);
                    foreach (var item in List)
                    {
                        System.IO.File.Delete(item.Path);
                    }
                    foreach (var file in Files)
                    {
                        var path = SaveFile(file, "Villas");
                        VillaAttachment att = new VillaAttachment()
                        {
                            Path = path,
                            VillaId = villa.id
                        };
                        Attachments.Add(att);
                        db.VillaAttachments.Add(att);
                        db.SaveChanges();

                    }


                }
                return RedirectToAction("Index");
            }
            return View(villa);
        }

        // GET: Villas/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Villa villa = await db.Villas.FindAsync(id);
            if (villa == null)
            {
                return HttpNotFound();
            }
            return View(villa);
        }

        // POST: Villas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Villa villa = await db.Villas.FindAsync(id);
            db.Villas.Remove(villa);
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
