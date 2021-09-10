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
    public class PricesOffersController : Controller
    {
        // GET: PricesOffers
        ApplicationDbContext db = new ApplicationDbContext();
        private PricesOffersDetailsService PricesOffersDetailsService;
        private PricesOffersDetailsServiceForIndex PricesOffersDetailsServiceForIndex;
        public PricesOffersController()
        {
            this.PricesOffersDetailsService = new PricesOffersDetailsService(db);
            this.PricesOffersDetailsServiceForIndex = new PricesOffersDetailsServiceForIndex(db);
        }
        // GET: BillPurchases
        public ActionResult Index(int? page)
        {
            var PricesOffersMaster = db.PricesOffersMaster
                .Where(x => x.isDeleted == false)
          .OrderByDescending(x => x.id).ToList().ToPagedList(page ?? 1, 1);
            if (PricesOffersMaster.Count() != 0)
            {
                Session["MasterIdFromIndex"] = PricesOffersMaster.FirstOrDefault().id;
            }
            else
            {
                Session["MasterIdFromIndex"] = 1;
            }
            return View(PricesOffersMaster);
        }
        /************ create ***********/
        public ActionResult Create(int? empID, int? customersId, string refNo, int? salesManId, DateTime? addDate,
            decimal? total)
        {
            Session["refNo"] = refNo;
            PricesOffersMaster PricesOffersMaster = new PricesOffersMaster();
            PricesOffersMaster.empID = empID;
            PricesOffersMaster.customersId = customersId;
            PricesOffersMaster.refNo = refNo;
            PricesOffersMaster.salesManId = salesManId;
            if (addDate == null)
            {
                PricesOffersMaster.addDate = DateTime.Now;
            }
            else
            {
                PricesOffersMaster.addDate = addDate;
            }

            PricesOffersMaster.total = total;
            // check moneylimit
            Decimal? currentValue = db.Customers.Where(x => x.Id == customersId).FirstOrDefault().currentValue;
            Decimal moneyLimit = db.Customers.Where(x => x.Id == customersId).FirstOrDefault().moneyLimit;
            //if ((billTypeCredit != "" && paid == net) || billTypeCredit == "")
            //{
            //    if (currentValue + net < moneyLimit)
            //    {
            PricesOffersDetailsService.createMaster(PricesOffersMaster);
            Session["BillPurchasesMasterId"] = PricesOffersMaster.id;
            Session["addDate"] = PricesOffersMaster.addDate;

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
            //    }
            //}
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
        public ActionResult EditingCustom_Read([DataSourceRequest] DataSourceRequest request)
        {
            return Json(/*BillSalesDetailsService.Read().ToDataSourceResult(*/request/*)*/);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EditingCustom_Update([DataSourceRequest] DataSourceRequest request,
            [Bind(Prefix = "models")]IEnumerable<PricesOffersDetailsViewModel> ItemRequests)
        {
            if (ItemRequests != null && ModelState.IsValid)
            {
                foreach (var ItemRequest in ItemRequests)
                {
                    PricesOffersDetailsService.Update(ItemRequest);
                }
            }

            return Json(ItemRequests.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EditingCustom_Create([DataSourceRequest] DataSourceRequest request,
            [Bind(Prefix = "models")]IEnumerable<PricesOffersDetailsViewModel> ItemRequests, int? empID, int? customersId
            , string refNo, int? salesManId, DateTime? addDate
          , decimal? total)
        {
            Session["refNo"] = refNo;
            PricesOffersMaster PricesOffersMaster = new PricesOffersMaster();
            PricesOffersMaster.empID = empID;
            PricesOffersMaster.customersId = customersId;
            PricesOffersMaster.refNo = refNo;
            PricesOffersMaster.salesManId = salesManId;
            if (addDate == null)
            {
                PricesOffersMaster.addDate = DateTime.Now;
            }
            else
            {
                PricesOffersMaster.addDate = addDate;
            }
            PricesOffersMaster.total = total;

            // check moneylimit
            //Decimal? currentValue = db.Customers.Where(x => x.Id == customersId).FirstOrDefault().currentValue;
            //Decimal moneyLimit = db.Customers.Where(x => x.Id == customersId).FirstOrDefault().moneyLimit;
            //if ((billTypeCredit != "" && paid == net) || billTypeCredit == "")
            //{
            //    if (currentValue + net < moneyLimit)
            //    {
            PricesOffersDetailsService.createMaster(PricesOffersMaster);
            Session["BillPurchasesMasterId"] = PricesOffersMaster.id;
            Session["addDate"] = PricesOffersMaster.addDate;
            //    }
            //}

            var results = new List<PricesOffersDetailsViewModel>();

            if (ItemRequests != null && ModelState.IsValid)
            {
                foreach (var ItemRequest in ItemRequests)
                {
                    PricesOffersDetailsService.Create(ItemRequest);
                    results.Add(ItemRequest);
                    db.SaveChanges();
                }
            }

            return Json(results.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EditingCustom_Destroy([DataSourceRequest] DataSourceRequest request,
            [Bind(Prefix = "models")]IEnumerable<PricesOffersDetailsViewModel> ItemRequests)
        {
            foreach (var ItemRequest in ItemRequests)
            {
                PricesOffersDetailsService.Destroy(ItemRequest);
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
        public ActionResult EditingCustom_ReadForIndex([DataSourceRequest] DataSourceRequest request)
        {
            return Json(PricesOffersDetailsServiceForIndex.Read().ToDataSourceResult(request));
        }

        /********************Edit ********************/
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Edit(int? id, int? empID, int? customersId, string refNo, int? salesManId,
            DateTime addDate, decimal? total)
        {
            Session["refNo"] = refNo;
            PricesOffersMaster PricesOffersMaster = db.PricesOffersMaster.Find(id);
            PricesOffersMaster.empID = empID;
            PricesOffersMaster.customersId = customersId;
            PricesOffersMaster.refNo = refNo;
            PricesOffersMaster.salesManId = salesManId;
            PricesOffersMaster.addDate = addDate;
            PricesOffersMaster.total = total;
            PricesOffersDetailsServiceForIndex.updateMaster(PricesOffersMaster);
            Session["BillPurchasesMasterId"] = PricesOffersMaster.id;
            Session["addDate"] = PricesOffersMaster.addDate;
            ViewBag.msgg = 1;

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
            return View(PricesOffersMaster);
        }
        public ActionResult Editing_CustomForEdit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PricesOffersMaster PricesOffersMaster = db.PricesOffersMaster.Find(id);
            ViewBag.Customers = new SelectList(db.Customers, "Id", "ARName");
            ViewBag.Employees = new SelectList(db.Employees, "Id", "ARName");
            ViewBag.SalesMans = new SelectList(db.Employees, "Id", "ARName");
            //ViewBag.billType = PricesOffersMaster.billType;
            ViewBag.refNo = PricesOffersMaster.refNo;
            //ViewBag.refType = PricesOffersMaster.refType;
            //ViewBag.paidType = PricesOffersMaster.paidType;
            ViewBag.addDate = PricesOffersMaster.addDate;
            //ViewBag.isApproval = PricesOffersMaster.isApproval;
            //ViewBag.paid = PricesOffersMaster.paid;
            //ViewBag.tax = PricesOffersMaster.tax;
            //ViewBag.disPer = PricesOffersMaster.disPer;
            //ViewBag.disMoney = PricesOffersMaster.disMoney;
            ViewBag.total = PricesOffersMaster.total;
            //ViewBag.net = PricesOffersMaster.net;
            //var query = db.Customers.Where(x => x.Id == PricesOffersMaster.customersId).ToList().FirstOrDefault();
            //var IsCredit = query.isCredit;
            //ViewBag.IsCredit = IsCredit;

            if (PricesOffersMaster == null)
            {
                return HttpNotFound();
            }
            PopulateItems();
            return View(PricesOffersMaster);
        }

        public ActionResult EditingCustom_ReadForEdit([DataSourceRequest] DataSourceRequest request)
        {
            return Json(PricesOffersDetailsServiceForIndex.Read().ToDataSourceResult(request));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EditingCustom_UpdateForEdit([DataSourceRequest] DataSourceRequest request,
            [Bind(Prefix = "models")]IEnumerable<PricesOffersDetailsViewModel> ItemRequests
            , int? id, int? empID, int? customersId
            , string refNo, int? salesManId, DateTime addDate,
            decimal? total)
        {
            Session["refNo"] = refNo;
            PricesOffersMaster PricesOffersMaster = db.PricesOffersMaster.Find(id);
            PricesOffersMaster.empID = empID;
            PricesOffersMaster.customersId = customersId;
            PricesOffersMaster.refNo = refNo;
            PricesOffersMaster.salesManId = salesManId;
            PricesOffersMaster.addDate = addDate;
            PricesOffersMaster.total = total;

            PricesOffersDetailsServiceForIndex.updateMaster(PricesOffersMaster);
            Session["BillPurchasesMasterId"] = PricesOffersMaster.id;
            Session["addDate"] = PricesOffersMaster.addDate;
            ViewBag.msgg = 1;

            if (ItemRequests != null && ModelState.IsValid)
            {
                foreach (var ItemRequest in ItemRequests)
                {
                    PricesOffersDetailsServiceForIndex.Update(ItemRequest);
                }
            }

            return Json(ItemRequests.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EditingCustom_CreateForEdit([DataSourceRequest] DataSourceRequest request,
                [Bind(Prefix = "models")]IEnumerable<PricesOffersDetailsViewModel> ItemRequests
            , int? id, int? empID, int? customersId
            , string refNo, int? salesManId, DateTime addDate,
             decimal? total)
        {
            Session["refNo"] = refNo;
            PricesOffersMaster PricesOffersMaster = db.PricesOffersMaster.Find(id);
            PricesOffersMaster.empID = empID;
            PricesOffersMaster.customersId = customersId;
            PricesOffersMaster.refNo = refNo;
            PricesOffersMaster.salesManId = salesManId;
            PricesOffersMaster.addDate = addDate;
            PricesOffersMaster.total = total;
            PricesOffersDetailsServiceForIndex.updateMaster(PricesOffersMaster);
            Session["BillPurchasesMasterId"] = PricesOffersMaster.id;
            Session["addDate"] = PricesOffersMaster.addDate;
            ViewBag.msgg = 1;
            var results = new List<PricesOffersDetailsViewModel>();

            if (ItemRequests != null && ModelState.IsValid)
            {
                foreach (var ItemRequest in ItemRequests)
                {
                    PricesOffersDetailsServiceForIndex.Create(ItemRequest);
                    results.Add(ItemRequest);
                    db.SaveChanges();
                }
            }

            return Json(results.ToDataSourceResult(request, ModelState));
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EditingCustom_DestroyForEdit([DataSourceRequest] DataSourceRequest request,
            [Bind(Prefix = "models")]IEnumerable<PricesOffersDetailsViewModel> ItemRequests)
        {
            foreach (var ItemRequest in ItemRequests)
            {
                PricesOffersDetailsServiceForIndex.Destroy(ItemRequest);
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

        public ActionResult EditingCustom_ReadForDelete([DataSourceRequest] DataSourceRequest request)
        {
            return Json(PricesOffersDetailsServiceForIndex.Read().ToDataSourceResult(request));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EditingCustom_UpdateForDelete([DataSourceRequest] DataSourceRequest request,
    [Bind(Prefix = "models")]IEnumerable<PricesOffersDetailsViewModel> ItemRequests)
        {
            if (ItemRequests != null && ModelState.IsValid)
            {
                foreach (var ItemRequest in ItemRequests)
                {
                    PricesOffersDetailsServiceForIndex.Update(ItemRequest);
                }
            }

            return Json(ItemRequests.ToDataSourceResult(request, ModelState));
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EditingCustom_DestroyForDelete([DataSourceRequest] DataSourceRequest request,
            [Bind(Prefix = "models")]IEnumerable<PricesOffersDetailsViewModel> ItemRequests)
        {
            foreach (var ItemRequest in ItemRequests)
            {
                PricesOffersDetailsServiceForIndex.Destroy(ItemRequest);
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
            PricesOffersMaster PricesOffersMaster = db.PricesOffersMaster.Find(id);
            if (PricesOffersMaster == null)
            {
                return HttpNotFound();
            }
            return View(PricesOffersMaster);
        }

        // POST: BillPurchasesMaster/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PricesOffersMaster PricesOffersMaster = db.PricesOffersMaster.Find(id);
            List<PricesOffersDetails> PricesOffersDetails = db.PricesOffersDetails.Where(x => x.billId == id).ToList();
            foreach (PricesOffersDetails i in PricesOffersDetails)
            {
                i.isDeleted = true;
                //StoreTransaction storeTransaction = db.StoreTransactions.Where(x => x.billId == id && x.ConstructionMaterialId == i.ConstructionMaterial.ID).FirstOrDefault();
                //storeTransaction.isDeleted = true;
                //db.StoreTransactions.Attach(storeTransaction);
                //db.Entry(storeTransaction).State = EntityState.Modified;
                //db.SaveChanges();

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
            PricesOffersMaster.isDeleted = true;
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
    }
}