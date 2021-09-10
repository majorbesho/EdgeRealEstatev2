using EdgeRealEstate.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EdgeRealEstate.Models.Services
{
    public class ContPaperReceiptReportService
    {
        private static bool UpdateDatabase = true;
        private ApplicationDbContext db;

        public ContPaperReceiptReportService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public IList<ContPaperReceiptViewModel> GetAll(DateTime? FromDate, DateTime? toDate
            //, int? FromSalesMan, int? toSalesMan,int? FromCust, int? toCust
            )
        {
            DateTime vFromDate = new DateTime(DateTime.Now.Year, 1, 1);
            var result = HttpContext.Current.Session["CustomerMove"] as IList<ContPaperReceiptViewModel>;
            if (result == null || UpdateDatabase)
            {
                if (FromDate == null && toDate == null
                    //&& FromCust == null && toCust == null
                    //&& FromSalesMan == null && toSalesMan == null
                    )
                {
                    result = (from i in db.ContPaperReceipts
                              where i.isDeleted == false
                              select new ContPaperReceiptViewModel
                              {
                                  indate = (DateTime)i.indate,
                                  ContributorId = (int)i.ContributorId,
                                  contName = i.Contributor.ARName,
                                  //salesManId = (int)i.salesManId,
                                  salesManName = i.Employee1.ARName,
                                  paid = (decimal)i.paid,
                                  FromDate = vFromDate,
                                  toDate = DateTime.Now
                              }).ToList();
                    return result;
                }
                else if (FromDate != null && toDate != null
                    //&& FromCust != null && toCust != null 
                    //&& FromSalesMan != null && toSalesMan != null
                    )
                {
                    result = (from i in db.ContPaperReceipts
                              where i.isDeleted == false
                              //&& i.customerId >= FromCust && i.customerId <= toCust
                              && i.indate >= FromDate && i.indate <= toDate
                              //&& i.salesManId >= FromSalesMan && i.salesManId <= toSalesMan
                              select new ContPaperReceiptViewModel
                              {
                                  indate = (DateTime)i.indate,
                                  ContributorId = (int)i.ContributorId,
                                  //salesManId = (int)i.salesManId,
                                  paid = (decimal)i.paid,
                                  contName = i.Contributor.ARName,
                                  salesManName = i.Employee1.ARName,
                                  FromDate = (DateTime)FromDate,
                                  toDate = (DateTime)toDate
                              }).ToList();
                    return result;
                }
                //                else if (FromDate != null && toDate != null && FromCust == null
                //              && toCust == null && FromSalesMan == null && toSalesMan == null)
                //                {
                //                    result = (from i in db.CustPaperReceipts
                //                              where i.isDeleted == false
                //                              && i.indate >= FromDate && i.indate <= toDate
                //                              select new CustPaperReceiptViewModel
                //                              {
                //                                  indate = (DateTime)i.indate,
                //                                  customerId = (int)i.customerId,
                //                                  salesManId = (int)i.salesManId,
                //                                  paid = (decimal)i.paid,
                //                                  custName = i.Customer.ARName,
                //                                  salesManName = i.Employee1.ARName,
                //                                  FromDate = (DateTime)FromDate,
                //                                  toDate = (DateTime)toDate
                //                              }).ToList();
                //                    return result;
                //                }
                //                else if (FromDate != null && toDate != null && FromCust != null
                //          && toCust != null && FromSalesMan == null && toSalesMan == null)
                //                {
                //                    result = (from i in db.CustPaperReceipts
                //                              where i.isDeleted == false
                //                              && i.customerId >= FromCust && i.customerId <= toCust
                //                              && i.indate >= FromDate && i.indate <= toDate
                //                              select new CustPaperReceiptViewModel
                //                              {
                //                                  indate = (DateTime)i.indate,
                //                                  customerId = (int)i.customerId,
                //                                  salesManId = (int)i.salesManId,
                //                                  paid = (decimal)i.paid,
                //                                  custName = i.Customer.ARName,
                //                                  salesManName = i.Employee1.ARName,
                //                                  FromDate = (DateTime)FromDate,
                //                                  toDate = (DateTime)toDate
                //                              }).ToList();
                //                    return result;
                //                }
                //                else if (FromDate == null && toDate == null && FromCust != null
                //&& toCust != null && FromSalesMan == null && toSalesMan == null)
                //                {
                //                    result = (from i in db.CustPaperReceipts
                //                              where i.isDeleted == false
                //                              && i.customerId >= FromCust && i.customerId <= toCust
                //                              select new CustPaperReceiptViewModel
                //                              {
                //                                  indate = (DateTime)i.indate,
                //                                  customerId = (int)i.customerId,
                //                                  salesManId = (int)i.salesManId,
                //                                  paid = (decimal)i.paid,
                //                                  custName = i.Customer.ARName,
                //                                  salesManName = i.Employee1.ARName,
                //                                  FromDate = vFromDate,
                //                                  toDate = DateTime.Now
                //                              }).ToList();
                //                    return result;
                //                }
                //                else if (FromDate == null && toDate == null && FromCust == null
                //&& toCust == null && FromSalesMan != null && toSalesMan != null)
                //                {
                //                    result = (from i in db.CustPaperReceipts
                //                              where i.isDeleted == false
                //                              && i.salesManId >= FromSalesMan && i.salesManId <= toSalesMan
                //                              select new CustPaperReceiptViewModel
                //                              {
                //                                  indate = (DateTime)i.indate,
                //                                  customerId = (int)i.customerId,
                //                                  salesManId = (int)i.salesManId,
                //                                  paid = (decimal)i.paid,
                //                                  custName = i.Customer.ARName,
                //                                  salesManName = i.Employee1.ARName,
                //                                  FromDate = vFromDate,
                //                                  toDate = DateTime.Now
                //                              }).ToList();
                //                    return result;
                //                }
                //                else if (FromDate != null && toDate != null && FromCust == null
                //                && toCust == null && FromSalesMan != null && toSalesMan != null)
                //                {
                //                    result = (from i in db.CustPaperReceipts
                //                              where i.isDeleted == false
                //                              && i.indate >= FromDate && i.indate <= toDate
                //                              && i.salesManId >= FromSalesMan && i.salesManId <= toSalesMan
                //                              select new CustPaperReceiptViewModel
                //                              {
                //                                  indate = (DateTime)i.indate,
                //                                  customerId = (int)i.customerId,
                //                                  salesManId = (int)i.salesManId,
                //                                  paid = (decimal)i.paid,
                //                                  custName = i.Customer.ARName,
                //                                  salesManName = i.Employee1.ARName,
                //                                  FromDate = (DateTime)FromDate,
                //                                  toDate = (DateTime)toDate
                //                              }).ToList();
                //                    return result;
                //                }
                //                else if (FromDate == null && toDate == null && FromCust != null
                //&& toCust != null && FromSalesMan != null && toSalesMan != null)
                //                {
                //                    result = (from i in db.CustPaperReceipts
                //                              where i.isDeleted == false
                //                              && i.customerId >= FromCust && i.customerId <= toCust
                //                              && i.salesManId >= FromSalesMan && i.salesManId <= toSalesMan
                //                              select new CustPaperReceiptViewModel
                //                              {
                //                                  indate = (DateTime)i.indate,
                //                                  customerId = (int)i.customerId,
                //                                  salesManId = (int)i.salesManId,
                //                                  paid = (decimal)i.paid,
                //                                  custName = i.Customer.ARName,
                //                                  salesManName = i.Employee1.ARName,
                //                                  FromDate = vFromDate,
                //                                  toDate = DateTime.Now
                //                              }).ToList();
                //                    return result;
                //                }
            }
            return result;
        }


        public IEnumerable<ContPaperReceiptViewModel> Read(DateTime? FromDate, DateTime? toDate
            //, int? FromSalesMan, int? toSalesMan,int? FromCust, int? toCust
            )
        {
            return GetAll(FromDate, toDate
                //, FromSalesMan, toSalesMan, FromCust, toCust
                );
        }
        public void Dispose()
        {
            db.Dispose();
        }
    }
}