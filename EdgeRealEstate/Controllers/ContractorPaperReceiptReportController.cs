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
    public class ContractorPaperReceiptReportController : Controller
    {
        // GET: ContractorPaperReceiptReport
        ApplicationDbContext db = new ApplicationDbContext();
        private ContractorPaperReceiptReportService ContractorPaperReceiptReportService;
        public ContractorPaperReceiptReportController()
        {
            this.ContractorPaperReceiptReportService = new ContractorPaperReceiptReportService(db);
        }
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult EditingCustom_Read([DataSourceRequest] DataSourceRequest request,
           DateTime? FromDate, DateTime? toDate
            //, int? FromCust, int? toCust
            //, int? FromSalesMan, int? toSalesMan
            )
        {

            return Json(ContractorPaperReceiptReportService.Read(FromDate, toDate
                //, FromCust, toCust, FromSalesMan, toSalesMan
                )
                .ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public ActionResult Download_PDF(DateTime? FromDate, DateTime? toDate)
        {
            ReportDocument rd = new ReportDocument();

            rd.Load(Path.Combine(Server.MapPath("~/Reports"), "ContractorPaperReceiptReport.rpt"));
            if (FromDate == null)
            {
                var result = (from i in db.contractorPaperReceipts
                              where i.isDeleted == false && i.indate <= toDate
                              select new contractorPaperReceiptViewModel
                              {
                                  indate = (DateTime)i.indate,
                                  contractorId = (int)i.contractorId,
                                  paid = (decimal)i.paid,
                                  contractorName = i.contractor.ARName,
                                  salesManName = i.Employee1.ARName,
                              }).ToList();

                rd.SetDataSource(result);
            }
            if (toDate == null)
            {
                var result = (from i in db.contractorPaperReceipts
                              where i.isDeleted == false && i.indate >= FromDate
                              select new contractorPaperReceiptViewModel
                              {
                                  indate = (DateTime)i.indate,
                                  contractorId = (int)i.contractorId,
                                  paid = (decimal)i.paid,
                                  contractorName = i.contractor.ARName,
                                  salesManName = i.Employee1.ARName,
                              }).ToList();

                rd.SetDataSource(result);
            }
            if (FromDate == null && toDate == null)
            {
                var result = (from i in db.contractorPaperReceipts
                              where i.isDeleted == false
                              select new contractorPaperReceiptViewModel
                              {
                                  indate = (DateTime)i.indate,
                                  contractorId = (int)i.contractorId,
                                  paid = (decimal)i.paid,
                                  contractorName = i.contractor.ARName,
                                  salesManName = i.Employee1.ARName,
                              }).ToList();

                rd.SetDataSource(result);
            }
            if (FromDate != null && toDate != null)
            {
                var result = (from i in db.contractorPaperReceipts
                              where i.isDeleted == false && i.indate >= FromDate && i.indate <= toDate
                              select new contractorPaperReceiptViewModel
                              {
                                  indate = (DateTime)i.indate,
                                  contractorId = (int)i.contractorId,
                                  paid = (decimal)i.paid,
                                  contractorName = i.contractor.ARName,
                                  salesManName = i.Employee1.ARName,
                              }).ToList();

                rd.SetDataSource(result);

            }
            string date1val, date2val;

            if (FromDate == null)
            {
                date1val = "لم يتم تحديد تاريخ";
            }
            else
            {
                date1val = FromDate.Value.ToShortDateString();
            }
            if (toDate == null)
            {
                date2val = "لم يتم تحديد تاريخ";
            }
            else
            {
                date2val = FromDate.Value.ToShortDateString();
            }
            rd.SetParameterValue("FromDate", date1val);
            rd.SetParameterValue("toDate", date2val);


            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();

            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);

            return File(stream, "application/pdf", "تقرير سندات صرف نقدية مقاول.pdf");
        }

        //public JsonResult ServerFiltering_GetCustomers(string text)
        //{
        //    var dataContext = new POSDbContext();

        //    var Customers = dataContext.Customers.Where(x => x.Type.Equals(false))
        //        .Select(i => new CustomerViewModel
        //        {
        //            Id = i.Id,
        //            ARName = i.ARName,
        //        });

        //    if (!string.IsNullOrEmpty(text))
        //    {
        //        Customers = Customers.Where(p => p.ARName.Contains(text));
        //    }

        //    return Json(Customers, JsonRequestBehavior.AllowGet);
        //}
        //public JsonResult ServerFiltering_GetSalesMan(string text)
        //{
        //    var dataContext = new POSDbContext();

        //    var Employees = dataContext.Employees
        //        .Select(i => new EmployeeViewModel
        //        {
        //            Id = i.Id,
        //            ARName = i.ARName,
        //        });

        //    if (!string.IsNullOrEmpty(text))
        //    {
        //        Employees = Employees.Where(p => p.ARName.Contains(text));
        //    }

        //    return Json(Employees, JsonRequestBehavior.AllowGet);
        //}

        //        public ActionResult ShowList(int? FromCust, int? toCust
        //            , DateTime? FromDate, DateTime? toDate, int? FromSalesMan, int? toSalesMan)
        //        {

        //            ConnectionInfo crconnectioninfo = new ConnectionInfo();
        //            ReportDocument cryrpt = new ReportDocument();
        //            TableLogOnInfos crtablelogoninfos = new TableLogOnInfos();
        //            TableLogOnInfo crtablelogoninfo = new TableLogOnInfo();

        //            Tables CrTables;

        //            crconnectioninfo.ServerName = "EDGETEST";
        //            crconnectioninfo.DatabaseName = "PointOfSaleAppDB";
        //            crconnectioninfo.UserID = "team3";
        //            crconnectioninfo.Password = "123456";
        //            ///////////////////////
        //            DateTime vFromDate = new DateTime(DateTime.Now.Year, 1, 1);
        //            var result = Session["CustomerMove"] as IList<PaperReceiptViewModel>;
        //            if (FromDate == null && toDate == null && FromCust == null && toCust == null
        //                             && FromSalesMan == null && toSalesMan == null)
        //            {
        //                result = (from i in db.PaperReceipts
        //                          where i.isDeleted == false
        //                          select new PaperReceiptViewModel
        //                          {
        //                              indate = (DateTime)i.indate,
        //                              customerId = (int)i.customerId,
        //                              custName = i.Customer.ARName,
        //                              salesManId = (int)i.salesManId,
        //                              salesManName = i.Employee1.ARName,
        //                              paid = (decimal)i.paid,
        //                              FromDate = vFromDate,
        //                              toDate = DateTime.Now
        //                          }).ToList();
        //            }
        //            else if (FromDate != null && toDate != null && FromCust != null
        //            && toCust != null && FromSalesMan != null && toSalesMan != null)
        //            {
        //                result = (from i in db.PaperReceipts
        //                          where i.isDeleted == false
        //                          && i.customerId >= FromCust && i.customerId <= toCust
        //                          && i.indate >= FromDate && i.indate <= toDate
        //                          && i.salesManId >= FromSalesMan && i.salesManId <= toSalesMan
        //                          select new PaperReceiptViewModel
        //                          {
        //                              indate = (DateTime)i.indate,
        //                              customerId = (int)i.customerId,
        //                              salesManId = (int)i.salesManId,
        //                              paid = (decimal)i.paid,
        //                              custName = i.Customer.ARName,
        //                              salesManName = i.Employee1.ARName,
        //                              FromDate = (DateTime)FromDate,
        //                              toDate = (DateTime)toDate
        //                          }).ToList();
        //            }
        //            else if (FromDate != null && toDate != null && FromCust == null
        //          && toCust == null && FromSalesMan == null && toSalesMan == null)
        //            {
        //                result = (from i in db.PaperPayments
        //                          where i.isDeleted == false
        //                          && i.indate >= FromDate && i.indate <= toDate
        //                          select new PaperReceiptViewModel
        //                          {
        //                              indate = (DateTime)i.indate,
        //                              customerId = (int)i.customerId,
        //                              salesManId = (int)i.salesManId,
        //                              paid = (decimal)i.paid,
        //                              custName = i.Customer.ARName,
        //                              salesManName = i.Employee1.ARName,
        //                              FromDate = (DateTime)FromDate,
        //                              toDate = (DateTime)toDate
        //                          }).ToList();
        //            }
        //            else if (FromDate != null && toDate != null && FromCust != null
        //      && toCust != null && FromSalesMan == null && toSalesMan == null)
        //            {
        //                result = (from i in db.PaperPayments
        //                          where i.isDeleted == false
        //                          && i.customerId >= FromCust && i.customerId <= toCust
        //                          && i.indate >= FromDate && i.indate <= toDate
        //                          select new PaperReceiptViewModel
        //                          {
        //                              indate = (DateTime)i.indate,
        //                              customerId = (int)i.customerId,
        //                              salesManId = (int)i.salesManId,
        //                              paid = (decimal)i.paid,
        //                              custName = i.Customer.ARName,
        //                              salesManName = i.Employee1.ARName,
        //                              FromDate = (DateTime)FromDate,
        //                              toDate = (DateTime)toDate
        //                          }).ToList();
        //            }
        //            else if (FromDate == null && toDate == null && FromCust != null
        //&& toCust != null && FromSalesMan == null && toSalesMan == null)
        //            {
        //                result = (from i in db.PaperPayments
        //                          where i.isDeleted == false
        //                          && i.customerId >= FromCust && i.customerId <= toCust
        //                          select new PaperReceiptViewModel
        //                          {
        //                              indate = (DateTime)i.indate,
        //                              customerId = (int)i.customerId,
        //                              salesManId = (int)i.salesManId,
        //                              paid = (decimal)i.paid,
        //                              custName = i.Customer.ARName,
        //                              salesManName = i.Employee1.ARName,
        //                              FromDate = vFromDate,
        //                              toDate = DateTime.Now
        //                          }).ToList();
        //            }
        //            else if (FromDate == null && toDate == null && FromCust == null
        //&& toCust == null && FromSalesMan != null && toSalesMan != null)
        //            {
        //                result = (from i in db.PaperPayments
        //                          where i.isDeleted == false
        //                          && i.salesManId >= FromSalesMan && i.salesManId <= toSalesMan
        //                          select new PaperReceiptViewModel
        //                          {
        //                              indate = (DateTime)i.indate,
        //                              customerId = (int)i.customerId,
        //                              salesManId = (int)i.salesManId,
        //                              paid = (decimal)i.paid,
        //                              custName = i.Customer.ARName,
        //                              salesManName = i.Employee1.ARName,
        //                              FromDate = vFromDate,
        //                              toDate = DateTime.Now
        //                          }).ToList();
        //            }
        //            else if (FromDate != null && toDate != null && FromCust == null
        //            && toCust == null && FromSalesMan != null && toSalesMan != null)
        //            {
        //                result = (from i in db.PaperPayments
        //                          where i.isDeleted == false
        //                          && i.indate >= FromDate && i.indate <= toDate
        //                          && i.salesManId >= FromSalesMan && i.salesManId <= toSalesMan
        //                          select new PaperReceiptViewModel
        //                          {
        //                              indate = (DateTime)i.indate,
        //                              customerId = (int)i.customerId,
        //                              salesManId = (int)i.salesManId,
        //                              paid = (decimal)i.paid,
        //                              custName = i.Customer.ARName,
        //                              salesManName = i.Employee1.ARName,
        //                              FromDate = (DateTime)FromDate,
        //                              toDate = (DateTime)toDate
        //                          }).ToList();
        //            }
        //            else if (FromDate == null && toDate == null && FromCust != null
        //&& toCust != null && FromSalesMan != null && toSalesMan != null)
        //            {
        //                result = (from i in db.PaperPayments
        //                          where i.isDeleted == false
        //                          && i.customerId >= FromCust && i.customerId <= toCust
        //                          && i.salesManId >= FromSalesMan && i.salesManId <= toSalesMan
        //                          select new PaperReceiptViewModel
        //                          {
        //                              indate = (DateTime)i.indate,
        //                              customerId = (int)i.customerId,
        //                              salesManId = (int)i.salesManId,
        //                              paid = (decimal)i.paid,
        //                              custName = i.Customer.ARName,
        //                              salesManName = i.Employee1.ARName,
        //                              FromDate = vFromDate,
        //                              toDate = DateTime.Now
        //                          }).ToList();
        //            }

        //            ///////////////////////////

        //            cryrpt.Load(Server.MapPath("~/Reports/PaperReceipt.rpt"));
        //            CrTables = cryrpt.Database.Tables;

        //            foreach (CrystalDecisions.CrystalReports.Engine.Table CrTable in CrTables)
        //            {
        //                crtablelogoninfo = CrTable.LogOnInfo;
        //                crtablelogoninfo.ConnectionInfo = crconnectioninfo;
        //                CrTable.ApplyLogOnInfo(crtablelogoninfo);
        //            }
        //            cryrpt.SetDataSource(result);
        //            Stream s = cryrpt.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
        //            return File(s, "application/pdf");
        //        }
    }
}