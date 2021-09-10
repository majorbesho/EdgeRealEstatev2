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
    public class WarehouseWarrantReportController : Controller
    {
        // GET: WarehouseWarrantReport
        ApplicationDbContext db = new ApplicationDbContext();
        private WarehouseWarrantReportService WarehouseWarrantReportService;
        public WarehouseWarrantReportController()
        {
            this.WarehouseWarrantReportService = new WarehouseWarrantReportService(db);
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

            return Json(WarehouseWarrantReportService.Read(FromCust, toCust, FromDate, toDate, FromSalesMan, toSalesMan
            , FrombillId, tobillId)
                .ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
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

//        public ActionResult ShowList(int? FromCust, int? toCust
//            , DateTime? FromDate, DateTime? toDate, int? FromSalesMan, int? toSalesMan
//            , int? FrombillId, int? tobillId)
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
//                && FromSalesMan == null && toSalesMan == null && FrombillId == null
//                && tobillId == null)
//            {
//                result = (from i in db.BillSalesDetails
//                          join m in db.BillSalesMasters
//                          on i.billId equals m.id
//                          where m.isDeleted == false
//                          group i by i.billId into items
//                          select new ItemMove2ViewModel
//                          {
//                              custId = (int)items.FirstOrDefault().BillSalesMaster.customersId,
//                              CustARName = items.FirstOrDefault().BillSalesMaster.Customer.ARName,
//                              salesManId = (int)items.FirstOrDefault().BillSalesMaster.salesManId,
//                              salesManARName = items.FirstOrDefault().BillSalesMaster.Employee1.ARName,
//                              addDate = (DateTime)items.FirstOrDefault().BillSalesMaster.addDate,
//                              billId = (int)items.Key,
//                              rowTotal = (decimal)items.Sum(x => x.Qu * x.price),
//                              tax = (decimal)items.Sum(x => x.tax),
//                              disNo = (decimal)(items.Sum(x => x.disNo)
//                              + items.FirstOrDefault().BillSalesMaster.disMoney),
//                              net = (decimal)items.FirstOrDefault().BillSalesMaster.net
//                          }).ToList();
//            }
//            else if (FromDate != null && toDate != null && FromCust == null && toCust == null
//               && FromSalesMan == null && toSalesMan == null && FrombillId == null
//               && tobillId == null)
//            {
//                result = (from i in db.BillSalesDetails
//                          join m in db.BillSalesMasters
//                          on i.billId equals m.id
//                          where m.isDeleted == false
//                          && m.addDate >= FromDate && m.addDate <= toDate
//                          group i by i.billId into items
//                          select new ItemMove2ViewModel
//                          {
//                              custId = (int)items.FirstOrDefault().BillSalesMaster.customersId,
//                              CustARName = items.FirstOrDefault().BillSalesMaster.Customer.ARName,
//                              salesManId = (int)items.FirstOrDefault().BillSalesMaster.salesManId,
//                              salesManARName = items.FirstOrDefault().BillSalesMaster.Employee1.ARName,
//                              addDate = (DateTime)items.FirstOrDefault().BillSalesMaster.addDate,
//                              billId = (int)items.Key,
//                              rowTotal = (decimal)items.Sum(x => x.Qu * x.price),
//                              tax = (decimal)items.Sum(x => x.tax),
//                              disNo = (decimal)(items.Sum(x => x.disNo)
//                              + items.FirstOrDefault().BillSalesMaster.disMoney),
//                              net = (decimal)items.FirstOrDefault().BillSalesMaster.net
//                          }).ToList();
//            }
//            else if (FromDate == null && toDate == null && FromCust != null && toCust != null
//               && FromSalesMan == null && toSalesMan == null && FrombillId == null
//               && tobillId == null)
//            {
//                result = (from i in db.BillSalesDetails
//                          join m in db.BillSalesMasters
//                          on i.billId equals m.id
//                          where m.isDeleted == false
//                          && m.customersId >= FromCust && m.customersId <= toCust
//                          group i by i.billId into items
//                          select new ItemMove2ViewModel
//                          {
//                              custId = (int)items.FirstOrDefault().BillSalesMaster.customersId,
//                              CustARName = items.FirstOrDefault().BillSalesMaster.Customer.ARName,
//                              salesManId = (int)items.FirstOrDefault().BillSalesMaster.salesManId,
//                              salesManARName = items.FirstOrDefault().BillSalesMaster.Employee1.ARName,
//                              addDate = (DateTime)items.FirstOrDefault().BillSalesMaster.addDate,
//                              billId = (int)items.Key,
//                              rowTotal = (decimal)items.Sum(x => x.Qu * x.price),
//                              tax = (decimal)items.Sum(x => x.tax),
//                              disNo = (decimal)(items.Sum(x => x.disNo)
//                              + items.FirstOrDefault().BillSalesMaster.disMoney),
//                              net = (decimal)items.FirstOrDefault().BillSalesMaster.net
//                          }).ToList();
//            }
//            else if (FromDate == null && toDate == null && FromCust == null && toCust == null
//             && FromSalesMan != null && toSalesMan != null && FrombillId == null
//             && tobillId == null)
//            {
//                result = (from i in db.BillSalesDetails
//                          join m in db.BillSalesMasters
//                          on i.billId equals m.id
//                          where m.isDeleted == false
//                          && m.salesManId >= FromSalesMan && m.salesManId <= toSalesMan
//                          group i by i.billId into items
//                          select new ItemMove2ViewModel
//                          {
//                              custId = (int)items.FirstOrDefault().BillSalesMaster.customersId,
//                              CustARName = items.FirstOrDefault().BillSalesMaster.Customer.ARName,
//                              salesManId = (int)items.FirstOrDefault().BillSalesMaster.salesManId,
//                              salesManARName = items.FirstOrDefault().BillSalesMaster.Employee1.ARName,
//                              addDate = (DateTime)items.FirstOrDefault().BillSalesMaster.addDate,
//                              billId = (int)items.Key,
//                              rowTotal = (decimal)items.Sum(x => x.Qu * x.price),
//                              tax = (decimal)items.Sum(x => x.tax),
//                              disNo = (decimal)(items.Sum(x => x.disNo)
//                              + items.FirstOrDefault().BillSalesMaster.disMoney),
//                              net = (decimal)items.FirstOrDefault().BillSalesMaster.net
//                          }).ToList();
//            }
//            else if (FromDate == null && toDate == null && FromCust == null && toCust == null
//            && FromSalesMan == null && toSalesMan == null && FrombillId != null
//            && tobillId != null)
//            {
//                result = (from i in db.BillSalesDetails
//                          join m in db.BillSalesMasters
//                          on i.billId equals m.id
//                          where m.isDeleted == false
//                          && m.id >= FrombillId && m.id <= tobillId
//                          group i by i.billId into items
//                          select new ItemMove2ViewModel
//                          {
//                              custId = (int)items.FirstOrDefault().BillSalesMaster.customersId,
//                              CustARName = items.FirstOrDefault().BillSalesMaster.Customer.ARName,
//                              salesManId = (int)items.FirstOrDefault().BillSalesMaster.salesManId,
//                              salesManARName = items.FirstOrDefault().BillSalesMaster.Employee1.ARName,
//                              addDate = (DateTime)items.FirstOrDefault().BillSalesMaster.addDate,
//                              billId = (int)items.Key,
//                              rowTotal = (decimal)items.Sum(x => x.Qu * x.price),
//                              tax = (decimal)items.Sum(x => x.tax),
//                              disNo = (decimal)(items.Sum(x => x.disNo)
//                              + items.FirstOrDefault().BillSalesMaster.disMoney),
//                              net = (decimal)items.FirstOrDefault().BillSalesMaster.net
//                          }).ToList();
//            }
//            //////////
//            else if (FromDate != null && toDate != null && FromCust != null && toCust != null
//                && FromSalesMan == null && toSalesMan == null && FrombillId == null
//                && tobillId == null)
//            {
//                result = (from i in db.BillSalesDetails
//                          join m in db.BillSalesMasters
//                          on i.billId equals m.id
//                          where m.isDeleted == false
//                          && m.addDate >= FromDate && m.addDate <= toDate
//                          && m.customersId >= FromCust && m.customersId <= toCust
//                          group i by i.billId into items
//                          select new ItemMove2ViewModel
//                          {
//                              custId = (int)items.FirstOrDefault().BillSalesMaster.customersId,
//                              CustARName = items.FirstOrDefault().BillSalesMaster.Customer.ARName,
//                              salesManId = (int)items.FirstOrDefault().BillSalesMaster.salesManId,
//                              salesManARName = items.FirstOrDefault().BillSalesMaster.Employee1.ARName,
//                              addDate = (DateTime)items.FirstOrDefault().BillSalesMaster.addDate,
//                              billId = (int)items.Key,
//                              rowTotal = (decimal)items.Sum(x => x.Qu * x.price),
//                              tax = (decimal)items.Sum(x => x.tax),
//                              disNo = (decimal)(items.Sum(x => x.disNo)
//                              + items.FirstOrDefault().BillSalesMaster.disMoney),
//                              net = (decimal)items.FirstOrDefault().BillSalesMaster.net
//                          }).ToList();
//            }
//            else if (FromDate != null && toDate != null && FromCust != null && toCust != null
//                && FromSalesMan != null && toSalesMan != null && FrombillId == null
//                && tobillId == null)
//            {
//                result = (from i in db.BillSalesDetails
//                          join m in db.BillSalesMasters
//                          on i.billId equals m.id
//                          where m.isDeleted == false
//                          && m.addDate >= FromDate && m.addDate <= toDate
//                          && m.customersId >= FromCust && m.customersId <= toCust
//                          && m.salesManId >= FromSalesMan && m.salesManId <= toSalesMan
//                          group i by i.billId into items
//                          select new ItemMove2ViewModel
//                          {
//                              custId = (int)items.FirstOrDefault().BillSalesMaster.customersId,
//                              CustARName = items.FirstOrDefault().BillSalesMaster.Customer.ARName,
//                              salesManId = (int)items.FirstOrDefault().BillSalesMaster.salesManId,
//                              salesManARName = items.FirstOrDefault().BillSalesMaster.Employee1.ARName,
//                              addDate = (DateTime)items.FirstOrDefault().BillSalesMaster.addDate,
//                              billId = (int)items.Key,
//                              rowTotal = (decimal)items.Sum(x => x.Qu * x.price),
//                              tax = (decimal)items.Sum(x => x.tax),
//                              disNo = (decimal)(items.Sum(x => x.disNo)
//                              + items.FirstOrDefault().BillSalesMaster.disMoney),
//                              net = (decimal)items.FirstOrDefault().BillSalesMaster.net
//                          }).ToList();
//            }
//            else if (FromDate != null && toDate != null && FromCust == null && toCust == null
//                && FromSalesMan != null && toSalesMan != null && FrombillId == null
//                && tobillId == null)
//            {
//                result = (from i in db.BillSalesDetails
//                          join m in db.BillSalesMasters
//                          on i.billId equals m.id
//                          where m.isDeleted == false
//                          && m.addDate >= FromDate && m.addDate <= toDate
//                          && m.salesManId >= FromSalesMan && m.salesManId <= toSalesMan
//                          group i by i.billId into items
//                          select new ItemMove2ViewModel
//                          {
//                              custId = (int)items.FirstOrDefault().BillSalesMaster.customersId,
//                              CustARName = items.FirstOrDefault().BillSalesMaster.Customer.ARName,
//                              salesManId = (int)items.FirstOrDefault().BillSalesMaster.salesManId,
//                              salesManARName = items.FirstOrDefault().BillSalesMaster.Employee1.ARName,
//                              addDate = (DateTime)items.FirstOrDefault().BillSalesMaster.addDate,
//                              billId = (int)items.Key,
//                              rowTotal = (decimal)items.Sum(x => x.Qu * x.price),
//                              tax = (decimal)items.Sum(x => x.tax),
//                              disNo = (decimal)(items.Sum(x => x.disNo)
//                              + items.FirstOrDefault().BillSalesMaster.disMoney),
//                              net = (decimal)items.FirstOrDefault().BillSalesMaster.net
//                          }).ToList();
//            }
//            else if (FromDate != null && toDate != null && FromCust == null && toCust == null
//               && FromSalesMan == null && toSalesMan == null && FrombillId != null
//               && tobillId != null)
//            {
//                result = (from i in db.BillSalesDetails
//                          join m in db.BillSalesMasters
//                          on i.billId equals m.id
//                          where m.isDeleted == false
//                          && m.addDate >= FromDate && m.addDate <= toDate
//                          && m.id >= FrombillId && m.id <= tobillId
//                          group i by i.billId into items
//                          select new ItemMove2ViewModel
//                          {
//                              custId = (int)items.FirstOrDefault().BillSalesMaster.customersId,
//                              CustARName = items.FirstOrDefault().BillSalesMaster.Customer.ARName,
//                              salesManId = (int)items.FirstOrDefault().BillSalesMaster.salesManId,
//                              salesManARName = items.FirstOrDefault().BillSalesMaster.Employee1.ARName,
//                              addDate = (DateTime)items.FirstOrDefault().BillSalesMaster.addDate,
//                              billId = (int)items.Key,
//                              rowTotal = (decimal)items.Sum(x => x.Qu * x.price),
//                              tax = (decimal)items.Sum(x => x.tax),
//                              disNo = (decimal)(items.Sum(x => x.disNo)
//                              + items.FirstOrDefault().BillSalesMaster.disMoney),
//                              net = (decimal)items.FirstOrDefault().BillSalesMaster.net
//                          }).ToList();
//            }
//            else if (FromDate != null && toDate != null && FromCust != null && toCust != null
//                && FromSalesMan == null && toSalesMan == null && FrombillId != null
//                && tobillId != null)
//            {
//                result = (from i in db.BillSalesDetails
//                          join m in db.BillSalesMasters
//                          on i.billId equals m.id
//                          where m.isDeleted == false
//                          && m.addDate >= FromDate && m.addDate <= toDate
//                          && m.customersId >= FromCust && m.customersId <= toCust
//                          && m.id >= FrombillId && m.id <= tobillId
//                          group i by i.billId into items
//                          select new ItemMove2ViewModel
//                          {
//                              custId = (int)items.FirstOrDefault().BillSalesMaster.customersId,
//                              CustARName = items.FirstOrDefault().BillSalesMaster.Customer.ARName,
//                              salesManId = (int)items.FirstOrDefault().BillSalesMaster.salesManId,
//                              salesManARName = items.FirstOrDefault().BillSalesMaster.Employee1.ARName,
//                              addDate = (DateTime)items.FirstOrDefault().BillSalesMaster.addDate,
//                              billId = (int)items.Key,
//                              rowTotal = (decimal)items.Sum(x => x.Qu * x.price),
//                              tax = (decimal)items.Sum(x => x.tax),
//                              disNo = (decimal)(items.Sum(x => x.disNo)
//                              + items.FirstOrDefault().BillSalesMaster.disMoney),
//                              net = (decimal)items.FirstOrDefault().BillSalesMaster.net
//                          }).ToList();
//            }
//            else if (FromDate != null && toDate != null && FromCust == null && toCust == null
//               && FromSalesMan != null && toSalesMan != null && FrombillId != null
//               && tobillId != null)
//            {
//                result = (from i in db.BillSalesDetails
//                          join m in db.BillSalesMasters
//                          on i.billId equals m.id
//                          where m.isDeleted == false
//                          && m.addDate >= FromDate && m.addDate <= toDate
//                          && m.salesManId >= FromSalesMan && m.salesManId <= toSalesMan
//                          && m.id >= FrombillId && m.id <= tobillId
//                          group i by i.billId into items
//                          select new ItemMove2ViewModel
//                          {
//                              custId = (int)items.FirstOrDefault().BillSalesMaster.customersId,
//                              CustARName = items.FirstOrDefault().BillSalesMaster.Customer.ARName,
//                              salesManId = (int)items.FirstOrDefault().BillSalesMaster.salesManId,
//                              salesManARName = items.FirstOrDefault().BillSalesMaster.Employee1.ARName,
//                              addDate = (DateTime)items.FirstOrDefault().BillSalesMaster.addDate,
//                              billId = (int)items.Key,
//                              rowTotal = (decimal)items.Sum(x => x.Qu * x.price),
//                              tax = (decimal)items.Sum(x => x.tax),
//                              disNo = (decimal)(items.Sum(x => x.disNo)
//                              + items.FirstOrDefault().BillSalesMaster.disMoney),
//                              net = (decimal)items.FirstOrDefault().BillSalesMaster.net
//                          }).ToList();
//            }
//            else if (FromDate == null && toDate == null && FromCust != null && toCust != null
//              && FromSalesMan != null && toSalesMan != null && FrombillId != null
//              && tobillId != null)
//            {
//                result = (from i in db.BillSalesDetails
//                          join m in db.BillSalesMasters
//                          on i.billId equals m.id
//                          where m.isDeleted == false
//                          && m.customersId >= FromCust && m.customersId <= toCust
//                          && m.salesManId >= FromSalesMan && m.salesManId <= toSalesMan
//                          && m.id >= FrombillId && m.id <= tobillId
//                          group i by i.billId into items
//                          select new ItemMove2ViewModel
//                          {
//                              custId = (int)items.FirstOrDefault().BillSalesMaster.customersId,
//                              CustARName = items.FirstOrDefault().BillSalesMaster.Customer.ARName,
//                              salesManId = (int)items.FirstOrDefault().BillSalesMaster.salesManId,
//                              salesManARName = items.FirstOrDefault().BillSalesMaster.Employee1.ARName,
//                              addDate = (DateTime)items.FirstOrDefault().BillSalesMaster.addDate,
//                              billId = (int)items.Key,
//                              rowTotal = (decimal)items.Sum(x => x.Qu * x.price),
//                              tax = (decimal)items.Sum(x => x.tax),
//                              disNo = (decimal)(items.Sum(x => x.disNo)
//                              + items.FirstOrDefault().BillSalesMaster.disMoney),
//                              net = (decimal)items.FirstOrDefault().BillSalesMaster.net
//                          }).ToList();
//            }
//            ///////
//            else if (FromDate == null && toDate == null && FromCust != null && toCust != null
//       && FromSalesMan != null && toSalesMan != null && FrombillId == null
//       && tobillId == null)
//            {
//                result = (from i in db.BillSalesDetails
//                          join m in db.BillSalesMasters
//                          on i.billId equals m.id
//                          where m.isDeleted == false
//                          && m.customersId >= FromCust && m.customersId <= toCust
//                          && m.salesManId >= FromSalesMan && m.salesManId <= toSalesMan
//                          group i by i.billId into items
//                          select new ItemMove2ViewModel
//                          {
//                              custId = (int)items.FirstOrDefault().BillSalesMaster.customersId,
//                              CustARName = items.FirstOrDefault().BillSalesMaster.Customer.ARName,
//                              salesManId = (int)items.FirstOrDefault().BillSalesMaster.salesManId,
//                              salesManARName = items.FirstOrDefault().BillSalesMaster.Employee1.ARName,
//                              addDate = (DateTime)items.FirstOrDefault().BillSalesMaster.addDate,
//                              billId = (int)items.Key,
//                              rowTotal = (decimal)items.Sum(x => x.Qu * x.price),
//                              tax = (decimal)items.Sum(x => x.tax),
//                              disNo = (decimal)(items.Sum(x => x.disNo)
//                              + items.FirstOrDefault().BillSalesMaster.disMoney),
//                              net = (decimal)items.FirstOrDefault().BillSalesMaster.net
//                          }).ToList();
//            }
//            else if (FromDate == null && toDate == null && FromCust != null && toCust != null
//   && FromSalesMan == null && toSalesMan == null && FrombillId != null
//   && tobillId != null)
//            {
//                result = (from i in db.BillSalesDetails
//                          join m in db.BillSalesMasters
//                          on i.billId equals m.id
//                          where m.isDeleted == false
//                          && m.customersId >= FromCust && m.customersId <= toCust
//                          && m.id >= FrombillId && m.id <= tobillId
//                          group i by i.billId into items
//                          select new ItemMove2ViewModel
//                          {
//                              custId = (int)items.FirstOrDefault().BillSalesMaster.customersId,
//                              CustARName = items.FirstOrDefault().BillSalesMaster.Customer.ARName,
//                              salesManId = (int)items.FirstOrDefault().BillSalesMaster.salesManId,
//                              salesManARName = items.FirstOrDefault().BillSalesMaster.Employee1.ARName,
//                              addDate = (DateTime)items.FirstOrDefault().BillSalesMaster.addDate,
//                              billId = (int)items.Key,
//                              rowTotal = (decimal)items.Sum(x => x.Qu * x.price),
//                              tax = (decimal)items.Sum(x => x.tax),
//                              disNo = (decimal)(items.Sum(x => x.disNo)
//                              + items.FirstOrDefault().BillSalesMaster.disMoney),
//                              net = (decimal)items.FirstOrDefault().BillSalesMaster.net
//                          }).ToList();
//            }
//            ///////
//            else if (FromDate == null && toDate == null && FromCust == null && toCust == null
//     && FromSalesMan != null && toSalesMan != null && FrombillId != null
//     && tobillId != null)
//            {
//                result = (from i in db.BillSalesDetails
//                          join m in db.BillSalesMasters
//                          on i.billId equals m.id
//                          where m.isDeleted == false
//                          && m.id >= FrombillId && m.id <= tobillId
//                          && m.salesManId >= FromSalesMan && m.salesManId <= toSalesMan
//                          group i by i.billId into items
//                          select new ItemMove2ViewModel
//                          {
//                              custId = (int)items.FirstOrDefault().BillSalesMaster.customersId,
//                              CustARName = items.FirstOrDefault().BillSalesMaster.Customer.ARName,
//                              salesManId = (int)items.FirstOrDefault().BillSalesMaster.salesManId,
//                              salesManARName = items.FirstOrDefault().BillSalesMaster.Employee1.ARName,
//                              addDate = (DateTime)items.FirstOrDefault().BillSalesMaster.addDate,
//                              billId = (int)items.Key,
//                              rowTotal = (decimal)items.Sum(x => x.Qu * x.price),
//                              tax = (decimal)items.Sum(x => x.tax),
//                              disNo = (decimal)(items.Sum(x => x.disNo)
//                              + items.FirstOrDefault().BillSalesMaster.disMoney),
//                              net = (decimal)items.FirstOrDefault().BillSalesMaster.net
//                          }).ToList();
//            }
//            else if (FromDate != null && toDate != null && FromCust != null && toCust != null
//&& FromSalesMan != null && toSalesMan != null && FrombillId != null
//&& tobillId != null)
//            {
//                result = (from i in db.BillSalesDetails
//                          join m in db.BillSalesMasters
//                          on i.billId equals m.id
//                          where m.isDeleted == false
//                          && m.customersId >= FromCust && m.customersId <= toCust
//                          && m.addDate >= FromDate && m.addDate <= toDate
//                          && m.id >= FrombillId && m.id <= tobillId
//                          group i by i.billId into items
//                          select new ItemMove2ViewModel
//                          {
//                              custId = (int)items.FirstOrDefault().BillSalesMaster.customersId,
//                              CustARName = items.FirstOrDefault().BillSalesMaster.Customer.ARName,
//                              salesManId = (int)items.FirstOrDefault().BillSalesMaster.salesManId,
//                              salesManARName = items.FirstOrDefault().BillSalesMaster.Employee1.ARName,
//                              addDate = (DateTime)items.FirstOrDefault().BillSalesMaster.addDate,
//                              billId = (int)items.Key,
//                              rowTotal = (decimal)items.Sum(x => x.Qu * x.price),
//                              tax = (decimal)items.Sum(x => x.tax),
//                              disNo = (decimal)(items.Sum(x => x.disNo)
//                              + items.FirstOrDefault().BillSalesMaster.disMoney),
//                              net = (decimal)items.FirstOrDefault().BillSalesMaster.net
//                          }).ToList();
//            }
//            else
//            {
//                result = (from i in db.BillSalesDetails
//                          join m in db.BillSalesMasters
//                          on i.billId equals m.id
//                          where m.isDeleted == false
//                          group i by i.billId into items
//                          select new ItemMove2ViewModel
//                          {
//                              custId = (int)items.FirstOrDefault().BillSalesMaster.customersId,
//                              CustARName = items.FirstOrDefault().BillSalesMaster.Customer.ARName,
//                              salesManId = (int)items.FirstOrDefault().BillSalesMaster.salesManId,
//                              salesManARName = items.FirstOrDefault().BillSalesMaster.Employee1.ARName,
//                              addDate = (DateTime)items.FirstOrDefault().BillSalesMaster.addDate,
//                              billId = (int)items.Key,
//                              rowTotal = (decimal)items.Sum(x => x.Qu * x.price),
//                              tax = (decimal)items.Sum(x => x.tax),
//                              disNo = (decimal)(items.Sum(x => x.disNo)
//                              + items.FirstOrDefault().BillSalesMaster.disMoney),
//                              net = (decimal)items.FirstOrDefault().BillSalesMaster.net
//                          }).ToList();
//            }
//            ///////////////////////////

//            cryrpt.Load(Server.MapPath("~/Reports/Sales.rpt"));
//            CrTables = cryrpt.Database.Tables;

//            foreach (CrystalDecisions.CrystalReports.Engine.Table CrTable in CrTables)
//            {
//                crtablelogoninfo = CrTable.LogOnInfo;
//                crtablelogoninfo.ConnectionInfo = crconnectioninfo;
//                CrTable.ApplyLogOnInfo(crtablelogoninfo);
//            }
//            cryrpt.SetDataSource(result);
//            //cryrpt.SetParameterValue("FromDate", FromDate);
//            //cryrpt.SetParameterValue("toDate", toDate);
//            //        cryrpt.SetParameterValue("paidType", paidType);
//            Stream s = cryrpt.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
//            return File(s, "application/pdf");
//        }

    }
}