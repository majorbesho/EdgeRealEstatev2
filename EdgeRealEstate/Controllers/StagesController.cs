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
using EdgeRealEstate.Models.Services;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using EdgeRealEstate.Models.ViewModels;

namespace EdgeRealEstate.Controllers
{
    [Authorize]

    public class StagesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();


        public ActionResult Create()
        {
            ViewBag.Stages = new SelectList(db.Stages, "id", "Aname");

            PopulateCategories();
            StageMainItemMasterVM model = new StageMainItemMasterVM();
            return View(model);
        }
        public ActionResult Save(StageMainItemMasterVM model)
        {
            StageMaster mo = new StageMaster()
            {
                Cost = model.Stage.Cost,
                Duration = model.Stage.Duration,
                StageId = model.Stage.StageId
            };
            db.StageMasters.Add(mo);
            db.SaveChanges();

            int StageMasterID = mo.ID;

            foreach (var item in model.Items)
            {
                MainItemDetail Master = new MainItemDetail()
                {
                    Cost = item.Cost,
                    Duration = item.Duration,
                    MainItemID = item.MainID,
                    StageMasterID = StageMasterID
                };
                db.MainItemDetails.Add(Master);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        public ActionResult Index()
        {
            ViewBag.Stages = new SelectList(db.Stages, "id", "Ename");
            PopulateCategories();
            return View();
        }

        public ActionResult Stages_Read([DataSourceRequest]DataSourceRequest request)
        {
            //ViewBag.MainProjects = new SelectList(db.MainProjects, "id", "Ename");

            IQueryable<Stage_VM> RList = db.StageMasters.Select(x => new Stage_VM
            {

                ID = x.ID,
                StageId = x.StageId,
                StageName = x.Stage.Aname,
                Cost = x.Cost,
                Duration = x.Duration
               
            });
            DataSourceResult result = RList.ToDataSourceResult(request);

            return Json(result);
        }

        public ActionResult Stages_Update([DataSourceRequest]DataSourceRequest request, Stage_VM Stage)
        {

            StageMaster model = db.StageMasters.Find(Stage.ID);


            model.StageId = Stage.StageId;
            model.Cost = Stage.Cost;
            model.Duration = Stage.Duration;

            db.SaveChanges();

            return Json(new[] { Stage }.ToDataSourceResult(request));

        }
        public ActionResult Stages_Destroy([DataSourceRequest]DataSourceRequest request, Stage_VM Stage)
        {
            var entity = new StageMaster();

            entity.ID = Stage.ID;

            db.StageMasters.Attach(entity);

            db.StageMasters.Remove(entity);

            var StageWithItems = db.MainItemDetails.Where(i => i.StageMasterID == entity.ID);

            foreach (var item in StageWithItems)
            {
                db.MainItemDetails.Remove(item);
            }

            db.SaveChanges();

            return Json(new[] { Stage }.ToDataSourceResult(request));

        }

        public ActionResult Items_Read([DataSourceRequest]DataSourceRequest request, int ID)
        {
            ViewBag.Stages = new SelectList(db.Stages, "id", "Ename");

            var result = db.MainItemDetails.Where(x => x.StageMasterID == ID).Select(i => new ItemVM
            {

                ID = i.Id,
                Duration = i.Duration,
                Cost = i.Cost,
                MainID = i.MainItemID


            }).ToList();



            return Json(result.ToDataSourceResult(request));

        }
        public ActionResult Items_Update([DataSourceRequest]DataSourceRequest request, int MainID, int ID, string Duration, string Cost)
        {
            MainItemDetail modelindb = db.MainItemDetails.Find(ID);

            modelindb.MainItemID = MainID;
            modelindb.Cost = Cost;
            modelindb.Duration = Duration;

            db.SaveChanges();

            return Json(new[] { ID }.ToDataSourceResult(request));

        }

        public ActionResult Items_Destroy([DataSourceRequest]DataSourceRequest request, ItemVM Item, int ID)
        {
            var entity = new MainItemDetail();

            entity.Id = ID;

            db.MainItemDetails.Attach(entity);

            db.MainItemDetails.Remove(entity);


            db.SaveChanges();

            return Json(new[] { Item }.ToDataSourceResult(request));

        }

        public ActionResult MasterOld()
        {
            PopulateCategories();

            return View();
        }

        private void PopulateCategories()
        {
            var dataContext = new ApplicationDbContext();
            var mainitems = dataContext.MianItemss.ToList();

            List<MainItemViewModel> list = new List<MainItemViewModel>();

            foreach (var item in mainitems)
            {
                MainItemViewModel model = new MainItemViewModel();
                model.ID = item.Id;
                model.Name = item.Ename;

                list.Add(model);
            }

            var categories = dataContext.MianItemss
                        .Select(c => new MainItemViewModel
                        {
                            ID = c.Id,
                            Name = c.Ename
                        })
                        .OrderBy(e => e.Name);

            ViewData["categories"] = categories;
            ViewData["defaultCategory"] = categories.First();
        }

        public async Task<ActionResult> Edit(int? ID)
        {
            ViewBag.Stages = new SelectList(db.Stages, "id", "Ename");

            PopulateCategories();
            if (ID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StageMaster stage = await db.StageMasters.FindAsync(ID);
            if (stage == null)
            {
                return HttpNotFound();
            }
            return View(stage);
        }

        // POST: Stages1/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(StageMaster stage)
        {
            if (ModelState.IsValid)
            {
                db.Entry(stage).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(stage);
        }



        //private StageMainItem stageMainItem;
        //public StagesController()
        //{
        //    this.stageMainItem = new StageMainItem(db);
        //}
        //GET: Stages1/Create

        [HttpGet]
        public async Task<ActionResult> CreateStage()
        {

            return View();
        }
        //POST: Stages1/Create
        //To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateStage(Stage stage)
        {
            if (ModelState.IsValid)
            {
                db.Stages.Add(stage);
                await db.SaveChangesAsync();
                //return RedirectToAction("Index");
                return View();
            }

            return View(stage);
        }


        //// GET: Stages1/Delete/5
        //public async Task<ActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Stage stage = await db.Stages.FindAsync(id);
        //    if (stage == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(stage);
        //}

        //// POST: Stages1/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> DeleteConfirmed(int id)
        //{
        //    Stage stage = await db.Stages.FindAsync(id);
        //    db.Stages.Remove(stage);
        //    await db.SaveChangesAsync();
        //    return RedirectToAction("Index");
        //}

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}

        //public ActionResult EditingCustom_Read([DataSourceRequest] DataSourceRequest request)
        //{
        //    return Json(stageMainItem.Read().ToDataSourceResult(request)/*, JsonRequestBehavior.AllowGet*/);
        //}

        //[AcceptVerbs(HttpVerbs.Post)]
        //public ActionResult EditingCustom_Update([DataSourceRequest] DataSourceRequest request,
        //    [Bind(Prefix = "models")]IEnumerable<StageVM> products)
        //{
        //    if (products != null && ModelState.IsValid)
        //    {
        //        foreach (var product in products)
        //        {
        //            stageMainItem.Update(product);
        //        }
        //    }

        //    return Json(products.ToDataSourceResult(request, ModelState));
        //}

        //[AcceptVerbs(HttpVerbs.Post)]
        //public ActionResult EditingCustom_Create([DataSourceRequest] DataSourceRequest request,
        //    [Bind(Prefix = "models")]IEnumerable<StageVM> products)
        //{
        //    var results = new List<StageVM>();

        //    if (products != null && ModelState.IsValid)
        //    {
        //        foreach (var product in products)
        //        {
        //            stageMainItem.Create(product);

        //            results.Add(product);
        //        }
        //    }

        //    return Json(results.ToDataSourceResult(request, ModelState));
        //}

        //[AcceptVerbs(HttpVerbs.Post)]
        //public ActionResult EditingCustom_Destroy([DataSourceRequest] DataSourceRequest request,
        //    [Bind(Prefix = "models")]IEnumerable<StageVM> products)
        //{
        //    foreach (var product in products)
        //    {
        //        stageMainItem.Destroy(product);
        //    }

        //    return Json(products.ToDataSourceResult(request, ModelState));
        //}








    }
}
