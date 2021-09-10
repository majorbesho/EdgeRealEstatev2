using EdgeRealEstate.Entities;
using EdgeRealEstate.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace EdgeRealEstate.Models.Services
{
    public class OrderJobDetailsServiceForIndex
    {
        private static bool UpdateDatabase = true;
        private ApplicationDbContext db;

        public OrderJobDetailsServiceForIndex(ApplicationDbContext db)
        {
            this.db = db;
        }

        public IList<OrderJobDetailsViewModel> GetAllFromIndex()
        {
            var result = HttpContext.Current.Session["BillSalesDetails"] as IList<OrderJobDetailsViewModel>;
            int BillSalesMasterId = 0;
            if (HttpContext.Current.Session["MasterIdFromIndex"] != null)
            {
                BillSalesMasterId = (int)HttpContext.Current.Session["MasterIdFromIndex"];
            }

            if (result == null || UpdateDatabase)
            {
                result = db.OrderJobDetails
                    .Where(x => x.OrderJobId == BillSalesMasterId && x.IsDeleted == false)
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

        public IEnumerable<OrderJobDetailsViewModel> Read()
        {
            return GetAllFromIndex();
        }
        public void Create(OrderJobDetailsViewModel IRD)
        {
            if (!UpdateDatabase)
            {
                var first = GetAllFromIndex().OrderByDescending(e => e.Id).FirstOrDefault();
                var id = (first != null) ? first.Id : 0;

                IRD.Id = id + 1;
                if (IRD.StageId == null)
                {
                    IRD.StageId = 1;
                }

                if (IRD.Stage == null)
                {
                    IRD.Stage = new StageVM();
                }
                if (IRD.MianItemsId == null)
                {
                    IRD.MianItemsId = 1;
                }

                if (IRD.MianItems == null)
                {
                    IRD.MianItems = new MainItemViewModel();
                }
                GetAllFromIndex().Insert(0, IRD);
            }
            else
            {
                var entity = new OrderJobDetails();
                entity.Id = IRD.Id;
                entity.Cost = IRD.Cost;
                entity.ExecutionTime = IRD.ExecutionTime;
                entity.Note = IRD.Note;
                entity.EndDateExpected = IRD.EndDateExpected;
                entity.EndDateAcutely = IRD.EndDateAcutely;
                entity.BeginDateAcutely = IRD.BeginDateAcutely;
                entity.BeginDateExpected = IRD.BeginDateExpected;
                entity.IsDeleted = false;
                entity.StageId = IRD.StageId;
                entity.MainItemDetailId = IRD.MianItemsId;
                entity.OrderJobId = (int)HttpContext.Current.Session["MasterIdFromIndex"];

                if (entity.StageId == null)
                {
                    entity.StageId = 1;
                }

                if (IRD.Stage != null)
                {
                    entity.StageId = IRD.Stage.id;
                }
                if (entity.MainItemDetailId == null)
                {
                    entity.MainItemDetailId = 1;
                }

                if (IRD.MianItems != null)
                {
                    entity.MainItemDetailId = IRD.MianItems.ID;
                }
                db.OrderJobDetails.Add(entity);
                db.SaveChanges();

                IRD.Id = entity.Id;          
            }
        }
        public void Update(OrderJobDetailsViewModel IRD)
        {
            if (!UpdateDatabase)
            {
                var target = One(e => e.Id == IRD.Id);

                if (target != null)
                {
                    target.Cost = IRD.Cost;
                    target.ExecutionTime = IRD.ExecutionTime;
                    target.Note = IRD.Note;
                    target.EndDateAcutely = IRD.EndDateAcutely;
                    target.EndDateExpected = IRD.EndDateExpected;
                    target.BeginDateExpected = IRD.BeginDateExpected;
                    target.BeginDateAcutely = IRD.BeginDateAcutely;
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
                entity.OrderJobId = (int)HttpContext.Current.Session["MasterIdFromIndex"];
                entity.Id = IRD.Id;
                entity.Cost = IRD.Cost;
                entity.ExecutionTime = IRD.ExecutionTime;
                entity.Note = IRD.Note;
                entity.EndDateExpected = IRD.EndDateExpected;
                entity.EndDateAcutely = IRD.EndDateAcutely;
                entity.BeginDateAcutely = IRD.BeginDateAcutely;
                entity.BeginDateExpected = IRD.BeginDateExpected;
                entity.IsDeleted = false;
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

        public void Destroy(OrderJobDetailsViewModel IRD)
        {
            if (!UpdateDatabase)
            {
                var target = GetAllFromIndex().FirstOrDefault(p => p.Id == IRD.Id);
                if (target != null)
                {
                    GetAllFromIndex().Remove(target);
                }
            }
            else
            {
                var entity = new OrderJobDetails();
                entity.OrderJobId = (int)HttpContext.Current.Session["MasterIdFromIndex"];
                entity.Id = IRD.Id;
                entity.IsDeleted = true;
                //entity.itmId = IRD.Items.id;
                entity.Cost = IRD.Cost;
                entity.ExecutionTime = IRD.ExecutionTime;
                entity.Note = IRD.Note;
                //entity.EndDateExpected = IRD.EndDateExpected;
                //entity.EndDateAcutely = IRD.EndDateAcutely;
                //entity.BeginDateAcutely = IRD.BeginDateAcutely;
                //entity.BeginDateExpected = IRD.BeginDateExpected;
                //entity.price = IRD.price;
                //entity.rowTotal = IRD.rowTotal;
                //entity.disPer = IRD.disPer;
                //entity.disNo = IRD.disNo;
                //entity.tax = IRD.tax;
                //entity.STOREID = IRD.STOREID;
                db.OrderJobDetails.Attach(entity);
                db.Entry(entity).State = EntityState.Modified;
                db.SaveChanges();


                //int billid = (int)HttpContext.Current.Session["BillSalesMasterId"];
                //StoreTransaction storeTransaction = db.StoreTransactions.Where(x => x.billId == billid && x.itmId == IRD.Items.id).FirstOrDefault();
                //storeTransaction.isDeleted = true;
                //db.StoreTransactions.Attach(storeTransaction);
                //db.Entry(storeTransaction).State = EntityState.Modified;
                //db.SaveChanges();
            }
        }

        public OrderJobDetailsViewModel One(Func<OrderJobDetailsViewModel, bool> predicate)
        {
            return GetAllFromIndex().FirstOrDefault(predicate);
        }

        public void Dispose()
        {
            db.Dispose();
        }

        public void updateMaster(OrderJob OrderJob)
        {
            OrderJob.IsDeleted = false;
            db.Entry(OrderJob).State = EntityState.Modified;
            db.SaveChanges();
        }
    }
}