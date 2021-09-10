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
using EdgeRealEstate.Models.ViewModels;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using System.IO;

namespace EdgeRealEstate.Controllers
{
    [Authorize]

    public class ProjectsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();


        // GET: Projects
        //public async Task<ActionResult> Index()
        //{
        //    ViewBag.MainProjects = db.MainProjects.ToList();

        //    return View(await db.Projectes.ToListAsync());
        //}
        public ActionResult EditingCustom_Read([DataSourceRequest] DataSourceRequest request)
        {
            return Json(/*BillSalesDetailsService.Read().ToDataSourceResult(*/request/*)*/);
        }
        public async Task<ActionResult> Index()
        {
            ViewBag.MainProjects = new SelectList(db.MainProjects, "id", "Aname");
            PopulateStages();
            return View();
        }

        public ActionResult Projects_Read([DataSourceRequest]DataSourceRequest request)
        {
            PopulateStages();

            ViewBag.MainProjects = new SelectList(db.MainProjects, "id", "Aname");

            IQueryable<ProjectVM> RList = db.Projectes.Select(x=> new ProjectVM {

                ID = x.id,
                Ename = x.Ename,
                Aname = x.Aname,
                AdminstrationLevelNO = x.AdminstrationLevelNO,
                attachedFileAndPic = x.attachedFileAndPic,
                BeginDateAcutely = x.BeginDateAcutely,
                EndDateAcutely = x.EndDateAcutely,
                BeginDateExpected = x.BeginDateExpected,
                EndDateExpected = x.EndDateExpected,
                FlatNO = x.FlatNO,
                ImgURL = x.ImgURL,
                IsExtand = x.IsExtand,
                LandNo = x.LandNo,
                LandSize = x.LandSize,
                LevelNO = x.LevelNO,
                MainProjectId = x.MainProjectId,
                ProjectDescriptionID = x.ProjectDescriptionID,
                MaxLevelForContributions = x.MaxLevelForContributions,
                nots = x.nots,
                RoveNO = x.RoveNO,
                ShopNO = x.ShopNO,
                VellaNO = x.VellaNO

            });
            DataSourceResult result = RList.ToDataSourceResult(request);

            return Json(result);
        }

        public ActionResult Projects_Update([DataSourceRequest]DataSourceRequest request, ProjectVM Project/*, HttpPostedFileBase Image, IEnumerable<HttpPostedFileBase> Files*/)
        {
            Projects model = db.Projectes.Find(Project.ID);

            model.Ename = Project.Ename;
            model.Aname = Project.Aname;
            model.AdminstrationLevelNO = Project.AdminstrationLevelNO;
            model.attachedFileAndPic = Project.attachedFileAndPic;
            model.BeginDateAcutely = Project.BeginDateAcutely;
            model.EndDateAcutely = Project.EndDateAcutely;
            model.BeginDateExpected = Project.BeginDateExpected;
            model.EndDateExpected = Project.EndDateExpected;
            model.FlatNO = Project.FlatNO;
            model.ImgURL = Project.ImgURL;
            model.IsExtand = Project.IsExtand;
            model.LandNo = Project.LandNo;
            model.LandSize = Project.LandSize;
            model.LevelNO = Project.LevelNO;
            model.MainProjectId = Project.MainProjectId;
            model.ProjectDescriptionID = Project.ProjectDescriptionID;
            model.MaxLevelForContributions = Project.MaxLevelForContributions;
            model.nots = Project.nots;
            model.RoveNO = Project.RoveNO;
            model.ShopNO = Project.ShopNO;
            model.VellaNO = Project.VellaNO;

            db.SaveChanges();

            return Json(new[] { Project }.ToDataSourceResult(request));

        }
        public ActionResult Projects_Destroy([DataSourceRequest]DataSourceRequest request, ProjectVM Project)
        {
            var entity = new Projects();

            entity.id = Project.ID;


            var ProjectsStage = db.ProjectStages.Where(i => i.ProjectID == entity.id);

            foreach (var item in ProjectsStage)
            {
                db.ProjectStages.Remove(item);
            }

            var ProjectsAttachments = db.ProjectsAttachment.Where(i => i.ProjectsId == entity.id);

            foreach (var item in ProjectsAttachments)
            {
                db.ProjectsAttachment.Remove(item);
            }


            db.Projectes.Attach(entity);

            db.Projectes.Remove(entity);
            db.SaveChanges();

            return Json(new[] { Project }.ToDataSourceResult(request));

        }

        public ActionResult Stages_Read([DataSourceRequest]DataSourceRequest request, int ID)
        {
            var result = db.ProjectStages.Where(x => x.ProjectID == ID).Select(i=> new StageGrid {

                ID = i.ID,
                Duration = i.Duration,
                StartDate = i.StartDate,
                EndDate = i.EndDate,
                StageID = i.StageID


            }).ToList();



            return Json(result.ToDataSourceResult(request));

        }
        public ActionResult Stages_Update([DataSourceRequest]DataSourceRequest request, int StageID, int ID, string StartDate, string EndDate,string Duration)
        {
            ProjectStage modelindb = db.ProjectStages.Find(ID);

            modelindb.StageID = StageID;
            modelindb.StartDate = StartDate;
            modelindb.EndDate = EndDate;
            modelindb.Duration = Duration;

            db.SaveChanges();

            return Json(new[] { ID }.ToDataSourceResult(request));

        }

        public ActionResult Stages_Destroy([DataSourceRequest]DataSourceRequest request, StageGrid Stage, int ID)
        {
            var entity = new ProjectStage();

            entity.ID = ID;

            db.ProjectStages.Attach(entity);

            db.ProjectStages.Remove(entity);


            db.SaveChanges();

            return Json(new[] { Stage }.ToDataSourceResult(request));

        }


        public async Task<ActionResult> Create()
        {
            ViewBag.MainProjects = new SelectList(db.MainProjects, "id", "Aname");

            PopulateStages();
            ProjectStageMaster model = new ProjectStageMaster();
            return View(model);
        }
        public ActionResult Save(ProjectStageMaster model , IEnumerable<HttpPostedFileBase> Files, HttpPostedFileBase Image)
        {
            

            Projects project = new Projects() {

                Ename = model.Project.Ename,
                Aname = model.Project.Aname,
                AdminstrationLevelNO = model.Project.AdminstrationLevelNO,
                attachedFileAndPic = model.Project.attachedFileAndPic,
                BeginDateAcutely = model.Project.BeginDateAcutely,
                EndDateAcutely = model.Project.EndDateAcutely,
                BeginDateExpected = model.Project.BeginDateExpected,
                EndDateExpected = model.Project.EndDateExpected,
                FlatNO = model.Project.FlatNO,
                ImgURL = model.Project.ImgURL,
                IsExtand = model.Project.IsExtand,
                LandNo = model.Project.LandNo,
                LandSize = model.Project.LandSize,
                LevelNO = model.Project.LevelNO,
                MainProjectId = model.Project.MainProjectId,
                ProjectDescriptionID = model.Project.ProjectDescriptionID,
                MaxLevelForContributions = model.Project.MaxLevelForContributions,
                nots = model.Project.nots,
                RoveNO = model.Project.RoveNO,
                ShopNO = model.Project.ShopNO,
                VellaNO = model.Project.VellaNO

            };
            if (Image != null)
            {
                project.ImgURL = SaveFile(Image, "Projects");
            }
            db.Projectes.Add(project);
            db.SaveChanges();

            int ProID = project.id;

            foreach (var item in model.Stages)
            {
                ProjectStage mo = new ProjectStage();

                mo.Duration = item.Duration;
                mo.EndDate = item.EndDate;
                mo.StartDate = item.StartDate;
                mo.ProjectID = ProID;
                mo.StageID = item.StageID;

                db.ProjectStages.Add(mo);
                db.SaveChanges();
            }

            List<ProjectsAttachment> Attachments = new List<ProjectsAttachment>();
            if (Files != null)
            {
                foreach (var file in Files)
                {
                    var rondom = Guid.NewGuid() + Path.GetFileName(file.FileName);
                    var path = Path.Combine(Server.MapPath("~/Uploads/Projects"), rondom);

                    file.SaveAs(path);

                    ProjectsAttachment att = new ProjectsAttachment()
                    {
                        Path = path,
                        ProjectsId = project.id
                    };
                    Attachments.Add(att);
                    db.ProjectsAttachment.Add(att);
                    db.SaveChanges();

                }


            }
            return RedirectToAction("Index");
        }
        private void PopulateStages()
        {
            var dataContext = new ApplicationDbContext();
            var mainitems = dataContext.Stages.ToList();

            List<StageListVM> list = new List<StageListVM>();

            foreach (var item in mainitems)
            {
                StageListVM model = new StageListVM();

                model.StageID = item.id;

                model.StageName = item.Ename;

                list.Add(model);
            }

            var categories = dataContext.Stages
                        .Select(c => new StageListVM
                        {
                            StageID = c.id,
                            StageName = c.Ename
                        })
                        .OrderBy(e => e.StageName);

            ViewData["Stages"] = categories;
            ViewData["defaultStage"] = categories.First();
        }


        /////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////



        // GET: Projects/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Projects projects = await db.Projectes.FindAsync(id);
            if (projects == null)
            {
                return HttpNotFound();
            }
            return View(projects);
        }

        // GET: Projects/Create
        //public ActionResult Create()
        //{
        //    ViewBag.MainProjects = db.MainProjects.ToList();
        //    return View();
        //}

        // POST: Projects/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> Create([Bind(Include = "id,MainProjectid,Aname,Ename,LandNo,LandSize,attachedFileAndPic,MaxLevelForContributions,ImgURL,BeginDateAcutely,BeginDateExpected,EndDateAcutely,EndDateExpected,IsExtand,FlatNO,RoveNO,ShopNO,LevelNO,VellaNO,AdminstrationLevelNO,ProjectDescriptionID,nots")] Projects projects)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Projectes.Add(projects);
        //        await db.SaveChangesAsync();
        //        return RedirectToAction("Index");
        //    }

        //    return View(projects);
        //}

        // GET: Projects/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            PopulateStages();

            ViewBag.MainProjects = new SelectList(db.MainProjects, "id", "Ename");

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Projects projects = await db.Projectes.FindAsync(id);
            if (projects == null)
            {
                return HttpNotFound();
            }
            return View(projects);
        }

        // POST: Projects/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "id,MainProjectid,Aname,Ename,LandNo,LandSize,attachedFileAndPic,MaxLevelForContributions,ImgURL,BeginDateAcutely,BeginDateExpected,EndDateAcutely,EndDateExpected,IsExtand,FlatNO,RoveNO,ShopNO,LevelNO,VellaNO,AdminstrationLevelNO,ProjectDescriptionID,nots")] Projects projects)
        {
            ViewBag.MainProjects = new SelectList(db.MainProjects, "id", "Ename");

            if (ModelState.IsValid)
            {
                db.Entry(projects).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(projects);
        }

        // GET: Projects/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Projects projects = await db.Projectes.FindAsync(id);
            if (projects == null)
            {
                return HttpNotFound();
            }
            return View(projects);
        }

        // POST: Projects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Projects projects = await db.Projectes.FindAsync(id);
            db.Projectes.Remove(projects);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }


        public string SaveFile(HttpPostedFileBase File, string Folder)
        {

            var rondom = Guid.NewGuid() + Path.GetFileName(File.FileName);
            var path = Path.Combine(Server.MapPath("~/Uploads/"+ Folder), rondom);
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
