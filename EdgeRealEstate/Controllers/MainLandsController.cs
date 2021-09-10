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
using Kendo.Mvc.UI;
using EdgeRealEstate.Models.ViewModels;
using Kendo.Mvc.Extensions;

namespace EdgeRealEstate.Controllers
{
    [Authorize]

    public class MainLandsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: MainLands
        public async Task<ActionResult> Index()
        {
            var mainLands = db.MainLands;//.Include(m => m.Aname);//.Include(m => m.District);
            return View(await mainLands.ToListAsync());
        }
        public async Task<ActionResult> SearchProjects()
        {
            return View();
        }
        public JsonResult GetMainLands(string text)
        {
            //NorthwindDataContext northwind = new NorthwindDataContext();

            var dataContext = new ApplicationDbContext();

            var MainLands = dataContext.MainLands
                .Select(i => new MainLandDropVM
                {
                    Id = i.id,
                    ARName = i.Aname,
                });

            //if (!string.IsNullOrEmpty(text))
            //{
            //    MainLands = MainLands.Where(p => p.ARName.Contains(text));
            //}

            //return Json(MainLands, JsonRequestBehavior.AllowGet);
            return Json(MainLands, JsonRequestBehavior.AllowGet);

        }


        public ActionResult HierarchyBinding_MainProjects([DataSourceRequest] DataSourceRequest request, int? MainLand_Id)
        {

            if (MainLand_Id != null)
            {
                var res = db.MainProjects.Where(m => m.MainLandId == MainLand_Id).Select(x => new MainProjectGridVM
                {
                    id = x.id,
                    LandNo = x.LandNo,
                    Aname = x.Aname,
                    Ename = x.Ename,
                    MaxLevelForContributions = x.MaxLevelForContributions,
                    nots = x.nots,
                    TypesInvestment = x.TypesInvestment

                }).ToList();
                return Json(res.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);

            }
            var result = db.MainProjects.Select(x => new MainProjectGridVM
            {
                id = x.id,
                LandNo = x.LandNo,
                Aname = x.Aname,
                Ename = x.Ename,
                MaxLevelForContributions = x.MaxLevelForContributions,
                nots = x.nots,
                TypesInvestment = x.TypesInvestment

            }).ToList();
            return Json(result.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);

        }

        public ActionResult HierarchyBinding_Projects(int id, [DataSourceRequest] DataSourceRequest request)
        {           
            var result = db.Projectes.Where(i => i.MainProjectId == id).Select(x=> new ProjectGridVM{
                id = x.id,
                Aname =x.Aname,
                MaxLevelForContributions=x.MaxLevelForContributions,
                LandSize = x.LandSize,
                VellaNO=x.VellaNO,
                FlatNO=x.FlatNO,
                
            }).ToList();
            return Json(result.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }
        // GET: MainLands/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MainLand mainLand = await db.MainLands.FindAsync(id);
            if (mainLand == null)
            {
                return HttpNotFound();
            }
            return View(mainLand);
        }

        // GET: MainLands/Create
        public ActionResult Create()
        {
            ViewBag.Cities = new SelectList(db.Cities, "Id", "Aname");
            ViewBag.Districts = new SelectList(db.Districts, "Id", "Aname");
            ViewBag.Streets = new SelectList(db.Streets, "Id", "Aname");
            return View();
        }

        // POST: MainLands/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(MainLand mainLand, IEnumerable<HttpPostedFileBase> Files, HttpPostedFileBase Image)
        {
            if (ModelState.IsValid)
            {
                if (Image != null)
                {
                    mainLand.ImgURL = SaveFile(Image, "MainLands");
                }
                db.MainLands.Add(mainLand);

                await db.SaveChangesAsync();

                int MainLandID = mainLand.id;

                List<MainLandAttachment> Attachments = new List<MainLandAttachment>();
                if (Files != null)
                {
                    foreach (var file in Files)
                    {
                        var path = SaveFile(file, "MainLands");
                        MainLandAttachment att = new MainLandAttachment()
                        {
                            Path = path,
                            MainLandId = MainLandID
                        };
                        Attachments.Add(att);
                        db.MainLandAttachments.Add(att);
                        db.SaveChanges();

                    }


                }
                return RedirectToAction("Index");
            }

            ViewBag.Cities = new SelectList(db.Cities, "Id", "Aname");
            ViewBag.Districts = new SelectList(db.Districts, "Id", "Aname");
            ViewBag.Streets = new SelectList(db.Streets, "Id", "Aname");

            return View(mainLand);
        }

        // GET: MainLands/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MainLand mainLand = await db.MainLands.FindAsync(id);
            if (mainLand == null)
            {
                return HttpNotFound();
            }
            ViewBag.Cities = new SelectList(db.Cities, "Id", "Aname");
            ViewBag.Districts = new SelectList(db.Districts, "Id", "Aname");
            ViewBag.Streets = new SelectList(db.Streets, "Id", "Aname");
            return View(mainLand);
        }

        // POST: MainLands/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(MainLand mainLand, IEnumerable<HttpPostedFileBase> Files, HttpPostedFileBase Image)
        {
            if (ModelState.IsValid)
            {

                if (Image != null)
                {
                    System.IO.File.Delete(mainLand.ImgURL);
                    mainLand.ImgURL = SaveFile(Image, "MainLands");
                }
                db.Entry(mainLand).State = EntityState.Modified;
                await db.SaveChangesAsync();

                List<MainLandAttachment> Attachments = new List<MainLandAttachment>();
                if (Files != null)
                {
                    var List = db.MainLandAttachments.Where(x=>x.MainLandId == mainLand.id);
                    foreach (var item in List)
                    {
                        System.IO.File.Delete(item.Path);
                    }
                    foreach (var file in Files)
                    {
                        var path = SaveFile(file, "MainLands");
                        MainLandAttachment att = new MainLandAttachment()
                        {
                            Path = path,
                            MainLandId = mainLand.id
                        };
                        Attachments.Add(att);
                        db.MainLandAttachments.Add(att);
                        db.SaveChanges();

                    }


                }
                return RedirectToAction("Index");
            }
            ViewBag.Cities = new SelectList(db.Cities, "Id", "Aname");
            ViewBag.Districts = new SelectList(db.Districts, "Id", "Aname");
            ViewBag.Streets = new SelectList(db.Streets, "Id", "Aname");

            return View(mainLand);
        }

        // GET: MainLands/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MainLand mainLand = await db.MainLands.FindAsync(id);
            if (mainLand == null)
            {
                return HttpNotFound();
            }
            return View(mainLand);
        }

        // POST: MainLands/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            MainLand mainLand = await db.MainLands.FindAsync(id);
            db.MainLands.Remove(mainLand);
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
