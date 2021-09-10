using EdgeRealEstate.Entities;
using EdgeRealEstate.Models;
using EdgeRealEstate.Models.Services;
using EdgeRealEstate.Models.ViewModels;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace EdgeRealEstate.Controllers
{
    public class BillPurchasesController : Controller
    {
        // GET: BillPurchases
        ApplicationDbContext db = new ApplicationDbContext();
        private BillPurchasesDetailsService BillPurchasesDetailsService;
        private BillPurchasesDetailsServiceForIndex BillPurchasesDetailsServiceForIndex;
        public BillPurchasesController()
        {
            this.BillPurchasesDetailsService = new BillPurchasesDetailsService(db);
            this.BillPurchasesDetailsServiceForIndex = new BillPurchasesDetailsServiceForIndex(db);
        }
        // GET: BillPurchases
        public ActionResult Index(int? page)
        {
            var BillPurchasesMasters = db.BillPurchasesMaster
                .Where(x => x.isDeleted == false)
          .OrderByDescending(x => x.id).ToList().ToPagedList(page ?? 1, 1);
            if (BillPurchasesMasters.Count() != 0)
            {
                Session["MasterIdFromIndex"] = BillPurchasesMasters.FirstOrDefault().id;
            }
            else
            {
                Session["MasterIdFromIndex"] = 1;
            }
            return View(BillPurchasesMasters);
        }
        /************ create ***********/
        public ActionResult Create(int? empID, int? customersId, string refNo, int? salesManId, string billType,
        string refType, string paidType, DateTime? addDate, bool isApproval, decimal? tax, double? disPer
            , decimal? disMoney, decimal? total, decimal? paid, decimal net, string billTypeCredit
            , decimal? disNosum)
        {
            Session["refType"] = refType;
            Session["refNo"] = refNo;
            BillPurchasesMaster BillPurchasesMasters = new BillPurchasesMaster();
            BillPurchasesMasters.empID = empID;
            BillPurchasesMasters.customersId = customersId;
            BillPurchasesMasters.refNo = refNo;
            BillPurchasesMasters.salesManId = salesManId;
            if (billTypeCredit != "")
            {
                BillPurchasesMasters.billType = billTypeCredit;
                billType = "";
            }
            else
            {
                BillPurchasesMasters.billType = billType;
            }
            BillPurchasesMasters.refType = refType;
            BillPurchasesMasters.paidType = paidType;
            if (addDate == null)
            {
                BillPurchasesMasters.addDate = DateTime.Now;
            }
            else
            {
                BillPurchasesMasters.addDate = addDate;
            }
            BillPurchasesMasters.isApproval = isApproval;
            BillPurchasesMasters.tax = tax;
            BillPurchasesMasters.disPer = disPer;
            BillPurchasesMasters.disMoney = disMoney;
            BillPurchasesMasters.total = total;
            BillPurchasesMasters.paid = paid;
            BillPurchasesMasters.net = net;
            // check moneylimit
            Decimal? currentValue = db.Customers.Where(x => x.Id == customersId).FirstOrDefault().currentValue;
            Decimal moneyLimit = db.Customers.Where(x => x.Id == customersId).FirstOrDefault().moneyLimit;
            if ((billTypeCredit != "" && paid == net) || billTypeCredit == "")
            {
                if (currentValue + net < moneyLimit)
                {
                    BillPurchasesDetailsService.createMaster(BillPurchasesMasters);
                    Session["BillPurchasesMasterId"] = BillPurchasesMasters.id;
                    Session["addDate"] = BillPurchasesMasters.addDate;

                    // create  bill history
                    //BillHistory billHistory = new BillHistory();
                    //billHistory.custId = customersId;
                    //billHistory.empId = empID;
                    //billHistory.billId = BillPurchasesMasters.id;
                    //billHistory.net = net;
                    //billHistory.paid = paid;
                    //billHistory.billType = billType;
                    //billHistory.IsDeleted = false;
                    //billHistory.IsSalesBill = false;
                    //db.BillHistory.Add(billHistory);
                    //db.SaveChanges();
                    //Session["billHistoryId"] = billHistory.id;
                }
            }
            //if (billType == "نقدى" || billTypeCredit != "" && paid == net)
            //{
            //    if (currentValue + net < moneyLimit)
            //    {
            //        CustPaperPayment CustPaperPayment = new CustPaperPayment();
            //        CustPaperPayment.customerId = customersId;
            //        CustPaperPayment.empId = empID;
            //        CustPaperPayment.salesManId = salesManId;
            //        CustPaperPayment.paidMethod = paidType;
            //        CustPaperPayment.paid = paid;
            //        CustPaperPayment.refType = refType;
            //        CustPaperPayment.indate = addDate;
            //        CustPaperPayment.isDeleted = false;
            //        CustPaperPayment.billId = BillPurchasesMasters.id;
            //        db.CustPaperPayments.Add(CustPaperPayment);
            //        db.SaveChanges();
            //    }

            //}
            //if (billType == "اجل")
            //{
            //    int billHistoryId = Convert.ToInt32(Session["billHistoryId"]);
            //    BillHistory billHistory = db.BillHistory.Find(billHistoryId);
            //    Customer customer = db.Customers.Find(customersId);
            //    billHistory.OldCurrentValue = customer.currentValue;
            //    decimal? NewCurrentValue = customer.currentValue + net - paid;
            //    customer.currentValue = customer.currentValue + net - paid;
            //    db.Customers.Attach(customer);
            //    db.Entry(customer).State = EntityState.Modified;
            //    db.SaveChanges();
            //    billHistory.NewCurrentValue = NewCurrentValue;
            //    db.BillHistory.Attach(billHistory);
            //    db.Entry(billHistory).State = EntityState.Modified;
            //    db.SaveChanges();
            //    if (paid != null)
            //    {
            //        if (currentValue + net < moneyLimit)
            //        {
            //            PaperPayment paperPayment = new PaperPayment();
            //            paperPayment.customerId = customersId;
            //            paperPayment.empId = empID;
            //            paperPayment.salesManId = salesManId;
            //            paperPayment.paidMethod = paidType;
            //            paperPayment.paid = paid;
            //            paperPayment.refType = refType;
            //            paperPayment.indate = addDate;
            //            paperPayment.isDeleted = false;
            //            paperPayment.billId = BillPurchasesMasters.id;
            //            db.PaperPayments.Add(paperPayment);
            //            db.SaveChanges();                        
            //        }
            //    }
            //}
            return View();
        }
        public ActionResult Editing_Custom()
        {
                ViewBag.Customers = new SelectList(db.Customers, "Id", "ARName");
                ViewBag.Employees = new SelectList(db.Employees, "Id", "ARName");
                ViewBag.SalesMans = new SelectList(db.Employees, "Id", "ARName");
                PopulateItems();
                PopulateLKStores();
                return View();
        }
        public ActionResult EditingCustom_Read([DataSourceRequest] DataSourceRequest request,int? PriceOfferId)
        {
            if (PriceOfferId == null)
            {
                return Json(/*BillPurchasesDetailsService.Read(PriceOfferId).ToDataSourceResult(*/request/*)*/);
            }
            else
            {
                return Json(BillPurchasesDetailsService.Read(PriceOfferId).ToDataSourceResult(request));
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EditingCustom_Update([DataSourceRequest] DataSourceRequest request,
            [Bind(Prefix = "models")]IEnumerable<BillPurchasesDetailsViewModel> ItemRequests,int? PriceOfferId)
        {
            if (ItemRequests != null && ModelState.IsValid)
            {
                foreach (var ItemRequest in ItemRequests)
                {
                    BillPurchasesDetailsService.Update(ItemRequest, PriceOfferId);
                }
            }

            return Json(ItemRequests.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EditingCustom_Create([DataSourceRequest] DataSourceRequest request,
            [Bind(Prefix = "models")]IEnumerable<BillPurchasesDetailsViewModel> ItemRequests, int? empID, int? customersId
            , string refNo, int? salesManId, string billType,string refType, string paidType, DateTime? addDate
            , bool isApproval, double? disPer
            , decimal? disMoney, decimal? total, decimal? paid, decimal net, string billTypeCredit
            , decimal? disNosum,int? PriceOfferId)
        {
            Session["refType"] = refType;
            Session["refNo"] = refNo;
            BillPurchasesMaster BillPurchasesMasters = new BillPurchasesMaster();
            BillPurchasesMasters.empID = empID;
            BillPurchasesMasters.customersId = customersId;
            BillPurchasesMasters.refNo = refNo;
            BillPurchasesMasters.salesManId = salesManId;
            if (billTypeCredit != "")
            {
                BillPurchasesMasters.billType = billTypeCredit;
                billType = "";
            }
            else
            {
                BillPurchasesMasters.billType = billType;
            }
            BillPurchasesMasters.refType = refType;
            BillPurchasesMasters.paidType = paidType;
            if (addDate == null)
            {
                BillPurchasesMasters.addDate = DateTime.Now;
            }
            else
            {
                BillPurchasesMasters.addDate = addDate;
            }
            BillPurchasesMasters.isApproval = isApproval;
            BillPurchasesMasters.disPer = disPer;
            BillPurchasesMasters.disMoney = disMoney;
            BillPurchasesMasters.total = total;
            BillPurchasesMasters.paid = paid;
            BillPurchasesMasters.net = net;
            // check moneylimit
            Decimal? currentValue = db.Customers.Where(x => x.Id == customersId).FirstOrDefault().currentValue;
            Decimal moneyLimit = db.Customers.Where(x => x.Id == customersId).FirstOrDefault().moneyLimit;
            if ((billTypeCredit != "" && paid == net) || billTypeCredit == "")
            {
                if (currentValue + net < moneyLimit)
                {
                    BillPurchasesDetailsService.createMaster(BillPurchasesMasters);
                    Session["BillPurchasesMasterId"] = BillPurchasesMasters.id;
                    Session["addDate"] = BillPurchasesMasters.addDate;
                }
            }

            var results = new List<BillPurchasesDetailsViewModel>();

            if (ItemRequests != null && ModelState.IsValid)
            {
                foreach (var ItemRequest in ItemRequests)
                {
                    BillPurchasesDetailsService.Create(ItemRequest, PriceOfferId);
                    results.Add(ItemRequest);
                    db.SaveChanges();
                }
            }

            return Json(results.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EditingCustom_Destroy([DataSourceRequest] DataSourceRequest request,
            [Bind(Prefix = "models")]IEnumerable<BillPurchasesDetailsViewModel> ItemRequests,int? PriceOfferId)
        {
            foreach (var ItemRequest in ItemRequests)
            {
                BillPurchasesDetailsService.Destroy(ItemRequest, PriceOfferId);
            }

            return Json(ItemRequests.ToDataSourceResult(request, ModelState));
        }


        /*************************Index****************************/
        public ActionResult Editing_CustomForIndex()
        {
            PopulateItems();
            PopulateLKStores();
            return View();
        }
        public ActionResult EditingCustom_ReadForIndex([DataSourceRequest] DataSourceRequest request,int? PriceOfferid)
        {
            return Json(BillPurchasesDetailsServiceForIndex.Read(PriceOfferid).ToDataSourceResult(request));
        }

        /********************Edit ********************/
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Edit(int? id, int? empID, int? customersId, string refNo, int? salesManId, string billType,
            string refType, string paidType, DateTime addDate, bool isApproval, decimal? paid, decimal? tax, double? disPer
            , decimal? disMoney, decimal? total, decimal? net, string billTypeCredit
            , decimal? disNosum)
        {
            Session["refType"] = refType;
            Session["refNo"] = refNo;
            BillPurchasesMaster BillPurchasesMasters = db.BillPurchasesMaster.Find(id);
            BillPurchasesMasters.empID = empID;
            BillPurchasesMasters.customersId = customersId;
            BillPurchasesMasters.refNo = refNo;
            BillPurchasesMasters.salesManId = salesManId;
            var query = db.Customers.Where(x => x.Id == customersId).ToList().FirstOrDefault();
            var IsCredit = query.isCredit;
            if (IsCredit == false)
            {
                BillPurchasesMasters.billType = billTypeCredit;
                billType = "";
            }
            else
            {
                BillPurchasesMasters.billType = billType;
            }
            BillPurchasesMasters.refType = refType;
            BillPurchasesMasters.paidType = paidType;
            BillPurchasesMasters.addDate = addDate;
            BillPurchasesMasters.isApproval = isApproval;
            BillPurchasesMasters.paid = paid;
            BillPurchasesMasters.tax = tax;
            BillPurchasesMasters.disPer = disPer;
            BillPurchasesMasters.disMoney = disMoney;
            BillPurchasesMasters.total = total;
            BillPurchasesMasters.net = net;
            Decimal? currentValue = db.Customers.Where(x => x.Id == customersId).FirstOrDefault().currentValue;
            Decimal moneyLimit = db.Customers.Where(x => x.Id == customersId).FirstOrDefault().moneyLimit;
            if ((IsCredit == false && paid == net) || IsCredit == true)
            {
                BillPurchasesDetailsServiceForIndex.updateMaster(BillPurchasesMasters);
                Session["BillPurchasesMasterId"] = BillPurchasesMasters.id;
                Session["addDate"] = BillPurchasesMasters.addDate;
                ViewBag.msgg = 1;
            }
            //if (billType == "نقدى" || IsCredit == false && paid == net)
            //{

            //    CustPaperPayment CustPaperPayment = db.CustPaperPayments.Where(x => x.billId == id).FirstOrDefault();
            //    CustPaperPayment.customerId = customersId;
            //    CustPaperPayment.empId = empID;
            //    CustPaperPayment.salesManId = salesManId;
            //    CustPaperPayment.paidMethod = paidType;
            //    CustPaperPayment.paid = paid;
            //    CustPaperPayment.refType = refType;
            //    CustPaperPayment.indate = addDate;
            //    CustPaperPayment.isDeleted = false;
            //    db.CustPaperPayments.Attach(CustPaperPayment);
            //    db.Entry(CustPaperPayment).State = EntityState.Modified;
            //    db.SaveChanges();
            //}
            //if (billType == "اجل")
            //{
            //    Customer customer = db.Customers.Find(customersId);
            //    BillHistory billHistory = db.BillHistory.Where(x => x.billId == id && x.IsSalesBill == false).FirstOrDefault();
            //    customer.currentValue = billHistory.OldCurrentValue + net - paid;
            //    db.Customers.Attach(customer);
            //    db.Entry(customer).State = EntityState.Modified;
            //    db.SaveChanges();
            //    BillHistory NewBillHistory = new BillHistory();
            //    NewBillHistory.custId = customersId;
            //    NewBillHistory.empId = empID;
            //    NewBillHistory.billId = id;
            //    NewBillHistory.net = net;
            //    NewBillHistory.paid = paid;
            //    NewBillHistory.billType = billType;
            //    NewBillHistory.OldCurrentValue = billHistory.OldCurrentValue;
            //    NewBillHistory.NewCurrentValue = customer.currentValue;
            //    NewBillHistory.IsDeleted = false;
            //    NewBillHistory.IsSalesBill = false;
            //    db.BillHistory.Add(NewBillHistory);
            //    db.SaveChanges();

            //    if (paid != null)
            //    {
            //        if (currentValue + net < moneyLimit)
            //        {
            //            PaperPayment PaperPayment = new PaperPayment();
            //            PaperPayment.customerId = customersId;
            //            PaperPayment.empId = empID;
            //            PaperPayment.salesManId = salesManId;
            //            PaperPayment.paidMethod = paidType;
            //            PaperPayment.paid = paid;
            //            PaperPayment.refType = refType;
            //            PaperPayment.indate = addDate;
            //            PaperPayment.isDeleted = false;
            //            PaperPayment.billId = BillPurchasesMasters.id;
            //            db.PaperPayments.Add(PaperPayment);
            //            db.SaveChanges();

            //        }
            //    }
            //}
            return View(BillPurchasesMasters);
        }
        public ActionResult Editing_CustomForEdit(int? id)
        {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                BillPurchasesMaster BillPurchasesMasters = db.BillPurchasesMaster.Find(id);
                ViewBag.Customers = new SelectList(db.Customers, "Id", "ARName");
                ViewBag.Employees = new SelectList(db.Employees, "Id", "ARName");
                ViewBag.SalesMans = new SelectList(db.Employees, "Id", "ARName");
                ViewBag.billType = BillPurchasesMasters.billType;
                ViewBag.refNo = BillPurchasesMasters.refNo;
                ViewBag.refType = BillPurchasesMasters.refType;
                ViewBag.paidType = BillPurchasesMasters.paidType;
                ViewBag.addDate = BillPurchasesMasters.addDate;
                ViewBag.isApproval = BillPurchasesMasters.isApproval;
                ViewBag.paid = BillPurchasesMasters.paid;
                ViewBag.tax = BillPurchasesMasters.tax;
                ViewBag.disPer = BillPurchasesMasters.disPer;
                ViewBag.disMoney = BillPurchasesMasters.disMoney;
                ViewBag.total = BillPurchasesMasters.total;
                ViewBag.net = BillPurchasesMasters.net;
                var query = db.Customers.Where(x => x.Id == BillPurchasesMasters.customersId).ToList().FirstOrDefault();
                var IsCredit = query.isCredit;
                ViewBag.IsCredit = IsCredit;

                if (BillPurchasesMasters == null)
                {
                    return HttpNotFound();
                }
                PopulateItems();
                return View(BillPurchasesMasters);
        }

        public ActionResult EditingCustom_ReadForEdit([DataSourceRequest] DataSourceRequest request,int? PriceOfferId)
        {
            return Json(BillPurchasesDetailsServiceForIndex.Read(PriceOfferId).ToDataSourceResult(request));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EditingCustom_UpdateForEdit([DataSourceRequest] DataSourceRequest request,
            [Bind(Prefix = "models")]IEnumerable<BillPurchasesDetailsViewModel> ItemRequests
            , int? id, int? empID, int? customersId
            , string refNo, int? salesManId, string billType,
            string refType, string paidType, DateTime addDate, bool isApproval,
            decimal? paid, double? disPer
            , decimal? disMoney, decimal? total, decimal? net, string billTypeCredit
            , decimal? disNosum,int? PriceOfferid)
        {

            Session["refType"] = refType;
            Session["refNo"] = refNo;
            BillPurchasesMaster BillPurchasesMasters = db.BillPurchasesMaster.Find(id);
            BillPurchasesMasters.empID = empID;
            BillPurchasesMasters.customersId = customersId;
            BillPurchasesMasters.refNo = refNo;
            BillPurchasesMasters.salesManId = salesManId;
            var query = db.Customers.Where(x => x.Id == customersId).ToList().FirstOrDefault();
            var IsCredit = query.isCredit;
            if (IsCredit == false)
            {
                BillPurchasesMasters.billType = billTypeCredit;
                billType = "";
            }
            else
            {
                BillPurchasesMasters.billType = billType;
            }
            BillPurchasesMasters.refType = refType;
            BillPurchasesMasters.paidType = paidType;
            BillPurchasesMasters.addDate = addDate;
            BillPurchasesMasters.isApproval = isApproval;
            BillPurchasesMasters.paid = paid;
            BillPurchasesMasters.disPer = disPer;
            BillPurchasesMasters.disMoney = disMoney;
            BillPurchasesMasters.total = total;
            BillPurchasesMasters.net = net;
            Decimal? currentValue = db.Customers.Where(x => x.Id == customersId).FirstOrDefault().currentValue;
            Decimal moneyLimit = db.Customers.Where(x => x.Id == customersId).FirstOrDefault().moneyLimit;
            if ((IsCredit == false && paid == net) || IsCredit == true)
            {
                BillPurchasesDetailsServiceForIndex.updateMaster(BillPurchasesMasters);
                Session["BillPurchasesMasterId"] = BillPurchasesMasters.id;
                Session["addDate"] = BillPurchasesMasters.addDate;
                ViewBag.msgg = 1;
            }
            if (ItemRequests != null && ModelState.IsValid)
            {
                foreach (var ItemRequest in ItemRequests)
                {
                    BillPurchasesDetailsServiceForIndex.Update(ItemRequest, PriceOfferid);
                }
            }

            return Json(ItemRequests.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EditingCustom_CreateForEdit([DataSourceRequest] DataSourceRequest request,
                [Bind(Prefix = "models")]IEnumerable<BillPurchasesDetailsViewModel> ItemRequests
            , int? id, int? empID, int? customersId
            , string refNo, int? salesManId, string billType,
            string refType, string paidType, DateTime addDate, bool isApproval,
            decimal? paid, double? disPer
            , decimal? disMoney, decimal? total, decimal? net, string billTypeCredit
            , decimal? disNosum,int? PriceOfferid)
        {
            Session["refType"] = refType;
            Session["refNo"] = refNo;
            BillPurchasesMaster BillPurchasesMasters = db.BillPurchasesMaster.Find(id);
            BillPurchasesMasters.empID = empID;
            BillPurchasesMasters.customersId = customersId;
            BillPurchasesMasters.refNo = refNo;
            BillPurchasesMasters.salesManId = salesManId;
            var query = db.Customers.Where(x => x.Id == customersId).ToList().FirstOrDefault();
            var IsCredit = query.isCredit;
            if (IsCredit == false)
            {
                BillPurchasesMasters.billType = billTypeCredit;
                billType = "";
            }
            else
            {
                BillPurchasesMasters.billType = billType;
            }
            BillPurchasesMasters.refType = refType;
            BillPurchasesMasters.paidType = paidType;
            BillPurchasesMasters.addDate = addDate;
            BillPurchasesMasters.isApproval = isApproval;
            BillPurchasesMasters.paid = paid;
            BillPurchasesMasters.disPer = disPer;
            BillPurchasesMasters.disMoney = disMoney;
            BillPurchasesMasters.total = total;
            BillPurchasesMasters.net = net;
            Decimal? currentValue = db.Customers.Where(x => x.Id == customersId).FirstOrDefault().currentValue;
            Decimal moneyLimit = db.Customers.Where(x => x.Id == customersId).FirstOrDefault().moneyLimit;
            if ((IsCredit == false && paid == net) || IsCredit == true)
            {
                BillPurchasesDetailsServiceForIndex.updateMaster(BillPurchasesMasters);
                Session["BillPurchasesMasterId"] = BillPurchasesMasters.id;
                Session["addDate"] = BillPurchasesMasters.addDate;
                ViewBag.msgg = 1;
            }
            var results = new List<BillPurchasesDetailsViewModel>();

            if (ItemRequests != null && ModelState.IsValid)
            {
                foreach (var ItemRequest in ItemRequests)
                {
                    BillPurchasesDetailsServiceForIndex.Create(ItemRequest, PriceOfferid);
                    results.Add(ItemRequest);
                    db.SaveChanges();
                }
            }

            return Json(results.ToDataSourceResult(request, ModelState));
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EditingCustom_DestroyForEdit([DataSourceRequest] DataSourceRequest request,
            [Bind(Prefix = "models")]IEnumerable<BillPurchasesDetailsViewModel> ItemRequests,int? PriceOfferid)
        {
            foreach (var ItemRequest in ItemRequests)
            {
                BillPurchasesDetailsServiceForIndex.Destroy(ItemRequest, PriceOfferid);
            }

            return Json(ItemRequests.ToDataSourceResult(request, ModelState));
        }
        /**********************Delete view************************/
        public ActionResult Editing_CustomForDelete()
        {
            PopulateItems();
            // PopulateLKStores();

            return View();
        }

        public ActionResult EditingCustom_ReadForDelete([DataSourceRequest] DataSourceRequest request,int? PriceOfferid)
        {
            return Json(BillPurchasesDetailsServiceForIndex.Read(PriceOfferid).ToDataSourceResult(request));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EditingCustom_UpdateForDelete([DataSourceRequest] DataSourceRequest request,
    [Bind(Prefix = "models")]IEnumerable<BillPurchasesDetailsViewModel> ItemRequests,int? PriceOfferid)
        {
            if (ItemRequests != null && ModelState.IsValid)
            {
                foreach (var ItemRequest in ItemRequests)
                {
                    BillPurchasesDetailsServiceForIndex.Update(ItemRequest, PriceOfferid);
                }
            }

            return Json(ItemRequests.ToDataSourceResult(request, ModelState));
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EditingCustom_DestroyForDelete([DataSourceRequest] DataSourceRequest request,
            [Bind(Prefix = "models")]IEnumerable<BillPurchasesDetailsViewModel> ItemRequests,int? PriceOfferid)
        {
            foreach (var ItemRequest in ItemRequests)
            {
                BillPurchasesDetailsServiceForIndex.Destroy(ItemRequest, PriceOfferid);
            }

            return Json(ItemRequests.ToDataSourceResult(request, ModelState));
        }
        /*******************Helper methods*******************/
        public JsonResult ServerFiltering_GetItems(string text)
        {
            var dataContext = new ApplicationDbContext();

            var Items = dataContext.ConstructionMaterials.Select(i => new ConstructionMaterialViewModel
            {
                ID = i.ID,
                MaterialName = i.MaterialName,
            });


            if (!string.IsNullOrEmpty(text))
            {
                Items = Items.Where(p => p.MaterialName.Contains(text));
            }

            return Json(Items, JsonRequestBehavior.AllowGet);
        }
        private void PopulateItems()
        {
            var dataContext = new ApplicationDbContext();
            var Items = dataContext.ConstructionMaterials
                        .Select(i => new ConstructionMaterialViewModel
                        {
                            ID = i.ID,
                            MaterialName = i.MaterialName,
                        });
            //.OrderBy(e => e.arName);

            ViewData["Items"] = Items;
            ViewData["defaultItems"] = Items.First();
        }
        private void PopulateLKStores()
        {
            var dataContext = new ApplicationDbContext();
            var Stores = dataContext.LKStores
                        .Select(s => new LKStoreViewModel
                        {
                            Id = s.Id,
                            ARName = s.ARName
                        })
                        .OrderBy(e => e.ARName);

            ViewData["Stores"] = Stores;
            ViewData["defaultStores"] = Stores.First();
        }


        [HttpGet]
        public JsonResult Getprice(int? ConstructionMaterialId)
        {
            var query = db.ConstructionMaterials.Where(x => x.ID == ConstructionMaterialId).ToList().FirstOrDefault();
            decimal? price = query.Price;
            return Json(price, JsonRequestBehavior.AllowGet);
        }

        //[HttpGet]
        //public JsonResult GetType(int itmId)
        //{
        //    bool type = db.Items.Where(x => x.id == itmId).FirstOrDefault().IsService;
        //    ViewBag.type = type;
        //    return Json(type, JsonRequestBehavior.AllowGet);
        //}

        //[HttpGet]
        //public JsonResult GetTax()
        //{
        //    var tax_query = db.CompanyData.ToList().FirstOrDefault();
        //    var tax = tax_query.tax;

        //    return Json(tax, JsonRequestBehavior.AllowGet);
        //}

        [HttpGet]
        public JsonResult GetIsCredit(int customersId)
        {
            var query = db.Customers.Where(x => x.Id == customersId).ToList().FirstOrDefault();
            var IsCredit = query.isCredit;

            return Json(IsCredit, JsonRequestBehavior.AllowGet);
        }

        // GET: BillPurchasesMaster/Delete/5
        public ActionResult Delete(int? id)
        {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                BillPurchasesMaster BillPurchasesMasters = db.BillPurchasesMaster.Find(id);
                if (BillPurchasesMasters == null)
                {
                    return HttpNotFound();
                }
                return View(BillPurchasesMasters);
        }

        // POST: BillPurchasesMaster/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BillPurchasesMaster BillPurchasesMasters = db.BillPurchasesMaster.Find(id);
            List<BillPurchasesDetails> BillPurchasesDetails = db.BillPurchasesDetails.Where(x => x.billId == id).ToList();
            foreach (BillPurchasesDetails i in BillPurchasesDetails)
            {
                i.isDeleted = true;
                StoreTransaction storeTransaction = db.StoreTransactions.Where(x => x.billId == id && x.ConstructionMaterialId == i.ConstructionMaterial.ID).FirstOrDefault();
                storeTransaction.isDeleted = true;
                db.StoreTransactions.Attach(storeTransaction);
                db.Entry(storeTransaction).State = EntityState.Modified;
                db.SaveChanges();

                //CustPaperPayment CustPaperPayment = db.CustPaperPayments.Where(x => x.billId == id).FirstOrDefault();
                //CustPaperPayment.isDeleted = true;
                //db.CustPaperPayments.Attach(CustPaperPayment);
                //db.Entry(CustPaperPayment).State = EntityState.Modified;
                //db.SaveChanges();

                // ItemQuanPrice
                //var ItemQuanPricequery = db.ItemQuanPrice
                //    .Where(x => x.itmId == i.itmId).ToList();
                //if (ItemQuanPricequery.Count() != 0)
                //{
                //    ItemQuanPrice itemQuanPrice = ItemQuanPricequery.FirstOrDefault();
                //    itemQuanPrice.Qu -= i.Qu;
                //    itemQuanPrice.changeDate = DateTime.Now;
                //    itemQuanPrice.price = ((itemQuanPrice.Qu * itemQuanPrice.price)
                //        + (i.Qu * i.price)) / (itemQuanPrice.Qu + i.Qu);
                //    db.ItemQuanPrice.Attach(itemQuanPrice);
                //    db.Entry(itemQuanPrice).State = EntityState.Modified;
                //    db.SaveChanges();
                //}
            }
            BillPurchasesMasters.isDeleted = true;
            db.SaveChanges();


            //if (BillPurchasesMasters.billType == "اجل")
            //{
            //    Customer customer = db.Customers.Find(BillPurchasesMasters.customersId);
            //    BillHistory billHistory = db.BillHistory.Where(x => x.billId == id && x.IsSalesBill == false).FirstOrDefault();
            //    customer.currentValue = billHistory.OldCurrentValue;
            //    db.Customers.Attach(customer);
            //    db.Entry(customer).State = EntityState.Modified;
            //    db.SaveChanges();
            //    BillHistory NewBillHistory = new BillHistory();
            //    NewBillHistory.custId = BillPurchasesMasters.customersId;
            //    NewBillHistory.empId = BillPurchasesMasters.empID;
            //    NewBillHistory.billId = id;
            //    NewBillHistory.net = BillPurchasesMasters.net;
            //    NewBillHistory.paid = BillPurchasesMasters.paid;
            //    NewBillHistory.billType = BillPurchasesMasters.billType;
            //    NewBillHistory.OldCurrentValue = billHistory.OldCurrentValue;
            //    NewBillHistory.NewCurrentValue = customer.currentValue;
            //    NewBillHistory.IsDeleted = true;
            //    NewBillHistory.IsSalesBill = true;
            //    db.BillHistory.Add(NewBillHistory);
            //    db.SaveChanges();
            //}
            return RedirectToAction("Index");
        }
        
        [HttpGet]
        public JsonResult getMoneyLimitCheck(int? customersId, decimal net)
        {
            Decimal? currentValue = db.Customers.Where(x => x.Id == customersId).FirstOrDefault().currentValue;
            Decimal moneyLimit = db.Customers.Where(x => x.Id == customersId).FirstOrDefault().moneyLimit;
            //ViewBag.checkmoneyLimit = currentValue + net < moneyLimit;
            bool check = currentValue + net < moneyLimit;
            return Json(check, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ServerFiltering_GetPriceOfferId(int text)
        {
            var dataContext = new ApplicationDbContext();

            var Customers = dataContext.PricesOffersMaster
                .Select(i => new PricesOffersMasterViewModel
                {
                    id = i.id,
                });

            if (/*!string.IsNullOrEmpty(text)*/ text != null)
            {
                Customers = Customers.Where(p => p.id == text/*.Contains(text)*/);
            }

            return Json(Customers, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult GetcustomersId(int PriceOfferId)
        {
            var query = db.PricesOffersMaster.Where(x => x.id == PriceOfferId).ToList().FirstOrDefault();
        
            return Json(new { customersId = query.customersId, ARName = query.Customer.ARName}, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult GetsalesManId(int PriceOfferId)
        {
            var query = db.PricesOffersMaster.Where(x => x.id == PriceOfferId).ToList().FirstOrDefault();
            var result = db.Employees.Where(x => x.Id == query.salesManId).ToList().FirstOrDefault();
          
            return Json(new { Id = result.Id, ARName = result.ARName }, JsonRequestBehavior.AllowGet);
        }
        
        [HttpGet]
        public JsonResult GetempID(int PriceOfferId)
        {
            var query = db.PricesOffersMaster.Where(x => x.id == PriceOfferId).ToList().FirstOrDefault();
            var result = db.Employees.Where(x => x.Id == query.empID).ToList().FirstOrDefault();

            return Json(new { Id = result.Id, ARName = result.ARName }, JsonRequestBehavior.AllowGet);
        }
    }
}