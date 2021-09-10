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
    public class PurchaseReportController : Controller
    {
        // GET: PurchaseReport
        ApplicationDbContext db = new ApplicationDbContext();
        private PurchaseReportService PurchaseReportService;
        public PurchaseReportController()
        {
            this.PurchaseReportService = new PurchaseReportService(db);
        }
        public ActionResult Index()
        {
                return View();
        }
        public ActionResult EditingCustom_Read([DataSourceRequest] DataSourceRequest request,
            int? FromCust, int? toCust
            , DateTime? FromDate, DateTime? toDate, int? FromSalesMan, int? toSalesMan
            , int? FrombillId, int? tobillId)
        {

            return Json(PurchaseReportService.Read(FromCust, toCust, FromDate, toDate, FromSalesMan, toSalesMan
            , FrombillId, tobillId)
                .ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public JsonResult ServerFiltering_GetCustomers(string text)
        {
            var dataContext = new ApplicationDbContext();

            var Customers = dataContext.Customers
                .Select(i => new CustomerViewModel
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

            var Employees = dataContext.Employees
                .Select(i => new EmployeeViewModel
                {
                    Id = i.Id,
                    ARName = i.ARName,
                });

            if (!string.IsNullOrEmpty(text))
            {
                Employees = Employees.Where(p => p.ARName.Contains(text));
            }

            return Json(Employees, JsonRequestBehavior.AllowGet);
        }

//        public ActionResult ShowList(int? FromCust, int? toCust
//          , DateTime? FromDate, DateTime? toDate, int? FromSalesMan, int? toSalesMan
//          , int? FrombillId, int? tobillId)
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
//            var result = Session["CustomerMove"] as IList<ItemMove2ViewModel>;
//            if (FromDate == null && toDate == null && FromCust == null && toCust == null
//                    && FromSalesMan == null && toSalesMan == null && FrombillId == null
//                    && tobillId == null)
//            {
//                result = (from i in db.BillPurchasesDetails
//                          join m in db.BillPurchasesMasters
//                          on i.billId equals m.id
//                          where m.isDeleted == false
//                          group i by i.billId into items
//                          select new ItemMove2ViewModel
//                          {
//                              custId = (int)items.FirstOrDefault().BillPurchasesMaster.customersId,
//                              CustARName = items.FirstOrDefault().BillPurchasesMaster.Customer.ARName,
//                              salesManId = (int)items.FirstOrDefault().BillPurchasesMaster.salesManId,
//                              salesManARName = items.FirstOrDefault().BillPurchasesMaster.Employee1.ARName,
//                              addDate = (DateTime)items.FirstOrDefault().BillPurchasesMaster.addDate,
//                              billId = (int)items.Key,
//                              rowTotal = (decimal)items.Sum(x => x.Qu * x.price),
//                              tax = (decimal)items.Sum(x => x.tax),
//                              disNo = (decimal)(items.Sum(x => x.disNo)
//                              + items.FirstOrDefault().BillPurchasesMaster.disMoney),
//                              net = (decimal)items.FirstOrDefault().BillPurchasesMaster.net
//                          }).ToList();
//            }
//            else if (FromDate != null && toDate != null && FromCust == null && toCust == null
//               && FromSalesMan == null && toSalesMan == null && FrombillId == null
//               && tobillId == null)
//            {
//                result = (from i in db.BillPurchasesDetails
//                          join m in db.BillPurchasesMasters
//                          on i.billId equals m.id
//                          where m.isDeleted == false
//                          && m.addDate >= FromDate && m.addDate <= toDate
//                          group i by i.billId into items
//                          select new ItemMove2ViewModel
//                          {
//                              custId = (int)items.FirstOrDefault().BillPurchasesMaster.customersId,
//                              CustARName = items.FirstOrDefault().BillPurchasesMaster.Customer.ARName,
//                              salesManId = (int)items.FirstOrDefault().BillPurchasesMaster.salesManId,
//                              salesManARName = items.FirstOrDefault().BillPurchasesMaster.Employee1.ARName,
//                              addDate = (DateTime)items.FirstOrDefault().BillPurchasesMaster.addDate,
//                              billId = (int)items.Key,
//                              rowTotal = (decimal)items.Sum(x => x.Qu * x.price),
//                              tax = (decimal)items.Sum(x => x.tax),
//                              disNo = (decimal)(items.Sum(x => x.disNo)
//                              + items.FirstOrDefault().BillPurchasesMaster.disMoney),
//                              net = (decimal)items.FirstOrDefault().BillPurchasesMaster.net
//                          }).ToList();
//            }
//            else if (FromDate == null && toDate == null && FromCust != null && toCust != null
//               && FromSalesMan == null && toSalesMan == null && FrombillId == null
//               && tobillId == null)
//            {
//                result = (from i in db.BillPurchasesDetails
//                          join m in db.BillPurchasesMasters
//                          on i.billId equals m.id
//                          where m.isDeleted == false
//                          && m.customersId >= FromCust && m.customersId <= toCust
//                          group i by i.billId into items
//                          select new ItemMove2ViewModel
//                          {
//                              custId = (int)items.FirstOrDefault().BillPurchasesMaster.customersId,
//                              CustARName = items.FirstOrDefault().BillPurchasesMaster.Customer.ARName,
//                              salesManId = (int)items.FirstOrDefault().BillPurchasesMaster.salesManId,
//                              salesManARName = items.FirstOrDefault().BillPurchasesMaster.Employee1.ARName,
//                              addDate = (DateTime)items.FirstOrDefault().BillPurchasesMaster.addDate,
//                              billId = (int)items.Key,
//                              rowTotal = (decimal)items.Sum(x => x.Qu * x.price),
//                              tax = (decimal)items.Sum(x => x.tax),
//                              disNo = (decimal)(items.Sum(x => x.disNo)
//                              + items.FirstOrDefault().BillPurchasesMaster.disMoney),
//                              net = (decimal)items.FirstOrDefault().BillPurchasesMaster.net
//                          }).ToList();
//            }
//            else if (FromDate == null && toDate == null && FromCust == null && toCust == null
//             && FromSalesMan != null && toSalesMan != null && FrombillId == null
//             && tobillId == null)
//            {
//                result = (from i in db.BillPurchasesDetails
//                          join m in db.BillPurchasesMasters
//                          on i.billId equals m.id
//                          where m.isDeleted == false
//                          && m.salesManId >= FromSalesMan && m.salesManId <= toSalesMan
//                          group i by i.billId into items
//                          select new ItemMove2ViewModel
//                          {
//                              custId = (int)items.FirstOrDefault().BillPurchasesMaster.customersId,
//                              CustARName = items.FirstOrDefault().BillPurchasesMaster.Customer.ARName,
//                              salesManId = (int)items.FirstOrDefault().BillPurchasesMaster.salesManId,
//                              salesManARName = items.FirstOrDefault().BillPurchasesMaster.Employee1.ARName,
//                              addDate = (DateTime)items.FirstOrDefault().BillPurchasesMaster.addDate,
//                              billId = (int)items.Key,
//                              rowTotal = (decimal)items.Sum(x => x.Qu * x.price),
//                              tax = (decimal)items.Sum(x => x.tax),
//                              disNo = (decimal)(items.Sum(x => x.disNo)
//                              + items.FirstOrDefault().BillPurchasesMaster.disMoney),
//                              net = (decimal)items.FirstOrDefault().BillPurchasesMaster.net
//                          }).ToList();
//            }
//            else if (FromDate == null && toDate == null && FromCust == null && toCust == null
//            && FromSalesMan == null && toSalesMan == null && FrombillId != null
//            && tobillId != null)
//            {
//                result = (from i in db.BillPurchasesDetails
//                          join m in db.BillPurchasesMasters
//                          on i.billId equals m.id
//                          where m.isDeleted == false
//                          && m.id >= FrombillId && m.id <= tobillId
//                          group i by i.billId into items
//                          select new ItemMove2ViewModel
//                          {
//                              custId = (int)items.FirstOrDefault().BillPurchasesMaster.customersId,
//                              CustARName = items.FirstOrDefault().BillPurchasesMaster.Customer.ARName,
//                              salesManId = (int)items.FirstOrDefault().BillPurchasesMaster.salesManId,
//                              salesManARName = items.FirstOrDefault().BillPurchasesMaster.Employee1.ARName,
//                              addDate = (DateTime)items.FirstOrDefault().BillPurchasesMaster.addDate,
//                              billId = (int)items.Key,
//                              rowTotal = (decimal)items.Sum(x => x.Qu * x.price),
//                              tax = (decimal)items.Sum(x => x.tax),
//                              disNo = (decimal)(items.Sum(x => x.disNo)
//                              + items.FirstOrDefault().BillPurchasesMaster.disMoney),
//                              net = (decimal)items.FirstOrDefault().BillPurchasesMaster.net
//                          }).ToList();
//            }
//            //////////
//            else if (FromDate != null && toDate != null && FromCust != null && toCust != null
//                && FromSalesMan == null && toSalesMan == null && FrombillId == null
//                && tobillId == null)
//            {
//                result = (from i in db.BillPurchasesDetails
//                          join m in db.BillPurchasesMasters
//                          on i.billId equals m.id
//                          where m.isDeleted == false
//                          && m.addDate >= FromDate && m.addDate <= toDate
//                          && m.customersId >= FromCust && m.customersId <= toCust
//                          group i by i.billId into items
//                          select new ItemMove2ViewModel
//                          {
//                              custId = (int)items.FirstOrDefault().BillPurchasesMaster.customersId,
//                              CustARName = items.FirstOrDefault().BillPurchasesMaster.Customer.ARName,
//                              salesManId = (int)items.FirstOrDefault().BillPurchasesMaster.salesManId,
//                              salesManARName = items.FirstOrDefault().BillPurchasesMaster.Employee1.ARName,
//                              addDate = (DateTime)items.FirstOrDefault().BillPurchasesMaster.addDate,
//                              billId = (int)items.Key,
//                              rowTotal = (decimal)items.Sum(x => x.Qu * x.price),
//                              tax = (decimal)items.Sum(x => x.tax),
//                              disNo = (decimal)(items.Sum(x => x.disNo)
//                              + items.FirstOrDefault().BillPurchasesMaster.disMoney),
//                              net = (decimal)items.FirstOrDefault().BillPurchasesMaster.net
//                          }).ToList();
//            }
//            else if (FromDate != null && toDate != null && FromCust != null && toCust != null
//                && FromSalesMan != null && toSalesMan != null && FrombillId == null
//                && tobillId == null)
//            {
//                result = (from i in db.BillPurchasesDetails
//                          join m in db.BillPurchasesMasters
//                          on i.billId equals m.id
//                          where m.isDeleted == false
//                          && m.addDate >= FromDate && m.addDate <= toDate
//                          && m.customersId >= FromCust && m.customersId <= toCust
//                          && m.salesManId >= FromSalesMan && m.salesManId <= toSalesMan
//                          group i by i.billId into items
//                          select new ItemMove2ViewModel
//                          {
//                              custId = (int)items.FirstOrDefault().BillPurchasesMaster.customersId,
//                              CustARName = items.FirstOrDefault().BillPurchasesMaster.Customer.ARName,
//                              salesManId = (int)items.FirstOrDefault().BillPurchasesMaster.salesManId,
//                              salesManARName = items.FirstOrDefault().BillPurchasesMaster.Employee1.ARName,
//                              addDate = (DateTime)items.FirstOrDefault().BillPurchasesMaster.addDate,
//                              billId = (int)items.Key,
//                              rowTotal = (decimal)items.Sum(x => x.Qu * x.price),
//                              tax = (decimal)items.Sum(x => x.tax),
//                              disNo = (decimal)(items.Sum(x => x.disNo)
//                              + items.FirstOrDefault().BillPurchasesMaster.disMoney),
//                              net = (decimal)items.FirstOrDefault().BillPurchasesMaster.net
//                          }).ToList();
//            }
//            else if (FromDate != null && toDate != null && FromCust == null && toCust == null
//                && FromSalesMan != null && toSalesMan != null && FrombillId == null
//                && tobillId == null)
//            {
//                result = (from i in db.BillPurchasesDetails
//                          join m in db.BillPurchasesMasters
//                          on i.billId equals m.id
//                          where m.isDeleted == false
//                          && m.addDate >= FromDate && m.addDate <= toDate
//                          && m.salesManId >= FromSalesMan && m.salesManId <= toSalesMan
//                          group i by i.billId into items
//                          select new ItemMove2ViewModel
//                          {
//                              custId = (int)items.FirstOrDefault().BillPurchasesMaster.customersId,
//                              CustARName = items.FirstOrDefault().BillPurchasesMaster.Customer.ARName,
//                              salesManId = (int)items.FirstOrDefault().BillPurchasesMaster.salesManId,
//                              salesManARName = items.FirstOrDefault().BillPurchasesMaster.Employee1.ARName,
//                              addDate = (DateTime)items.FirstOrDefault().BillPurchasesMaster.addDate,
//                              billId = (int)items.Key,
//                              rowTotal = (decimal)items.Sum(x => x.Qu * x.price),
//                              tax = (decimal)items.Sum(x => x.tax),
//                              disNo = (decimal)(items.Sum(x => x.disNo)
//                              + items.FirstOrDefault().BillPurchasesMaster.disMoney),
//                              net = (decimal)items.FirstOrDefault().BillPurchasesMaster.net
//                          }).ToList();
//            }
//            else if (FromDate != null && toDate != null && FromCust == null && toCust == null
//               && FromSalesMan == null && toSalesMan == null && FrombillId != null
//               && tobillId != null)
//            {
//                result = (from i in db.BillPurchasesDetails
//                          join m in db.BillPurchasesMasters
//                          on i.billId equals m.id
//                          where m.isDeleted == false
//                          && m.addDate >= FromDate && m.addDate <= toDate
//                          && m.id >= FrombillId && m.id <= tobillId
//                          group i by i.billId into items
//                          select new ItemMove2ViewModel
//                          {
//                              custId = (int)items.FirstOrDefault().BillPurchasesMaster.customersId,
//                              CustARName = items.FirstOrDefault().BillPurchasesMaster.Customer.ARName,
//                              salesManId = (int)items.FirstOrDefault().BillPurchasesMaster.salesManId,
//                              salesManARName = items.FirstOrDefault().BillPurchasesMaster.Employee1.ARName,
//                              addDate = (DateTime)items.FirstOrDefault().BillPurchasesMaster.addDate,
//                              billId = (int)items.Key,
//                              rowTotal = (decimal)items.Sum(x => x.Qu * x.price),
//                              tax = (decimal)items.Sum(x => x.tax),
//                              disNo = (decimal)(items.Sum(x => x.disNo)
//                              + items.FirstOrDefault().BillPurchasesMaster.disMoney),
//                              net = (decimal)items.FirstOrDefault().BillPurchasesMaster.net
//                          }).ToList();
//            }
//            else if (FromDate != null && toDate != null && FromCust != null && toCust != null
//                && FromSalesMan == null && toSalesMan == null && FrombillId != null
//                && tobillId != null)
//            {
//                result = (from i in db.BillPurchasesDetails
//                          join m in db.BillPurchasesMasters
//                          on i.billId equals m.id
//                          where m.isDeleted == false
//                          && m.addDate >= FromDate && m.addDate <= toDate
//                          && m.customersId >= FromCust && m.customersId <= toCust
//                          && m.id >= FrombillId && m.id <= tobillId
//                          group i by i.billId into items
//                          select new ItemMove2ViewModel
//                          {
//                              custId = (int)items.FirstOrDefault().BillPurchasesMaster.customersId,
//                              CustARName = items.FirstOrDefault().BillPurchasesMaster.Customer.ARName,
//                              salesManId = (int)items.FirstOrDefault().BillPurchasesMaster.salesManId,
//                              salesManARName = items.FirstOrDefault().BillPurchasesMaster.Employee1.ARName,
//                              addDate = (DateTime)items.FirstOrDefault().BillPurchasesMaster.addDate,
//                              billId = (int)items.Key,
//                              rowTotal = (decimal)items.Sum(x => x.Qu * x.price),
//                              tax = (decimal)items.Sum(x => x.tax),
//                              disNo = (decimal)(items.Sum(x => x.disNo)
//                              + items.FirstOrDefault().BillPurchasesMaster.disMoney),
//                              net = (decimal)items.FirstOrDefault().BillPurchasesMaster.net
//                          }).ToList();
//            }
//            else if (FromDate != null && toDate != null && FromCust == null && toCust == null
//               && FromSalesMan != null && toSalesMan != null && FrombillId != null
//               && tobillId != null)
//            {
//                result = (from i in db.BillPurchasesDetails
//                          join m in db.BillPurchasesMasters
//                          on i.billId equals m.id
//                          where m.isDeleted == false
//                          && m.addDate >= FromDate && m.addDate <= toDate
//                          && m.salesManId >= FromSalesMan && m.salesManId <= toSalesMan
//                          && m.id >= FrombillId && m.id <= tobillId
//                          group i by i.billId into items
//                          select new ItemMove2ViewModel
//                          {
//                              custId = (int)items.FirstOrDefault().BillPurchasesMaster.customersId,
//                              CustARName = items.FirstOrDefault().BillPurchasesMaster.Customer.ARName,
//                              salesManId = (int)items.FirstOrDefault().BillPurchasesMaster.salesManId,
//                              salesManARName = items.FirstOrDefault().BillPurchasesMaster.Employee1.ARName,
//                              addDate = (DateTime)items.FirstOrDefault().BillPurchasesMaster.addDate,
//                              billId = (int)items.Key,
//                              rowTotal = (decimal)items.Sum(x => x.Qu * x.price),
//                              tax = (decimal)items.Sum(x => x.tax),
//                              disNo = (decimal)(items.Sum(x => x.disNo)
//                              + items.FirstOrDefault().BillPurchasesMaster.disMoney),
//                              net = (decimal)items.FirstOrDefault().BillPurchasesMaster.net
//                          }).ToList();
//            }
//            else if (FromDate == null && toDate == null && FromCust != null && toCust != null
//              && FromSalesMan != null && toSalesMan != null && FrombillId != null
//              && tobillId != null)
//            {
//                result = (from i in db.BillPurchasesDetails
//                          join m in db.BillPurchasesMasters
//                          on i.billId equals m.id
//                          where m.isDeleted == false
//                          && m.customersId >= FromCust && m.customersId <= toCust
//                          && m.salesManId >= FromSalesMan && m.salesManId <= toSalesMan
//                          && m.id >= FrombillId && m.id <= tobillId
//                          group i by i.billId into items
//                          select new ItemMove2ViewModel
//                          {
//                              custId = (int)items.FirstOrDefault().BillPurchasesMaster.customersId,
//                              CustARName = items.FirstOrDefault().BillPurchasesMaster.Customer.ARName,
//                              salesManId = (int)items.FirstOrDefault().BillPurchasesMaster.salesManId,
//                              salesManARName = items.FirstOrDefault().BillPurchasesMaster.Employee1.ARName,
//                              addDate = (DateTime)items.FirstOrDefault().BillPurchasesMaster.addDate,
//                              billId = (int)items.Key,
//                              rowTotal = (decimal)items.Sum(x => x.Qu * x.price),
//                              tax = (decimal)items.Sum(x => x.tax),
//                              disNo = (decimal)(items.Sum(x => x.disNo)
//                              + items.FirstOrDefault().BillPurchasesMaster.disMoney),
//                              net = (decimal)items.FirstOrDefault().BillPurchasesMaster.net
//                          }).ToList();
//            }
//            ///////
//            else if (FromDate == null && toDate == null && FromCust != null && toCust != null
//       && FromSalesMan != null && toSalesMan != null && FrombillId == null
//       && tobillId == null)
//            {
//                result = (from i in db.BillPurchasesDetails
//                          join m in db.BillPurchasesMasters
//                          on i.billId equals m.id
//                          where m.isDeleted == false
//                          && m.customersId >= FromCust && m.customersId <= toCust
//                          && m.salesManId >= FromSalesMan && m.salesManId <= toSalesMan
//                          group i by i.billId into items
//                          select new ItemMove2ViewModel
//                          {
//                              custId = (int)items.FirstOrDefault().BillPurchasesMaster.customersId,
//                              CustARName = items.FirstOrDefault().BillPurchasesMaster.Customer.ARName,
//                              salesManId = (int)items.FirstOrDefault().BillPurchasesMaster.salesManId,
//                              salesManARName = items.FirstOrDefault().BillPurchasesMaster.Employee1.ARName,
//                              addDate = (DateTime)items.FirstOrDefault().BillPurchasesMaster.addDate,
//                              billId = (int)items.Key,
//                              rowTotal = (decimal)items.Sum(x => x.Qu * x.price),
//                              tax = (decimal)items.Sum(x => x.tax),
//                              disNo = (decimal)(items.Sum(x => x.disNo)
//                              + items.FirstOrDefault().BillPurchasesMaster.disMoney),
//                              net = (decimal)items.FirstOrDefault().BillPurchasesMaster.net
//                          }).ToList();
//            }
//            else if (FromDate == null && toDate == null && FromCust != null && toCust != null
//   && FromSalesMan == null && toSalesMan == null && FrombillId != null
//   && tobillId != null)
//            {
//                result = (from i in db.BillPurchasesDetails
//                          join m in db.BillPurchasesMasters
//                          on i.billId equals m.id
//                          where m.isDeleted == false
//                          && m.customersId >= FromCust && m.customersId <= toCust
//                          && m.id >= FrombillId && m.id <= tobillId
//                          group i by i.billId into items
//                          select new ItemMove2ViewModel
//                          {
//                              custId = (int)items.FirstOrDefault().BillPurchasesMaster.customersId,
//                              CustARName = items.FirstOrDefault().BillPurchasesMaster.Customer.ARName,
//                              salesManId = (int)items.FirstOrDefault().BillPurchasesMaster.salesManId,
//                              salesManARName = items.FirstOrDefault().BillPurchasesMaster.Employee1.ARName,
//                              addDate = (DateTime)items.FirstOrDefault().BillPurchasesMaster.addDate,
//                              billId = (int)items.Key,
//                              rowTotal = (decimal)items.Sum(x => x.Qu * x.price),
//                              tax = (decimal)items.Sum(x => x.tax),
//                              disNo = (decimal)(items.Sum(x => x.disNo)
//                              + items.FirstOrDefault().BillPurchasesMaster.disMoney),
//                              net = (decimal)items.FirstOrDefault().BillPurchasesMaster.net
//                          }).ToList();
//            }
//            ///////
//            else if (FromDate == null && toDate == null && FromCust == null && toCust == null
//     && FromSalesMan != null && toSalesMan != null && FrombillId != null
//     && tobillId != null)
//            {
//                result = (from i in db.BillPurchasesDetails
//                          join m in db.BillPurchasesMasters
//                          on i.billId equals m.id
//                          where m.isDeleted == false
//                          && m.id >= FrombillId && m.id <= tobillId
//                          && m.salesManId >= FromSalesMan && m.salesManId <= toSalesMan
//                          group i by i.billId into items
//                          select new ItemMove2ViewModel
//                          {
//                              custId = (int)items.FirstOrDefault().BillPurchasesMaster.customersId,
//                              CustARName = items.FirstOrDefault().BillPurchasesMaster.Customer.ARName,
//                              salesManId = (int)items.FirstOrDefault().BillPurchasesMaster.salesManId,
//                              salesManARName = items.FirstOrDefault().BillPurchasesMaster.Employee1.ARName,
//                              addDate = (DateTime)items.FirstOrDefault().BillPurchasesMaster.addDate,
//                              billId = (int)items.Key,
//                              rowTotal = (decimal)items.Sum(x => x.Qu * x.price),
//                              tax = (decimal)items.Sum(x => x.tax),
//                              disNo = (decimal)(items.Sum(x => x.disNo)
//                              + items.FirstOrDefault().BillPurchasesMaster.disMoney),
//                              net = (decimal)items.FirstOrDefault().BillPurchasesMaster.net
//                          }).ToList();
//            }
//            else if (FromDate != null && toDate != null && FromCust != null && toCust != null
//&& FromSalesMan != null && toSalesMan != null && FrombillId != null
//&& tobillId != null)
//            {
//                result = (from i in db.BillPurchasesDetails
//                          join m in db.BillPurchasesMasters
//                          on i.billId equals m.id
//                          where m.isDeleted == false
//                          && m.customersId >= FromCust && m.customersId <= toCust
//                          && m.addDate >= FromDate && m.addDate <= toDate
//                          && m.id >= FrombillId && m.id <= tobillId
//                          group i by i.billId into items
//                          select new ItemMove2ViewModel
//                          {
//                              custId = (int)items.FirstOrDefault().BillPurchasesMaster.customersId,
//                              CustARName = items.FirstOrDefault().BillPurchasesMaster.Customer.ARName,
//                              salesManId = (int)items.FirstOrDefault().BillPurchasesMaster.salesManId,
//                              salesManARName = items.FirstOrDefault().BillPurchasesMaster.Employee1.ARName,
//                              addDate = (DateTime)items.FirstOrDefault().BillPurchasesMaster.addDate,
//                              billId = (int)items.Key,
//                              rowTotal = (decimal)items.Sum(x => x.Qu * x.price),
//                              tax = (decimal)items.Sum(x => x.tax),
//                              disNo = (decimal)(items.Sum(x => x.disNo)
//                              + items.FirstOrDefault().BillPurchasesMaster.disMoney),
//                              net = (decimal)items.FirstOrDefault().BillPurchasesMaster.net
//                          }).ToList();
//            }
//            else
//            {
//                result = (from i in db.BillPurchasesDetails
//                          join m in db.BillPurchasesMasters
//                          on i.billId equals m.id
//                          where m.isDeleted == false
//                          group i by i.billId into items
//                          select new ItemMove2ViewModel
//                          {
//                              custId = (int)items.FirstOrDefault().BillPurchasesMaster.customersId,
//                              CustARName = items.FirstOrDefault().BillPurchasesMaster.Customer.ARName,
//                              salesManId = (int)items.FirstOrDefault().BillPurchasesMaster.salesManId,
//                              salesManARName = items.FirstOrDefault().BillPurchasesMaster.Employee1.ARName,
//                              addDate = (DateTime)items.FirstOrDefault().BillPurchasesMaster.addDate,
//                              billId = (int)items.Key,
//                              rowTotal = (decimal)items.Sum(x => x.Qu * x.price),
//                              tax = (decimal)items.Sum(x => x.tax),
//                              disNo = (decimal)(items.Sum(x => x.disNo)
//                              + items.FirstOrDefault().BillPurchasesMaster.disMoney),
//                              net = (decimal)items.FirstOrDefault().BillPurchasesMaster.net
//                          }).ToList();
//            }
//            ///////////////////////////

//            cryrpt.Load(Server.MapPath("~/Reports/Purchase.rpt"));
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