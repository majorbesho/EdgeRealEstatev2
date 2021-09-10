using EdgeRealEstate.Entities;
using EdgeRealEstate.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace EdgeRealEstate.Models.Services
{
    public class OrderJobFollowUpServiceForIndex
    {
        private static bool UpdateDatabase = true;
        private ApplicationDbContext db;

        public OrderJobFollowUpServiceForIndex(ApplicationDbContext db)
        {
            this.db = db;
        }

        public IList<OrderJobDetailsViewModel> GetAllFromIndex(int? OrderJobCode)
        {
            var result = HttpContext.Current.Session["BillSalesDetails"] as IList<OrderJobDetailsViewModel>;

            if (result == null || UpdateDatabase)
            {
                result = db.OrderJobDetails
                    .Where(x => x.OrderJobId == OrderJobCode && x.IsDeleted == false)
                    .Select(IRD => new OrderJobDetailsViewModel
                    {
                        Id = IRD.Id,
                        Cost = IRD.Cost,
                        ExecutionTime = IRD.ExecutionTime,
                        Note = IRD.Note,
                        BeginDateExpected = IRD.BeginDateExpected,
                        BeginDateAcutely = IRD.BeginDateAcutely,
                        EndDateAcutely = IRD.EndDateAcutely,
                        EndDateExpected = IRD.EndDateExpected,
                        CompletionRate = IRD.CompletionRate,
                        Stage = new StageVM()
                        {
                            id = IRD.Stage.Stage.id,
                            Aname = IRD.Stage.Stage.Aname
                        },
                        MianItems = new MainItemViewModel()
                        {
                            ID = IRD.MainItemDetail.MainItem.Id,
                            Name = IRD.MainItemDetail.MainItem.Aname
                        }
                    }).ToList();

                HttpContext.Current.Session["BillSalesDetails"] = result;
            }

            return result;
        }

        public IEnumerable<OrderJobDetailsViewModel> Read(int? Id)
        {
            return GetAllFromIndex(Id);
        }
       
        public void Update(OrderJobDetailsViewModel IRD, int? Code)
        {
            if (!UpdateDatabase)
            {
                var target = One(e => e.Id == IRD.Id, Code);

                if (target != null)
                {
                    target.Cost = IRD.Cost;
                    target.ExecutionTime = IRD.ExecutionTime;
                    target.Note = IRD.Note;
                    target.EndDateAcutely = IRD.EndDateAcutely;
                    target.EndDateExpected = IRD.EndDateExpected;
                    target.BeginDateExpected = IRD.BeginDateExpected;
                    target.BeginDateAcutely = IRD.BeginDateAcutely;
                    target.CompletionRate = IRD.CompletionRate;
                    target.IsDeleted = false;
                    if (IRD.StageId == null)
                    {
                        IRD.StageId = 1;
                    }

                    if (IRD.Stage != null)
                    {
                        IRD.StageId = IRD.Stage.id;
                    }
                    else
                    {
                        IRD.Stage = new StageVM()
                        {
                            id = (int)IRD.StageId,
                            Aname = db.Stages.Where(s => s.id == IRD.StageId).Select(s => s.Aname).First()
                        };
                    }

                    target.StageId = IRD.StageId;
                    target.Stage = IRD.Stage;

                    if (IRD.MianItemsId == null)
                    {
                        IRD.MianItemsId = 1;
                    }

                    if (IRD.MianItems != null)
                    {
                        IRD.MianItemsId = IRD.MianItems.ID;
                    }
                    else
                    {
                        IRD.MianItems = new MainItemViewModel()
                        {
                            ID = (int)IRD.MianItemsId,
                            Name = db.MianItemss.Where(s => s.Id == IRD.MianItemsId).Select(s => s.Aname).First()
                        };
                    }

                    target.MianItemsId = IRD.MianItemsId;
                    target.MianItems = IRD.MianItems;
                }
            }
            else
            {
                var entity = new OrderJobDetails();
                entity.OrderJobId = Code /*(int)HttpContext.Current.Session["MasterIdFromIndex"]*/;
                entity.Id = IRD.Id;
                entity.Cost = IRD.Cost;
                entity.ExecutionTime = IRD.ExecutionTime;
                entity.Note = IRD.Note;
                entity.EndDateExpected = IRD.EndDateExpected;
                entity.EndDateAcutely = IRD.EndDateAcutely;
                entity.BeginDateAcutely = IRD.BeginDateAcutely;
                entity.BeginDateExpected = IRD.BeginDateExpected;
                entity.IsDeleted = false;
                entity.CompletionRate = IRD.CompletionRate;
                entity.StageId = IRD.StageId;
                entity.MainItemDetailId = IRD.MianItemsId;
                if (IRD.Stage != null)
                {
                    entity.StageId = IRD.Stage.id;
                }
                if (IRD.MianItems != null)
                {
                    entity.MainItemDetailId = IRD.MianItems.ID;
                }
                db.OrderJobDetails.Attach(entity);
                db.Entry(entity).State = EntityState.Modified;
                db.SaveChanges();

            }
        }

        
        public OrderJobDetailsViewModel One(Func<OrderJobDetailsViewModel, bool> predicate,int? OrderJobCode)
        {
            return GetAllFromIndex(OrderJobCode).FirstOrDefault(predicate);
        }

        public void Dispose()
        {
            db.Dispose();
        }
    }
}