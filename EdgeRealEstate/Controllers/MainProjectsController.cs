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

    public class MainProjectsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: MainProjects
        public async Task<ActionResult> Index()
        {
            return View(await db.MainProjects.ToListAsync());
        }

        // GET: MainProjects/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MainProject mainProject = await db.MainProjects.FindAsync(id);
            if (mainProject == null)
            {
                return HttpNotFound();
            }
            return View(mainProject);
        }

        // GET: MainProjects/Create
        public ActionResult Create()
        {
            ViewBag.MainLands = new SelectList(db.MainLands, "id", "Aname");
            return View();
        }

        // POST: MainProjects/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(MainProject mainProject, IEnumerable<HttpPostedFileBase> Files, HttpPostedFileBase Image)
        {
            ViewBag.MainLands = new SelectList(db.Cities, "id", "Aname");

            if (ModelState.IsValid)
            {
                if (Image != null)
                {
                    mainProject.ImgURL = SaveFile(Image, "MainProjects");
                }
                db.MainProjects.Add(mainProject);
                await db.SaveChangesAsync();


                List<MainProjectAttachment> Attachments = new List<MainProjectAttachment>();
                if (Files != null)
                {
                    foreach (var file in Files)
                    {
                        var path = SaveFile(file, "MainProjects");
                        MainProjectAttachment att = new MainProjectAttachment()
                        {
                            Path = path,
                            MainProjectId = mainProject.id
                        };
                        Attachments.Add(att);
                        db.MainProjectAttachments.Add(att);
                        db.SaveChanges();

                    }


                }
                return RedirectToAction("Index");
            }

            return View(mainProject);
        }

        // GET: MainProjects/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            ViewBag.MainLands = new SelectList(db.Cities, "id", "Aname");

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MainProject mainProject = await db.MainProjects.FindAsync(id);
            if (mainProject == null)
            {
                return HttpNotFound();
            }
            return View(mainProject);
        }

        // POST: MainProjects/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(MainProject mainProject, IEnumerable<HttpPostedFileBase> Files, HttpPostedFileBase Image)
        {
            ViewBag.MainLands = new SelectList(db.Cities, "id", "Aname");

            if (ModelState.IsValid)
            {
                if (Image != null)
                {
                    System.IO.File.Delete(mainProject.ImgURL);
                    mainProject.ImgURL = SaveFile(Image, "MainProjects");
                }
                db.Entry(mainProject).State = EntityState.Modified;
                await db.SaveChangesAsync();

                List<MainProjectAttachment> Attachments = new List<MainProjectAttachment>();
                if (Files != null)
                {
                    var List = db.MainProjectAttachments.Where(x => x.MainProjectId == mainProject.id);
                    foreach (var item in List)
                    {
                        System.IO.File.Delete(item.Path);
                    }
                    foreach (var file in Files)
                    {
                        var path = SaveFile(file, "MainProjects");
                        MainProjectAttachment att = new MainProjectAttachment()
                        {
                            Path = path,
                            MainProjectId = mainProject.id
                        };
                        Attachments.Add(att);
                        db.MainProjectAttachments.Add(att);
                        db.SaveChanges();

                    }


                }

                return RedirectToAction("Index");
            }
            return View(mainProject);
        }

        // GET: MainProjects/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MainProject mainProject = await db.MainProjects.FindAsync(id);
            if (mainProject == null)
            {
                return HttpNotFound();
            }
            return View(mainProject);
        }

        // POST: MainProjects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            MainProject mainProject = await db.MainProjects.FindAsync(id);
            db.MainProjects.Remove(mainProject);
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
