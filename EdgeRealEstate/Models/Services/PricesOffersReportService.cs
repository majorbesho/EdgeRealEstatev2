using EdgeRealEstate.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EdgeRealEstate.Models.Services
{
    public class PricesOffersReportService
    {
        private static bool UpdateDatabase = true;
        private ApplicationDbContext db;

        public PricesOffersReportService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public IList<ItemMoveViewModel> GetAll(int? FromCust, int? toCust
            , DateTime? FromDate, DateTime? toDate, int? FromSalesMan, int? toSalesMan
            , int? FrombillId, int? tobillId)
        {
            var result = HttpContext.Current.Session["CustomerMove"] as IList<ItemMoveViewModel>;
            if (result == null || UpdateDatabase)
            {
                if (FromDate == null && toDate == null && FromCust == null && toCust == null
                    && FromSalesMan == null && toSalesMan == null && FrombillId == null
                    && tobillId == null)
                {
                    result = (from i in db.PricesOffersDetails
                              join m in db.PricesOffersMaster
                              on i.billId equals m.id
                              where m.isDeleted == false
                              group i by i.billId into items
                              select new ItemMoveViewModel
                              {
                                  custId = items.FirstOrDefault().PricesOffersMaster.customersId,
                                  CustARName = items.FirstOrDefault().PricesOffersMaster.Customer.ARName,
                                  salesManId = items.FirstOrDefault().PricesOffersMaster.salesManId,
                                  salesManARName = items.FirstOrDefault().PricesOffersMaster.Employee1.ARName,
                                  addDate = items.FirstOrDefault().PricesOffersMaster.addDate,
                                  billId = items.Key,
                                  rowTotal = items.Sum(x => x.Qu * x.price),
                                  //tax = items.Sum(x => x.tax),
                                  //disNo = items.Sum(x => x.disNo)
                                  //+ items.FirstOrDefault().BillPurchasesMaster.disMoney,
                                  //net = items.FirstOrDefault().BillPurchasesMaster.net
                              }).ToList();
                    return result;
                }
                else if (FromDate != null && toDate != null && FromCust == null && toCust == null
                   && FromSalesMan == null && toSalesMan == null && FrombillId == null
                   && tobillId == null)
                {
                    result = (from i in db.PricesOffersDetails
                              join m in db.PricesOffersMaster
                              on i.billId equals m.id
                              where m.isDeleted == false
                              && m.addDate >= FromDate && m.addDate <= toDate
                              group i by i.billId into items
                              select new ItemMoveViewModel
                              {
                                  custId = items.FirstOrDefault().PricesOffersMaster.customersId,
                                  CustARName = items.FirstOrDefault().PricesOffersMaster.Customer.ARName,
                                  salesManId = items.FirstOrDefault().PricesOffersMaster.salesManId,
                                  salesManARName = items.FirstOrDefault().PricesOffersMaster.Employee1.ARName,
                                  addDate = items.FirstOrDefault().PricesOffersMaster.addDate,
                                  billId = items.Key,
                                  rowTotal = items.Sum(x => x.Qu * x.price),
                                  //tax = items.Sum(x => x.tax),
                                  //disNo = items.Sum(x => x.disNo)
                                  //+ items.FirstOrDefault().BillPurchasesMaster.disMoney,
                                  //net = items.FirstOrDefault().BillPurchasesMaster.net
                              }).ToList();
                    return result;
                }
                else if (FromDate == null && toDate == null && FromCust != null && toCust != null
                   && FromSalesMan == null && toSalesMan == null && FrombillId == null
                   && tobillId == null)
                {
                    result = (from i in db.PricesOffersDetails
                              join m in db.PricesOffersMaster
                              on i.billId equals m.id
                              where m.isDeleted == false
                              && m.customersId >= FromCust && m.customersId <= toCust
                              group i by i.billId into items
                              select new ItemMoveViewModel
                              {
                                  custId = items.FirstOrDefault().PricesOffersMaster.customersId,
                                  CustARName = items.FirstOrDefault().PricesOffersMaster.Customer.ARName,
                                  salesManId = items.FirstOrDefault().PricesOffersMaster.salesManId,
                                  salesManARName = items.FirstOrDefault().PricesOffersMaster.Employee1.ARName,
                                  addDate = items.FirstOrDefault().PricesOffersMaster.addDate,
                                  billId = items.Key,
                                  rowTotal = items.Sum(x => x.Qu * x.price),
                                  //tax = items.Sum(x => x.tax),
                                  //disNo = items.Sum(x => x.disNo)
                                  //+ items.FirstOrDefault().BillPurchasesMaster.disMoney,
                                  //net = items.FirstOrDefault().BillPurchasesMaster.net
                              }).ToList();
                    return result;
                }
                else if (FromDate == null && toDate == null && FromCust == null && toCust == null
                 && FromSalesMan != null && toSalesMan != null && FrombillId == null
                 && tobillId == null)
                {
                    result = (from i in db.PricesOffersDetails
                              join m in db.PricesOffersMaster
                              on i.billId equals m.id
                              where m.isDeleted == false
                              && m.salesManId >= FromSalesMan && m.salesManId <= toSalesMan
                              group i by i.billId into items
                              select new ItemMoveViewModel
                              {
                                  custId = items.FirstOrDefault().PricesOffersMaster.customersId,
                                  CustARName = items.FirstOrDefault().PricesOffersMaster.Customer.ARName,
                                  salesManId = items.FirstOrDefault().PricesOffersMaster.salesManId,
                                  salesManARName = items.FirstOrDefault().PricesOffersMaster.Employee1.ARName,
                                  addDate = items.FirstOrDefault().PricesOffersMaster.addDate,
                                  billId = items.Key,
                                  rowTotal = items.Sum(x => x.Qu * x.price),
                                  //tax = items.Sum(x => x.tax),
                                  //disNo = items.Sum(x => x.disNo)
                                  //+ items.FirstOrDefault().BillPurchasesMaster.disMoney,
                                  //net = items.FirstOrDefault().BillPurchasesMaster.net
                              }).ToList();
                    return result;
                }
                else if (FromDate == null && toDate == null && FromCust == null && toCust == null
                && FromSalesMan == null && toSalesMan == null && FrombillId != null
                && tobillId != null)
                {
                    result = (from i in db.PricesOffersDetails
                              join m in db.PricesOffersMaster
                              on i.billId equals m.id
                              where m.isDeleted == false
                              && m.id >= FrombillId && m.id <= tobillId
                              group i by i.billId into items
                              select new ItemMoveViewModel
                              {
                                  custId = items.FirstOrDefault().PricesOffersMaster.customersId,
                                  CustARName = items.FirstOrDefault().PricesOffersMaster.Customer.ARName,
                                  salesManId = items.FirstOrDefault().PricesOffersMaster.salesManId,
                                  salesManARName = items.FirstOrDefault().PricesOffersMaster.Employee1.ARName,
                                  addDate = items.FirstOrDefault().PricesOffersMaster.addDate,
                                  billId = items.Key,
                                  rowTotal = items.Sum(x => x.Qu * x.price),
                                  //tax = items.Sum(x => x.tax),
                                  //disNo = items.Sum(x => x.disNo)
                                  //+ items.FirstOrDefault().BillPurchasesMaster.disMoney,
                                  //net = items.FirstOrDefault().BillPurchasesMaster.net
                              }).ToList();
                    return result;
                }
                //////////
                else if (FromDate != null && toDate != null && FromCust != null && toCust != null
                    && FromSalesMan == null && toSalesMan == null && FrombillId == null
                    && tobillId == null)
                {
                    result = (from i in db.PricesOffersDetails
                              join m in db.PricesOffersMaster
                              on i.billId equals m.id
                              where m.isDeleted == false
                              && m.addDate >= FromDate && m.addDate <= toDate
                              && m.customersId >= FromCust && m.customersId <= toCust
                              group i by i.billId into items
                              select new ItemMoveViewModel
                              {
                                  custId = items.FirstOrDefault().PricesOffersMaster.customersId,
                                  CustARName = items.FirstOrDefault().PricesOffersMaster.Customer.ARName,
                                  salesManId = items.FirstOrDefault().PricesOffersMaster.salesManId,
                                  salesManARName = items.FirstOrDefault().PricesOffersMaster.Employee1.ARName,
                                  addDate = items.FirstOrDefault().PricesOffersMaster.addDate,
                                  billId = items.Key,
                                  rowTotal = items.Sum(x => x.Qu * x.price),
                                  //tax = items.Sum(x => x.tax),
                                  //disNo = items.Sum(x => x.disNo)
                                  //+ items.FirstOrDefault().BillPurchasesMaster.disMoney,
                                  //net = items.FirstOrDefault().BillPurchasesMaster.net
                              }).ToList();
                    return result;
                }
                else if (FromDate != null && toDate != null && FromCust != null && toCust != null
                    && FromSalesMan != null && toSalesMan != null && FrombillId == null
                    && tobillId == null)
                {
                    result = (from i in db.PricesOffersDetails
                              join m in db.PricesOffersMaster
                              on i.billId equals m.id
                              where m.isDeleted == false
                              && m.addDate >= FromDate && m.addDate <= toDate
                              && m.customersId >= FromCust && m.customersId <= toCust
                              && m.salesManId >= FromSalesMan && m.salesManId <= toSalesMan
                              group i by i.billId into items
                              select new ItemMoveViewModel
                              {
                                  custId = items.FirstOrDefault().PricesOffersMaster.customersId,
                                  CustARName = items.FirstOrDefault().PricesOffersMaster.Customer.ARName,
                                  salesManId = items.FirstOrDefault().PricesOffersMaster.salesManId,
                                  salesManARName = items.FirstOrDefault().PricesOffersMaster.Employee1.ARName,
                                  addDate = items.FirstOrDefault().PricesOffersMaster.addDate,
                                  billId = items.Key,
                                  rowTotal = items.Sum(x => x.Qu * x.price),
                                  //tax = items.Sum(x => x.tax),
                                  //disNo = items.Sum(x => x.disNo)
                                  //+ items.FirstOrDefault().BillPurchasesMaster.disMoney,
                                  //net = items.FirstOrDefault().BillPurchasesMaster.net
                              }).ToList();
                    return result;
                }
                else if (FromDate != null && toDate != null && FromCust == null && toCust == null
                    && FromSalesMan != null && toSalesMan != null && FrombillId == null
                    && tobillId == null)
                {
                    result = (from i in db.PricesOffersDetails
                              join m in db.PricesOffersMaster
                              on i.billId equals m.id
                              where m.isDeleted == false
                              && m.addDate >= FromDate && m.addDate <= toDate
                              && m.salesManId >= FromSalesMan && m.salesManId <= toSalesMan
                              group i by i.billId into items
                              select new ItemMoveViewModel
                              {
                                  custId = items.FirstOrDefault().PricesOffersMaster.customersId,
                                  CustARName = items.FirstOrDefault().PricesOffersMaster.Customer.ARName,
                                  salesManId = items.FirstOrDefault().PricesOffersMaster.salesManId,
                                  salesManARName = items.FirstOrDefault().PricesOffersMaster.Employee1.ARName,
                                  addDate = items.FirstOrDefault().PricesOffersMaster.addDate,
                                  billId = items.Key,
                                  rowTotal = items.Sum(x => x.Qu * x.price),
                                  //tax = items.Sum(x => x.tax),
                                  //disNo = items.Sum(x => x.disNo)
                                  //+ items.FirstOrDefault().BillPurchasesMaster.disMoney,
                                  //net = items.FirstOrDefault().BillPurchasesMaster.net
                              }).ToList();
                    return result;
                }
                else if (FromDate != null && toDate != null && FromCust == null && toCust == null
                   && FromSalesMan == null && toSalesMan == null && FrombillId != null
                   && tobillId != null)
                {
                    result = (from i in db.PricesOffersDetails
                              join m in db.PricesOffersMaster
                              on i.billId equals m.id
                              where m.isDeleted == false
                              && m.addDate >= FromDate && m.addDate <= toDate
                              && m.id >= FrombillId && m.id <= tobillId
                              group i by i.billId into items
                              select new ItemMoveViewModel
                              {
                                  custId = items.FirstOrDefault().PricesOffersMaster.customersId,
                                  CustARName = items.FirstOrDefault().PricesOffersMaster.Customer.ARName,
                                  salesManId = items.FirstOrDefault().PricesOffersMaster.salesManId,
                                  salesManARName = items.FirstOrDefault().PricesOffersMaster.Employee1.ARName,
                                  addDate = items.FirstOrDefault().PricesOffersMaster.addDate,
                                  billId = items.Key,
                                  rowTotal = items.Sum(x => x.Qu * x.price),
                                  //tax = items.Sum(x => x.tax),
                                  //disNo = items.Sum(x => x.disNo)
                                  //+ items.FirstOrDefault().BillPurchasesMaster.disMoney,
                                  //net = items.FirstOrDefault().BillPurchasesMaster.net
                              }).ToList();
                    return result;
                }
                else if (FromDate != null && toDate != null && FromCust != null && toCust != null
                    && FromSalesMan == null && toSalesMan == null && FrombillId != null
                    && tobillId != null)
                {
                    result = (from i in db.PricesOffersDetails
                              join m in db.PricesOffersMaster
                              on i.billId equals m.id
                              where m.isDeleted == false
                              && m.addDate >= FromDate && m.addDate <= toDate
                              && m.customersId >= FromCust && m.customersId <= toCust
                              && m.id >= FrombillId && m.id <= tobillId
                              group i by i.billId into items
                              select new ItemMoveViewModel
                              {
                                  custId = items.FirstOrDefault().PricesOffersMaster.customersId,
                                  CustARName = items.FirstOrDefault().PricesOffersMaster.Customer.ARName,
                                  salesManId = items.FirstOrDefault().PricesOffersMaster.salesManId,
                                  salesManARName = items.FirstOrDefault().PricesOffersMaster.Employee1.ARName,
                                  addDate = items.FirstOrDefault().PricesOffersMaster.addDate,
                                  billId = items.Key,
                                  rowTotal = items.Sum(x => x.Qu * x.price),
                                  //tax = items.Sum(x => x.tax),
                                  //disNo = items.Sum(x => x.disNo)
                                  //+ items.FirstOrDefault().BillPurchasesMaster.disMoney,
                                  //net = items.FirstOrDefault().BillPurchasesMaster.net
                              }).ToList();
                    return result;
                }
                else if (FromDate != null && toDate != null && FromCust == null && toCust == null
                   && FromSalesMan != null && toSalesMan != null && FrombillId != null
                   && tobillId != null)
                {
                    result = (from i in db.PricesOffersDetails
                              join m in db.PricesOffersMaster
                              on i.billId equals m.id
                              where m.isDeleted == false
                              && m.addDate >= FromDate && m.addDate <= toDate
                              && m.salesManId >= FromSalesMan && m.salesManId <= toSalesMan
                              && m.id >= FrombillId && m.id <= tobillId
                              group i by i.billId into items
                              select new ItemMoveViewModel
                              {
                                  custId = items.FirstOrDefault().PricesOffersMaster.customersId,
                                  CustARName = items.FirstOrDefault().PricesOffersMaster.Customer.ARName,
                                  salesManId = items.FirstOrDefault().PricesOffersMaster.salesManId,
                                  salesManARName = items.FirstOrDefault().PricesOffersMaster.Employee1.ARName,
                                  addDate = items.FirstOrDefault().PricesOffersMaster.addDate,
                                  billId = items.Key,
                                  rowTotal = items.Sum(x => x.Qu * x.price),
                                  //tax = items.Sum(x => x.tax),
                                  //disNo = items.Sum(x => x.disNo)
                                  //+ items.FirstOrDefault().BillPurchasesMaster.disMoney,
                                  //net = items.FirstOrDefault().BillPurchasesMaster.net
                              }).ToList();
                    return result;
                }
                else if (FromDate == null && toDate == null && FromCust != null && toCust != null
                  && FromSalesMan != null && toSalesMan != null && FrombillId != null
                  && tobillId != null)
                {
                    result = (from i in db.PricesOffersDetails
                              join m in db.PricesOffersMaster
                              on i.billId equals m.id
                              where m.isDeleted == false
                              && m.customersId >= FromCust && m.customersId <= toCust
                              && m.salesManId >= FromSalesMan && m.salesManId <= toSalesMan
                              && m.id >= FrombillId && m.id <= tobillId
                              group i by i.billId into items
                              select new ItemMoveViewModel
                              {
                                  custId = items.FirstOrDefault().PricesOffersMaster.customersId,
                                  CustARName = items.FirstOrDefault().PricesOffersMaster.Customer.ARName,
                                  salesManId = items.FirstOrDefault().PricesOffersMaster.salesManId,
                                  salesManARName = items.FirstOrDefault().PricesOffersMaster.Employee1.ARName,
                                  addDate = items.FirstOrDefault().PricesOffersMaster.addDate,
                                  billId = items.Key,
                                  rowTotal = items.Sum(x => x.Qu * x.price),
                                  //tax = items.Sum(x => x.tax),
                                  //disNo = items.Sum(x => x.disNo)
                                  //+ items.FirstOrDefault().BillPurchasesMaster.disMoney,
                                  //net = items.FirstOrDefault().BillPurchasesMaster.net
                              }).ToList();
                    return result;
                }
                ///////
                else if (FromDate == null && toDate == null && FromCust != null && toCust != null
           && FromSalesMan != null && toSalesMan != null && FrombillId == null
           && tobillId == null)
                {
                    result = (from i in db.PricesOffersDetails
                              join m in db.PricesOffersMaster
                              on i.billId equals m.id
                              where m.isDeleted == false
                              && m.customersId >= FromCust && m.customersId <= toCust
                              && m.salesManId >= FromSalesMan && m.salesManId <= toSalesMan
                              group i by i.billId into items
                              select new ItemMoveViewModel
                              {
                                  custId = items.FirstOrDefault().PricesOffersMaster.customersId,
                                  CustARName = items.FirstOrDefault().PricesOffersMaster.Customer.ARName,
                                  salesManId = items.FirstOrDefault().PricesOffersMaster.salesManId,
                                  salesManARName = items.FirstOrDefault().PricesOffersMaster.Employee1.ARName,
                                  addDate = items.FirstOrDefault().PricesOffersMaster.addDate,
                                  billId = items.Key,
                                  rowTotal = items.Sum(x => x.Qu * x.price),
                                  //tax = items.Sum(x => x.tax),
                                  //disNo = items.Sum(x => x.disNo)
                                  //+ items.FirstOrDefault().BillPurchasesMaster.disMoney,
                                  //net = items.FirstOrDefault().BillPurchasesMaster.net
                              }).ToList();
                    return result;
                }
                else if (FromDate == null && toDate == null && FromCust != null && toCust != null
       && FromSalesMan == null && toSalesMan == null && FrombillId != null
       && tobillId != null)
                {
                    result = (from i in db.PricesOffersDetails
                              join m in db.PricesOffersMaster
                              on i.billId equals m.id
                              where m.isDeleted == false
                              && m.customersId >= FromCust && m.customersId <= toCust
                              && m.id >= FrombillId && m.id <= tobillId
                              group i by i.billId into items
                              select new ItemMoveViewModel
                              {
                                  custId = items.FirstOrDefault().PricesOffersMaster.customersId,
                                  CustARName = items.FirstOrDefault().PricesOffersMaster.Customer.ARName,
                                  salesManId = items.FirstOrDefault().PricesOffersMaster.salesManId,
                                  salesManARName = items.FirstOrDefault().PricesOffersMaster.Employee1.ARName,
                                  addDate = items.FirstOrDefault().PricesOffersMaster.addDate,
                                  billId = items.Key,
                                  rowTotal = items.Sum(x => x.Qu * x.price),
                                  //tax = items.Sum(x => x.tax),
                                  //disNo = items.Sum(x => x.disNo)
                                  //+ items.FirstOrDefault().BillPurchasesMaster.disMoney,
                                  //net = items.FirstOrDefault().BillPurchasesMaster.net
                              }).ToList();
                    return result;
                }
                ///////
                else if (FromDate == null && toDate == null && FromCust == null && toCust == null
         && FromSalesMan != null && toSalesMan != null && FrombillId != null
         && tobillId != null)
                {
                    result = (from i in db.PricesOffersDetails
                              join m in db.PricesOffersMaster
                              on i.billId equals m.id
                              where m.isDeleted == false
                              && m.id >= FrombillId && m.id <= tobillId
                              && m.salesManId >= FromSalesMan && m.salesManId <= toSalesMan
                              group i by i.billId into items
                              select new ItemMoveViewModel
                              {
                                  custId = items.FirstOrDefault().PricesOffersMaster.customersId,
                                  CustARName = items.FirstOrDefault().PricesOffersMaster.Customer.ARName,
                                  salesManId = items.FirstOrDefault().PricesOffersMaster.salesManId,
                                  salesManARName = items.FirstOrDefault().PricesOffersMaster.Employee1.ARName,
                                  addDate = items.FirstOrDefault().PricesOffersMaster.addDate,
                                  billId = items.Key,
                                  rowTotal = items.Sum(x => x.Qu * x.price),
                                  //tax = items.Sum(x => x.tax),
                                  //disNo = items.Sum(x => x.disNo)
                                  //+ items.FirstOrDefault().BillPurchasesMaster.disMoney,
                                  //net = items.FirstOrDefault().BillPurchasesMaster.net
                              }).ToList();
                    return result;
                }
                else if (FromDate != null && toDate != null && FromCust != null && toCust != null
&& FromSalesMan != null && toSalesMan != null && FrombillId != null
&& tobillId != null)
                {
                    result = (from i in db.PricesOffersDetails
                              join m in db.PricesOffersMaster
                              on i.billId equals m.id
                              where m.isDeleted == false
                              && m.customersId >= FromCust && m.customersId <= toCust
                              && m.addDate >= FromDate && m.addDate <= toDate
                              && m.id >= FrombillId && m.id <= tobillId
                              group i by i.billId into items
                              select new ItemMoveViewModel
                              {
                                  custId = items.FirstOrDefault().PricesOffersMaster.customersId,
                                  CustARName = items.FirstOrDefault().PricesOffersMaster.Customer.ARName,
                                  salesManId = items.FirstOrDefault().PricesOffersMaster.salesManId,
                                  salesManARName = items.FirstOrDefault().PricesOffersMaster.Employee1.ARName,
                                  addDate = items.FirstOrDefault().PricesOffersMaster.addDate,
                                  billId = items.Key,
                                  rowTotal = items.Sum(x => x.Qu * x.price),
                                  //tax = items.Sum(x => x.tax),
                                  //disNo = items.Sum(x => x.disNo)
                                  //+ items.FirstOrDefault().BillPurchasesMaster.disMoney,
                                  //net = items.FirstOrDefault().BillPurchasesMaster.net
                              }).ToList();
                    return result;
                }
                else
                {
                    result = (from i in db.PricesOffersDetails
                              join m in db.PricesOffersMaster
                              on i.billId equals m.id
                              where m.isDeleted == false
                              group i by i.billId into items
                              select new ItemMoveViewModel
                              {
                                  custId = items.FirstOrDefault().PricesOffersMaster.customersId,
                                  CustARName = items.FirstOrDefault().PricesOffersMaster.Customer.ARName,
                                  salesManId = items.FirstOrDefault().PricesOffersMaster.salesManId,
                                  salesManARName = items.FirstOrDefault().PricesOffersMaster.Employee1.ARName,
                                  addDate = items.FirstOrDefault().PricesOffersMaster.addDate,
                                  billId = items.Key,
                                  rowTotal = items.Sum(x => x.Qu * x.price),
                                  //tax = items.Sum(x => x.tax),
                                  //disNo = items.Sum(x => x.disNo)
                                  //+ items.FirstOrDefault().BillPurchasesMaster.disMoney,
                                  //net = items.FirstOrDefault().BillPurchasesMaster.net
                              }).ToList();
                    return result;
                }
            }
            return result;
        }

        public IEnumerable<ItemMoveViewModel> Read(int? FromCust, int? toCust
            , DateTime? FromDate, DateTime? toDate, int? FromSalesMan, int? toSalesMan
            , int? FrombillId, int? tobillId)
        {
            return GetAll(FromCust, toCust, FromDate, toDate, FromSalesMan, toSalesMan
            , FrombillId, tobillId);
        }
        public void Dispose()
        {
            db.Dispose();
        }
    }
}