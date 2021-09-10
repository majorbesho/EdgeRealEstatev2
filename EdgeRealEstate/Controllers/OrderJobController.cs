using EdgeRealEstate.Entities;
using EdgeRealEstate.Models;
using EdgeRealEstate.Models.Services;
using EdgeRealEstate.Models.ViewModels;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace EdgeRealEstate.Controllers
{
    public class OrderJobController : Controller
    {
        // GET: OrderJob
        ApplicationDbContext db = new ApplicationDbContext();
        private OrderJobDetailsService OrderJobDetailsService;
        private OrderJobDetailsServiceForIndex OrderJobDetailsServiceForIndex;
        public OrderJobController()
        {
            this.OrderJobDetailsService = new OrderJobDetailsService(db);
            this.OrderJobDetailsServiceForIndex = new OrderJobDetailsServiceForIndex(db);
        }
        // GET: ItemInventory
        public ActionResult Index(int? page)
        {
            var OrderJob = db.OrderJob.Where(x => x.IsDeleted == false)
            .OrderByDescending(x => x.Id).ToList().ToPagedList(page ?? 1, 1);
            if (OrderJob.Count() != 0)
            {
                Session["MasterIdFromIndex"] = OrderJob.FirstOrDefault().Id;
            }
            else
            {
                Session["MasterIdFromIndex"] = 1;
            }
            return View(OrderJob);
        }
        /************ create ***********/
        public ActionResult Create(string OrderJobCode, string Note
            //, bool? IsCreateContract,
            //int? contractorId, int? MainProjectId, int? ProjectsId, int? receiptEngenneringId
            //, int? SupervisorEngenneringId, string SpecialNote
            )
        {
            OrderJob OrderJob = new OrderJob();
            OrderJob.OrderJobCode = OrderJobCode;
            //OrderJob.SpecialNote = SpecialNote;
            OrderJob.Note = Note;
            //OrderJob.IsCreateContract = IsCreateContract;
            //OrderJob.contractorId = contractorId;
            //OrderJob.MainProjectId = MainProjectId;
            //OrderJob.ProjectsId = ProjectsId;
            //OrderJob.receiptEngenneringId = receiptEngenneringId;
            //OrderJob.SupervisorEngenneringId = SupervisorEngenneringId;
            OrderJob.IsDeleted = false;
            OrderJobDetailsService.createMaster(OrderJob);
            Session["ItemInventoryMasterId"] = OrderJob.Id;
            return View();
        }
        public ActionResult Editing_Custom()
        {
            string val = "0";
            if (db.OrderJob.Any())
            {
                val = db.OrderJob.ToList().OrderByDescending(x => x.Id).First().OrderJobCode;
            }
            ViewBag.codeval = (Int32.Parse(val) + 1).ToString();
            ViewBag.contractor = new SelectList(db.contractor, "Id", "ARName");
            ViewBag.SupervisorEngennering = new SelectList(db.Engennerings, "id", "Aname");
            ViewBag.receiptEngennering = new SelectList(db.Engennerings, "id", "Aname");
            ViewBag.MainProjects = new SelectList(db.MainProjects, "id", "Aname");
            ViewBag.Projectes = new SelectList(db.Projectes, "id", "Aname");
            PopulateStage();
            PopulateMainItems();
            return View();
        }
        public ActionResult EditingCustom_Read([DataSourceRequest] DataSourceRequest request)
        {
            return Json(/*BillSalesDetailsService.Read().ToDataSourceResult(*/request/*)*/);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EditingCustom_Update([DataSourceRequest] DataSourceRequest request,
            [Bind(Prefix = "models")]IEnumerable<OrderJobDetailsViewModel> ItemRequests)
        {
            if (ItemRequests != null && ModelState.IsValid)
            {
                foreach (var ItemRequest in ItemRequests)
                {
                    OrderJobDetailsService.Update(ItemRequest);
                }
            }

            return Json(ItemRequests.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EditingCustom_Create([DataSourceRequest] DataSourceRequest request,
            [Bind(Prefix = "models")]IEnumerable<OrderJobDetailsViewModel> ItemRequests, string OrderJobCode, string Note
              , bool IsCreateContract,int? contractorId, int? MainProjectId, int? ProjectsId, int? receiptEngenneringId
              , int? SupervisorEngenneringId, string SpecialNote)
        {
            OrderJob OrderJob = new OrderJob();
            OrderJob.OrderJobCode = OrderJobCode;
            OrderJob.SpecialNote = SpecialNote;
            OrderJob.Note = Note;
            OrderJob.IsCreateContract = IsCreateContract;
            OrderJob.contractorId = contractorId;
            OrderJob.MainProjectId = MainProjectId;
            OrderJob.ProjectsId = ProjectsId;
            OrderJob.receiptEngenneringId = receiptEngenneringId;
            OrderJob.SupervisorEngenneringId = SupervisorEngenneringId;
            OrderJob.IsDeleted = false;
            //OrderJobDetailsService.createMaster(OrderJob);
            OrderJob.IsDeleted = false;
            db.OrderJob.Add(OrderJob);
            db.SaveChanges();
            Session["ItemInventoryMasterId"] = OrderJob.Id;
            var results = new List<OrderJobDetailsViewModel>();
            if (ItemRequests != null)
            {
                foreach (var ItemRequest in ItemRequests)
                {
                    OrderJobDetailsService.Create(ItemRequest);
                    results.Add(ItemRequest);
                    db.SaveChanges();
                }
            }

            //if (ItemRequests != null && ModelState.IsValid)
            //{
            //    foreach (var ItemRequest in ItemRequests)
            //    {
            //        OrderJobDetailsService.Create(ItemRequest);
            //        results.Add(ItemRequest);
            //        db.SaveChanges();
            //    }
            //}

            return Json(results.ToDataSourceResult(request, ModelState));
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EditingCustom_Destroy([DataSourceRequest] DataSourceRequest request,
            [Bind(Prefix = "models")]IEnumerable<OrderJobDetailsViewModel> ItemRequests)
        {
            foreach (var ItemRequest in ItemRequests)
            {
                OrderJobDetailsService.Destroy(ItemRequest);
            }

            return Json(ItemRequests.ToDataSourceResult(request, ModelState));
        }


        /*************************Index****************************/
        public ActionResult Editing_CustomForIndex()
        {
            PopulateStage();
            PopulateMainItems();
            return View();
        }
        public ActionResult EditingCustom_ReadForIndex([DataSourceRequest] DataSourceRequest request)
        {
            return Json(OrderJobDetailsServiceForIndex.Read().ToDataSourceResult(request));
        }

        /********************Edit ********************/
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Edit(int? Id, string OrderJobCode, string SpecialNote, string Note, bool IsCreateContract,
            int contractorId, int MainProjectId, int ProjectsId, int receiptEngenneringId
            , int SupervisorEngenneringId)
        {
            OrderJob OrderJob = db.OrderJob.Find(Id);
            OrderJob.OrderJobCode = OrderJobCode;
            OrderJob.SpecialNote = SpecialNote;
            OrderJob.Note = Note;
            OrderJob.IsCreateContract = IsCreateContract;
            OrderJob.contractorId = contractorId;
            OrderJob.MainProjectId = MainProjectId;
            OrderJob.ProjectsId = ProjectsId;
            OrderJob.receiptEngenneringId = receiptEngenneringId;
            OrderJob.SupervisorEngenneringId = SupervisorEngenneringId;

            OrderJobDetailsServiceForIndex.updateMaster(OrderJob);
            //Session["ItemInventoryMasterId"] = OrderJob.Id;
            return View(OrderJob);
        }
        public ActionResult Editing_CustomForEdit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderJob OrderJob = db.OrderJob.Find(id);
            ViewBag.IsCreateContract = OrderJob.IsCreateContract;
            ViewBag.Note = OrderJob.Note;
            ViewBag.SpecialNote = OrderJob.SpecialNote;
            ViewBag.OrderJobCode = OrderJob.OrderJobCode;
            ViewBag.contractor = new SelectList(db.contractor, "Id", "ARName");
            ViewBag.SupervisorEngennering = new SelectList(db.Engennerings, "id", "Aname");
            ViewBag.receiptEngennering = new SelectList(db.Engennerings, "id", "Aname");
            ViewBag.MainProjects = new SelectList(db.MainProjects, "id", "Aname");
            ViewBag.Projectes = new SelectList(db.Projectes, "id", "Aname");
            if (OrderJob == null)
            {
                return HttpNotFound();
            }
            PopulateStage();
            PopulateMainItems();
            return View(OrderJob);
        }

        public ActionResult EditingCustom_ReadForEdit([DataSourceRequest] DataSourceRequest request)
        {
            return Json(OrderJobDetailsServiceForIndex.Read().ToDataSourceResult(request));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EditingCustom_UpdateForEdit([DataSourceRequest] DataSourceRequest request,
            [Bind(Prefix = "models")]IEnumerable<OrderJobDetailsViewModel> ItemRequests
            , int? id, string OrderJobCode, string SpecialNote, string Note, bool IsCreateContract,
            int? contractorId, int? MainProjectId, int? ProjectsId, int? receiptEngenneringId
            , int? SupervisorEngenneringId)
        {
            OrderJob OrderJob = db.OrderJob.Find(id);
            OrderJob.OrderJobCode = OrderJobCode;
            OrderJob.SpecialNote = SpecialNote;
            OrderJob.Note = Note;
            OrderJob.IsCreateContract = IsCreateContract;
            OrderJob.contractorId = contractorId;
            OrderJob.MainProjectId = MainProjectId;
            OrderJob.ProjectsId = ProjectsId;
            OrderJob.receiptEngenneringId = receiptEngenneringId;
            OrderJob.SupervisorEngenneringId = SupervisorEngenneringId;

            OrderJobDetailsServiceForIndex.updateMaster(OrderJob);
            Session["ItemInventoryMasterId"] = OrderJob.Id;

            if (ItemRequests != null && ModelState.IsValid)
            {
                foreach (var ItemRequest in ItemRequests)
                {
                    OrderJobDetailsServiceForIndex.Update(ItemRequest);
                }
            }

            return Json(ItemRequests.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EditingCustom_CreateForEdit([DataSourceRequest] DataSourceRequest request,
                [Bind(Prefix = "models")]IEnumerable<OrderJobDetailsViewModel> ItemRequests
             , int? Id, string OrderJobCode, string SpecialNote, string Note, bool IsCreateContract,
            int? contractorId, int? MainProjectId, int? ProjectsId, int? receiptEngenneringId
            , int? SupervisorEngenneringId)
        {
            OrderJob OrderJob = db.OrderJob.Find(Id);
            OrderJob.OrderJobCode = OrderJobCode;
            OrderJob.SpecialNote = SpecialNote;
            OrderJob.Note = Note;
            OrderJob.IsCreateContract = IsCreateContract;
            OrderJob.contractorId = contractorId;
            OrderJob.MainProjectId = MainProjectId;
            OrderJob.ProjectsId = ProjectsId;
            OrderJob.receiptEngenneringId = receiptEngenneringId;
            OrderJob.SupervisorEngenneringId = SupervisorEngenneringId;

            OrderJobDetailsServiceForIndex.updateMaster(OrderJob);
            Session["ItemInventoryMasterId"] = OrderJob.Id;

            var results = new List<OrderJobDetailsViewModel>();

            if (ItemRequests != null && ModelState.IsValid)
            {
                foreach (var ItemRequest in ItemRequests)
                {
                    OrderJobDetailsServiceForIndex.Create(ItemRequest);
                    results.Add(ItemRequest);
                    db.SaveChanges();
                }
            }

            return Json(results.ToDataSourceResult(request, ModelState));
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EditingCustom_DestroyForEdit([DataSourceRequest] DataSourceRequest request,
            [Bind(Prefix = "models")]IEnumerable<OrderJobDetailsViewModel> ItemRequests)
        {
            foreach (var ItemRequest in ItemRequests)
            {
                OrderJobDetailsServiceForIndex.Destroy(ItemRequest);
            }

            return Json(ItemRequests.ToDataSourceResult(request, ModelState));
        }
        /**********************Delete view************************/
        public ActionResult Editing_CustomForDelete()
        {
            PopulateStage();
            PopulateMainItems();

            return View();
        }

        public ActionResult EditingCustom_ReadForDelete([DataSourceRequest] DataSourceRequest request)
        {
            return Json(OrderJobDetailsServiceForIndex.Read().ToDataSourceResult(request));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EditingCustom_UpdateForDelete([DataSourceRequest] DataSourceRequest request,
    [Bind(Prefix = "models")]IEnumerable<OrderJobDetailsViewModel> ItemRequests)
        {
            if (ItemRequests != null && ModelState.IsValid)
            {
                foreach (var ItemRequest in ItemRequests)
                {
                    OrderJobDetailsServiceForIndex.Update(ItemRequest);
                }
            }

            return Json(ItemRequests.ToDataSourceResult(request, ModelState));
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EditingCustom_DestroyForDelete([DataSourceRequest] DataSourceRequest request,
            [Bind(Prefix = "models")]IEnumerable<OrderJobDetailsViewModel> ItemRequests)
        {
            foreach (var ItemRequest in ItemRequests)
            {
                OrderJobDetailsServiceForIndex.Destroy(ItemRequest);
            }

            return Json(ItemRequests.ToDataSourceResult(request, ModelState));
        }

        /*******************Helper methods*******************/
        public JsonResult ServerFiltering_GetStage(string text)
        {
            var dataContext = new ApplicationDbContext();


            var Stage = dataContext.StageMasters.Select(i => new StageVM
            {
                id = i.ID,
                Aname = i.Stage.Aname,
                code = i.Stage.code
            });

            if (!string.IsNullOrEmpty(text))
            {
                Stage = Stage.Where(p => p.Aname.Contains(text) || p.code.Contains(text));
            }

            return Json(Stage, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ServerFiltering_GetMainItems(string text ,int val)
        {
            var dataContext = new ApplicationDbContext();


            var MainItems = dataContext.MainItemDetails.Select(i => new MainItemViewModel
            {
                ID = i.Id,
                Name = i.MainItem.Aname

            });

            if (!string.IsNullOrEmpty(text))
            {
                MainItems = MainItems.Where(p => p.Name.Contains(text) /*|| p.code.Contains(text)*/);
            }

            return Json(MainItems, JsonRequestBehavior.AllowGet);
        }
        private void PopulateStage()
        {
            var dataContext = new ApplicationDbContext();
            var Stage = dataContext.StageMasters
                        .Select(i => new StageVM
                        {
                            id = i.ID,
                            Aname = i.Stage.Aname,
                        });
            //.OrderBy(e => e.arName);

            ViewData["Stage"] = Stage;
            ViewData["defaultStage"] = Stage.First();
        }
        private void PopulateMainItems()
        {
            var dataContext = new ApplicationDbContext();
            var MainItems = dataContext.MainItemDetails
                        .Select(s => new MainItemViewModel
                        {
                            ID = s.Id,
                            Name = s.MainItem.Aname
                        })
                        .OrderBy(e => e.Name);

            ViewData["MainItems"] = MainItems;
            ViewData["defaultMainItems"] = MainItems.First();
        }


        // GET: BillPurchasesMaster/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderJob OrderJob = db.OrderJob.Find(id);
            if (OrderJob == null)
            {
                return HttpNotFound();
            }
            return View(OrderJob);
        }

        // POST: BillPurchasesMaster/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            OrderJob OrderJob = db.OrderJob.Find(id);
            List<OrderJobDetails> OrderJobDetails = db.OrderJobDetails.Where(x => x.OrderJobId == id).ToList();
            foreach (OrderJobDetails i in OrderJobDetails)
            {
                i.IsDeleted = true;
            }
            OrderJob.IsDeleted = true;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}