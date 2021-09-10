using EdgeRealEstate.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EdgeRealEstate.Models.Services
{
    public class OrderJobFollowUpReportService
    {
        private static bool UpdateDatabase = true;
        private ApplicationDbContext db;

        public OrderJobFollowUpReportService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public IList<OrderJobDetailsViewModel> GetAll(int? FromCust, int? toCust
            //, DateTime? FromDate, DateTime? toDate
            , int? FromProject, int? toProject
            , int? FromSalesMan, int? toSalesMan
            , int? FrombillId, int? tobillId)
        {
            var result = HttpContext.Current.Session["CustomerMove"] as IList<OrderJobDetailsViewModel>;
            if (result == null || UpdateDatabase)
            {
                if (/*FromDate == null && toDate == null &&*/
                    FromProject == null && toProject == null &&
                    FromCust == null && toCust == null
                    && FromSalesMan == null && toSalesMan == null && FrombillId == null
                    && tobillId == null)
                {
                    result = (from i in db.OrderJobDetails
                              join m in db.OrderJob
                              on i.OrderJobId equals m.Id
                              where m.IsDeleted == false
                              //group i by i.OrderJobId into items
                              select new OrderJobDetailsViewModel
                              {
                                  contractorId = m.contractorId,
                                  contractorName = m.contractor.ARName,
                                  SupervisorEngenneringId = m.SupervisorEngenneringId,
                                  SupervisorEngenneringName = m.Engennering.Aname,
                                  OrderJobId = i.OrderJobId,
                                  MainProjectId = m.MainProjectId,
                                  MainProjectName = m.MainProject.Aname,
                                  //Cost = i.Cost,
                                  //ExecutionTime = i.ExecutionTime,
                                  //BeginDateAcutely = i.BeginDateAcutely,
                                  //BeginDateExpected = i.BeginDateExpected,
                                  //EndDateAcutely = i.EndDateAcutely,
                                  //EndDateExpected = i.EndDateExpected,
                                  //StageId = i.StageId,
                                  //StageName = i.Stage.Aname,
                                  //MianItemsName = i.MianItems.Aname,
                                  //MianItemsId = i.MianItemsId,
                                  //Note = i.Note
                              }).ToList();
                    return result;
                }
                else if (FromProject != null && toProject != null &&
                    FromCust == null && toCust == null
                   && FromSalesMan == null && toSalesMan == null && FrombillId == null
                   && tobillId == null)
                {
                    result = (from i in db.OrderJobDetails
                              join m in db.OrderJob
                              on i.OrderJobId equals m.Id
                              where m.IsDeleted == false
                              && m.MainProjectId >= FromProject && m.MainProjectId <= toProject
                              //group i by i.OrderJobId into items
                              select new OrderJobDetailsViewModel
                              {
                                  contractorId = m.contractorId,
                                  contractorName = m.contractor.ARName,
                                  SupervisorEngenneringId = m.SupervisorEngenneringId,
                                  SupervisorEngenneringName = m.Engennering.Aname,
                                  OrderJobId = i.OrderJobId,
                                  MainProjectId = m.MainProjectId,
                                  MainProjectName = m.MainProject.Aname,
                                  //Cost = i.Cost,
                                  //ExecutionTime = i.ExecutionTime,
                                  //BeginDateAcutely = i.BeginDateAcutely,
                                  //BeginDateExpected = i.BeginDateExpected,
                                  //EndDateAcutely = i.EndDateAcutely,
                                  //EndDateExpected = i.EndDateExpected,
                                  //StageId = i.StageId,
                                  //StageName = i.Stage.Aname,
                                  //MianItemsName = i.MianItems.Aname,
                                  //MianItemsId = i.MianItemsId,
                                  //Note = i.Note
                              }).ToList();
                    return result;
                }
                else if (FromProject == null && toProject == null &&
                    FromCust != null && toCust != null
                   && FromSalesMan == null && toSalesMan == null && FrombillId == null
                   && tobillId == null)
                {
                    result = (from i in db.OrderJobDetails
                              join m in db.OrderJob
                              on i.OrderJobId equals m.Id
                              where m.IsDeleted == false
                              && m.contractorId >= FromCust && m.contractorId <= toCust
                              //group i by i.billId into items
                              select new OrderJobDetailsViewModel
                              {
                                  contractorId = m.contractorId,
                                  contractorName = m.contractor.ARName,
                                  SupervisorEngenneringId = m.SupervisorEngenneringId,
                                  SupervisorEngenneringName = m.Engennering.Aname,
                                  OrderJobId = i.OrderJobId,
                                  MainProjectId = m.MainProjectId,
                                  MainProjectName = m.MainProject.Aname,
                                  //Cost = i.Cost,
                                  //ExecutionTime = i.ExecutionTime,
                                  //BeginDateAcutely = i.BeginDateAcutely,
                                  //BeginDateExpected = i.BeginDateExpected,
                                  //EndDateAcutely = i.EndDateAcutely,
                                  //EndDateExpected = i.EndDateExpected,
                                  //StageId = i.StageId,
                                  //StageName = i.Stage.Aname,
                                  //MianItemsName = i.MianItems.Aname,
                                  //MianItemsId = i.MianItemsId,
                                  //Note = i.Note
                              }).ToList();
                    return result;
                }
                else if (FromProject == null && toProject == null &&
                    FromCust == null && toCust == null
                 && FromSalesMan != null && toSalesMan != null && FrombillId == null
                 && tobillId == null)
                {
                    result = (from i in db.OrderJobDetails
                              join m in db.OrderJob
                              on i.OrderJobId equals m.Id
                              where m.IsDeleted == false
                              && m.SupervisorEngenneringId >= FromSalesMan && m.SupervisorEngenneringId <= toSalesMan
                              //group i by i.billId into items
                              select new OrderJobDetailsViewModel
                              {
                                  contractorId = m.contractorId,
                                  contractorName = m.contractor.ARName,
                                  SupervisorEngenneringId = m.SupervisorEngenneringId,
                                  SupervisorEngenneringName = m.Engennering.Aname,
                                  OrderJobId = i.OrderJobId,
                                  MainProjectId = m.MainProjectId,
                                  MainProjectName = m.MainProject.Aname,
                                  //Cost = i.Cost,
                                  //ExecutionTime = i.ExecutionTime,
                                  //BeginDateAcutely = i.BeginDateAcutely,
                                  //BeginDateExpected = i.BeginDateExpected,
                                  //EndDateAcutely = i.EndDateAcutely,
                                  //EndDateExpected = i.EndDateExpected,
                                  //StageId = i.StageId,
                                  //StageName = i.Stage.Aname,
                                  //MianItemsName = i.MianItems.Aname,
                                  //MianItemsId = i.MianItemsId,
                                  //Note = i.Note
                              }).ToList();
                    return result;
                }
                else if (FromProject == null && toProject == null &&
                    FromCust == null && toCust == null
                && FromSalesMan == null && toSalesMan == null && FrombillId != null
                && tobillId != null)
                {
                    result = (from i in db.OrderJobDetails
                              join m in db.OrderJob
                              on i.OrderJobId equals m.Id
                              where m.IsDeleted == false
                              && m.Id >= FrombillId && m.Id <= tobillId
                              //group i by i.billId into items
                              select new OrderJobDetailsViewModel
                              {
                                  contractorId = m.contractorId,
                                  contractorName = m.contractor.ARName,
                                  SupervisorEngenneringId = m.SupervisorEngenneringId,
                                  SupervisorEngenneringName = m.Engennering.Aname,
                                  OrderJobId = i.OrderJobId,
                                  MainProjectId = m.MainProjectId,
                                  MainProjectName = m.MainProject.Aname,
                                  //Cost = i.Cost,
                                  //ExecutionTime = i.ExecutionTime,
                                  //BeginDateAcutely = i.BeginDateAcutely,
                                  //BeginDateExpected = i.BeginDateExpected,
                                  //EndDateAcutely = i.EndDateAcutely,
                                  //EndDateExpected = i.EndDateExpected,
                                  //StageId = i.StageId,
                                  //StageName = i.Stage.Aname,
                                  //MianItemsName = i.MianItems.Aname,
                                  //MianItemsId = i.MianItemsId,
                                  //Note = i.Note
                              }).ToList();
                    return result;
                }
                //////////
                else if (FromProject != null && toProject != null &&
                    FromCust != null && toCust != null
                    && FromSalesMan == null && toSalesMan == null && FrombillId == null
                    && tobillId == null)
                {
                    result = (from i in db.OrderJobDetails
                              join m in db.OrderJob
                              on i.OrderJobId equals m.Id
                              where m.IsDeleted == false
                                && m.MainProjectId >= FromProject && m.MainProjectId <= toProject
                                && m.contractorId >= FromCust && m.contractorId <= toCust
                              //group i by i.billId into items
                              select new OrderJobDetailsViewModel
                              {
                                  contractorId = m.contractorId,
                                  contractorName = m.contractor.ARName,
                                  SupervisorEngenneringId = m.SupervisorEngenneringId,
                                  SupervisorEngenneringName = m.Engennering.Aname,
                                  OrderJobId = i.OrderJobId,
                                  MainProjectId = m.MainProjectId,
                                  MainProjectName = m.MainProject.Aname,
                                  //Cost = i.Cost,
                                  //ExecutionTime = i.ExecutionTime,
                                  //BeginDateAcutely = i.BeginDateAcutely,
                                  //BeginDateExpected = i.BeginDateExpected,
                                  //EndDateAcutely = i.EndDateAcutely,
                                  //EndDateExpected = i.EndDateExpected,
                                  //StageId = i.StageId,
                                  //StageName = i.Stage.Aname,
                                  //MianItemsName = i.MianItems.Aname,
                                  //MianItemsId = i.MianItemsId,
                                  //Note = i.Note
                              }).ToList();
                    return result;
                }
                else if (FromProject != null && toProject != null &&
                    FromCust != null && toCust != null
                    && FromSalesMan != null && toSalesMan != null && FrombillId == null
                    && tobillId == null)
                {
                    result = (from i in db.OrderJobDetails
                              join m in db.OrderJob
                              on i.OrderJobId equals m.Id
                              where m.IsDeleted == false
                              && m.MainProjectId >= FromProject && m.MainProjectId <= toProject
                              && m.contractorId >= FromCust && m.contractorId <= toCust
                              && m.SupervisorEngenneringId >= FromSalesMan && m.SupervisorEngenneringId <= toSalesMan
                              //group i by i.billId into items
                              select new OrderJobDetailsViewModel
                              {
                                  contractorId = m.contractorId,
                                  contractorName = m.contractor.ARName,
                                  SupervisorEngenneringId = m.SupervisorEngenneringId,
                                  SupervisorEngenneringName = m.Engennering.Aname,
                                  OrderJobId = i.OrderJobId,
                                  MainProjectId = m.MainProjectId,
                                  MainProjectName = m.MainProject.Aname,
                                  //Cost = i.Cost,
                                  //ExecutionTime = i.ExecutionTime,
                                  //BeginDateAcutely = i.BeginDateAcutely,
                                  //BeginDateExpected = i.BeginDateExpected,
                                  //EndDateAcutely = i.EndDateAcutely,
                                  //EndDateExpected = i.EndDateExpected,
                                  //StageId = i.StageId,
                                  //StageName = i.Stage.Aname,
                                  //MianItemsName = i.MianItems.Aname,
                                  //MianItemsId = i.MianItemsId,
                                  //Note = i.Note
                              }).ToList();
                    return result;
                }
                else if (FromProject != null && toProject != null &&
                    FromCust == null && toCust == null
                    && FromSalesMan != null && toSalesMan != null && FrombillId == null
                    && tobillId == null)
                {
                    result = (from i in db.OrderJobDetails
                              join m in db.OrderJob
                              on i.OrderJobId equals m.Id
                              where m.IsDeleted == false
                              && m.MainProjectId >= FromProject && m.MainProjectId <= toProject
                              && m.SupervisorEngenneringId >= FromSalesMan && m.SupervisorEngenneringId <= toSalesMan
                              //group i by i.billId into items
                              select new OrderJobDetailsViewModel
                              {
                                  contractorId = m.contractorId,
                                  contractorName = m.contractor.ARName,
                                  SupervisorEngenneringId = m.SupervisorEngenneringId,
                                  SupervisorEngenneringName = m.Engennering.Aname,
                                  OrderJobId = i.OrderJobId,
                                  MainProjectId = m.MainProjectId,
                                  MainProjectName = m.MainProject.Aname,
                                  //Cost = i.Cost,
                                  //ExecutionTime = i.ExecutionTime,
                                  //BeginDateAcutely = i.BeginDateAcutely,
                                  //BeginDateExpected = i.BeginDateExpected,
                                  //EndDateAcutely = i.EndDateAcutely,
                                  //EndDateExpected = i.EndDateExpected,
                                  //StageId = i.StageId,
                                  //StageName = i.Stage.Aname,
                                  //MianItemsName = i.MianItems.Aname,
                                  //MianItemsId = i.MianItemsId,
                                  //Note = i.Note
                              }).ToList();
                    return result;
                }
                else if (FromProject != null && toProject != null &&
                    FromCust == null && toCust == null
                   && FromSalesMan == null && toSalesMan == null && FrombillId != null
                   && tobillId != null)
                {
                    result = (from i in db.OrderJobDetails
                              join m in db.OrderJob
                              on i.OrderJobId equals m.Id
                              where m.IsDeleted == false
                              && m.MainProjectId >= FromProject && m.MainProjectId <= toProject
                              && m.Id >= FrombillId && m.Id <= tobillId
                              //group i by i.billId into items
                              select new OrderJobDetailsViewModel
                              {
                                  contractorId = m.contractorId,
                                  contractorName = m.contractor.ARName,
                                  SupervisorEngenneringId = m.SupervisorEngenneringId,
                                  SupervisorEngenneringName = m.Engennering.Aname,
                                  OrderJobId = i.OrderJobId,
                                  MainProjectId = m.MainProjectId,
                                  MainProjectName = m.MainProject.Aname,
                                  //Cost = i.Cost,
                                  //ExecutionTime = i.ExecutionTime,
                                  //BeginDateAcutely = i.BeginDateAcutely,
                                  //BeginDateExpected = i.BeginDateExpected,
                                  //EndDateAcutely = i.EndDateAcutely,
                                  //EndDateExpected = i.EndDateExpected,
                                  //StageId = i.StageId,
                                  //StageName = i.Stage.Aname,
                                  //MianItemsName = i.MianItems.Aname,
                                  //MianItemsId = i.MianItemsId,
                                  //Note = i.Note
                              }).ToList();
                    return result;
                }
                else if (FromProject != null && toProject != null &&
                    FromCust != null && toCust != null
                    && FromSalesMan == null && toSalesMan == null && FrombillId != null
                    && tobillId != null)
                {
                    result = (from i in db.OrderJobDetails
                              join m in db.OrderJob
                              on i.OrderJobId equals m.Id
                              where m.IsDeleted == false
                              && m.MainProjectId >= FromProject && m.MainProjectId <= toProject
                              && m.contractorId >= FromCust && m.contractorId <= toCust
                              && m.Id >= FrombillId && m.Id <= tobillId
                              //group i by i.billId into items
                              select new OrderJobDetailsViewModel
                              {
                                  contractorId = m.contractorId,
                                  contractorName = m.contractor.ARName,
                                  SupervisorEngenneringId = m.SupervisorEngenneringId,
                                  SupervisorEngenneringName = m.Engennering.Aname,
                                  OrderJobId = i.OrderJobId,
                                  MainProjectId = m.MainProjectId,
                                  MainProjectName = m.MainProject.Aname,
                                  //Cost = i.Cost,
                                  //ExecutionTime = i.ExecutionTime,
                                  //BeginDateAcutely = i.BeginDateAcutely,
                                  //BeginDateExpected = i.BeginDateExpected,
                                  //EndDateAcutely = i.EndDateAcutely,
                                  //EndDateExpected = i.EndDateExpected,
                                  //StageId = i.StageId,
                                  //StageName = i.Stage.Aname,
                                  //MianItemsName = i.MianItems.Aname,
                                  //MianItemsId = i.MianItemsId,
                                  //Note = i.Note
                              }).ToList();
                    return result;
                }
                else if (FromProject != null && toProject != null &&
                    FromCust == null && toCust == null
                   && FromSalesMan != null && toSalesMan != null && FrombillId != null
                   && tobillId != null)
                {
                    result = (from i in db.OrderJobDetails
                              join m in db.OrderJob
                              on i.OrderJobId equals m.Id
                              where m.IsDeleted == false
                              && m.MainProjectId >= FromProject && m.MainProjectId <= toProject
                              && m.SupervisorEngenneringId >= FromSalesMan && m.SupervisorEngenneringId <= toSalesMan
                              && m.Id >= FrombillId && m.Id <= tobillId
                              //group i by i.billId into items
                              select new OrderJobDetailsViewModel
                              {
                                  contractorId = m.contractorId,
                                  contractorName = m.contractor.ARName,
                                  SupervisorEngenneringId = m.SupervisorEngenneringId,
                                  SupervisorEngenneringName = m.Engennering.Aname,
                                  OrderJobId = i.OrderJobId,
                                  MainProjectId = m.MainProjectId,
                                  MainProjectName = m.MainProject.Aname,
                                  //Cost = i.Cost,
                                  //ExecutionTime = i.ExecutionTime,
                                  //BeginDateAcutely = i.BeginDateAcutely,
                                  //BeginDateExpected = i.BeginDateExpected,
                                  //EndDateAcutely = i.EndDateAcutely,
                                  //EndDateExpected = i.EndDateExpected,
                                  //StageId = i.StageId,
                                  //StageName = i.Stage.Aname,
                                  //MianItemsName = i.MianItems.Aname,
                                  //MianItemsId = i.MianItemsId,
                                  //Note = i.Note
                              }).ToList();
                    return result;
                }
                else if (FromProject == null && toProject == null &&
                    FromCust != null && toCust != null
                  && FromSalesMan != null && toSalesMan != null && FrombillId != null
                  && tobillId != null)
                {
                    result = (from i in db.OrderJobDetails
                              join m in db.OrderJob
                              on i.OrderJobId equals m.Id
                              where m.IsDeleted == false
                              && m.contractorId >= FromCust && m.contractorId <= toCust
                              && m.SupervisorEngenneringId >= FromSalesMan && m.SupervisorEngenneringId <= toSalesMan
                              && m.Id >= FrombillId && m.Id <= tobillId
                              //group i by i.billId into items
                              select new OrderJobDetailsViewModel
                              {
                                  contractorId = m.contractorId,
                                  contractorName = m.contractor.ARName,
                                  SupervisorEngenneringId = m.SupervisorEngenneringId,
                                  SupervisorEngenneringName = m.Engennering.Aname,
                                  OrderJobId = i.OrderJobId,
                                  MainProjectId = m.MainProjectId,
                                  MainProjectName = m.MainProject.Aname,
                                  //Cost = i.Cost,
                                  //ExecutionTime = i.ExecutionTime,
                                  //BeginDateAcutely = i.BeginDateAcutely,
                                  //BeginDateExpected = i.BeginDateExpected,
                                  //EndDateAcutely = i.EndDateAcutely,
                                  //EndDateExpected = i.EndDateExpected,
                                  //StageId = i.StageId,
                                  //StageName = i.Stage.Aname,
                                  //MianItemsName = i.MianItems.Aname,
                                  //MianItemsId = i.MianItemsId,
                                  //Note = i.Note
                              }).ToList();
                    return result;
                }
                ///////
                else if (FromProject == null && toProject == null &&
                    FromCust != null && toCust != null
           && FromSalesMan != null && toSalesMan != null && FrombillId == null
           && tobillId == null)
                {
                    result = (from i in db.OrderJobDetails
                              join m in db.OrderJob
                              on i.OrderJobId equals m.Id
                              where m.IsDeleted == false
                              && m.contractorId >= FromCust && m.contractorId <= toCust
                              && m.SupervisorEngenneringId >= FromSalesMan && m.SupervisorEngenneringId <= toSalesMan
                              //group i by i.billId into items
                              select new OrderJobDetailsViewModel
                              {
                                  contractorId = m.contractorId,
                                  contractorName = m.contractor.ARName,
                                  SupervisorEngenneringId = m.SupervisorEngenneringId,
                                  SupervisorEngenneringName = m.Engennering.Aname,
                                  OrderJobId = i.OrderJobId,
                                  MainProjectId = m.MainProjectId,
                                  MainProjectName = m.MainProject.Aname,
                                  //Cost = i.Cost,
                                  //ExecutionTime = i.ExecutionTime,
                                  //BeginDateAcutely = i.BeginDateAcutely,
                                  //BeginDateExpected = i.BeginDateExpected,
                                  //EndDateAcutely = i.EndDateAcutely,
                                  //EndDateExpected = i.EndDateExpected,
                                  //StageId = i.StageId,
                                  //StageName = i.Stage.Aname,
                                  //MianItemsName = i.MianItems.Aname,
                                  //MianItemsId = i.MianItemsId,
                                  //Note = i.Note
                              }).ToList();
                    return result;
                }
                else if (FromProject == null && toProject == null &&
                    FromCust != null && toCust != null
       && FromSalesMan == null && toSalesMan == null && FrombillId != null
       && tobillId != null)
                {
                    result = (from i in db.OrderJobDetails
                              join m in db.OrderJob
                              on i.OrderJobId equals m.Id
                              where m.IsDeleted == false
                              && m.contractorId >= FromCust && m.contractorId <= toCust
                              && m.Id >= FrombillId && m.Id <= tobillId
                              //group i by i.billId into items
                              select new OrderJobDetailsViewModel
                              {
                                  contractorId = m.contractorId,
                                  contractorName = m.contractor.ARName,
                                  SupervisorEngenneringId = m.SupervisorEngenneringId,
                                  SupervisorEngenneringName = m.Engennering.Aname,
                                  OrderJobId = i.OrderJobId,
                                  MainProjectId = m.MainProjectId,
                                  MainProjectName = m.MainProject.Aname,
                                  //Cost = i.Cost,
                                  //ExecutionTime = i.ExecutionTime,
                                  //BeginDateAcutely = i.BeginDateAcutely,
                                  //BeginDateExpected = i.BeginDateExpected,
                                  //EndDateAcutely = i.EndDateAcutely,
                                  //EndDateExpected = i.EndDateExpected,
                                  //StageId = i.StageId,
                                  //StageName = i.Stage.Aname,
                                  //MianItemsName = i.MianItems.Aname,
                                  //MianItemsId = i.MianItemsId,
                                  //Note = i.Note
                              }).ToList();
                    return result;
                }
                ///////
                else if (FromProject == null && toProject == null &&
                    FromCust == null && toCust == null
         && FromSalesMan != null && toSalesMan != null && FrombillId != null
         && tobillId != null)
                {
                    result = (from i in db.OrderJobDetails
                              join m in db.OrderJob
                              on i.OrderJobId equals m.Id
                              where m.IsDeleted == false
                              && m.Id >= FrombillId && m.Id <= tobillId
                              && m.SupervisorEngenneringId >= FromSalesMan && m.SupervisorEngenneringId <= toSalesMan
                              //group i by i.billId into items
                              select new OrderJobDetailsViewModel
                              {
                                  contractorId = m.contractorId,
                                  contractorName = m.contractor.ARName,
                                  SupervisorEngenneringId = m.SupervisorEngenneringId,
                                  SupervisorEngenneringName = m.Engennering.Aname,
                                  OrderJobId = i.OrderJobId,
                                  MainProjectId = m.MainProjectId,
                                  MainProjectName = m.MainProject.Aname,
                                  //Cost = i.Cost,
                                  //ExecutionTime = i.ExecutionTime,
                                  //BeginDateAcutely = i.BeginDateAcutely,
                                  //BeginDateExpected = i.BeginDateExpected,
                                  //EndDateAcutely = i.EndDateAcutely,
                                  //EndDateExpected = i.EndDateExpected,
                                  //StageId = i.StageId,
                                  //StageName = i.Stage.Aname,
                                  //MianItemsName = i.MianItems.Aname,
                                  //MianItemsId = i.MianItemsId,
                                  //Note = i.Note
                              }).ToList();
                    return result;
                }
                else if (FromProject != null && toProject != null &&
                    FromCust != null && toCust != null
&& FromSalesMan != null && toSalesMan != null && FrombillId != null
&& tobillId != null)
                {
                    result = (from i in db.OrderJobDetails
                              join m in db.OrderJob
                              on i.OrderJobId equals m.Id
                              where m.IsDeleted == false
                              && m.contractorId >= FromCust && m.contractorId <= toCust
                              && m.MainProjectId >= FromProject && m.MainProjectId <= toProject
                              && m.Id >= FrombillId && m.Id <= tobillId
                              //group i by i.billId into items
                              select new OrderJobDetailsViewModel
                              {
                                  contractorId = m.contractorId,
                                  contractorName = m.contractor.ARName,
                                  SupervisorEngenneringId = m.SupervisorEngenneringId,
                                  SupervisorEngenneringName = m.Engennering.Aname,
                                  OrderJobId = i.OrderJobId,
                                  MainProjectId = m.MainProjectId,
                                  MainProjectName = m.MainProject.Aname,
                                  //Cost = i.Cost,
                                  //ExecutionTime = i.ExecutionTime,
                                  //BeginDateAcutely = i.BeginDateAcutely,
                                  //BeginDateExpected = i.BeginDateExpected,
                                  //EndDateAcutely = i.EndDateAcutely,
                                  //EndDateExpected = i.EndDateExpected,
                                  //StageId = i.StageId,
                                  //StageName = i.Stage.Aname,
                                  //MianItemsName = i.MianItems.Aname,
                                  //MianItemsId = i.MianItemsId,
                                  //Note = i.Note
                              }).ToList();
                    return result;
                }
                else
                {
                    result = (from i in db.OrderJobDetails
                              join m in db.OrderJob
                              on i.OrderJobId equals m.Id
                              where m.IsDeleted == false
                              //group i by i.billId into items
                              select new OrderJobDetailsViewModel
                              {
                                  contractorId = m.contractorId,
                                  contractorName = m.contractor.ARName,
                                  SupervisorEngenneringId = m.SupervisorEngenneringId,
                                  SupervisorEngenneringName = m.Engennering.Aname,
                                  OrderJobId = i.OrderJobId,
                                  MainProjectId = m.MainProjectId,
                                  MainProjectName = m.MainProject.Aname,
                                  //Cost = i.Cost,
                                  //ExecutionTime = i.ExecutionTime,
                                  //BeginDateAcutely = i.BeginDateAcutely,
                                  //BeginDateExpected = i.BeginDateExpected,
                                  //EndDateAcutely = i.EndDateAcutely,
                                  //EndDateExpected = i.EndDateExpected,
                                  //StageId = i.StageId,
                                  //StageName = i.Stage.Aname,
                                  //MianItemsName = i.MianItems.Aname,
                                  //MianItemsId = i.MianItemsId,
                                  //Note = i.Note
                              }).ToList();
                    return result;
                }
            }
            return result;
        }

        public IEnumerable<OrderJobDetailsViewModel> Read(int? FromCust, int? toCust
            //, DateTime? FromDate, DateTime? toDate
            , int? FromProject, int? toProject
            , int? FromSalesMan, int? toSalesMan
            , int? FrombillId, int? tobillId)
        {
            return GetAll(FromCust, toCust
                //, FromDate, toDate
                , FromProject, toProject
                , FromSalesMan, toSalesMan
            , FrombillId, tobillId);
        }
        public void Dispose()
        {
            db.Dispose();
        }
    }
}