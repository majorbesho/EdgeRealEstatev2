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
    public class WarehouseWarrantController : Controller
    {
        // GET: WarehouseWarrant

        ApplicationDbContext db = new ApplicationDbContext();
        private WarehouseWarrantDetailsService WarehouseWarrantDetailsService;
        private WarehouseWarrantDetailsServiceForIndex WarehouseWarrantDetailsServiceForIndex;
        public WarehouseWarrantController()
        {
            this.WarehouseWarrantDetailsService = new WarehouseWarrantDetailsService(db);
            this.WarehouseWarrantDetailsServiceForIndex = new WarehouseWarrantDetailsServiceForIndex(db);
        }
        // GET: MasterDetailsTry
        public ActionResult Index(int? page, int? id)
        {
            var WarehouseWarrantMaster = db.WarehouseWarrantMaster.Where(x => x.isDeleted == false)
            .OrderByDescending(x => x.id).ToList().ToPagedList(page ?? 1, 1);
            if (WarehouseWarrantMaster.Count() != 0)
            {
                Session["MasterIdFromIndex"] = WarehouseWarrantMaster.FirstOrDefault().id;
            }
            else
            {
                Session["MasterIdFromIndex"] = 1;
            }

            return View(WarehouseWarrantMaster);
        }

        // GET: BillSalesMaster/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WarehouseWarrantMaster WarehouseWarrantMaster = db.WarehouseWarrantMaster.Find(id);
            if (WarehouseWarrantMaster == null)
            {
                return HttpNotFound();
            }
            return View(WarehouseWarrantMaster);

        }

        // POST: BillSalesMaster/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            WarehouseWarrantMaster WarehouseWarrantMaster = db.WarehouseWarrantMaster.Find(id);
            List<WarehouseWarrantDetails> WarehouseWarrantDetails = db.WarehouseWarrantDetails.Where(x => x.billId == id).ToList();
            foreach (WarehouseWarrantDetails i in WarehouseWarrantDetails)
            {
                i.isDeleted = true;
                StoreTransaction storeTransaction = db.StoreTransactions.Where(x => x.billId == id 
                && x.ConstructionMaterialId == i.ConstructionMaterial.ID).FirstOrDefault();
                if (storeTransaction != null)
                {
                    storeTransaction.isDeleted = true;
                    db.StoreTransactions.Attach(storeTransaction);
                    db.Entry(storeTransaction).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }
            WarehouseWarrantMaster.isDeleted = true;
            db.SaveChanges();

            //PaperReceipt paperReceipt = db.PaperReceipts.Where(x => x.billId == id).FirstOrDefault();
            //if (paperReceipt != null)
            //{
            //    paperReceipt.isDeleted = true;
            //    db.PaperReceipts.Attach(paperReceipt);
            //    db.Entry(paperReceipt).State = EntityState.Modified;
            //    db.SaveChanges();
            //}

            //if (BillSalesMasters.billType == "اجل")
            //{
            //    Customer customer = db.Customers.Find(BillSalesMasters.customersId);
            //    BillHistory billHistory = db.BillHistory.Where(x => x.billId == id && x.IsSalesBill == true).FirstOrDefault();
            //    customer.currentValue = billHistory.OldCurrentValue;
            //    db.Customers.Attach(customer);
            //    db.Entry(customer).State = EntityState.Modified;
            //    db.SaveChanges();
            //    BillHistory NewBillHistory = new BillHistory();
            //    NewBillHistory.custId = BillSalesMasters.customersId;
            //    NewBillHistory.empId = BillSalesMasters.empID;
            //    NewBillHistory.billId = id;
            //    NewBillHistory.net = BillSalesMasters.net;
            //    NewBillHistory.paid = BillSalesMasters.paid;
            //    NewBillHistory.billType = BillSalesMasters.billType;
            //    NewBillHistory.OldCurrentValue = billHistory.OldCurrentValue;
            //    NewBillHistory.NewCurrentValue = customer.currentValue;
            //    NewBillHistory.IsDeleted = true;
            //    NewBillHistory.IsSalesBill = true;
            //    db.BillHistory.Add(NewBillHistory);
            //    db.SaveChanges();
            //}
            return RedirectToAction("Index");
        }
        /************ create ***********/
        public ActionResult Create(int? empID, int? contractorId, string refNo, int? EngenneringId,
        string refType,DateTime? addDate, decimal? total)
        {
            Session["refType"] = refType;
            Session["refNo"] = refNo;
            WarehouseWarrantMaster WarehouseWarrantMaster = new WarehouseWarrantMaster();
            WarehouseWarrantMaster.empID = empID;
            WarehouseWarrantMaster.contractorId = contractorId;
            WarehouseWarrantMaster.refNo = refNo;
            WarehouseWarrantMaster.EngenneringId = EngenneringId;
            WarehouseWarrantMaster.refType = refType;
            if (addDate == null)
            {
                WarehouseWarrantMaster.addDate = DateTime.Now;
            }
            else
            {
                WarehouseWarrantMaster.addDate = addDate;
            }

            WarehouseWarrantMaster.total = total;


            //Decimal? currentValue = db.Customers.Where(x => x.Id == customersId).FirstOrDefault().currentValue;
            //Decimal moneyLimit = db.Customers.Where(x => x.Id == customersId).FirstOrDefault().moneyLimit;
            //if ((billTypeCredit != "" && paid == net) || billTypeCredit == "")
            //{
            //    if (currentValue + net < moneyLimit)
            //    {
                    WarehouseWarrantDetailsService.createMaster(WarehouseWarrantMaster);
                    Session["BillSalesMasterId"] = WarehouseWarrantMaster.id;
                    Session["addDate"] = WarehouseWarrantMaster.addDate;
                    // create  bill history
                    //BillHistory billHistory = new BillHistory();
                    //billHistory.custId = customersId;
                    //billHistory.empId = empID;
                    //billHistory.billId = BillSalesMasters.id;
                    //billHistory.net = net;
                    //billHistory.paid = paid;
                    //billHistory.billType = billType;
                    //billHistory.IsDeleted = false;
                    //billHistory.IsSalesBill = true;
                    //db.BillHistory.Add(billHistory);
                    //db.SaveChanges();
                    //Session["billHistoryId"] = billHistory.id;
            //    }
            //}
            //if (billType == "نقدى" || billTypeCredit != "" && paid == net)
            //{

            //    if (currentValue + net < moneyLimit)
            //    {
            //        PaperReceipt paperReceipt = new PaperReceipt();
            //        paperReceipt.customerId = customersId;
            //        paperReceipt.empId = empID;
            //        paperReceipt.salesManId = salesManId;
            //        paperReceipt.paidMethod = paidType;
            //        paperReceipt.paid = paid;
            //        paperReceipt.refType = refType;
            //        paperReceipt.indate = addDate;
            //        paperReceipt.isDeleted = false;
            //        paperReceipt.billId = BillSalesMasters.id;
            //        db.PaperReceipts.Add(paperReceipt);
            //        db.SaveChanges();
            //    }
            //}
            //if (billType == "اجل")
            //{
            //    if (currentValue + net < moneyLimit)
            //    {
            //        int billHistoryId = Convert.ToInt32(Session["billHistoryId"]);
            //        BillHistory billHistory = db.BillHistory.Find(billHistoryId);
            //        Customer customer = db.Customers.Find(customersId);
            //        billHistory.OldCurrentValue = customer.currentValue;
            //        decimal? NewCurrentValue = customer.currentValue - paid;
            //        customer.currentValue = customer.currentValue - paid;
            //        db.Customers.Attach(customer);
            //        db.Entry(customer).State = EntityState.Modified;
            //        db.SaveChanges();
            //        billHistory.NewCurrentValue = NewCurrentValue;
            //        db.BillHistory.Attach(billHistory);
            //        db.Entry(billHistory).State = EntityState.Modified;
            //        db.SaveChanges();

            //        if (paid != null)
            //        {
            //            PaperReceipt paperReceipt = new PaperReceipt();
            //            paperReceipt.customerId = customersId;
            //            paperReceipt.empId = empID;
            //            paperReceipt.salesManId = salesManId;
            //            paperReceipt.paidMethod = paidType;
            //            paperReceipt.paid = paid;
            //            paperReceipt.refType = refType;
            //            paperReceipt.indate = addDate;
            //            paperReceipt.isDeleted = false;
            //            paperReceipt.billId = BillSalesMasters.id;
            //            db.PaperReceipts.Add(paperReceipt);
            //            db.SaveChanges();
            //        }
            //    }
            //}
            return View();
        }
        public ActionResult Editing_Custom()
        {
            ViewBag.contractor = new SelectList(db.contractor, "Id", "ARName");
            ViewBag.Employees = new SelectList(db.Employees, "Id", "ARName");
            ViewBag.Engennerings = new SelectList(db.Engennerings, "id", "Aname");
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
            [Bind(Prefix = "models")]IEnumerable<WarehouseWarrantDetailsViewModel> ItemRequests)
        {
            if (ItemRequests != null && ModelState.IsValid)
            {
                foreach (var ItemRequest in ItemRequests)
                {
                    WarehouseWarrantDetailsService.Update(ItemRequest);
                }
            }

            return Json(ItemRequests.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EditingCustom_Create([DataSourceRequest] DataSourceRequest request,
            [Bind(Prefix = "models")]IEnumerable<WarehouseWarrantDetailsViewModel> ItemRequests,
            int? empID, int? contractorId, string refNo, int? EngenneringId,
        string refType, DateTime? addDate, decimal? total)
        {

            Session["refType"] = refType;
            Session["refNo"] = refNo;
            WarehouseWarrantMaster WarehouseWarrantMaster = new WarehouseWarrantMaster();
            WarehouseWarrantMaster.empID = empID;
            WarehouseWarrantMaster.contractorId = contractorId;
            WarehouseWarrantMaster.refNo = refNo;
            WarehouseWarrantMaster.EngenneringId = EngenneringId;
            WarehouseWarrantMaster.refType = refType;
            if (addDate == null)
            {
                WarehouseWarrantMaster.addDate = DateTime.Now;
            }
            else
            {
                WarehouseWarrantMaster.addDate = addDate;
            }
            WarehouseWarrantMaster.total = total;

            WarehouseWarrantDetailsService.createMaster(WarehouseWarrantMaster);
            Session["BillSalesMasterId"] = WarehouseWarrantMaster.id;
            Session["addDate"] = WarehouseWarrantMaster.addDate;

            var results = new List<WarehouseWarrantDetailsViewModel>();

            if (ItemRequests != null && ModelState.IsValid)
            {
                foreach (var ItemRequest in ItemRequests)
                {
                    WarehouseWarrantDetailsService.Create(ItemRequest);
                    results.Add(ItemRequest);
                    db.SaveChanges();
                }
            }

            return Json(results.ToDataSourceResult(request, ModelState));
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EditingCustom_Destroy([DataSourceRequest] DataSourceRequest request,
            [Bind(Prefix = "models")]IEnumerable<WarehouseWarrantDetailsViewModel> ItemRequests)
        {
            foreach (var ItemRequest in ItemRequests)
            {
                WarehouseWarrantDetailsService.Destroy(ItemRequest);
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
            return Json(WarehouseWarrantDetailsServiceForIndex.Read().ToDataSourceResult(request));
        }

        /********************Edit ********************/
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Edit(int? id, int? empID, int? contractorId, string refNo, int? EngenneringId,
        string refType, DateTime? addDate, decimal? total)
        {
            WarehouseWarrantMaster WarehouseWarrantMaster = db.WarehouseWarrantMaster.Find(id);
            WarehouseWarrantMaster.empID = empID;
            WarehouseWarrantMaster.contractorId = contractorId;
            WarehouseWarrantMaster.refNo = refNo;
            WarehouseWarrantMaster.EngenneringId = EngenneringId;
            WarehouseWarrantMaster.refType = refType;
            WarehouseWarrantMaster.addDate = addDate;
            WarehouseWarrantMaster.total = total;
            //Decimal? currentValue = db.Customers.Where(x => x.Id == customersId).FirstOrDefault().currentValue;
            //Decimal moneyLimit = db.Customers.Where(x => x.Id == customersId).FirstOrDefault().moneyLimit;
            //if ((IsCredit == false && paid == net) || IsCredit == true)
            //{
                WarehouseWarrantDetailsServiceForIndex.updateMaster(WarehouseWarrantMaster);
                Session["BillSalesMasterId"] = WarehouseWarrantMaster.id;
                Session["addDate"] = WarehouseWarrantMaster.addDate;
                ViewBag.msgg = 1;
            //}
            //if (billType == "نقدى" || IsCredit == false && paid == net)
            //{
            //    PaperReceipt paperReceipt = db.PaperReceipts.Where(x => x.billId == id).FirstOrDefault();
            //    paperReceipt.customerId = customersId;
            //    paperReceipt.empId = empID;
            //    paperReceipt.salesManId = salesManId;
            //    paperReceipt.paidMethod = paidType;
            //    paperReceipt.paid = paid;
            //    paperReceipt.refType = refType;
            //    paperReceipt.indate = addDate;
            //    paperReceipt.isDeleted = false;
            //    db.PaperReceipts.Attach(paperReceipt);
            //    db.Entry(paperReceipt).State = EntityState.Modified;
            //    db.SaveChanges();

            //}
            //if (billType == "اجل")
            //{

            //    Customer customer = db.Customers.Find(customersId);
            //    BillHistory billHistory = db.BillHistory.Where(x => x.billId == id && x.IsSalesBill == true).FirstOrDefault();
            //    customer.currentValue = billHistory.OldCurrentValue - paid;
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
            //    NewBillHistory.IsSalesBill = true;
            //    db.BillHistory.Add(NewBillHistory);
            //    db.SaveChanges();

            //    if (paid != null)
            //    {

            //        PaperReceipt paperReceipt = new PaperReceipt();
            //        paperReceipt.customerId = customersId;
            //        paperReceipt.empId = empID;
            //        paperReceipt.salesManId = salesManId;
            //        paperReceipt.paidMethod = paidType;
            //        paperReceipt.paid = paid;
            //        paperReceipt.refType = refType;
            //        paperReceipt.indate = addDate;
            //        paperReceipt.isDeleted = false;
            //        paperReceipt.billId = BillSalesMasters.id;
            //        db.PaperReceipts.Add(paperReceipt);
            //        db.SaveChanges();
            //    }
            //}
            return View(WarehouseWarrantMaster);
        }
        public ActionResult Editing_CustomForEdit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WarehouseWarrantMaster WarehouseWarrantMaster = db.WarehouseWarrantMaster.Find(id);
            ViewBag.contractor = new SelectList(db.contractor, "Id", "ARName");
            ViewBag.Employees = new SelectList(db.Employees, "Id", "ARName");
            ViewBag.Engennerings = new SelectList(db.Engennerings, "id", "Aname");
            ViewBag.refNo = WarehouseWarrantMaster.refNo;
            ViewBag.refType = WarehouseWarrantMaster.refType;
            ViewBag.addDate = WarehouseWarrantMaster.addDate;
            ViewBag.total = WarehouseWarrantMaster.total;
            if (WarehouseWarrantMaster == null)
            {
                return HttpNotFound();
            }
            PopulateItems();
            return View(WarehouseWarrantMaster);
        }

        public ActionResult EditingCustom_ReadForEdit([DataSourceRequest] DataSourceRequest request)
        {
            return Json(WarehouseWarrantDetailsServiceForIndex.Read().ToDataSourceResult(request));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EditingCustom_UpdateForEdit([DataSourceRequest] DataSourceRequest request,
            [Bind(Prefix = "models")]IEnumerable<WarehouseWarrantDetailsViewModel> ItemRequests,
            int? id, int? empID, int? contractorId, string refNo, int? EngenneringId,
        string refType, DateTime? addDate, decimal? total)
        {
            WarehouseWarrantMaster WarehouseWarrantMaster = db.WarehouseWarrantMaster.Find(id);
            WarehouseWarrantMaster.empID = empID;
            WarehouseWarrantMaster.contractorId = contractorId;
            WarehouseWarrantMaster.refNo = refNo;
            WarehouseWarrantMaster.EngenneringId = EngenneringId;
            WarehouseWarrantMaster.refType = refType;
            WarehouseWarrantMaster.addDate = addDate;
            WarehouseWarrantMaster.total = total;

            WarehouseWarrantDetailsServiceForIndex.updateMaster(WarehouseWarrantMaster);
            Session["BillSalesMasterId"] = WarehouseWarrantMaster.id;
            Session["addDate"] = WarehouseWarrantMaster.addDate;
            ViewBag.msgg = 1;

            if (ItemRequests != null && ModelState.IsValid)
            {
                foreach (var ItemRequest in ItemRequests)
                {
                    WarehouseWarrantDetailsServiceForIndex.Update(ItemRequest);
                }
            }

            return Json(ItemRequests.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EditingCustom_CreateForEdit([DataSourceRequest] DataSourceRequest request,
                [Bind(Prefix = "models")]IEnumerable<WarehouseWarrantDetailsViewModel> ItemRequests,
        int? id, int? empID, int? contractorId, string refNo, int? EngenneringId,
        string refType, DateTime? addDate, decimal? total)
        {
            WarehouseWarrantMaster WarehouseWarrantMaster = db.WarehouseWarrantMaster.Find(id);
            WarehouseWarrantMaster.empID = empID;
            WarehouseWarrantMaster.contractorId = contractorId;
            WarehouseWarrantMaster.refNo = refNo;
            WarehouseWarrantMaster.EngenneringId = EngenneringId;
            WarehouseWarrantMaster.refType = refType;
            WarehouseWarrantMaster.addDate = addDate;
            WarehouseWarrantMaster.total = total;

            WarehouseWarrantDetailsServiceForIndex.updateMaster(WarehouseWarrantMaster);
            Session["BillSalesMasterId"] = WarehouseWarrantMaster.id;
            Session["addDate"] = WarehouseWarrantMaster.addDate;
            ViewBag.msgg = 1;

            var results = new List<WarehouseWarrantDetailsViewModel>();

            if (ItemRequests != null && ModelState.IsValid)
            {
                foreach (var ItemRequest in ItemRequests)
                {
                    WarehouseWarrantDetailsServiceForIndex.Create(ItemRequest);
                    results.Add(ItemRequest);
                    db.SaveChanges();
                }
            }

            return Json(results.ToDataSourceResult(request, ModelState));
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EditingCustom_DestroyForEdit([DataSourceRequest] DataSourceRequest request,
            [Bind(Prefix = "models")]IEnumerable<WarehouseWarrantDetailsViewModel> ItemRequests)
        {
            foreach (var ItemRequest in ItemRequests)
            {
                WarehouseWarrantDetailsServiceForIndex.Destroy(ItemRequest);
            }

            return Json(ItemRequests.ToDataSourceResult(request, ModelState));
        }
        /**********************Delete view************************/
        public ActionResult Editing_CustomForDelete()
        {
            PopulateItems();
            //PopulateLKStores();

            return View();
        }

        public ActionResult EditingCustom_ReadForDelete([DataSourceRequest] DataSourceRequest request)
        {
            return Json(WarehouseWarrantDetailsServiceForIndex.Read().ToDataSourceResult(request));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EditingCustom_UpdateForDelete([DataSourceRequest] DataSourceRequest request,
    [Bind(Prefix = "models")]IEnumerable<WarehouseWarrantDetailsViewModel> ItemRequests)
        {
            if (ItemRequests != null && ModelState.IsValid)
            {
                foreach (var ItemRequest in ItemRequests)
                {
                    WarehouseWarrantDetailsServiceForIndex.Update(ItemRequest);
                }
            }

            return Json(ItemRequests.ToDataSourceResult(request, ModelState));
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EditingCustom_DestroyForDelete([DataSourceRequest] DataSourceRequest request,
            [Bind(Prefix = "models")]IEnumerable<WarehouseWarrantDetailsViewModel> ItemRequests)
        {
            foreach (var ItemRequest in ItemRequests)
            {
                WarehouseWarrantDetailsServiceForIndex.Destroy(ItemRequest);
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
        //public JsonResult GetTax()
        //{
        //    var tax_query = db.CompanyData.ToList().FirstOrDefault();
        //    var tax = tax_query.tax;

        //    return Json(tax, JsonRequestBehavior.AllowGet);
        //}

        //[HttpGet]
        //public JsonResult GetIsCredit(int customersId)
        //{
        //    var query = db.Customers.Where(x => x.Id == customersId).ToList().FirstOrDefault();
        //    var IsCredit = query.isCredit;

        //    return Json(IsCredit, JsonRequestBehavior.AllowGet);
        //}


        //[HttpGet]
        //public JsonResult getMoneyLimitCheck(int? customersId, decimal net)
        //{
        //    Decimal? currentValue = db.Customers.Where(x => x.Id == customersId).FirstOrDefault().currentValue;
        //    Decimal moneyLimit = db.Customers.Where(x => x.Id == customersId).FirstOrDefault().moneyLimit;
        //    //ViewBag.checkmoneyLimit = currentValue + net < moneyLimit;
        //    bool check = currentValue + net < moneyLimit;
        //    return Json(check, JsonRequestBehavior.AllowGet);
        //}
    }
}