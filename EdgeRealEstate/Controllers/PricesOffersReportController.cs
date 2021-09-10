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
    public class PricesOffersReportController : Controller
    {
        // GET: PricesOffersReport
        ApplicationDbContext db = new ApplicationDbContext();
        private PricesOffersReportService PricesOffersReportService;
        public PricesOffersReportController()
        {
            this.PricesOffersReportService = new PricesOffersReportService(db);
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

            return Json(PricesOffersReportService.Read(FromCust, toCust, FromDate, toDate, FromSalesMan, toSalesMan
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

    }
}