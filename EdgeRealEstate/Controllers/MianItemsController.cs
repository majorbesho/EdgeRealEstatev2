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
using Newtonsoft.Json;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using System.IO;

namespace EdgeRealEstate.Controllers
{
    [Authorize]

    public class MianItemsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();


        public ActionResult EditingCustom_Read([DataSourceRequest] DataSourceRequest request)
        {
            return Json(request);
        }
        public ActionResult EditingCustom_Create([DataSourceRequest] DataSourceRequest request)
        {
            return Json(request);
        }
        public ActionResult EditingCustom_Update([DataSourceRequest] DataSourceRequest request)
        {
            return Json(request);
        }
        public ActionResult EditingCustom_Destroy([DataSourceRequest] DataSourceRequest request)
        {
            return Json(request);
        }
        /// ////////////////////////////////////////////////////////////////
        /// ///////////////////////////////////////////////////////////////
        /// ///////////////////////////////////////////////////////////////
        ///        
        public async Task<ActionResult> Index()
    {
        
        PopulateStages();
        return View();
    }

    public ActionResult MainItems_Read([DataSourceRequest]DataSourceRequest request)
    {
        PopulateStages();

        ViewBag.MainProjects = new SelectList(db.MainProjects, "id", "Ename");

        IQueryable<MItemVM> RList = db.MianItemss.Select(x => new MItemVM
        {

            Id = x.Id,
            Ename = x.Ename,
            Aname = x.Aname,
            AttachedId = x.AttachedId,
            DegreeOfExcellenceId = x.DegreeOfExcellenceId,
            Description = x.Description,
            HaveItem = x.HaveItem,
            NoOfDaies = x.NoOfDaies,
            Nots = x.Nots

        });
        DataSourceResult result = RList.ToDataSourceResult(request);

        return Json(result);
    }

    public ActionResult MainItems_Update([DataSourceRequest]DataSourceRequest request, MItemVM MainItem/*, HttpPostedFileBase Image, IEnumerable<HttpPostedFileBase> Files*/)
    {
        MianItems model = db.MianItemss.Find(MainItem.Id);

        model.Ename = MainItem.Ename;
        model.Aname = MainItem.Aname;
        model.AttachedId = MainItem.AttachedId;
        model.DegreeOfExcellenceId = MainItem.DegreeOfExcellenceId;
        model.Description = MainItem.Description;
        model.HaveItem = MainItem.HaveItem;
        model.NoOfDaies = MainItem.NoOfDaies;
        model.Nots = MainItem.Nots;

        db.SaveChanges();

        return Json(new[] { MainItem }.ToDataSourceResult(request));

    }
    public ActionResult MainItems_Destroy([DataSourceRequest]DataSourceRequest request, MItemVM MainItem)
    {
        var entity = new MianItems();

        entity.Id = MainItem.Id;


        var Materials = db.MaterialDetails.Where(i => i.MianItemsId == entity.Id);
        List<PricesOffersDetails> list = new List<PricesOffersDetails>();
        foreach (var item in Materials)
        {
            List<PricesOffersDetails> PricesOffers = db.PricesOffersDetails.Where(i => i.ConstructionMaterialId == item.ConstructionMaterialId).ToList();

            foreach (var Price in PricesOffers)
            {
                    list.Add(Price);

            }

            db.MaterialDetails.Remove(item);
        }
        foreach (var item in list)
        {

            db.PricesOffersDetails.Remove(item);
        }
        foreach (var item in Materials)
        {

            db.MaterialDetails.Remove(item);
        }
        db.MianItemss.Attach(entity);
        db.MianItemss.Remove(entity);
        db.SaveChanges();

        return Json(new[] { MainItem }.ToDataSourceResult(request));

    }

    public ActionResult Materials_Read([DataSourceRequest]DataSourceRequest request, int ID)
    {
        var result = db.ConstructionMaterials/*.Where(x => x.MianItemsId == ID)*/.Select(i => new MaterialVM
        {

            ID = i.ID,
            Brand = i.Brand,
            effective = i.effective,
            Image = i.Image,
            Length = i.Length,
            MasuringUnit = i.MasuringUnit,
            Specifications = i.Specifications,
            MaterialName = i.MaterialName,
            Notes = i.Notes,
            Price = i.Price,
            ProcessingTime = i.ProcessingTime,
            Quantity = i.Quantity,
            Total = i.Total,
            Width = i.Width,
            Type = i.Type


        }).ToList();



        return Json(result.ToDataSourceResult(request));

    }
    public ActionResult Materials_Update([DataSourceRequest]DataSourceRequest request, int ID, string Brand, bool effective, string Image, string Length, string MasuringUnit, string MaterialName, string Notes, string ProcessingTime, decimal Price, int Quantity, string Specifications, int Total, string Type, string Width)
    {
        ConstructionMaterial modelindb = db.ConstructionMaterials.Find(ID);
        
        modelindb.Brand = Brand;
        modelindb.effective = effective;
        modelindb.Image = Image;
        modelindb.Length = Length;
        modelindb.MasuringUnit = MasuringUnit;
        modelindb.MaterialName = MaterialName;
        modelindb.Notes = Notes;
        modelindb.ProcessingTime = ProcessingTime;
        modelindb.Price = Price;
        modelindb.Quantity = Quantity;
        modelindb.Specifications = Specifications;
        modelindb.Total = Total;
        modelindb.Type = Type;
        modelindb.Width = Width;

        db.SaveChanges();

        return Json(new[] { ID }.ToDataSourceResult(request));

    }

    public ActionResult Materials_Destroy([DataSourceRequest]DataSourceRequest request, MaterialVM M, int ID)
    {
        var entity = new ConstructionMaterial();

        entity.ID = ID;

        db.ConstructionMaterials.Attach(entity);

        db.ConstructionMaterials.Remove(entity);


        db.SaveChanges();

        return Json(new[] { M }.ToDataSourceResult(request));

    }

    public ActionResult SaveImage(HttpPostedFileBase file)
    {
        var fileName = Path.GetFileName(file.FileName) + Guid.NewGuid();
        var physicalPath = Path.Combine(Server.MapPath("~/Uploads/Materials"), fileName);

        file.SaveAs(physicalPath);

        return Json(new { Image = fileName }, "text/plain");
    }

    public ActionResult SaveFiles(IEnumerable<HttpPostedFileBase> Files)
    {
        List<string> Names = new List<string>();

        foreach (var item in Files)
        {
            var fileName = Path.GetFileName(item.FileName) + Guid.NewGuid();
            var physicalPath = Path.Combine(Server.MapPath("~/Uploads/Materials"), fileName);

            Names.Add(fileName);
            item.SaveAs(physicalPath);
        }

        return Json(new { Files = Names }, "text/plain");
    }
        public async Task<ActionResult> Create()
    {
        ViewBag.DegreeOfExe = new SelectList(db.DegreeOfExcellences, "Id", "Ename");
            PopulateMaterials();
            MainItemMaterialMaster model = new MainItemMaterialMaster();
        return View(model);
    }
    public ActionResult Save(MainItemMaterialMaster model, IEnumerable<HttpPostedFileBase> Files
        )
    {
        

        MianItems MainItem = new MianItems()
        {
            Ename = model.MainItem.Ename,
            Aname = model.MainItem.Aname,
            AttachedId = model.MainItem.AttachedId,
            DegreeOfExcellenceId = model.MainItem.DegreeOfExcellenceId,
            Description = model.MainItem.Description,
            HaveItem = model.MainItem.HaveItem,
            NoOfDaies = model.MainItem.NoOfDaies,
            Nots = model.MainItem.Nots
        };

        db.MianItemss.Add(MainItem);
        db.SaveChanges();

        int MID = MainItem.Id;
        List<MianItemsAttachment> Attachments = new List<MianItemsAttachment>();
        if (Files != null)
        {
            foreach (var file in Files)
            {
                var path = SaveFile(file, "MainItems");
                MianItemsAttachment att = new MianItemsAttachment()
                {
                    Path = path,
                    MianItemsId = MID
                };
                Attachments.Add(att);
                db.MianItemsAttachments.Add(att);
                db.SaveChanges();

            }


                }
        foreach (var item in model.MaterialsDetails)
        {
            MaterialDetail mo = new MaterialDetail();
            mo.Quantity = item.Quantity;
            mo.price = item.price;
            mo.MianItemsId = MID;
            mo.ConstructionMaterialId = item.MaterialID;

            db.MaterialDetails.Add(mo);
            db.SaveChanges();
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
    public string SaveFile(HttpPostedFileBase File, string Folder)
    {

        var rondom = Guid.NewGuid() + Path.GetFileName(File.FileName);
        var path = Path.Combine(Server.MapPath("~/Uploads/" + Folder), rondom);
        File.SaveAs(path);
        return path;

    }

        /////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////
        /// <returns></returns>
        // GET: MianItems
        //public async Task<ActionResult> Index()
        //    {
        //        //return View(await db.MianItemss.Include(x=>x.ConstructionMaterials).ToListAsync());
        //        return View(await db.MianItemss.Include(x=>x.ConstructionMaterials).ToListAsync());

        //    }

        // GET: MianItems/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MianItems mianItems = await db.MianItemss.FindAsync(id);
            if (mianItems == null)
            {
                return HttpNotFound();
            }
            return View(mianItems);
        }

        // GET: MianItems/Create
        //public ActionResult Create()
        //{
        //    //var ConstructionMaterials = db.ConstructionMaterials.ToList();

        //    //MainItmsVM model = new MainItmsVM
        //    //{
        //    //    SelectedMatrials = ConstructionMaterials.Select(v=> new CheckBoxItem()
        //    //    {
        //    //        ID = v.ID,
        //    //        Name = v.MaterialName
        //    //    }
                
        //    //    ).ToList()
        //    //};

        //    ViewBag.Matrials = db.ConstructionMaterials.ToList();

        //    return View();
        //}
        public ActionResult MasterDetails2()
        {
            return View();
        }

        public ActionResult MasterDetails()
        {
            return View(db.MianItemss/*.Include(x => x.ConstructionMaterials)*/.ToList());

        }

        public ActionResult SaveItem(string Ename, string Aname, string Description, int NoOfDaies, int DegreeOfExcellenceId, string AttachedId, string Nots, bool HaveItem, ConstructionMaterial[] ConMat)
        {
            string result = "Add Error!";
            if (Ename != null && Aname != null && Description != null 
                && NoOfDaies != null && DegreeOfExcellenceId != null && AttachedId != null 
                && Nots != null && HaveItem != null && ConMat != null)
            {
                MianItems model = new MianItems();
                //model.Id = 45;
                model.Aname = Aname;
                model.Ename = Ename;
                model.Description = Description;
                model.NoOfDaies = NoOfDaies;
                model.DegreeOfExcellenceId = DegreeOfExcellenceId;
                model.AttachedId = AttachedId;
                model.Nots = Nots;
                model.HaveItem = HaveItem;
                model.NoOfDaies = NoOfDaies;
                db.MianItemss.Add(model);

                foreach (var item in ConMat)
                {
                    ConstructionMaterial C = new ConstructionMaterial();
                    C.MaterialName = item.MaterialName;
                    C.MasuringUnit = item.MasuringUnit;
                    C.Price = item.Price;
                    C.Brand = item.Brand;
                    C.effective = item.effective;
                    C.Length = item.Length;
                    C.Width = item.Width;
                    C.Type = item.Type;
                    C.ProcessingTime = item.ProcessingTime;
                    C.Specifications = item.Specifications;
                    C.Notes = item.Notes;
                    //C.MianItemsId = model.Id;
                    db.ConstructionMaterials.Add(C);
                }
                db.SaveChanges();
                result = "Success! Main Item Added!";
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult AddNewCM(int MainItemID, string MaterialName, string MasuringUnit, string Length, string Width, decimal Price, string Brand, string ProcessingTime, string Specifications, string Type, string Notes, bool effective)
        {
            bool result = false;

                ConstructionMaterial C = new ConstructionMaterial();

                    C.MaterialName = MaterialName;
                    C.MasuringUnit = MasuringUnit;
                    C.Price = Price;
                    C.Brand = Brand;
                    C.effective = effective;
                    C.Length = Length;
                    C.Width = Width;
                    C.Type = Type;
                    C.ProcessingTime = ProcessingTime;
                    C.Specifications = Specifications;
                    C.Notes = Notes;
                    //C.MianItemsId = MainItemID;

                    db.ConstructionMaterials.Add(C);

                    db.SaveChanges();

                    result = true;
                    int id = C.ID;
                    return Json(id, JsonRequestBehavior.AllowGet);

                //else
                //{
                //    result = false;
                //    return Json(result, JsonRequestBehavior.AllowGet);
                //}

        }
        public ActionResult EditCM(int ID, string MaterialName, string MasuringUnit, string Length, string Width, decimal Price, string Brand, string ProcessingTime, string Specifications, string Type, string Notes, bool effective)
        {
            bool result = false;

            ConstructionMaterial C = db.ConstructionMaterials.SingleOrDefault(x => x.ID.Equals(ID));

                    C.MaterialName = MaterialName;
                    C.MasuringUnit = MasuringUnit;
                    C.Price = Price;
                    C.Brand = Brand;
                    C.effective = effective;
                    C.Length = Length;
                    C.Width = Width;
                    C.Type = Type;
                    C.ProcessingTime = ProcessingTime;
                    C.Specifications = Specifications;
                    C.Notes = Notes;
                    

                    db.SaveChanges();

                    result = true;
                    return Json(result, JsonRequestBehavior.AllowGet);


        }
        public ActionResult EditMainItem(int ID, string Ename, string Aname, string Description, int NoOfDaies, int DegreeOfExcellenceId, string AttachedId, string Nots, bool HaveItem)
        {
            bool result = false;

            MianItems M = db.MianItemss.SingleOrDefault(x => x.Id.Equals(ID));

            M.Ename = Ename;
            M.Aname = Aname;
            M.Description = Description;
            M.NoOfDaies = NoOfDaies;
            M.DegreeOfExcellenceId = DegreeOfExcellenceId;
            M.AttachedId = AttachedId;
            M.Nots = Nots;
            M.HaveItem = HaveItem;
                   

            db.SaveChanges();

            result = true;
            return Json(result, JsonRequestBehavior.AllowGet);


        }

        //public JsonResult getProductMaterials()
        //{
        //    List<ConstructionMaterial> Materials = new List<ConstructionMaterial>();
        //    Materials = db.ConstructionMaterials.OrderBy(a => a.MaterialName).ToList();

        //    return new JsonResult { Data = Materials, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        //}

        public JsonResult GetItemData(int ID)
        {
            var MainItems = db.MianItemss.FirstOrDefault(a => a.Id.Equals(ID));

            List<ConstructionMaterial> constructionMaterials = db.ConstructionMaterials/*.Where(x => x.MianItemsId == ID)*/.ToList();

            MainItemConVM Model = new MainItemConVM()
            {

                MainItem = MainItems,
                ConstructionMaterial = constructionMaterials

            };

            string json = JsonConvert.SerializeObject(Model, Formatting.None, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
            return Json(json, JsonRequestBehavior.AllowGet);


        }
        public JsonResult GetMaterialData(int ID)
        {
            var Material = db.ConstructionMaterials.FirstOrDefault(a => a.ID.Equals(ID));


            string json = JsonConvert.SerializeObject(Material, Formatting.None, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
            return Json(json, JsonRequestBehavior.AllowGet);


        }
        public JsonResult DeleteItemData(int ID)
        {

            var MainItems = db.MianItemss.FirstOrDefault(a => a.Id.Equals(ID));
            db.MianItemss.Remove(MainItems);
            db.SaveChanges();

            return new JsonResult { Data = new { status = true } };


        }
        public JsonResult DeleteConMat(int ID)
        {

            var ConstructioMaterial = db.ConstructionMaterials.FirstOrDefault(a => a.ID.Equals(ID));
            db.ConstructionMaterials.Remove(ConstructioMaterial);
            db.SaveChanges();

            return new JsonResult { Data = new { status = true } };


        }
        public JsonResult ServerFiltering_GetMaterials(string text)
        {
            var dataContext = new ApplicationDbContext();


            var Materials = dataContext.ConstructionMaterials.Select(i => new MaterialVM
            {
                ID = i.ID,
                MaterialName = i.MaterialName
            });

            if (!string.IsNullOrEmpty(text))
            {
                Materials = Materials.Where(p => p.MaterialName.Contains(text));
            }

            return Json(Materials, JsonRequestBehavior.AllowGet);
        }
        //public JsonResult save(MianItems MainItem)
        //{
        //    bool status = false;

        //    var isValidModel = TryUpdateModel(MainItem);
        //    if (isValidModel)
        //    {
        //        db.MianItemss.Add(MainItem);
        //        db.SaveChanges();
        //        status = true;

        //    }
        //    return new JsonResult { Data = new { status = status } };
        //}

        // POST: MianItems/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> Create(MainItmsVM Model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        MianItems model = Model.MianItems;
        //        db.MianItemss.Add(model);

        //        db.SaveChanges();

        //        List<_MianItemsConstructionMaterial> mo = new List<_MianItemsConstructionMaterial>();

        //        foreach (var item in Model.SelectedMatrialsIds)
        //        {
        //            mo.Add(new _MianItemsConstructionMaterial()
        //            {

        //                MianItemsId = Model.MianItems.Id,
        //                ConstructionMaterialId = item
        //            });
        //        }
        //        foreach (var item in mo)
        //        {
        //            db.MianItemsConstructionMaterials.Add(item);
        //        }

        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    ViewBag.Matrials = db.ConstructionMaterials.ToList();

        //    return View(Model);
        //}
        private void PopulateMaterials()
        {
            var dataContext = new ApplicationDbContext();
            var mainitems = dataContext.ConstructionMaterials.ToList();

            List<StageListVM> list = new List<StageListVM>();

            foreach (var item in mainitems)
            {
                StageListVM model = new StageListVM();

                model.StageID = item.ID;

                model.StageName = item.MaterialName;

                list.Add(model);
            }

            var categories = dataContext.ConstructionMaterials
                        .Select(c => new StageListVM
                        {
                            StageID = c.ID,
                            StageName = c.MaterialName
                        })
                        .OrderBy(e => e.StageName);

            ViewData["Materials"] = categories;
            ViewData["defaultStage"] = categories.First();
        }

        // GET: MianItems/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MianItems mianItems = await db.MianItemss.FindAsync(id);
            if (mianItems == null)
            {
                return HttpNotFound();
            }
            return View(mianItems);
        }

        // POST: MianItems/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(MianItems mianItems, IEnumerable<HttpPostedFileBase> Files)
        {
            if (ModelState.IsValid)
            {

                db.Entry(mianItems).State = EntityState.Modified;
                await db.SaveChangesAsync();

                List<MianItemsAttachment> Attachments = new List<MianItemsAttachment>();
                if (Files != null)
                {
                    var List = db.MianItemsAttachments.Where(x => x.MianItemsId == mianItems.Id);
                    foreach (var item in List)
                    {
                        System.IO.File.Delete(item.Path);
                    }
                    foreach (var file in Files)
                    {
                        var path = SaveFile(file, "MainItems");
                        MianItemsAttachment att = new MianItemsAttachment()
                        {
                            Path = path,
                            MianItemsId = mianItems.Id
                        };
                        Attachments.Add(att);
                        db.MianItemsAttachments.Add(att);
                        db.SaveChanges();

                    }


                }

                return RedirectToAction("Index");
            }
            return View(mianItems);
        }

        // GET: MianItems/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MianItems mianItems = await db.MianItemss.FindAsync(id);
            if (mianItems == null)
            {
                return HttpNotFound();
            }
            return View(mianItems);
        }

        // POST: MianItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            MianItems mianItems = await db.MianItemss.FindAsync(id);
            db.MianItemss.Remove(mianItems);
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
