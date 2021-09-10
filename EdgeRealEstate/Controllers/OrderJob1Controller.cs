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
    public class OrderJob1Controller : Controller
    {
        // GET: OrderJob
        int FormId = 40;
        ApplicationDbContext db = new ApplicationDbContext();
        private OrderJobDetailsService OrderJobDetailsService;
        private OrderJobDetailsServiceForIndex OrderJobDetailsServiceForIndex;
        public OrderJob1Controller()
        {
            this.OrderJobDetailsService = new OrderJobDetailsService(db);
            this.OrderJobDetailsServiceForIndex = new OrderJobDetailsServiceForIndex(db);
        }
        // GET: MasterDetailsTry
        public ActionResult Index(int? page/*, int? id*/)
        {
            ///////////// get permission
            //  int empId = (int)Session["empId"];
            //  var masterid = db.PermissionMaster.Where(x => x.empId == empId).FirstOrDefault().id;
            //  var query = db.PermissionDetail.Where(x => x.PermissionMasterId == masterid
            //&& x.PageId == FormId).ToList();
            //  bool? pAdd, pEdit, pDel, pBrowser = null;

            //  if (query.Count() != 0)
            //  {
            //      pAdd = query.FirstOrDefault().pAdd;
            //      ViewBag.pAdd = pAdd;
            //      pEdit = query.FirstOrDefault().pEdit;
            //      ViewBag.pEdit = pEdit;
            //      pDel = query.FirstOrDefault().pDel;
            //      ViewBag.pDel = pDel;
            //      pBrowser = query.FirstOrDefault().pBrowser;
            //  }
            //  else
            //  {
            //      ViewBag.pAdd = true;
            //      ViewBag.pEdit = true;
            //      ViewBag.pDel = true;
            //  }
            //  ///////////////
            //  if (pBrowser == false)
            //  {
            //      return RedirectToAction("AccessDeny");
            //  }
            //  else
            //  {
            //if (id == null)
            //{
            var BillSalesMasters = db.OrderJob.Where(x => x.IsDeleted == false)
            .OrderByDescending(x => x.Id).ToList().ToPagedList(page ?? 1, 1);
            if (BillSalesMasters.Count() != 0)
            {
                Session["MasterIdFromIndex"] = BillSalesMasters.FirstOrDefault().Id;
            }
            else
            {
                Session["MasterIdFromIndex"] = 1;
            }

            return View(BillSalesMasters);
        }
        //}
        //else
        //{
        //    var BillSalesMasters = db.BillSalesMasters
        //        .Where(x => x.isDeleted == false && x.id == id)
        //        .OrderByDescending(x => x.id).ToList().ToPagedList(page ?? 1, 1);
        //    if (BillSalesMasters.Count() != 0)
        //    {
        //        Session["MasterIdFromIndex"] = BillSalesMasters.FirstOrDefault().id;
        //    }
        //    else
        //    {
        //        Session["MasterIdFromIndex"] = 1;
        //    }

        //    return View(BillSalesMasters);
        //}
        //}
        //}

        // GET: BillSalesMaster/Delete/5
        public ActionResult Delete(int? id)
        {
            //        ///////////// get permission
            //        int empId = (int)Session["empId"];
            //        var masterid = db.PermissionMaster.Where(x => x.empId == empId).FirstOrDefault().id;
            //        var query = db.PermissionDetail.Where(x => x.PermissionMasterId == masterid
            //&& x.PageId == FormId).ToList();
            //        bool? pDel = null;
            //        if (query.Count() != 0)
            //        {
            //            pDel = query.FirstOrDefault().pDel;
            //        }
            //        ///////////////
            //        if (pDel == false)
            //        {
            //            return RedirectToAction("AccessDeny");
            //        }
            //        else
            //        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderJob BillSalesMasters = db.OrderJob.Find(id);
            if (BillSalesMasters == null)
            {
                return HttpNotFound();
            }
            return View(BillSalesMasters);
        }
        //}

        // POST: BillSalesMaster/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            OrderJob BillSalesMasters = db.OrderJob.Find(id);
            List<OrderJobDetails> BillSalesDetails = db.OrderJobDetails.Where(x => x.OrderJobId == id).ToList();
            foreach (OrderJobDetails i in BillSalesDetails)
            {
                i.IsDeleted = true;
                //StoreTransaction storeTransaction = db.StoreTransactions.Where(x => x.billId == id && x.itmId == i.Items.id).FirstOrDefault();
                //if (storeTransaction != null)
                //{
                //    storeTransaction.isDeleted = true;
                //    db.StoreTransactions.Attach(storeTransaction);
                //    db.Entry(storeTransaction).State = EntityState.Modified;
                //    db.SaveChanges();
                //}
            }
            BillSalesMasters.IsDeleted = true;
            db.SaveChanges();

            //PaperReceipt paperReceipt = db.PaperReceipts.Where(x => x.billId == id).FirstOrDefault();
            //if (paperReceipt != null)
            //{
            //    paperReceipt.isDeleted = true;
            //    db.PaperReceipts.Attach(paperReceipt);
            //    db.Entry(paperReceipt).State = EntityState.Modified;
            //    db.SaveChanges();
            //}

            // سندات القبض
            //Journal journal = db.Journal.Where(x => x.billId == id && x.flag == 2).FirstOrDefault();
            //journal.IsDeleted = true;
            //db.Journal.Attach(journal);
            //db.Entry(journal).State = EntityState.Modified;
            //db.SaveChanges();
            //var jornalDetail = db.JornalDetail.Where(x => x.JornalId == journal.id && x.flag == 2).ToList();
            //foreach (var i in jornalDetail)
            //{
            //    i.IsDeleted = true;
            //    db.JornalDetail.Attach(i);
            //    db.Entry(i).State = EntityState.Modified;
            //    db.SaveChanges();
            //}
            ////////////////

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
        public ActionResult Create(string OrderJobCode, string SpecialNote,string Note,bool IsCreateContract,
            int? contractorId,int? MainProjectId,int? ProjectsId,int? receiptEngenneringId
            ,int? SupervisorEngenneringId)
        //    (int? empID, int? customersId, string refNo, int? salesManId, string billType,
        //string refType, string paidType, DateTime? addDate, bool isApproval, decimal? tax, double? disPer
        //    , decimal? disMoney, decimal? total, decimal? paid, decimal? net, string billTypeCredit
        //    , decimal? disNosum, decimal? sumtax)
        {
            //Session["refType"] = refType;
            //Session["refNo"] = refNo;
            OrderJob OrderJob = new OrderJob();
            OrderJob.OrderJobCode = OrderJobCode ;
            OrderJob.SpecialNote = SpecialNote;
            OrderJob.Note = Note;
            OrderJob.IsCreateContract = IsCreateContract;
            OrderJob.contractorId = contractorId;
            OrderJob.MainProjectId = MainProjectId;
            OrderJob.ProjectsId = ProjectsId;
            OrderJob.receiptEngenneringId = receiptEngenneringId;
            OrderJob.SupervisorEngenneringId = SupervisorEngenneringId;
            //BillSalesMasters.refNo = refNo;
            //BillSalesMasters.salesManId = salesManId;
            //if (billTypeCredit != "")
            //{
            //    BillSalesMasters.billType = billTypeCredit;
            //    billType = "";
            //}
            //else
            //{
            //    BillSalesMasters.billType = billType;
            //}

            //BillSalesMasters.refType = refType;
            //BillSalesMasters.paidType = paidType;
            //if (addDate == null)
            //{
            //    BillSalesMasters.addDate = DateTime.Now;
            //}
            //else
            //{
            //    BillSalesMasters.addDate = addDate;
            //}
            //BillSalesMasters.isApproval = isApproval;
            //BillSalesMasters.tax = tax;
            //BillSalesMasters.disPer = disPer;
            //BillSalesMasters.disMoney = disMoney;
            //BillSalesMasters.total = total;
            //BillSalesMasters.paid = paid;
            //BillSalesMasters.net = net/*(total + tax) - disMoney*/;
            //Decimal? currentValue = db.Customers.Where(x => x.Id == customersId).FirstOrDefault().currentValue;
            //Decimal moneyLimit = db.Customers.Where(x => x.Id == customersId).FirstOrDefault().moneyLimit;
            //if ((billTypeCredit != "" && paid == net) || billTypeCredit == "")
            //{
            //    if (currentValue + net < moneyLimit)
            //    {
            OrderJobDetailsService.createMaster(OrderJob);
            Session["BillSalesMasterId"] = OrderJob.Id;
            //Session["addDate"] = BillSalesMasters.addDate;
            // create  bill history
            //        BillHistory billHistory = new BillHistory();
            //        billHistory.custId = customersId;
            //        billHistory.empId = empID;
            //        billHistory.billId = BillSalesMasters.id;
            //        billHistory.net = net;
            //        billHistory.paid = paid;
            //        billHistory.billType = billType;
            //        billHistory.IsDeleted = false;
            //        billHistory.IsSalesBill = true;
            //        db.BillHistory.Add(billHistory);
            //        db.SaveChanges();
            //        Session["billHistoryId"] = billHistory.id;
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
            //        //Journal journal = new Journal();
            //        //journal.billId = (int)Session["BillSalesMasterId"];
            //        //journal.accountId = 17;
            //        //journal.JournalDate = addDate;
            //        //journal.accountValue = net;
            //        //journal.disMoney = disMoney + disNosum;
            //        ////journal.total = net + disMoney + disNosum;
            //        //journal.total = journal.accountValue + journal.disMoney;
            //        //journal.flag = 2;
            //        //var query = db.Journal.Where(x => x.flag == 2).Max(x => x.PaperId);
            //        //int? PaperId = 0;
            //        //if (query == null)
            //        //{
            //        //    PaperId = 1;
            //        //}
            //        //else
            //        //{
            //        //    PaperId = (query) + 1;
            //        //}
            //        //journal.PaperId = PaperId;
            //        //db.Journal.Add(journal);
            //        //db.SaveChanges();
            //        //Session["journalId"] = journal.id;
            //        ////الصندوق
            //        //JornalDetail jornalDetail1 = new JornalDetail();
            //        //jornalDetail1.JornalId = (int)Session["journalId"];
            //        //jornalDetail1.AccountID = 17;
            //        //jornalDetail1.Debit = net;
            //        //jornalDetail1.flag = 2;
            //        //db.JornalDetail.Add(jornalDetail1);
            //        //db.SaveChanges();
            //        //// الخصم
            //        //JornalDetail jornalDetail2 = new JornalDetail();
            //        //jornalDetail2.JornalId = (int)Session["journalId"];
            //        //jornalDetail2.AccountID = 111;
            //        //jornalDetail2.Debit = disMoney + disNosum;
            //        //jornalDetail2.flag = 2;
            //        //db.JornalDetail.Add(jornalDetail2);
            //        //db.SaveChanges();

            //        ////حساب المبيعات
            //        //JornalDetail jornalDetail3 = new JornalDetail();
            //        //jornalDetail3.JornalId = (int)Session["journalId"];
            //        //jornalDetail3.AccountID = 113;
            //        //jornalDetail3.Credit = net - sumtax + (disMoney + disNosum);
            //        //jornalDetail3.flag = 2;
            //        //db.JornalDetail.Add(jornalDetail3);
            //        //db.SaveChanges();
            //        ////حساب الضريبة
            //        //JornalDetail jornalDetail4 = new JornalDetail();
            //        //jornalDetail4.JornalId = (int)Session["journalId"];
            //        //jornalDetail4.AccountID = 112;
            //        //jornalDetail4.Credit = sumtax;
            //        //jornalDetail4.flag = 2;
            //        //db.JornalDetail.Add(jornalDetail4);
            //        //db.SaveChanges();
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

            //            //Journal journal = new Journal();
            //            //journal.billId = (int)Session["BillSalesMasterId"];
            //            //journal.accountId = 17;
            //            //journal.JournalDate = addDate;
            //            //journal.accountValue = net;
            //            //journal.disMoney = disMoney + disNosum;
            //            ////journal.total = net + disMoney + disNosum;
            //            //journal.total = journal.accountValue + journal.disMoney;
            //            //journal.flag = 2;
            //            //var query = db.Journal.Where(x => x.flag == 2).Max(x => x.PaperId);
            //            //int? PaperId = 0;
            //            //if (query == null)
            //            //{
            //            //    PaperId = 1;
            //            //}
            //            //else
            //            //{
            //            //    PaperId = (query) + 1;
            //            //}
            //            //journal.PaperId = PaperId;
            //            //db.Journal.Add(journal);
            //            //db.SaveChanges();
            //            //Session["journalId"] = journal.id;
            //            ////الصندوق
            //            //JornalDetail jornalDetail1 = new JornalDetail();
            //            //jornalDetail1.JornalId = (int)Session["journalId"];
            //            //jornalDetail1.AccountID = 17;
            //            //jornalDetail1.Debit = net;
            //            //jornalDetail1.flag = 2;
            //            //db.JornalDetail.Add(jornalDetail1);
            //            //db.SaveChanges();
            //            //// الخصم
            //            //JornalDetail jornalDetail2 = new JornalDetail();
            //            //jornalDetail2.JornalId = (int)Session["journalId"];
            //            //jornalDetail2.AccountID = 111;
            //            //jornalDetail2.Debit = disMoney + disNosum;
            //            //jornalDetail2.flag = 2;
            //            //db.JornalDetail.Add(jornalDetail2);
            //            //db.SaveChanges();

            //            ////حساب المبيعات
            //            //JornalDetail jornalDetail3 = new JornalDetail();
            //            //jornalDetail3.JornalId = (int)Session["journalId"];
            //            //jornalDetail3.AccountID = 113;
            //            //jornalDetail3.Credit = net - sumtax + (disMoney + disNosum);
            //            //jornalDetail3.flag = 2;
            //            //db.JornalDetail.Add(jornalDetail3);
            //            //db.SaveChanges();
            //            ////حساب الضريبة
            //            //JornalDetail jornalDetail4 = new JornalDetail();
            //            //jornalDetail4.JornalId = (int)Session["journalId"];
            //            //jornalDetail4.AccountID = 112;
            //            //jornalDetail4.Credit = sumtax;
            //            //jornalDetail4.flag = 2;
            //            //db.JornalDetail.Add(jornalDetail4);
            //            //db.SaveChanges();
            //        }
            //    }
            //}
            return View();
        }
        public ActionResult Editing_Custom()
        {
            // ///////////// get permission
            // int empId = (int)Session["empId"];
            // var masterid = db.PermissionMaster.Where(x => x.empId == empId).FirstOrDefault().id;
            // var query = db.PermissionDetail.Where(x => x.PermissionMasterId == masterid
            //&& x.PageId == FormId).ToList();
            // bool? pAdd = null;
            // if (query.Count() != 0)
            // {
            //     pAdd = query.FirstOrDefault().pAdd;
            // }
            // ///////////////
            // if (pAdd == false)
            // {
            //     //return RedirectToAction("AccessDeny");
            //     return RedirectToAction("Index");
            // }
            // else
            // {
            ViewBag.contractor = new SelectList(db.contractor, "Id", "ARName");
            ViewBag.SupervisorEngennering = new SelectList(db.Engennerings, "id", "Aname");
            ViewBag.receiptEngennering = new SelectList(db.Engennerings, "id", "Aname");
            ViewBag.MainProjects = new SelectList(db.MainProjects, "id", "Aname");
            ViewBag.Projectes = new SelectList(db.Projectes, "id", "Aname");
            //PopulateItems();
            //PopulateLKStores();
            return View();
            //}
        }
        public ActionResult EditingCustom_Read([DataSourceRequest] DataSourceRequest request)
        {
            return Json(/*BillSalesDetailsService.Read().ToDataSourceResult(*/request/*)*/);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EditingCustom_Update([DataSourceRequest] DataSourceRequest request,
            [Bind(Prefix = "models")]IEnumerable<OrderJobDetailsViewModel> ItemRequests)
        {
            if (ItemRequests != null && ModelState.IsValid)
            {
                foreach (var ItemRequest in ItemRequests)
                {
                    OrderJobDetailsService.Update(ItemRequest);
                }
            }

            return Json(ItemRequests.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EditingCustom_Create([DataSourceRequest] DataSourceRequest request,
            [Bind(Prefix = "models")]IEnumerable<OrderJobDetailsViewModel> ItemRequests)
        {
            var results = new List<OrderJobDetailsViewModel>();

            if (ItemRequests != null && ModelState.IsValid)
            {
                foreach (var ItemRequest in ItemRequests)
                {
                    OrderJobDetailsService.Create(ItemRequest);
                    results.Add(ItemRequest);
                    db.SaveChanges();
                }
            }

            return Json(results.ToDataSourceResult(request, ModelState));
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EditingCustom_Destroy([DataSourceRequest] DataSourceRequest request,
            [Bind(Prefix = "models")]IEnumerable<OrderJobDetailsViewModel> ItemRequests)
        {
            foreach (var ItemRequest in ItemRequests)
            {
                OrderJobDetailsService.Destroy(ItemRequest);
            }

            return Json(ItemRequests.ToDataSourceResult(request, ModelState));
        }


        /*************************Index****************************/
        public ActionResult Editing_CustomForIndex()
        {
            PopulateItems();
            //PopulateLKStores();
            return View();
        }
        public ActionResult EditingCustom_ReadForIndex([DataSourceRequest] DataSourceRequest request)
        {
            return Json(OrderJobDetailsServiceForIndex.Read().ToDataSourceResult(request));
        }

        /********************Edit ********************/
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Edit(int? id, string OrderJobCode, string SpecialNote, string Note, bool IsCreateContract,
            int contractorId, int MainProjectId, int ProjectsId, int receiptEngenneringId
            , int SupervisorEngenneringId)
        //, int? empID, int? customersId, string refNo, int? salesManId, string billType,
        //string refType, string paidType, DateTime addDate, bool isApproval, decimal? paid, decimal? tax, double? disPer
        //, decimal? disMoney, decimal? total, decimal? net, string billTypeCredit
        //, decimal? disNosum, decimal? sumtax)
        {
            OrderJob OrderJob = db.OrderJob.Find(id);
            OrderJob.OrderJobCode = OrderJobCode;
            OrderJob.SpecialNote = SpecialNote;
            OrderJob.Note = Note;
            OrderJob.IsCreateContract = IsCreateContract;
            OrderJob.contractorId = contractorId;
            OrderJob.MainProjectId = MainProjectId;
            OrderJob.ProjectsId = ProjectsId;
            OrderJob.receiptEngenneringId = receiptEngenneringId;
            OrderJob.SupervisorEngenneringId = SupervisorEngenneringId;
            //BillSalesMasters.refNo = refNo;
            //BillSalesMasters.salesManId = salesManId;
            //var query = db.Customers.Where(x => x.Id == customersId).ToList().FirstOrDefault();
            //var IsCredit = query.isCredit;
            //if (IsCredit == false)
            //{
            //    BillSalesMasters.billType = billTypeCredit;
            //    billType = "";
            //}
            //else
            //{
            //    BillSalesMasters.billType = billType;
            //}
            //BillSalesMasters.refType = refType;
            //BillSalesMasters.paidType = paidType;
            //BillSalesMasters.addDate = addDate;
            //BillSalesMasters.isApproval = isApproval;
            //BillSalesMasters.paid = paid;
            //BillSalesMasters.tax = tax;
            //BillSalesMasters.disPer = disPer;
            //BillSalesMasters.disMoney = disMoney;
            //BillSalesMasters.total = total;
            //BillSalesMasters.net = net;
            //Decimal? currentValue = db.Customers.Where(x => x.Id == customersId).FirstOrDefault().currentValue;
            //Decimal moneyLimit = db.Customers.Where(x => x.Id == customersId).FirstOrDefault().moneyLimit;
            //if ((IsCredit == false && paid == net) || IsCredit == true)
            //{
            //if (currentValue + net < moneyLimit)
            //{
            OrderJobDetailsServiceForIndex.updateMaster(OrderJob);
            Session["BillSalesMasterId"] = OrderJob.Id;
            //Session["addDate"] = BillSalesMasters.addDate;
            ViewBag.msgg = 1;
            //}
            //}
            //if (billType == "نقدى" || IsCredit == false && paid == net)
            //{
            //if (currentValue + net < moneyLimit)
            //{
            //int billId = (int)Session["BillSalesMasterId"];
            //Journal journal = db.Journal.Where(x => x.billId == billId).FirstOrDefault();
            //journal.JournalDate = addDate;
            //journal.accountValue = net;
            //journal.disMoney = disMoney + disNosum;
            //journal.total = journal.accountValue + journal.disMoney;
            //db.Journal.Attach(journal);
            //db.Entry(journal).State = EntityState.Modified;
            //db.SaveChanges();
            //Session["journalId"] = journal.id;
            //int journalId = (int)Session["journalId"];
            ////الصندوق
            //JornalDetail jornalDetail1 = db.JornalDetail
            //    .Where(x => x.JornalId == journalId && x.AccountID == 17)
            //    .FirstOrDefault();
            //jornalDetail1.Debit = net;
            //db.JornalDetail.Attach(jornalDetail1);
            //db.Entry(jornalDetail1).State = EntityState.Modified;
            //db.SaveChanges();
            //// الخصم
            //JornalDetail jornalDetail2 = db.JornalDetail
            //    .Where(x => x.JornalId == journalId && x.AccountID == 111)
            //    .FirstOrDefault();
            //jornalDetail2.Debit = disMoney + disNosum;
            //db.JornalDetail.Attach(jornalDetail2);
            //db.Entry(jornalDetail2).State = EntityState.Modified;
            //db.SaveChanges();
            ////حساب المبيعات
            //JornalDetail jornalDetail3 = db.JornalDetail
            //    .Where(x => x.JornalId == journalId && x.AccountID == 113)
            //    .FirstOrDefault();
            //jornalDetail3.Credit = net - sumtax + (disMoney + disNosum);
            //db.JornalDetail.Attach(jornalDetail3);
            //db.Entry(jornalDetail3).State = EntityState.Modified;
            //db.SaveChanges();
            ////حساب الضريبة
            //JornalDetail jornalDetail4 = db.JornalDetail
            //    .Where(x => x.JornalId == journalId && x.AccountID == 112)
            //    .FirstOrDefault();
            //jornalDetail4.Credit = sumtax;
            //db.JornalDetail.Attach(jornalDetail4);
            //db.Entry(jornalDetail4).State = EntityState.Modified;
            //db.SaveChanges();

            //PaperReceipt paperReceipt = db.PaperReceipts.Where(x => x.billId == id).FirstOrDefault();
            //paperReceipt.customerId = customersId;
            //paperReceipt.empId = empID;
            //paperReceipt.salesManId = salesManId;
            //paperReceipt.paidMethod = paidType;
            //paperReceipt.paid = paid;
            //paperReceipt.refType = refType;
            //paperReceipt.indate = addDate;
            //paperReceipt.isDeleted = false;
            ////paperReceipt.billId = BillSalesMasters.id;
            //db.PaperReceipts.Attach(paperReceipt);
            //db.Entry(paperReceipt).State = EntityState.Modified;
            //db.SaveChanges();
            //}
            //}
            //if (billType == "اجل")
            //{
            //    //if (currentValue + net < moneyLimit)
            //    //{
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
            //    //}
            //    if (paid != null)
            //    {
            //        //if (currentValue + net < moneyLimit)
            //        //{
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
            //        //int billId = (int)Session["BillSalesMasterId"];
            //        //Journal journal = db.Journal.Where(x => x.billId == billId).FirstOrDefault();
            //        //journal.JournalDate = addDate;
            //        //journal.accountValue = net;
            //        //journal.disMoney = disMoney + disNosum;
            //        //journal.total = journal.accountValue + journal.disMoney;
            //        //db.Journal.Attach(journal);
            //        //db.Entry(journal).State = EntityState.Modified;
            //        //db.SaveChanges();
            //        //Session["journalId"] = journal.id;
            //        //int journalId = (int)Session["journalId"];
            //        ////الصندوق
            //        //JornalDetail jornalDetail1 = db.JornalDetail
            //        //    .Where(x => x.JornalId == journalId && x.AccountID == 17)
            //        //    .FirstOrDefault();
            //        //jornalDetail1.Debit = net;
            //        //db.JornalDetail.Attach(jornalDetail1);
            //        //db.Entry(jornalDetail1).State = EntityState.Modified;
            //        //db.SaveChanges();
            //        //// الخصم
            //        //JornalDetail jornalDetail2 = db.JornalDetail
            //        //    .Where(x => x.JornalId == journalId && x.AccountID == 111)
            //        //    .FirstOrDefault();
            //        //jornalDetail2.Debit = disMoney + disNosum;
            //        //db.JornalDetail.Attach(jornalDetail2);
            //        //db.Entry(jornalDetail2).State = EntityState.Modified;
            //        //db.SaveChanges();
            //        ////حساب المبيعات
            //        //JornalDetail jornalDetail3 = db.JornalDetail
            //        //    .Where(x => x.JornalId == journalId && x.AccountID == 113)
            //        //    .FirstOrDefault();
            //        //jornalDetail3.Credit = net - sumtax + (disMoney + disNosum);
            //        //db.JornalDetail.Attach(jornalDetail3);
            //        //db.Entry(jornalDetail3).State = EntityState.Modified;
            //        //db.SaveChanges();
            //        ////حساب الضريبة
            //        //JornalDetail jornalDetail4 = db.JornalDetail
            //        //    .Where(x => x.JornalId == journalId && x.AccountID == 112)
            //        //    .FirstOrDefault();
            //        //jornalDetail4.Credit = sumtax;
            //        //db.JornalDetail.Attach(jornalDetail4);
            //        //db.Entry(jornalDetail4).State = EntityState.Modified;
            //        //db.SaveChanges();
            //        ////}
            //    }
            //}
            return View(OrderJob);
        }
        public ActionResult Editing_CustomForEdit(int? id)
        {
          //  ///////////// get permission
          //  int empId = (int)Session["empId"];
          //  var masterid = db.PermissionMaster.Where(x => x.empId == empId).FirstOrDefault().id;
          //  var query = db.PermissionDetail.Where(x => x.PermissionMasterId == masterid
          //&& x.PageId == FormId).ToList();
          //  bool? pEdit = null;
          //  if (query.Count() != 0)
          //  {
          //      pEdit = query.FirstOrDefault().pEdit;
          //  }
          //  ///////////////
          //  if (pEdit == false)
          //  {
          //      return RedirectToAction("AccessDeny");
          //  }
          //  else
          //  {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                OrderJob OrderJob = db.OrderJob.Find(id);
            //ViewBag.Customers = new SelectList(db.Customers.Where(x => x.Type.Equals(false)), "Id", "ARName");
            //ViewBag.Employees = new SelectList(db.Employees, "Id", "ARName");
            //ViewBag.SalesMans = new SelectList(db.Employees, "Id", "ARName");
            ViewBag.OrderJobCode = OrderJob.OrderJobCode;
            ViewBag.SpecialNote = OrderJob.SpecialNote;
            ViewBag.Note = OrderJob.Note;
            ViewBag.IsCreateContract = OrderJob.IsCreateContract;
            ViewBag.contractorId = OrderJob.contractorId;
            ViewBag.MainProjectId = OrderJob.MainProjectId;
            ViewBag.ProjectsId = OrderJob.ProjectsId;
            ViewBag.receiptEngenneringId = OrderJob.receiptEngenneringId;
            ViewBag.SupervisorEngenneringId = OrderJob.SupervisorEngenneringId;
            //ViewBag.refType = BillSalesMasters.refType;
            //ViewBag.paidType = BillSalesMasters.paidType;
            //ViewBag.addDate = BillSalesMasters.addDate;
            //ViewBag.isApproval = BillSalesMasters.isApproval;
            //ViewBag.paid = BillSalesMasters.paid;
            //ViewBag.tax = BillSalesMasters.tax;
            //ViewBag.disPer = BillSalesMasters.disPer;
            //ViewBag.disMoney = BillSalesMasters.disMoney;
            //ViewBag.total = BillSalesMasters.total;
            //ViewBag.net = BillSalesMasters.net;
            //var query1 = db.Customers.Where(x => x.Id == BillSalesMasters.customersId).ToList().FirstOrDefault();
            //var IsCredit = query1.isCredit;
            //ViewBag.IsCredit = IsCredit;
            if (OrderJob == null)
                {
                    return HttpNotFound();
                }
                // PopulateItems();
                return View(OrderJob);
            //}
        }

        public ActionResult EditingCustom_ReadForEdit([DataSourceRequest] DataSourceRequest request)
        {
            return Json(OrderJobDetailsServiceForIndex.Read().ToDataSourceResult(request));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EditingCustom_UpdateForEdit([DataSourceRequest] DataSourceRequest request,
            [Bind(Prefix = "models")]IEnumerable<OrderJobDetailsViewModel> ItemRequests)
        {
            if (ItemRequests != null && ModelState.IsValid)
            {
                foreach (var ItemRequest in ItemRequests)
                {
                    OrderJobDetailsServiceForIndex.Update(ItemRequest);
                }
            }

            return Json(ItemRequests.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EditingCustom_CreateForEdit([DataSourceRequest] DataSourceRequest request,
                [Bind(Prefix = "models")]IEnumerable<OrderJobDetailsViewModel> ItemRequests)
        {
            var results = new List<OrderJobDetailsViewModel>();

            if (ItemRequests != null && ModelState.IsValid)
            {
                foreach (var ItemRequest in ItemRequests)
                {
                    OrderJobDetailsServiceForIndex.Create(ItemRequest);
                    results.Add(ItemRequest);
                    db.SaveChanges();
                }
            }

            return Json(results.ToDataSourceResult(request, ModelState));
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EditingCustom_DestroyForEdit([DataSourceRequest] DataSourceRequest request,
            [Bind(Prefix = "models")]IEnumerable<OrderJobDetailsViewModel> ItemRequests)
        {
            foreach (var ItemRequest in ItemRequests)
            {
                OrderJobDetailsServiceForIndex.Destroy(ItemRequest);
            }

            return Json(ItemRequests.ToDataSourceResult(request, ModelState));
        }
        /**********************Delete view************************/
        public ActionResult Editing_CustomForDelete()
        {
            // PopulateItems();
            //PopulateLKStores();

            return View();
        }

        public ActionResult EditingCustom_ReadForDelete([DataSourceRequest] DataSourceRequest request)
        {
            return Json(OrderJobDetailsServiceForIndex.Read().ToDataSourceResult(request));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EditingCustom_UpdateForDelete([DataSourceRequest] DataSourceRequest request,
    [Bind(Prefix = "models")]IEnumerable<OrderJobDetailsViewModel> ItemRequests)
        {
            if (ItemRequests != null && ModelState.IsValid)
            {
                foreach (var ItemRequest in ItemRequests)
                {
                    OrderJobDetailsServiceForIndex.Update(ItemRequest);
                }
            }

            return Json(ItemRequests.ToDataSourceResult(request, ModelState));
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EditingCustom_DestroyForDelete([DataSourceRequest] DataSourceRequest request,
            [Bind(Prefix = "models")]IEnumerable<OrderJobDetailsViewModel> ItemRequests)
        {
            foreach (var ItemRequest in ItemRequests)
            {
                OrderJobDetailsServiceForIndex.Destroy(ItemRequest);
            }

            return Json(ItemRequests.ToDataSourceResult(request, ModelState));
        }
        /*******************Helper methods*******************/
        public JsonResult ServerFiltering_GetStage(string text)
        {
            var dataContext = new ApplicationDbContext();


            var Items = dataContext.Stages.Select(i => new StageVM
            {
                id = i.id,
                Aname = i.Aname,
                code = i.code
            });

            if (!string.IsNullOrEmpty(text))
            {
                Items = Items.Where(p => p.Aname.Contains(text) || p.code.Contains(text));
            }

            return Json(Items, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ServerFiltering_GetMainItems(string text)
        {
            var dataContext = new ApplicationDbContext();


            var Items = dataContext.MianItemss.Select(i => new MainItemViewModel
            {
                ID = i.Id,
                Name = i.Aname
             
            });

            if (!string.IsNullOrEmpty(text))
            {
                Items = Items.Where(p => p.Name.Contains(text) /*|| p.code.Contains(text)*/);
            }

            return Json(Items, JsonRequestBehavior.AllowGet);
        }
        private void PopulateItems()
        {
            var dataContext = new ApplicationDbContext();
            var Items = dataContext.Stages
                        .Select(i => new StageVM
                        {
                            id = i.id,
                            Aname = i.Aname

                        });
            //.OrderBy(e => e.arName);

            ViewData["Items"] = Items;
            ViewData["defaultItems"] = Items.First();
        }
        private void PopulateLKStores()
        {
            var dataContext = new ApplicationDbContext();
            var Items = dataContext.Stages
                        .Select(i => new StageVM
                        {
                            id = i.id,
                            Aname = i.Aname

                        });
            //.OrderBy(e => e.arName);

            ViewData["Items"] = Items;
            ViewData["defaultItems"] = Items.First();
        }


        //      [HttpGet]
        //      public JsonResult Getprice(int itmId, int customersId)
        //      {
        //          // int billId = (int)Session["BillSalesMasterId"];
        //          // var Master = db.BillSalesMasters.Where(x => x.id == billId).ToList().FirstOrDefault();
        //          var customer = db.Customers.Where(x => x.Id == customersId).ToList().FirstOrDefault();
        //          var priceplane = customer.pricePlanId;
        //          var query = db.ItemPrices.Where(x => x.ItemId == itmId).ToList().FirstOrDefault();
        //          decimal? price;
        //          if (priceplane == 1)
        //          {
        //              price = query.price1;
        //          }
        //          else if (priceplane == 2)
        //          {
        //              price = query.price2;
        //          }
        //          else if (priceplane == 3)
        //          {
        //              price = query.price3;
        //          }
        //          else if (priceplane == 4)
        //          {
        //              price = query.price4;
        //          }
        //          else if (priceplane == 5)
        //          {
        //              price = query.price5;
        //          }
        //          else
        //          {
        //              price = query.price1;
        //          }

        //          return Json(price, JsonRequestBehavior.AllowGet);
        //      }

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

        //      public ActionResult AccessDeny()
        //      {
        //          ///////////// get permission
        //          int empId = (int)Session["empId"];
        //          var masterid = db.PermissionMaster.Where(x => x.empId == empId).FirstOrDefault().id;
        //          var query = db.PermissionDetail.Where(x => x.PermissionMasterId == masterid
        //&& x.PageId == FormId).ToList();
        //          bool? pBrowser = null;
        //          if (query.Count() != 0)
        //          {
        //              pBrowser = query.FirstOrDefault().pBrowser;
        //              ViewBag.pBrowser = pBrowser;
        //          }
        //          else
        //          {
        //              ViewBag.pBrowser = true;
        //          }
        //          ///////////////           
        //          return View();
        //      }

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