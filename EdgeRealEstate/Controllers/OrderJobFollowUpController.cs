using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EdgeRealEstate.Entities;
using EdgeRealEstate.Models;
using EdgeRealEstate.Models.Services;
using EdgeRealEstate.Models.ViewModels;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using PagedList;

namespace EdgeRealEstate.Controllers
{
    public class OrderJobFollowUpController : Controller
    {
        // GET: OrderJobFollowUp
        ApplicationDbContext db = new ApplicationDbContext();
        private OrderJobFollowUpServiceForIndex OrderJobFollowUpServiceForIndex;
        public OrderJobFollowUpController()
        {
            this.OrderJobFollowUpServiceForIndex = new OrderJobFollowUpServiceForIndex(db);
        }
        public ActionResult test()
        {
            return View();
        }
       

        /********************Edit ********************/
        //public ActionResult Editing_CustomForEdit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    OrderJob OrderJob = db.OrderJob.Find(id);
        //    ViewBag.IsCreateContract = OrderJob.IsCreateContract;
        //    ViewBag.Note = OrderJob.Note;
        //    ViewBag.SpecialNote = OrderJob.SpecialNote;
        //    ViewBag.OrderJobCode = OrderJob.OrderJobCode;
        //    ViewBag.contractor = new SelectList(db.contractor, "Id", "ARName");
        //    ViewBag.SupervisorEngennering = new SelectList(db.Engennerings, "id", "Aname");
        //    ViewBag.receiptEngennering = new SelectList(db.Engennerings, "id", "Aname");
        //    ViewBag.MainProjects = new SelectList(db.MainProjects, "id", "Aname");
        //    ViewBag.Projectes = new SelectList(db.Projectes, "id", "Aname");
        //    if (OrderJob == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    PopulateStage();
        //    PopulateMainItems();
        //    return View(OrderJob);
        //}

        public ActionResult EditingCustom_ReadForEdit([DataSourceRequest] DataSourceRequest request,int? Code)
        {
            return Json(OrderJobFollowUpServiceForIndex.Read(Code).ToDataSourceResult(request));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EditingCustom_UpdateForEdit([DataSourceRequest] DataSourceRequest request,
            [Bind(Prefix = "models")]IEnumerable<OrderJobDetailsViewModel> ItemRequests, int? Code
            //, int? id, string OrderJobCode, string SpecialNote, string Note, bool IsCreateContract,
            //int? contractorId, int? MainProjectId, int? ProjectsId, int? receiptEngenneringId
            //, int? SupervisorEngenneringId
            )
        {


            if (ItemRequests != null/* && ModelState.IsValid*/)
            {
                foreach (var ItemRequest in ItemRequests)
                {
                    OrderJobFollowUpServiceForIndex.Update(ItemRequest, Code);
                }
            }

            return Json(ItemRequests.ToDataSourceResult(request, ModelState));
        }

        /*******************Helper methods*******************/
        public JsonResult ServerFiltering_GetStage(string text)
        {
            var dataContext = new ApplicationDbContext();


            var Stage = dataContext.Stages.Select(i => new StageVM
            {
                id = i.id,
                Aname = i.Aname,
                code = i.code
            });

            if (!string.IsNullOrEmpty(text))
            {
                Stage = Stage.Where(p => p.Aname.Contains(text) || p.code.Contains(text));
            }

            return Json(Stage, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ServerFiltering_GetMainItems(string text)
        {
            var dataContext = new ApplicationDbContext();


            var MainItems = dataContext.MianItemss.Select(i => new MainItemViewModel
            {
                ID = i.Id,
                Name = i.Aname

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
            var Stage = dataContext.Stages
                        .Select(i => new StageVM
                        {
                            id = i.id,
                            Aname = i.Aname,
                        });
            //.OrderBy(e => e.arName);

            ViewData["Stage"] = Stage;
            ViewData["defaultStage"] = Stage.First();
        }
        private void PopulateMainItems()
        {
            var dataContext = new ApplicationDbContext();
            var MainItems = dataContext.MianItemss
                        .Select(s => new MainItemViewModel
                        {
                            ID = s.Id,
                            Name = s.Aname
                        })
                        .OrderBy(e => e.Name);

            ViewData["MainItems"] = MainItems;
            ViewData["defaultMainItems"] = MainItems.First();
        }


        public JsonResult ServerFiltering_GetOrderJobCode(string text)
        {
            var dataContext = new ApplicationDbContext();

            var Customers = dataContext.OrderJob
                .Select(i => new OrderJobViewModel
                {
                    Id = i.Id,
                    OrderJobCode = i.OrderJobCode,
                });

            if (!string.IsNullOrEmpty(text))
            {
                Customers = Customers.Where(p => p.OrderJobCode.Contains(text));
            }

            return Json(Customers, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetMasterData(int? Code,int? Project, int? SalesMan, int? Cust)
        {
            int? Id = Code;
            int? MainProjectId = Project;
            int? SupervisorEngenneringId = SalesMan;
            int? contractorId = Cust;

            var query = new OrderJob();
            if (Id != null && MainProjectId == null && SupervisorEngenneringId == null && contractorId == null)
            {
                query = db.OrderJob.Where(x => x.Id == Id).ToList().FirstOrDefault();
            }
            else if (Id == null && MainProjectId != null && SupervisorEngenneringId == null && contractorId == null)
            {
                query = db.OrderJob.Where(x => x.MainProjectId == MainProjectId).ToList().FirstOrDefault();
            }
            else if (Id == null && MainProjectId == null && SupervisorEngenneringId != null && contractorId == null)
            {
                query = db.OrderJob.Where(x => x.SupervisorEngenneringId == SupervisorEngenneringId).ToList().FirstOrDefault();
            }
            else if (Id == null && MainProjectId == null && SupervisorEngenneringId == null && contractorId != null)
            {
                query = db.OrderJob.Where(x => x.contractorId == contractorId).ToList().FirstOrDefault();
            }
            else if (Id != null && MainProjectId != null && SupervisorEngenneringId == null && contractorId == null)
            {
                query = db.OrderJob.Where(x => x.Id == Id && x.MainProjectId == MainProjectId).ToList().FirstOrDefault();
            }
            else if (Id != null && MainProjectId == null && SupervisorEngenneringId != null && contractorId == null)
            {
                query = db.OrderJob.Where(x => x.Id == Id && x.SupervisorEngenneringId == SupervisorEngenneringId).ToList().FirstOrDefault();
            }
            else if (Id != null && MainProjectId == null && SupervisorEngenneringId == null && contractorId != null)
            {
                query = db.OrderJob.Where(x => x.Id == Id && x.contractorId == contractorId).ToList().FirstOrDefault();
            }
            else if (Id != null && MainProjectId != null && SupervisorEngenneringId != null && contractorId == null)
            {
                query = db.OrderJob.Where(x => x.Id == Id && x.MainProjectId == MainProjectId && x.SupervisorEngenneringId == SupervisorEngenneringId).ToList().FirstOrDefault();
            }
            else if (Id != null && MainProjectId != null && SupervisorEngenneringId == null && contractorId != null)
            {
                query = db.OrderJob.Where(x => x.Id == Id && x.MainProjectId == MainProjectId && x.contractorId == contractorId).ToList().FirstOrDefault();
            }
            else if (Id != null && MainProjectId != null && SupervisorEngenneringId != null && contractorId != null)
            {
                query = db.OrderJob.Where(x => x.Id == Id && x.MainProjectId == MainProjectId && x.SupervisorEngenneringId == SupervisorEngenneringId && x.contractorId == contractorId).ToList().FirstOrDefault();
            }
            else if (Id != null && MainProjectId == null && SupervisorEngenneringId != null && contractorId != null)
            {
                query = db.OrderJob.Where(x => x.Id == Id && x.SupervisorEngenneringId == SupervisorEngenneringId && x.contractorId == contractorId).ToList().FirstOrDefault();
            }
            else if (Id == null && MainProjectId != null && SupervisorEngenneringId != null && contractorId == null)
            {
                query = db.OrderJob.Where(x =>  x.SupervisorEngenneringId == SupervisorEngenneringId && x.MainProjectId == MainProjectId).ToList().FirstOrDefault();
            }
            else if (Id == null && MainProjectId != null && SupervisorEngenneringId == null && contractorId != null)
            {
                query = db.OrderJob.Where(x => x.MainProjectId == MainProjectId && x.contractorId == contractorId).ToList().FirstOrDefault();
            }
            else if (Id == null && MainProjectId != null && SupervisorEngenneringId != null && contractorId != null)
            {
                query = db.OrderJob.Where(x => x.MainProjectId == MainProjectId && x.contractorId == contractorId && x.SupervisorEngenneringId == SupervisorEngenneringId).ToList().FirstOrDefault();
            }
            else if (Id == null && MainProjectId == null && SupervisorEngenneringId != null && contractorId != null)
            {
                query = db.OrderJob.Where(x => x.contractorId == contractorId && x.SupervisorEngenneringId == SupervisorEngenneringId).ToList().FirstOrDefault();
            }

            return Json(new { Note = query.Note, SpecialNote = query.SpecialNote, Id = query.Id,
                IsCreateContract = query.IsCreateContract,Aname = query.Projects.Aname,
            }, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult GetMainProject(int? Code, int? Project, int? SalesMan, int? Cust)
        {
            int? Id = Code;
            int? MainProjectId = Project;
            int? SupervisorEngenneringId = SalesMan;
            int? contractorId = Cust;

            var query = new OrderJob();

            if (Id != null && MainProjectId == null && SupervisorEngenneringId == null && contractorId == null)
            {
                query = db.OrderJob.Where(x => x.Id == Id).ToList().FirstOrDefault();
            }
            else if (Id == null && MainProjectId != null && SupervisorEngenneringId == null && contractorId == null)
            {
                query = db.OrderJob.Where(x => x.MainProjectId == MainProjectId).ToList().FirstOrDefault();
            }
            else if (Id == null && MainProjectId == null && SupervisorEngenneringId != null && contractorId == null)
            {
                query = db.OrderJob.Where(x => x.SupervisorEngenneringId == SupervisorEngenneringId).ToList().FirstOrDefault();
            }
            else if (Id == null && MainProjectId == null && SupervisorEngenneringId == null && contractorId != null)
            {
                query = db.OrderJob.Where(x => x.contractorId == contractorId).ToList().FirstOrDefault();
            }
            else if (Id != null && MainProjectId != null && SupervisorEngenneringId == null && contractorId == null)
            {
                query = db.OrderJob.Where(x => x.Id == Id && x.MainProjectId == MainProjectId).ToList().FirstOrDefault();
            }
            else if (Id != null && MainProjectId == null && SupervisorEngenneringId != null && contractorId == null)
            {
                query = db.OrderJob.Where(x => x.Id == Id && x.SupervisorEngenneringId == SupervisorEngenneringId).ToList().FirstOrDefault();
            }
            else if (Id != null && MainProjectId == null && SupervisorEngenneringId == null && contractorId != null)
            {
                query = db.OrderJob.Where(x => x.Id == Id && x.contractorId == contractorId).ToList().FirstOrDefault();
            }
            else if (Id != null && MainProjectId != null && SupervisorEngenneringId != null && contractorId == null)
            {
                query = db.OrderJob.Where(x => x.Id == Id && x.MainProjectId == MainProjectId && x.SupervisorEngenneringId == SupervisorEngenneringId).ToList().FirstOrDefault();
            }
            else if (Id != null && MainProjectId != null && SupervisorEngenneringId == null && contractorId != null)
            {
                query = db.OrderJob.Where(x => x.Id == Id && x.MainProjectId == MainProjectId && x.contractorId == contractorId).ToList().FirstOrDefault();
            }
            else if (Id != null && MainProjectId != null && SupervisorEngenneringId != null && contractorId != null)
            {
                query = db.OrderJob.Where(x => x.Id == Id && x.MainProjectId == MainProjectId && x.SupervisorEngenneringId == SupervisorEngenneringId && x.contractorId == contractorId).ToList().FirstOrDefault();
            }
            else if (Id != null && MainProjectId == null && SupervisorEngenneringId != null && contractorId != null)
            {
                query = db.OrderJob.Where(x => x.Id == Id && x.SupervisorEngenneringId == SupervisorEngenneringId && x.contractorId == contractorId).ToList().FirstOrDefault();
            }
            else if (Id == null && MainProjectId != null && SupervisorEngenneringId != null && contractorId == null)
            {
                query = db.OrderJob.Where(x => x.SupervisorEngenneringId == SupervisorEngenneringId && x.MainProjectId == MainProjectId).ToList().FirstOrDefault();
            }
            else if (Id == null && MainProjectId != null && SupervisorEngenneringId == null && contractorId != null)
            {
                query = db.OrderJob.Where(x => x.MainProjectId == MainProjectId && x.contractorId == contractorId).ToList().FirstOrDefault();
            }
            else if (Id == null && MainProjectId != null && SupervisorEngenneringId != null && contractorId != null)
            {
                query = db.OrderJob.Where(x => x.MainProjectId == MainProjectId && x.contractorId == contractorId && x.SupervisorEngenneringId == SupervisorEngenneringId).ToList().FirstOrDefault();
            }
            else if (Id == null && MainProjectId == null && SupervisorEngenneringId != null && contractorId != null)
            {
                query = db.OrderJob.Where(x => x.contractorId == contractorId && x.SupervisorEngenneringId == SupervisorEngenneringId).ToList().FirstOrDefault();
            }
            var MainProject = query.MainProject.Aname;

            return Json(MainProject, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult GetreceiptEngennering(int? Code, int? Project, int? SalesMan, int? Cust)
        {
            int? Id = Code;
            int? MainProjectId = Project;
            int? SupervisorEngenneringId = SalesMan;
            int? contractorId = Cust;

            var query = new OrderJob();

            if (Id != null && MainProjectId == null && SupervisorEngenneringId == null && contractorId == null)
            {
                query = db.OrderJob.Where(x => x.Id == Id).ToList().FirstOrDefault();
            }
            else if (Id == null && MainProjectId != null && SupervisorEngenneringId == null && contractorId == null)
            {
                query = db.OrderJob.Where(x => x.MainProjectId == MainProjectId).ToList().FirstOrDefault();
            }
            else if (Id == null && MainProjectId == null && SupervisorEngenneringId != null && contractorId == null)
            {
                query = db.OrderJob.Where(x => x.SupervisorEngenneringId == SupervisorEngenneringId).ToList().FirstOrDefault();
            }
            else if (Id == null && MainProjectId == null && SupervisorEngenneringId == null && contractorId != null)
            {
                query = db.OrderJob.Where(x => x.contractorId == contractorId).ToList().FirstOrDefault();
            }
            else if (Id != null && MainProjectId != null && SupervisorEngenneringId == null && contractorId == null)
            {
                query = db.OrderJob.Where(x => x.Id == Id && x.MainProjectId == MainProjectId).ToList().FirstOrDefault();
            }
            else if (Id != null && MainProjectId == null && SupervisorEngenneringId != null && contractorId == null)
            {
                query = db.OrderJob.Where(x => x.Id == Id && x.SupervisorEngenneringId == SupervisorEngenneringId).ToList().FirstOrDefault();
            }
            else if (Id != null && MainProjectId == null && SupervisorEngenneringId == null && contractorId != null)
            {
                query = db.OrderJob.Where(x => x.Id == Id && x.contractorId == contractorId).ToList().FirstOrDefault();
            }
            else if (Id != null && MainProjectId != null && SupervisorEngenneringId != null && contractorId == null)
            {
                query = db.OrderJob.Where(x => x.Id == Id && x.MainProjectId == MainProjectId && x.SupervisorEngenneringId == SupervisorEngenneringId).ToList().FirstOrDefault();
            }
            else if (Id != null && MainProjectId != null && SupervisorEngenneringId == null && contractorId != null)
            {
                query = db.OrderJob.Where(x => x.Id == Id && x.MainProjectId == MainProjectId && x.contractorId == contractorId).ToList().FirstOrDefault();
            }
            else if (Id != null && MainProjectId != null && SupervisorEngenneringId != null && contractorId != null)
            {
                query = db.OrderJob.Where(x => x.Id == Id && x.MainProjectId == MainProjectId && x.SupervisorEngenneringId == SupervisorEngenneringId && x.contractorId == contractorId).ToList().FirstOrDefault();
            }
            else if (Id != null && MainProjectId == null && SupervisorEngenneringId != null && contractorId != null)
            {
                query = db.OrderJob.Where(x => x.Id == Id && x.SupervisorEngenneringId == SupervisorEngenneringId && x.contractorId == contractorId).ToList().FirstOrDefault();
            }
            else if (Id == null && MainProjectId != null && SupervisorEngenneringId != null && contractorId == null)
            {
                query = db.OrderJob.Where(x => x.SupervisorEngenneringId == SupervisorEngenneringId && x.MainProjectId == MainProjectId).ToList().FirstOrDefault();
            }
            else if (Id == null && MainProjectId != null && SupervisorEngenneringId == null && contractorId != null)
            {
                query = db.OrderJob.Where(x => x.MainProjectId == MainProjectId && x.contractorId == contractorId).ToList().FirstOrDefault();
            }
            else if (Id == null && MainProjectId != null && SupervisorEngenneringId != null && contractorId != null)
            {
                query = db.OrderJob.Where(x => x.MainProjectId == MainProjectId && x.contractorId == contractorId && x.SupervisorEngenneringId == SupervisorEngenneringId).ToList().FirstOrDefault();
            }
            else if (Id == null && MainProjectId == null && SupervisorEngenneringId != null && contractorId != null)
            {
                query = db.OrderJob.Where(x => x.contractorId == contractorId && x.SupervisorEngenneringId == SupervisorEngenneringId).ToList().FirstOrDefault();
            }

            var result = db.Engennerings.Where(x => x.id == query.receiptEngenneringId).ToList().FirstOrDefault();
            var receiptEngenneringId = result.Aname;

            return Json(receiptEngenneringId, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetSupervisorEngennering(int? Code, int? Project, int? SalesMan, int? Cust)
        {
            int? Id = Code;
            int? MainProjectId = Project;
            int? SupervisorEngenneringId = SalesMan;
            int? contractorId = Cust;

            var query = new OrderJob();

            if (Id != null && MainProjectId == null && SupervisorEngenneringId == null && contractorId == null)
            {
                query = db.OrderJob.Where(x => x.Id == Id).ToList().FirstOrDefault();
            }
            else if (Id == null && MainProjectId != null && SupervisorEngenneringId == null && contractorId == null)
            {
                query = db.OrderJob.Where(x => x.MainProjectId == MainProjectId).ToList().FirstOrDefault();
            }
            else if (Id == null && MainProjectId == null && SupervisorEngenneringId != null && contractorId == null)
            {
                query = db.OrderJob.Where(x => x.SupervisorEngenneringId == SupervisorEngenneringId).ToList().FirstOrDefault();
            }
            else if (Id == null && MainProjectId == null && SupervisorEngenneringId == null && contractorId != null)
            {
                query = db.OrderJob.Where(x => x.contractorId == contractorId).ToList().FirstOrDefault();
            }
            else if (Id != null && MainProjectId != null && SupervisorEngenneringId == null && contractorId == null)
            {
                query = db.OrderJob.Where(x => x.Id == Id && x.MainProjectId == MainProjectId).ToList().FirstOrDefault();
            }
            else if (Id != null && MainProjectId == null && SupervisorEngenneringId != null && contractorId == null)
            {
                query = db.OrderJob.Where(x => x.Id == Id && x.SupervisorEngenneringId == SupervisorEngenneringId).ToList().FirstOrDefault();
            }
            else if (Id != null && MainProjectId == null && SupervisorEngenneringId == null && contractorId != null)
            {
                query = db.OrderJob.Where(x => x.Id == Id && x.contractorId == contractorId).ToList().FirstOrDefault();
            }
            else if (Id != null && MainProjectId != null && SupervisorEngenneringId != null && contractorId == null)
            {
                query = db.OrderJob.Where(x => x.Id == Id && x.MainProjectId == MainProjectId && x.SupervisorEngenneringId == SupervisorEngenneringId).ToList().FirstOrDefault();
            }
            else if (Id != null && MainProjectId != null && SupervisorEngenneringId == null && contractorId != null)
            {
                query = db.OrderJob.Where(x => x.Id == Id && x.MainProjectId == MainProjectId && x.contractorId == contractorId).ToList().FirstOrDefault();
            }
            else if (Id != null && MainProjectId != null && SupervisorEngenneringId != null && contractorId != null)
            {
                query = db.OrderJob.Where(x => x.Id == Id && x.MainProjectId == MainProjectId && x.SupervisorEngenneringId == SupervisorEngenneringId && x.contractorId == contractorId).ToList().FirstOrDefault();
            }
            else if (Id != null && MainProjectId == null && SupervisorEngenneringId != null && contractorId != null)
            {
                query = db.OrderJob.Where(x => x.Id == Id && x.SupervisorEngenneringId == SupervisorEngenneringId && x.contractorId == contractorId).ToList().FirstOrDefault();
            }
            else if (Id == null && MainProjectId != null && SupervisorEngenneringId != null && contractorId == null)
            {
                query = db.OrderJob.Where(x => x.SupervisorEngenneringId == SupervisorEngenneringId && x.MainProjectId == MainProjectId).ToList().FirstOrDefault();
            }
            else if (Id == null && MainProjectId != null && SupervisorEngenneringId == null && contractorId != null)
            {
                query = db.OrderJob.Where(x => x.MainProjectId == MainProjectId && x.contractorId == contractorId).ToList().FirstOrDefault();
            }
            else if (Id == null && MainProjectId != null && SupervisorEngenneringId != null && contractorId != null)
            {
                query = db.OrderJob.Where(x => x.MainProjectId == MainProjectId && x.contractorId == contractorId && x.SupervisorEngenneringId == SupervisorEngenneringId).ToList().FirstOrDefault();
            }
            else if (Id == null && MainProjectId == null && SupervisorEngenneringId != null && contractorId != null)
            {
                query = db.OrderJob.Where(x => x.contractorId == contractorId && x.SupervisorEngenneringId == SupervisorEngenneringId).ToList().FirstOrDefault();
            }
            var result = db.Engennerings.Where(x => x.id == query.SupervisorEngenneringId).ToList().FirstOrDefault();
            var SupervisorEngennering = result.Aname;

            return Json(SupervisorEngennering, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult Getcontractor(int? Code, int? Project, int? SalesMan, int? Cust)
        {
            int? Id = Code;
            int? MainProjectId = Project;
            int? SupervisorEngenneringId = SalesMan;
            int? contractorId = Cust;

            var query = new OrderJob();

            if (Id != null && MainProjectId == null && SupervisorEngenneringId == null && contractorId == null)
            {
                query = db.OrderJob.Where(x => x.Id == Id).ToList().FirstOrDefault();
            }
            else if (Id == null && MainProjectId != null && SupervisorEngenneringId == null && contractorId == null)
            {
                query = db.OrderJob.Where(x => x.MainProjectId == MainProjectId).ToList().FirstOrDefault();
            }
            else if (Id == null && MainProjectId == null && SupervisorEngenneringId != null && contractorId == null)
            {
                query = db.OrderJob.Where(x => x.SupervisorEngenneringId == SupervisorEngenneringId).ToList().FirstOrDefault();
            }
            else if (Id == null && MainProjectId == null && SupervisorEngenneringId == null && contractorId != null)
            {
                query = db.OrderJob.Where(x => x.contractorId == contractorId).ToList().FirstOrDefault();
            }
            else if (Id != null && MainProjectId != null && SupervisorEngenneringId == null && contractorId == null)
            {
                query = db.OrderJob.Where(x => x.Id == Id && x.MainProjectId == MainProjectId).ToList().FirstOrDefault();
            }
            else if (Id != null && MainProjectId == null && SupervisorEngenneringId != null && contractorId == null)
            {
                query = db.OrderJob.Where(x => x.Id == Id && x.SupervisorEngenneringId == SupervisorEngenneringId).ToList().FirstOrDefault();
            }
            else if (Id != null && MainProjectId == null && SupervisorEngenneringId == null && contractorId != null)
            {
                query = db.OrderJob.Where(x => x.Id == Id && x.contractorId == contractorId).ToList().FirstOrDefault();
            }
            else if (Id != null && MainProjectId != null && SupervisorEngenneringId != null && contractorId == null)
            {
                query = db.OrderJob.Where(x => x.Id == Id && x.MainProjectId == MainProjectId && x.SupervisorEngenneringId == SupervisorEngenneringId).ToList().FirstOrDefault();
            }
            else if (Id != null && MainProjectId != null && SupervisorEngenneringId == null && contractorId != null)
            {
                query = db.OrderJob.Where(x => x.Id == Id && x.MainProjectId == MainProjectId && x.contractorId == contractorId).ToList().FirstOrDefault();
            }
            else if (Id != null && MainProjectId != null && SupervisorEngenneringId != null && contractorId != null)
            {
                query = db.OrderJob.Where(x => x.Id == Id && x.MainProjectId == MainProjectId && x.SupervisorEngenneringId == SupervisorEngenneringId && x.contractorId == contractorId).ToList().FirstOrDefault();
            }
            else if (Id != null && MainProjectId == null && SupervisorEngenneringId != null && contractorId != null)
            {
                query = db.OrderJob.Where(x => x.Id == Id && x.SupervisorEngenneringId == SupervisorEngenneringId && x.contractorId == contractorId).ToList().FirstOrDefault();
            }
            else if (Id == null && MainProjectId != null && SupervisorEngenneringId != null && contractorId == null)
            {
                query = db.OrderJob.Where(x => x.SupervisorEngenneringId == SupervisorEngenneringId && x.MainProjectId == MainProjectId).ToList().FirstOrDefault();
            }
            else if (Id == null && MainProjectId != null && SupervisorEngenneringId == null && contractorId != null)
            {
                query = db.OrderJob.Where(x => x.MainProjectId == MainProjectId && x.contractorId == contractorId).ToList().FirstOrDefault();
            }
            else if (Id == null && MainProjectId != null && SupervisorEngenneringId != null && contractorId != null)
            {
                query = db.OrderJob.Where(x => x.MainProjectId == MainProjectId && x.contractorId == contractorId && x.SupervisorEngenneringId == SupervisorEngenneringId).ToList().FirstOrDefault();
            }
            else if (Id == null && MainProjectId == null && SupervisorEngenneringId != null && contractorId != null)
            {
                query = db.OrderJob.Where(x => x.contractorId == contractorId && x.SupervisorEngenneringId == SupervisorEngenneringId).ToList().FirstOrDefault();
            }
            var contractor = query.contractor.ARName;

            return Json(contractor, JsonRequestBehavior.AllowGet);
        }
    }
}