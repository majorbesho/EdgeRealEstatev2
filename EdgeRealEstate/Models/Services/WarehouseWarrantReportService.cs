using EdgeRealEstate.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EdgeRealEstate.Models.Services
{
    public class WarehouseWarrantReportService
    {
        private static bool UpdateDatabase = true;
        private ApplicationDbContext db;

        public WarehouseWarrantReportService(ApplicationDbContext db)
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
                    result = (from i in db.WarehouseWarrantDetails
                              join m in db.WarehouseWarrantMaster
                              on i.billId equals m.id
                              where m.isDeleted == false
                              group i by i.billId into items
                              select new ItemMoveViewModel
                              {
                                  custId = items.FirstOrDefault().WarehouseWarrantMaster.contractorId,
                                  CustARName = items.FirstOrDefault().WarehouseWarrantMaster.contractor.ARName,
                                  salesManId = items.FirstOrDefault().WarehouseWarrantMaster.EngenneringId,
                                  salesManARName = items.FirstOrDefault().WarehouseWarrantMaster.Engennering.Aname,
                                  addDate = items.FirstOrDefault().WarehouseWarrantMaster.addDate,
                                  billId = items.Key,
                                  rowTotal = items.Sum(x => x.Qu * x.price),
                              }).ToList();
                    return result;
                }
                else if (FromDate != null && toDate != null && FromCust == null && toCust == null
                   && FromSalesMan == null && toSalesMan == null && FrombillId == null
                   && tobillId == null)
                {
                    result = (from i in db.WarehouseWarrantDetails
                              join m in db.WarehouseWarrantMaster
                              on i.billId equals m.id
                              where m.isDeleted == false
                              && m.addDate >= FromDate && m.addDate <= toDate
                              group i by i.billId into items
                              select new ItemMoveViewModel
                              {
                                  custId = items.FirstOrDefault().WarehouseWarrantMaster.contractorId,
                                  CustARName = items.FirstOrDefault().WarehouseWarrantMaster.contractor.ARName,
                                  salesManId = items.FirstOrDefault().WarehouseWarrantMaster.EngenneringId,
                                  salesManARName = items.FirstOrDefault().WarehouseWarrantMaster.Engennering.Aname,
                                  addDate = items.FirstOrDefault().WarehouseWarrantMaster.addDate,
                                  billId = items.Key,
                                  rowTotal = items.Sum(x => x.Qu * x.price),
                              }).ToList();
                    return result;
                }
                else if (FromDate == null && toDate == null && FromCust != null && toCust != null
                   && FromSalesMan == null && toSalesMan == null && FrombillId == null
                   && tobillId == null)
                {
                    result = (from i in db.WarehouseWarrantDetails
                              join m in db.WarehouseWarrantMaster
                              on i.billId equals m.id
                              where m.isDeleted == false
                              && m.contractorId >= FromCust && m.contractorId <= toCust
                              group i by i.billId into items
                              select new ItemMoveViewModel
                              {
                                  custId = items.FirstOrDefault().WarehouseWarrantMaster.contractorId,
                                  CustARName = items.FirstOrDefault().WarehouseWarrantMaster.contractor.ARName,
                                  salesManId = items.FirstOrDefault().WarehouseWarrantMaster.EngenneringId,
                                  salesManARName = items.FirstOrDefault().WarehouseWarrantMaster.Engennering.Aname,
                                  addDate = items.FirstOrDefault().WarehouseWarrantMaster.addDate,
                                  billId = items.Key,
                                  rowTotal = items.Sum(x => x.Qu * x.price),
                              }).ToList();
                    return result;
                }
                else if (FromDate == null && toDate == null && FromCust == null && toCust == null
                 && FromSalesMan != null && toSalesMan != null && FrombillId == null
                 && tobillId == null)
                {
                    result = (from i in db.WarehouseWarrantDetails
                              join m in db.WarehouseWarrantMaster
                              on i.billId equals m.id
                              where m.isDeleted == false
                              && m.EngenneringId >= FromSalesMan && m.EngenneringId <= toSalesMan
                              group i by i.billId into items
                              select new ItemMoveViewModel
                              {
                                  custId = items.FirstOrDefault().WarehouseWarrantMaster.contractorId,
                                  CustARName = items.FirstOrDefault().WarehouseWarrantMaster.contractor.ARName,
                                  salesManId = items.FirstOrDefault().WarehouseWarrantMaster.EngenneringId,
                                  salesManARName = items.FirstOrDefault().WarehouseWarrantMaster.Engennering.Aname,
                                  addDate = items.FirstOrDefault().WarehouseWarrantMaster.addDate,
                                  billId = items.Key,
                                  rowTotal = items.Sum(x => x.Qu * x.price),
                              }).ToList();
                    return result;
                }
                else if (FromDate == null && toDate == null && FromCust == null && toCust == null
                && FromSalesMan == null && toSalesMan == null && FrombillId != null
                && tobillId != null)
                {
                    result = (from i in db.WarehouseWarrantDetails
                              join m in db.WarehouseWarrantMaster
                              on i.billId equals m.id
                              where m.isDeleted == false
                              && m.id >= FrombillId && m.id <= tobillId
                              group i by i.billId into items
                              select new ItemMoveViewModel
                              {
                                  custId = items.FirstOrDefault().WarehouseWarrantMaster.contractorId,
                                  CustARName = items.FirstOrDefault().WarehouseWarrantMaster.contractor.ARName,
                                  salesManId = items.FirstOrDefault().WarehouseWarrantMaster.EngenneringId,
                                  salesManARName = items.FirstOrDefault().WarehouseWarrantMaster.Engennering.Aname,
                                  addDate = items.FirstOrDefault().WarehouseWarrantMaster.addDate,
                                  billId = items.Key,
                                  rowTotal = items.Sum(x => x.Qu * x.price),
                              }).ToList();
                    return result;
                }
                //////////
                else if (FromDate != null && toDate != null && FromCust != null && toCust != null
                    && FromSalesMan == null && toSalesMan == null && FrombillId == null
                    && tobillId == null)
                {
                    result = (from i in db.WarehouseWarrantDetails
                              join m in db.WarehouseWarrantMaster
                              on i.billId equals m.id
                              where m.isDeleted == false
                              && m.addDate >= FromDate && m.addDate <= toDate
                              && m.contractorId >= FromCust && m.contractorId <= toCust
                              group i by i.billId into items
                              select new ItemMoveViewModel
                              {
                                  custId = items.FirstOrDefault().WarehouseWarrantMaster.contractorId,
                                  CustARName = items.FirstOrDefault().WarehouseWarrantMaster.contractor.ARName,
                                  salesManId = items.FirstOrDefault().WarehouseWarrantMaster.EngenneringId,
                                  salesManARName = items.FirstOrDefault().WarehouseWarrantMaster.Engennering.Aname,
                                  addDate = items.FirstOrDefault().WarehouseWarrantMaster.addDate,
                                  billId = items.Key,
                                  rowTotal = items.Sum(x => x.Qu * x.price),
                              }).ToList();
                    return result;
                }
                else if (FromDate != null && toDate != null && FromCust != null && toCust != null
                    && FromSalesMan != null && toSalesMan != null && FrombillId == null
                    && tobillId == null)
                {
                    result = (from i in db.WarehouseWarrantDetails
                              join m in db.WarehouseWarrantMaster
                              on i.billId equals m.id
                              where m.isDeleted == false
                              && m.addDate >= FromDate && m.addDate <= toDate
                              && m.contractorId >= FromCust && m.contractorId <= toCust
                              && m.EngenneringId >= FromSalesMan && m.EngenneringId <= toSalesMan
                              group i by i.billId into items
                              select new ItemMoveViewModel
                              {
                                  custId = items.FirstOrDefault().WarehouseWarrantMaster.contractorId,
                                  CustARName = items.FirstOrDefault().WarehouseWarrantMaster.contractor.ARName,
                                  salesManId = items.FirstOrDefault().WarehouseWarrantMaster.EngenneringId,
                                  salesManARName = items.FirstOrDefault().WarehouseWarrantMaster.Engennering.Aname,
                                  addDate = items.FirstOrDefault().WarehouseWarrantMaster.addDate,
                                  billId = items.Key,
                                  rowTotal = items.Sum(x => x.Qu * x.price),
                              }).ToList();
                    return result;
                }
                else if (FromDate != null && toDate != null && FromCust == null && toCust == null
                    && FromSalesMan != null && toSalesMan != null && FrombillId == null
                    && tobillId == null)
                {
                    result = (from i in db.WarehouseWarrantDetails
                              join m in db.WarehouseWarrantMaster
                              on i.billId equals m.id
                              where m.isDeleted == false
                              && m.addDate >= FromDate && m.addDate <= toDate
                              && m.EngenneringId >= FromSalesMan && m.EngenneringId <= toSalesMan
                              group i by i.billId into items
                              select new ItemMoveViewModel
                              {
                                  custId = items.FirstOrDefault().WarehouseWarrantMaster.contractorId,
                                  CustARName = items.FirstOrDefault().WarehouseWarrantMaster.contractor.ARName,
                                  salesManId = items.FirstOrDefault().WarehouseWarrantMaster.EngenneringId,
                                  salesManARName = items.FirstOrDefault().WarehouseWarrantMaster.Engennering.Aname,
                                  addDate = items.FirstOrDefault().WarehouseWarrantMaster.addDate,
                                  billId = items.Key,
                                  rowTotal = items.Sum(x => x.Qu * x.price),
                              }).ToList();
                    return result;
                }
                else if (FromDate != null && toDate != null && FromCust == null && toCust == null
                   && FromSalesMan == null && toSalesMan == null && FrombillId != null
                   && tobillId != null)
                {
                    result = (from i in db.WarehouseWarrantDetails
                              join m in db.WarehouseWarrantMaster
                              on i.billId equals m.id
                              where m.isDeleted == false
                              && m.addDate >= FromDate && m.addDate <= toDate
                              && m.id >= FrombillId && m.id <= tobillId
                              group i by i.billId into items
                              select new ItemMoveViewModel
                              {
                                  custId = items.FirstOrDefault().WarehouseWarrantMaster.contractorId,
                                  CustARName = items.FirstOrDefault().WarehouseWarrantMaster.contractor.ARName,
                                  salesManId = items.FirstOrDefault().WarehouseWarrantMaster.EngenneringId,
                                  salesManARName = items.FirstOrDefault().WarehouseWarrantMaster.Engennering.Aname,
                                  addDate = items.FirstOrDefault().WarehouseWarrantMaster.addDate,
                                  billId = items.Key,
                                  rowTotal = items.Sum(x => x.Qu * x.price),
                              }).ToList();
                    return result;
                }
                else if (FromDate != null && toDate != null && FromCust != null && toCust != null
                    && FromSalesMan == null && toSalesMan == null && FrombillId != null
                    && tobillId != null)
                {
                    result = (from i in db.WarehouseWarrantDetails
                              join m in db.WarehouseWarrantMaster
                              on i.billId equals m.id
                              where m.isDeleted == false
                              && m.addDate >= FromDate && m.addDate <= toDate
                              && m.contractorId >= FromCust && m.contractorId <= toCust
                              && m.id >= FrombillId && m.id <= tobillId
                              group i by i.billId into items
                              select new ItemMoveViewModel
                              {
                                  custId = items.FirstOrDefault().WarehouseWarrantMaster.contractorId,
                                  CustARName = items.FirstOrDefault().WarehouseWarrantMaster.contractor.ARName,
                                  salesManId = items.FirstOrDefault().WarehouseWarrantMaster.EngenneringId,
                                  salesManARName = items.FirstOrDefault().WarehouseWarrantMaster.Engennering.Aname,
                                  addDate = items.FirstOrDefault().WarehouseWarrantMaster.addDate,
                                  billId = items.Key,
                                  rowTotal = items.Sum(x => x.Qu * x.price),
                              }).ToList();
                    return result;
                }
                else if (FromDate != null && toDate != null && FromCust == null && toCust == null
                   && FromSalesMan != null && toSalesMan != null && FrombillId != null
                   && tobillId != null)
                {
                    result = (from i in db.WarehouseWarrantDetails
                              join m in db.WarehouseWarrantMaster
                              on i.billId equals m.id
                              where m.isDeleted == false
                              && m.addDate >= FromDate && m.addDate <= toDate
                              && m.EngenneringId >= FromSalesMan && m.EngenneringId <= toSalesMan
                              && m.id >= FrombillId && m.id <= tobillId
                              group i by i.billId into items
                              select new ItemMoveViewModel
                              {
                                  custId = items.FirstOrDefault().WarehouseWarrantMaster.contractorId,
                                  CustARName = items.FirstOrDefault().WarehouseWarrantMaster.contractor.ARName,
                                  salesManId = items.FirstOrDefault().WarehouseWarrantMaster.EngenneringId,
                                  salesManARName = items.FirstOrDefault().WarehouseWarrantMaster.Engennering.Aname,
                                  addDate = items.FirstOrDefault().WarehouseWarrantMaster.addDate,
                                  billId = items.Key,
                                  rowTotal = items.Sum(x => x.Qu * x.price),
                              }).ToList();
                    return result;
                }
                else if (FromDate == null && toDate == null && FromCust != null && toCust != null
                  && FromSalesMan != null && toSalesMan != null && FrombillId != null
                  && tobillId != null)
                {
                    result = (from i in db.WarehouseWarrantDetails
                              join m in db.WarehouseWarrantMaster
                              on i.billId equals m.id
                              where m.isDeleted == false
                              && m.contractorId >= FromCust && m.contractorId <= toCust
                              && m.EngenneringId >= FromSalesMan && m.EngenneringId <= toSalesMan
                              && m.id >= FrombillId && m.id <= tobillId
                              group i by i.billId into items
                              select new ItemMoveViewModel
                              {
                                  custId = items.FirstOrDefault().WarehouseWarrantMaster.contractorId,
                                  CustARName = items.FirstOrDefault().WarehouseWarrantMaster.contractor.ARName,
                                  salesManId = items.FirstOrDefault().WarehouseWarrantMaster.EngenneringId,
                                  salesManARName = items.FirstOrDefault().WarehouseWarrantMaster.Engennering.Aname,
                                  addDate = items.FirstOrDefault().WarehouseWarrantMaster.addDate,
                                  billId = items.Key,
                                  rowTotal = items.Sum(x => x.Qu * x.price),
                              }).ToList();
                    return result;
                }
                ///////
                else if (FromDate == null && toDate == null && FromCust != null && toCust != null
           && FromSalesMan != null && toSalesMan != null && FrombillId == null
           && tobillId == null)
                {
                    result = (from i in db.WarehouseWarrantDetails
                              join m in db.WarehouseWarrantMaster
                              on i.billId equals m.id
                              where m.isDeleted == false
                              && m.contractorId >= FromCust && m.contractorId <= toCust
                              && m.EngenneringId >= FromSalesMan && m.EngenneringId <= toSalesMan
                              group i by i.billId into items
                              select new ItemMoveViewModel
                              {
                                  custId = items.FirstOrDefault().WarehouseWarrantMaster.contractorId,
                                  CustARName = items.FirstOrDefault().WarehouseWarrantMaster.contractor.ARName,
                                  salesManId = items.FirstOrDefault().WarehouseWarrantMaster.EngenneringId,
                                  salesManARName = items.FirstOrDefault().WarehouseWarrantMaster.Engennering.Aname,
                                  addDate = items.FirstOrDefault().WarehouseWarrantMaster.addDate,
                                  billId = items.Key,
                                  rowTotal = items.Sum(x => x.Qu * x.price),
                              }).ToList();
                    return result;
                }
                else if (FromDate == null && toDate == null && FromCust != null && toCust != null
       && FromSalesMan == null && toSalesMan == null && FrombillId != null
       && tobillId != null)
                {
                    result = (from i in db.WarehouseWarrantDetails
                              join m in db.WarehouseWarrantMaster
                              on i.billId equals m.id
                              where m.isDeleted == false
                              && m.contractorId >= FromCust && m.contractorId <= toCust
                              && m.id >= FrombillId && m.id <= tobillId
                              group i by i.billId into items
                              select new ItemMoveViewModel
                              {
                                  custId = items.FirstOrDefault().WarehouseWarrantMaster.contractorId,
                                  CustARName = items.FirstOrDefault().WarehouseWarrantMaster.contractor.ARName,
                                  salesManId = items.FirstOrDefault().WarehouseWarrantMaster.EngenneringId,
                                  salesManARName = items.FirstOrDefault().WarehouseWarrantMaster.Engennering.Aname,
                                  addDate = items.FirstOrDefault().WarehouseWarrantMaster.addDate,
                                  billId = items.Key,
                                  rowTotal = items.Sum(x => x.Qu * x.price),
                              }).ToList();
                    return result;
                }
                ///////
                else if (FromDate == null && toDate == null && FromCust == null && toCust == null
         && FromSalesMan != null && toSalesMan != null && FrombillId != null
         && tobillId != null)
                {
                    result = (from i in db.WarehouseWarrantDetails
                              join m in db.WarehouseWarrantMaster
                              on i.billId equals m.id
                              where m.isDeleted == false
                              && m.id >= FrombillId && m.id <= tobillId
                              && m.EngenneringId >= FromSalesMan && m.EngenneringId <= toSalesMan
                              group i by i.billId into items
                              select new ItemMoveViewModel
                              {
                                  custId = items.FirstOrDefault().WarehouseWarrantMaster.contractorId,
                                  CustARName = items.FirstOrDefault().WarehouseWarrantMaster.contractor.ARName,
                                  salesManId = items.FirstOrDefault().WarehouseWarrantMaster.EngenneringId,
                                  salesManARName = items.FirstOrDefault().WarehouseWarrantMaster.Engennering.Aname,
                                  addDate = items.FirstOrDefault().WarehouseWarrantMaster.addDate,
                                  billId = items.Key,
                                  rowTotal = items.Sum(x => x.Qu * x.price),
       
                              }).ToList();
                    return result;
                }
                else if (FromDate != null && toDate != null && FromCust != null && toCust != null
&& FromSalesMan != null && toSalesMan != null && FrombillId != null
&& tobillId != null)
                {
                    result = (from i in db.WarehouseWarrantDetails
                              join m in db.WarehouseWarrantMaster
                              on i.billId equals m.id
                              where m.isDeleted == false
                              && m.contractorId >= FromCust && m.contractorId <= toCust
                              && m.addDate >= FromDate && m.addDate <= toDate
                              && m.id >= FrombillId && m.id <= tobillId
                              group i by i.billId into items
                              select new ItemMoveViewModel
                              {
                                  custId = items.FirstOrDefault().WarehouseWarrantMaster.contractorId,
                                  CustARName = items.FirstOrDefault().WarehouseWarrantMaster.contractor.ARName,
                                  salesManId = items.FirstOrDefault().WarehouseWarrantMaster.EngenneringId,
                                  salesManARName = items.FirstOrDefault().WarehouseWarrantMaster.Engennering.Aname,
                                  addDate = items.FirstOrDefault().WarehouseWarrantMaster.addDate,
                                  billId = items.Key,
                                  rowTotal = items.Sum(x => x.Qu * x.price),
                              }).ToList();
                    return result;
                }
                else
                {
                    result = (from i in db.WarehouseWarrantDetails
                              join m in db.WarehouseWarrantMaster
                              on i.billId equals m.id
                              where m.isDeleted == false
                              group i by i.billId into items
                              select new ItemMoveViewModel
                              {
                                  custId = items.FirstOrDefault().WarehouseWarrantMaster.contractorId,
                                  CustARName = items.FirstOrDefault().WarehouseWarrantMaster.contractor.ARName,
                                  salesManId = items.FirstOrDefault().WarehouseWarrantMaster.EngenneringId,
                                  salesManARName = items.FirstOrDefault().WarehouseWarrantMaster.Engennering.Aname,
                                  addDate = items.FirstOrDefault().WarehouseWarrantMaster.addDate,
                                  billId = items.Key,
                                  rowTotal = items.Sum(x => x.Qu * x.price),
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