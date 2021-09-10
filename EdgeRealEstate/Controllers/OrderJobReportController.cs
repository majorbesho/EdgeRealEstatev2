using CrystalDecisions.CrystalReports.Engine;
using EdgeRealEstate.Models;
using EdgeRealEstate.Models.Services;
using EdgeRealEstate.Models.ViewModels;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EdgeRealEstate.Controllers
{
    public class OrderJobReportController : Controller
    {
        // GET: OrderJobReport
        ApplicationDbContext db = new ApplicationDbContext();
        private OrderJobReportService OrderJobReportService;
        public OrderJobReportController()
        {
            this.OrderJobReportService = new OrderJobReportService(db);
        }
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Index2()
        {
            return View();
        }
        public ActionResult EditingCustom_Read([DataSourceRequest] DataSourceRequest request,
            int? FromCust, int? toCust
            //, DateTime? FromDate, DateTime? toDate
            , int? FromProject, int? toProject
            , int? FromSalesMan, int? toSalesMan
            , int? FrombillId, int? tobillId)
        {

            return Json(OrderJobReportService.Read(FromCust, toCust,
                //FromDate, toDate,
                FromProject , toProject,
                FromSalesMan, toSalesMan
            , FrombillId, tobillId)
                .ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }
       public ActionResult EditingCustom_Read2([DataSourceRequest] DataSourceRequest request,int? Cust, int? Project, int? SalesMan, int? FrombillId)
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
            else if (Project != null && Cust == null && SalesMan == null){
                result = (from i in db.OrderJobDetails
                          join m in db.OrderJob
                          on i.OrderJobId equals m.Id
                          where m.IsDeleted == false && m.MainProjectId == Project
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
            else if (Project == null && Cust != null && SalesMan == null)
            {
                result = (from i in db.OrderJobDetails
                          join m in db.OrderJob
                          on i.OrderJobId equals m.Id
                          where m.IsDeleted == false && m.contractorId == Cust
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
            else if (Project == null && Cust == null && SalesMan != null)
            {
                result = (from i in db.OrderJobDetails
                          join m in db.OrderJob
                          on i.OrderJobId equals m.Id
                          where m.IsDeleted == false && m.SupervisorEngenneringId == SalesMan
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
            else if (Project != null && Cust != null && SalesMan == null)
            {
                result = (from i in db.OrderJobDetails
                          join m in db.OrderJob
                          on i.OrderJobId equals m.Id
                          where m.IsDeleted == false && m.contractorId == Cust && m.MainProjectId == Project
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
            else if (Project != null && Cust == null && SalesMan != null)
            {
                result = (from i in db.OrderJobDetails
                          join m in db.OrderJob
                          on i.OrderJobId equals m.Id
                          where m.IsDeleted == false && m.MainProjectId == Project && m.SupervisorEngenneringId == SalesMan
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
            else if (Project != null && Cust != null && SalesMan != null)
            {
                result = (from i in db.OrderJobDetails
                          join m in db.OrderJob
                          on i.OrderJobId equals m.Id
                          where m.IsDeleted == false && m.contractorId == Cust && m.MainProjectId == Project && m.SupervisorEngenneringId == SalesMan
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
            else if (Project == null && Cust != null && SalesMan != null)
            {
                result = (from i in db.OrderJobDetails
                          join m in db.OrderJob
                          on i.OrderJobId equals m.Id
                          where m.IsDeleted == false && m.contractorId == Cust && m.SupervisorEngenneringId == SalesMan
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
            else if (Project != null && Cust != null && SalesMan != null)
            {
                result = (from i in db.OrderJobDetails
                          join m in db.OrderJob
                          on i.OrderJobId equals m.Id
                          where m.IsDeleted == false && m.contractorId == Cust && m.MainProjectId == Project && m.SupervisorEngenneringId == SalesMan
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
        public ActionResult Download_PDF(int? Cust, int? Project, int? SalesMan, int? FrombillId)
        {

            ReportDocument rd = new ReportDocument();

            rd.Load(Path.Combine(Server.MapPath("~/Reports"), "OrderJobDetailsReport.rpt"));

            if (Project == null && Cust == null && SalesMan == null)
            {
                var r = (from i in db.OrderJobDetails
                          join m in db.OrderJob
                          on i.OrderJobId equals m.Id
                          where m.IsDeleted == false
                          select new OrderJobDetailsVMReport
                          {
                              contractorName = m.contractor.ARName,
                              SupervisorEngenneringName = m.Engennering.Aname,
                              OrderJobId = i.OrderJobId.Value,
                              MainProjectName = m.MainProject.Aname,
                          }).ToList();

                rd.SetDataSource(r);
            }
            else if (Project != null && Cust == null && SalesMan == null)
            {
                var r = (from i in db.OrderJobDetails
                          join m in db.OrderJob
                          on i.OrderJobId equals m.Id
                          where m.IsDeleted == false && m.MainProjectId == Project
                          select new OrderJobDetailsVMReport
                          {
                              contractorName = m.contractor.ARName,
                              SupervisorEngenneringName = m.Engennering.Aname,
                              OrderJobId = i.OrderJobId.Value,
                              MainProjectName = m.MainProject.Aname,
                          }).ToList();
                rd.SetDataSource(r);
            }
            else if (Project == null && Cust != null && SalesMan == null)
            {
                var r = (from i in db.OrderJobDetails
                          join m in db.OrderJob
                          on i.OrderJobId equals m.Id
                          where m.IsDeleted == false && m.contractorId == Cust
                          select new OrderJobDetailsVMReport
                          {
                              contractorName = m.contractor.ARName,
                              SupervisorEngenneringName = m.Engennering.Aname,
                              OrderJobId = i.OrderJobId.Value,
                              MainProjectName = m.MainProject.Aname,
                          }).ToList();
                rd.SetDataSource(r);
            }
            else if (Project == null && Cust == null && SalesMan != null)
            {
                var r = (from i in db.OrderJobDetails
                          join m in db.OrderJob
                          on i.OrderJobId equals m.Id
                          where m.IsDeleted == false && m.SupervisorEngenneringId == SalesMan
                          select new OrderJobDetailsVMReport
                          {
                              contractorName = m.contractor.ARName,
                              SupervisorEngenneringName = m.Engennering.Aname,
                              OrderJobId = i.OrderJobId.Value,
                              MainProjectName = m.MainProject.Aname,
                          }).ToList();
                rd.SetDataSource(r);
            }
            else if (Project != null && Cust != null && SalesMan == null)
            {
                var r = (from i in db.OrderJobDetails
                          join m in db.OrderJob
                          on i.OrderJobId equals m.Id
                          where m.IsDeleted == false && m.contractorId == Cust && m.MainProjectId == Project
                          select new OrderJobDetailsVMReport
                          {
                              contractorName = m.contractor.ARName,
                              SupervisorEngenneringName = m.Engennering.Aname,
                              OrderJobId = i.OrderJobId.Value,
                              MainProjectName = m.MainProject.Aname,
                          }).ToList();
                rd.SetDataSource(r);
            }
            else if (Project != null && Cust == null && SalesMan != null)
            {
                var r = (from i in db.OrderJobDetails
                          join m in db.OrderJob
                          on i.OrderJobId equals m.Id
                          where m.IsDeleted == false && m.MainProjectId == Project && m.SupervisorEngenneringId == SalesMan
                          select new OrderJobDetailsVMReport
                          {
                              contractorName = m.contractor.ARName,
                              SupervisorEngenneringName = m.Engennering.Aname,
                              OrderJobId = i.OrderJobId.Value,
                              MainProjectName = m.MainProject.Aname,
                          }).ToList();
                rd.SetDataSource(r);
            }
            else if (Project != null && Cust != null && SalesMan != null)
            {
                var r = (from i in db.OrderJobDetails
                          join m in db.OrderJob
                          on i.OrderJobId equals m.Id
                          where m.IsDeleted == false && m.contractorId == Cust && m.MainProjectId == Project && m.SupervisorEngenneringId == SalesMan
                          select new OrderJobDetailsVMReport
                          {
                              contractorName = m.contractor.ARName,
                              SupervisorEngenneringName = m.Engennering.Aname,
                              OrderJobId = i.OrderJobId.Value,
                              MainProjectName = m.MainProject.Aname,
                          }).ToList();
                rd.SetDataSource(r);
            }
            else if (Project == null && Cust != null && SalesMan != null)
            {
                var r = (from i in db.OrderJobDetails
                          join m in db.OrderJob
                          on i.OrderJobId equals m.Id
                          where m.IsDeleted == false && m.contractorId == Cust && m.SupervisorEngenneringId == SalesMan
                          select new OrderJobDetailsVMReport
                          {
                              contractorName = m.contractor.ARName,
                              SupervisorEngenneringName = m.Engennering.Aname,
                              OrderJobId = i.OrderJobId.Value,
                              MainProjectName = m.MainProject.Aname,
                          }).ToList();
                rd.SetDataSource(r);
            }
            else if (Project != null && Cust != null && SalesMan != null)
            {
                var r = (from i in db.OrderJobDetails
                          join m in db.OrderJob
                          on i.OrderJobId equals m.Id
                          where m.IsDeleted == false && m.contractorId == Cust && m.MainProjectId == Project && m.SupervisorEngenneringId == SalesMan
                          select new OrderJobDetailsVMReport
                          {
                              contractorName = m.contractor.ARName,
                              SupervisorEngenneringName = m.Engennering.Aname,
                              OrderJobId = i.OrderJobId.Value,
                              MainProjectName = m.MainProject.Aname,
                          }).ToList();
                rd.SetDataSource(r);
            }

            string date1val ,
                   date2val ,
                   date3val ;

            if (db.MainProjects.SingleOrDefault(i => i.id == Project) == null)
            {
                date1val = "----";
            }
            else
            {
                date1val = db.MainProjects.SingleOrDefault(i => i.id == Project).Aname;
            }
            if (db.contractor.SingleOrDefault(i => i.Id == Cust) == null)
            {
                date2val = "----";
            }
            else
            {
                date2val = db.contractor.SingleOrDefault(i => i.Id == Cust).ARName;
            }
            if (db.Engennerings.SingleOrDefault(i => i.id == SalesMan) == null)
            {
                date3val = "----";
            }
            else
            {
                date3val = db.Engennerings.SingleOrDefault(i => i.id == SalesMan).Aname;
            }
            rd.SetParameterValue("ProjectName", date1val);
            rd.SetParameterValue("contractorName", date2val);
            rd.SetParameterValue("SupervisorEngenneringName", date3val);
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();

            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);

            return File(stream, "application/pdf", "تقرير اوامر الشغل.pdf");
        }
    }
}