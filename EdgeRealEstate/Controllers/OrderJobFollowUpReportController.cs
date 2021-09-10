using EdgeRealEstate.Entities;
using EdgeRealEstate.Models;
using EdgeRealEstate.Models.Services;
using EdgeRealEstate.Models.ViewModels;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EdgeRealEstate.Controllers
{
    public class OrderJobFollowUpReportController : Controller
    {
        // GET: OrderJobFollowUpReport
        ApplicationDbContext db = new ApplicationDbContext();
        private OrderJobFollowUpReportService OrderJobFollowUpReportService;
        public OrderJobFollowUpReportController()
        {
            this.OrderJobFollowUpReportService = new OrderJobFollowUpReportService(db);
        }
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Index2()
        {
            return View();
        }
        public ActionResult Index3()
        {
            return View();
        }
        public ActionResult HierarchyBinding_OrderJob([DataSourceRequest] DataSourceRequest request, int? Code, int? Project, int? SalesMan, int? Cust)
        {
            int? Id = Code;
            int? MainProjectId = Project;
            int? SupervisorEngenneringId = SalesMan;
            int? contractorId = Cust;

            List<OrderJobFUVM> result = new List<OrderJobFUVM>();

            if (Code == null && Project == null && Cust == null && SalesMan == null)
            {
                result = (from i in db.OrderJobDetails
                          join m in db.OrderJob
                          on i.OrderJobId equals m.Id
                          where m.IsDeleted == false
                          select new OrderJobFUVM
                          {
                              Id = m.Id,ContractorId = m.contractorId,
                              Contractor = m.contractor.ARName,
                              SupervisorEngenneringId = m.SupervisorEngenneringId,
                              SupervisorEngennering = m.Engennering.Aname,
                              Code = m.OrderJobCode,
                              MainProjectId = m.MainProjectId,
                              MainProject = m.MainProject.Aname,
                              Project = m.Projects.Aname,
                              ProjectId = m.Projects.id,
                              SpecialNote = m.SpecialNote,
                              Note = m.Note,
                          }).ToList();
            }
            else if (Id != null && MainProjectId == null && SupervisorEngenneringId == null && contractorId == null)
            {
                result = (from i in db.OrderJobDetails
                          join m in db.OrderJob
                          on i.OrderJobId equals m.Id
                          where m.IsDeleted == false && m.Id == Id
                          select new OrderJobFUVM
                          {
                              Id = m.Id,ContractorId = m.contractorId,
                              Contractor = m.contractor.ARName,
                              SupervisorEngenneringId = m.SupervisorEngenneringId,
                              SupervisorEngennering = m.Engennering.Aname,
                              Code = m.OrderJobCode,
                              MainProjectId = m.MainProjectId,
                              MainProject = m.MainProject.Aname,
                              Project = m.Projects.Aname,
                              ProjectId = m.Projects.id,
                              SpecialNote = m.SpecialNote,
                              Note = m.Note,
                          }).ToList();
            }
            else if (Id == null && MainProjectId != null && SupervisorEngenneringId == null && contractorId == null)
            {
                result = (from i in db.OrderJobDetails
                          join m in db.OrderJob
                          on i.OrderJobId equals m.Id
                          where m.IsDeleted == false && m.MainProjectId == MainProjectId
                          select new OrderJobFUVM
                          {
                              Id = m.Id,ContractorId = m.contractorId,
                              Contractor = m.contractor.ARName,
                              SupervisorEngenneringId = m.SupervisorEngenneringId,
                              SupervisorEngennering = m.Engennering.Aname,
                              Code = m.OrderJobCode,
                              MainProjectId = m.MainProjectId,
                              MainProject = m.MainProject.Aname,
                              Project = m.Projects.Aname,
                              ProjectId = m.Projects.id,
                              SpecialNote = m.SpecialNote,
                              Note = m.Note,
                          }).ToList();
            }
            else if (Id == null && MainProjectId == null && SupervisorEngenneringId != null && contractorId == null)
            {
                result = (from i in db.OrderJobDetails
                          join m in db.OrderJob
                          on i.OrderJobId equals m.Id
                          where m.IsDeleted == false && m.SupervisorEngenneringId == SupervisorEngenneringId
                          select new OrderJobFUVM
                          {
                              Id = m.Id,ContractorId = m.contractorId,
                              Contractor = m.contractor.ARName,
                              SupervisorEngenneringId = m.SupervisorEngenneringId,
                              SupervisorEngennering = m.Engennering.Aname,
                              Code = m.OrderJobCode,
                              MainProjectId = m.MainProjectId,
                              MainProject = m.MainProject.Aname,
                              Project = m.Projects.Aname,
                              ProjectId = m.Projects.id,
                              SpecialNote = m.SpecialNote,
                              Note = m.Note,
                          }).ToList();
            }
            else if (Id == null && MainProjectId == null && SupervisorEngenneringId == null && contractorId != null)
            {
                result = (from i in db.OrderJobDetails
                          join m in db.OrderJob
                          on i.OrderJobId equals m.Id
                          where m.IsDeleted == false && m.contractorId == contractorId
                          select new OrderJobFUVM
                          {
                              Id = m.Id,ContractorId = m.contractorId,
                              Contractor = m.contractor.ARName,
                              SupervisorEngenneringId = m.SupervisorEngenneringId,
                              SupervisorEngennering = m.Engennering.Aname,
                              Code = m.OrderJobCode,
                              MainProjectId = m.MainProjectId,
                              MainProject = m.MainProject.Aname,
                              Project = m.Projects.Aname,
                              ProjectId = m.Projects.id,
                              SpecialNote = m.SpecialNote,
                              Note = m.Note,
                          }).ToList();
            }
            else if (Id != null && MainProjectId != null && SupervisorEngenneringId == null && contractorId == null)
            {
                result = (from i in db.OrderJobDetails
                          join m in db.OrderJob
                          on i.OrderJobId equals m.Id
                          where m.IsDeleted == false && m.Id == Id && m.MainProjectId == MainProjectId
                          select new OrderJobFUVM
                          {
                              Id = m.Id,ContractorId = m.contractorId,
                              Contractor = m.contractor.ARName,
                              SupervisorEngenneringId = m.SupervisorEngenneringId,
                              SupervisorEngennering = m.Engennering.Aname,
                              Code = m.OrderJobCode,
                              MainProjectId = m.MainProjectId,
                              MainProject = m.MainProject.Aname,
                              Project = m.Projects.Aname,
                              ProjectId = m.Projects.id,
                              SpecialNote = m.SpecialNote,
                              Note = m.Note,
                          }).ToList();
            }
            else if (Id != null && MainProjectId == null && SupervisorEngenneringId != null && contractorId == null)
            {
                result = (from i in db.OrderJobDetails
                          join m in db.OrderJob
                          on i.OrderJobId equals m.Id
                          where m.IsDeleted == false && m.Id == Id && m.SupervisorEngenneringId == SupervisorEngenneringId
                          select new OrderJobFUVM
                          {
                              Id = m.Id,ContractorId = m.contractorId,
                              Contractor = m.contractor.ARName,
                              SupervisorEngenneringId = m.SupervisorEngenneringId,
                              SupervisorEngennering = m.Engennering.Aname,
                              Code = m.OrderJobCode,
                              MainProjectId = m.MainProjectId,
                              MainProject = m.MainProject.Aname,
                              Project = m.Projects.Aname,
                              ProjectId = m.Projects.id,
                              SpecialNote = m.SpecialNote,
                              Note = m.Note,
                          }).ToList();
            }
            else if (Id != null && MainProjectId == null && SupervisorEngenneringId == null && contractorId != null)
            {
                result = (from i in db.OrderJobDetails
                          join m in db.OrderJob
                          on i.OrderJobId equals m.Id
                          where m.IsDeleted == false && m.MainProjectId == MainProjectId && m.contractorId == contractorId
                          select new OrderJobFUVM
                          {
                              Id = m.Id,ContractorId = m.contractorId,
                              Contractor = m.contractor.ARName,
                              SupervisorEngenneringId = m.SupervisorEngenneringId,
                              SupervisorEngennering = m.Engennering.Aname,
                              Code = m.OrderJobCode,
                              MainProjectId = m.MainProjectId,
                              MainProject = m.MainProject.Aname,
                              Project = m.Projects.Aname,
                              ProjectId = m.Projects.id,
                              SpecialNote = m.SpecialNote,
                              Note = m.Note,
                          }).ToList();
            }
            else if (Id != null && MainProjectId != null && SupervisorEngenneringId != null && contractorId == null)
            {
                result = (from i in db.OrderJobDetails
                          join m in db.OrderJob
                          on i.OrderJobId equals m.Id
                          where m.IsDeleted == false && m.Id == Id && m.MainProjectId == MainProjectId && m.SupervisorEngenneringId == SupervisorEngenneringId
                          select new OrderJobFUVM
                          {
                              Id = m.Id,ContractorId = m.contractorId,
                              Contractor = m.contractor.ARName,
                              SupervisorEngenneringId = m.SupervisorEngenneringId,
                              SupervisorEngennering = m.Engennering.Aname,
                              Code = m.OrderJobCode,
                              MainProjectId = m.MainProjectId,
                              MainProject = m.MainProject.Aname,
                              Project = m.Projects.Aname,
                              ProjectId = m.Projects.id,
                              SpecialNote = m.SpecialNote,
                              Note = m.Note,
                          }).ToList();
            }
            else if (Id != null && MainProjectId != null && SupervisorEngenneringId == null && contractorId != null)
            {
                result = (from i in db.OrderJobDetails
                          join m in db.OrderJob
                          on i.OrderJobId equals m.Id
                          where m.IsDeleted == false && m.Id == Id && m.MainProjectId == MainProjectId && m.contractorId == contractorId
                          select new OrderJobFUVM
                          {
                              Id = m.Id,ContractorId = m.contractorId,
                              Contractor = m.contractor.ARName,
                              SupervisorEngenneringId = m.SupervisorEngenneringId,
                              SupervisorEngennering = m.Engennering.Aname,
                              Code = m.OrderJobCode,
                              MainProjectId = m.MainProjectId,
                              MainProject = m.MainProject.Aname,
                              Project = m.Projects.Aname,
                              ProjectId = m.Projects.id,
                              SpecialNote = m.SpecialNote,
                              Note = m.Note,
                          }).ToList();
            }
            else if (Id != null && MainProjectId != null && SupervisorEngenneringId != null && contractorId != null)
            {
                result = (from i in db.OrderJobDetails
                          join m in db.OrderJob
                          on i.OrderJobId equals m.Id
                          where m.IsDeleted == false && m.Id == Id && m.MainProjectId == MainProjectId && m.SupervisorEngenneringId == SupervisorEngenneringId && m.contractorId == contractorId
                          select new OrderJobFUVM
                          {
                              Id = m.Id,ContractorId = m.contractorId,
                              Contractor = m.contractor.ARName,
                              SupervisorEngenneringId = m.SupervisorEngenneringId,
                              SupervisorEngennering = m.Engennering.Aname,
                              Code = m.OrderJobCode,
                              MainProjectId = m.MainProjectId,
                              MainProject = m.MainProject.Aname,
                              Project = m.Projects.Aname,
                              ProjectId = m.Projects.id,
                              SpecialNote = m.SpecialNote,
                              Note = m.Note,
                          }).ToList();
            }
            else if (Id != null && MainProjectId == null && SupervisorEngenneringId != null && contractorId != null)
            {
                result = (from i in db.OrderJobDetails
                          join m in db.OrderJob
                          on i.OrderJobId equals m.Id
                          where m.IsDeleted == false && m.Id == Id && m.SupervisorEngenneringId == SupervisorEngenneringId && m.contractorId == contractorId
                          select new OrderJobFUVM
                          {
                              Id = m.Id,ContractorId = m.contractorId,
                              Contractor = m.contractor.ARName,
                              SupervisorEngenneringId = m.SupervisorEngenneringId,
                              SupervisorEngennering = m.Engennering.Aname,
                              Code = m.OrderJobCode,
                              MainProjectId = m.MainProjectId,
                              MainProject = m.MainProject.Aname,
                              Project = m.Projects.Aname,
                              ProjectId = m.Projects.id,
                              SpecialNote = m.SpecialNote,
                              Note = m.Note,
                          }).ToList();
            }
            else if (Id == null && MainProjectId != null && SupervisorEngenneringId != null && contractorId == null)
            {
                result = (from i in db.OrderJobDetails
                          join m in db.OrderJob
                          on i.OrderJobId equals m.Id
                          where m.IsDeleted == false && m.SupervisorEngenneringId == SupervisorEngenneringId 
                          select new OrderJobFUVM
                          {
                              Id = m.Id,ContractorId = m.contractorId,
                              Contractor = m.contractor.ARName,
                              SupervisorEngenneringId = m.SupervisorEngenneringId,
                              SupervisorEngennering = m.Engennering.Aname,
                              Code = m.OrderJobCode,
                              MainProjectId = m.MainProjectId,
                              MainProject = m.MainProject.Aname,
                              Project = m.Projects.Aname,
                              ProjectId = m.Projects.id,
                              SpecialNote = m.SpecialNote,
                              Note = m.Note,
                          }).ToList();
            }
            else if (Id == null && MainProjectId != null && SupervisorEngenneringId == null && contractorId != null)
            {
                result = (from i in db.OrderJobDetails
                          join m in db.OrderJob
                          on i.OrderJobId equals m.Id
                          where m.IsDeleted == false && m.MainProjectId == MainProjectId && m.contractorId == contractorId
                          select new OrderJobFUVM
                          {
                              Id = m.Id,ContractorId = m.contractorId,
                              Contractor = m.contractor.ARName,
                              SupervisorEngenneringId = m.SupervisorEngenneringId,
                              SupervisorEngennering = m.Engennering.Aname,
                              Code = m.OrderJobCode,
                              MainProjectId = m.MainProjectId,
                              MainProject = m.MainProject.Aname,
                              Project = m.Projects.Aname,
                              ProjectId = m.Projects.id,
                              SpecialNote = m.SpecialNote,
                              Note = m.Note,
                          }).ToList();
            }
            else if (Id == null && MainProjectId != null && SupervisorEngenneringId != null && contractorId != null)
            {
                result = (from i in db.OrderJobDetails
                          join m in db.OrderJob
                          on i.OrderJobId equals m.Id
                          where m.IsDeleted == false && m.MainProjectId == MainProjectId && m.SupervisorEngenneringId == SupervisorEngenneringId && m.contractorId == contractorId
                          select new OrderJobFUVM
                          {
                              Id = m.Id,ContractorId = m.contractorId,
                              Contractor = m.contractor.ARName,
                              SupervisorEngenneringId = m.SupervisorEngenneringId,
                              SupervisorEngennering = m.Engennering.Aname,
                              Code = m.OrderJobCode,
                              MainProjectId = m.MainProjectId,
                              MainProject = m.MainProject.Aname,
                              Project = m.Projects.Aname,
                              ProjectId = m.Projects.id,
                              SpecialNote = m.SpecialNote,
                              Note = m.Note,
                          }).ToList();
            }
            else if (Id == null && MainProjectId == null && SupervisorEngenneringId != null && contractorId != null)
            {
                result = (from i in db.OrderJobDetails
                          join m in db.OrderJob
                          on i.OrderJobId equals m.Id
                          where m.IsDeleted == false && m.SupervisorEngenneringId == SupervisorEngenneringId && m.contractorId == contractorId
                          select new OrderJobFUVM
                          {
                              Id = m.Id,ContractorId = m.contractorId,
                              Contractor = m.contractor.ARName,
                              SupervisorEngenneringId = m.SupervisorEngenneringId,
                              SupervisorEngennering = m.Engennering.Aname,
                              Code = m.OrderJobCode,
                              MainProjectId = m.MainProjectId,
                              MainProject = m.MainProject.Aname,
                              Project = m.Projects.Aname,
                              ProjectId = m.Projects.id,
                              SpecialNote = m.SpecialNote,
                              Note = m.Note,
                          }).ToList();
            }
            return Json(result.ToDataSourceResult(request));

        }
        public ActionResult HierarchyBinding_OrderJobFollow(int? Id, [DataSourceRequest] DataSourceRequest request)
        {

        var result = db.OrderJobDetails
            .Where(x => x.OrderJobId == Id && x.IsDeleted == false)
            .Select(IRD => new OrderJobDetailsGridVM
            {
                Id = IRD.Id,
                Cost = IRD.Cost,
                ExecutionTime = IRD.ExecutionTime,
                Note = IRD.Note,
                BeginDateExpected = IRD.BeginDateExpected.ToString(),
                BeginDateAcutely = IRD.BeginDateAcutely.ToString(),
                EndDateAcutely = IRD.EndDateAcutely.ToString(),
                EndDateExpected = IRD.EndDateExpected.ToString(),
                CompletionRate = IRD.CompletionRate,
                StageName = IRD.Stage.Stage.Aname,
                StageId = IRD.Stage.Stage.id,
                MainIttemName = IRD.MainItemDetail.MainItem.Aname,
                MainIttemID = IRD.MainItemDetail.MainItem.Id,

            }).ToList(); 
            return Json(result.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);

        }
        public ActionResult EditingCustom_Read([DataSourceRequest] DataSourceRequest request,
            int? FromCust, int? toCust
            //, DateTime? FromDate, DateTime? toDate
            , int? FromProject, int? toProject
            , int? FromSalesMan, int? toSalesMan
            , int? FrombillId, int? tobillId)
        {

            return Json(OrderJobFollowUpReportService.Read(FromCust, toCust,
                //FromDate, toDate,
                FromProject, toProject,
                FromSalesMan, toSalesMan
            , FrombillId, tobillId)
                .ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }
        public ActionResult EditingCustom_Read2([DataSourceRequest] DataSourceRequest request,int? Cust, int? Project, int? SalesMan)
        {
            List<OrderJobDetailsViewModel> result = new List<OrderJobDetailsViewModel>();
            if (Project == null && Cust == null && SalesMan == null)
            {
                result = (from i in db.OrderJobDetails
                          join m in db.OrderJob
                          on i.OrderJobId equals m.Id
                          where m.IsDeleted == false
                          select new OrderJobDetailsViewModel
                          {
                              contractorId = m.contractorId,
                              contractorName = m.contractor.ARName,
                              SupervisorEngenneringId = m.SupervisorEngenneringId,
                              SupervisorEngenneringName = m.Engennering.Aname,
                              OrderJobId = i.OrderJobId,
                              MainProjectId = m.MainProjectId,
                              MainProjectName = m.MainProject.Aname,
                          }).ToList();
            }

            return Json(result.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }
        public JsonResult ServerFiltering_GetCustomers(string text)
        {
            var dataContext = new ApplicationDbContext();

            var Customers = dataContext.contractor
                .Select(i => new contractorViewModel
                {
                    Id = i.Id,
                    ARName = i.ARName,
                });

            if (!string.IsNullOrEmpty(text))
            {
                Customers = Customers.Where(p => p.ARName.Contains(text));
            }

            return Json(Customers, JsonRequestBehavior.AllowGet);
        }
        public JsonResult ServerFiltering_GetSalesMan(string text)
        {
            var dataContext = new ApplicationDbContext();

            var Employees = dataContext.Engennerings
                .Select(i => new EngenneringViewModel
                {
                    Id = i.id,
                    ARName = i.Aname,
                });

            if (!string.IsNullOrEmpty(text))
            {
                Employees = Employees.Where(p => p.ARName.Contains(text));
            }

            return Json(Employees, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ServerFiltering_GetMainProject(string text)
        {
            var dataContext = new ApplicationDbContext();

            var Customers = dataContext.MainProjects
                .Select(i => new MainProjectsViewModel
                {
                    id = i.id,
                    Aname = i.Aname,
                });

            if (!string.IsNullOrEmpty(text))
            {
                Customers = Customers.Where(p => p.Aname.Contains(text));
            }

            return Json(Customers, JsonRequestBehavior.AllowGet);
        }
    }
}