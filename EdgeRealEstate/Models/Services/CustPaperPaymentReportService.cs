using EdgeRealEstate.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EdgeRealEstate.Models.Services
{
    public class CustPaperPaymentReportService
    {
        private static bool UpdateDatabase = true;
        private ApplicationDbContext db;

        public CustPaperPaymentReportService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public IList<CustPaperPaymentViewModel> GetAll(
            //int? FromCust, int? toCust, int? FromSalesMan, int? toSalesMan,
            DateTime? FromDate, DateTime? toDate
           )
        {
            DateTime vFromDate = new DateTime(DateTime.Now.Year, 1, 1);
            var result = HttpContext.Current.Session["CustomerMove"] as IList<CustPaperPaymentViewModel>;
            if (result == null || UpdateDatabase)
            {
                if (FromDate == null && toDate == null 
                    //&& FromCust == null && toCust == null && FromSalesMan == null && toSalesMan == null
                    )
                {
                    result = (from i in db.CustPaperPayments
                              where i.isDeleted == false
                              select new CustPaperPaymentViewModel
                              {
                                  billId=(int)i.billId,
                                  paidMethod = i.paidMethod,
                                  indate = (DateTime)i.indate,
                                  customerId = (int)i.customerId,
                                  //salesManId = (int)i.salesManId,
                                  paid = (decimal)i.paid,
                                  custName = i.Customer.ARName,
                                  salesManName = i.Employee1.ARName,
                                  FromDate = vFromDate,
                                  toDate = DateTime.Now
                              }).ToList();
                    return result;
                }
                else if (FromDate != null && toDate != null 
                    //&& FromCust != null && toCust != null && FromSalesMan != null && toSalesMan != null
                    )
                {
                    result = (from i in db.CustPaperPayments
                              where i.isDeleted == false
                              //&& i.customerId >= FromCust && i.customerId <= toCust
                              && i.indate >= FromDate && i.indate <= toDate
                              //&& i.salesManId >= FromSalesMan && i.salesManId <= toSalesMan
                              select new CustPaperPaymentViewModel
                              {
                                   billId=(int)i.billId,
                                   paidMethod = i.paidMethod,
                                  indate = (DateTime)i.indate,
                                  customerId = (int)i.customerId,
                                  //salesManId = (int)i.salesManId,
                                  paid = (decimal)i.paid,
                                  custName = i.Customer.ARName,
                                  salesManName = i.Employee1.ARName,
                                  FromDate = (DateTime)FromDate,
                                  toDate = (DateTime)toDate
                              }).ToList();
                    return result;
                }
//                else if (FromDate != null && toDate != null && FromCust == null
//              && toCust == null && FromSalesMan == null && toSalesMan == null)
//                {
//                    result = (from i in db.CustPaperPayments
//                              where i.isDeleted == false
//                              && i.indate >= FromDate && i.indate <= toDate
//                              select new CustPaperPaymentViewModel
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
//                    result = (from i in db.CustPaperPayments
//                              where i.isDeleted == false
//                              && i.customerId >= FromCust && i.customerId <= toCust
//                              && i.indate >= FromDate && i.indate <= toDate
//                              select new CustPaperPaymentViewModel
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
//                    result = (from i in db.CustPaperPayments
//                              where i.isDeleted == false
//                              && i.customerId >= FromCust && i.customerId <= toCust
//                              select new CustPaperPaymentViewModel
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
//                    result = (from i in db.CustPaperPayments
//                              where i.isDeleted == false
//                              && i.salesManId >= FromSalesMan && i.salesManId <= toSalesMan
//                              select new CustPaperPaymentViewModel
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
//                    result = (from i in db.CustPaperPayments
//                              where i.isDeleted == false
//                              && i.indate >= FromDate && i.indate <= toDate
//                              && i.salesManId >= FromSalesMan && i.salesManId <= toSalesMan
//                              select new CustPaperPaymentViewModel
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
//                    result = (from i in db.CustPaperPayments
//                              where i.isDeleted == false
//                              && i.customerId >= FromCust && i.customerId <= toCust
//                              && i.salesManId >= FromSalesMan && i.salesManId <= toSalesMan
//                              select new CustPaperPaymentViewModel
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


        public IEnumerable<CustPaperPaymentViewModel> Read( DateTime? FromDate, DateTime? toDate
          //, int? FromCust, int? toCust
          //   , int? FromSalesMan, int? toSalesMan
            )
        {
            return GetAll( FromDate, toDate
                //, FromCust, toCust, FromSalesMan, toSalesMan
                );
        }
        public void Dispose()
        {
            db.Dispose();
        }
    }
}