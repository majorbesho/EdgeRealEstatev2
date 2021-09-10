using EdgeRealEstate.Entities;
using EdgeRealEstate.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace EdgeRealEstate.Models.Services
{
    public class OrderJobDetailsService
    {
        private static bool UpdateDatabase = true;
        private ApplicationDbContext db;

        public OrderJobDetailsService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public IList<OrderJobDetailsViewModel> GetAll()
        {

            var result = HttpContext.Current.Session["BillSalesDetails"] as IList<OrderJobDetailsViewModel>;
            int BillSalesMasterId = 0;
            if (HttpContext.Current.Session["ItemInventoryMasterId"] != null)
            {
                BillSalesMasterId = (int)HttpContext.Current.Session["ItemInventoryMasterId"];
            }
            if (result == null || UpdateDatabase)
            {
                result = db.OrderJobDetails
                    //.Where(x => x.billId == BillSalesMasterId)
                    .Select(IRD => new OrderJobDetailsViewModel
                    {
                        Cost = IRD.Cost,
                        ExecutionTime = IRD.ExecutionTime,
                        Note = IRD.Note,
                        //EndDateExpected = IRD.EndDateExpected,
                        //EndDateAcutely = IRD.EndDateAcutely,
                        //BeginDateAcutely = IRD.BeginDateAcutely,
                        //BeginDateExpected = IRD.BeginDateExpected,
                        //itmId = IRD.itmId,
                        //Storeid = IRD.Storeid,
                        //Items = new ItemsViewModel()
                        //{
                        //    id = IRD.Items.id,
                        //    arName = IRD.Items.arName
                        //},
                        //LKStore = new LKStoreViewModel()
                        //{
                        //    Id = IRD.LKStore.Id,
                        //    ARName = IRD.LKStore.ARName
                        //}
                    }).ToList();

                HttpContext.Current.Session["BillSalesDetails"] = result;
            }

            return result;
        }

        public IEnumerable<OrderJobDetailsViewModel> Read()
        {
            return GetAll();
        }
        public void Create(OrderJobDetailsViewModel IRD)
        {
            if (!UpdateDatabase)
            {
                var first = GetAll().OrderByDescending(e => e.Id).FirstOrDefault();
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
                GetAll().Insert(0, IRD);
            }
            else
            {
                var entity = new OrderJobDetails();
                entity.Cost = IRD.Cost;
                entity.ExecutionTime = IRD.ExecutionTime;
                entity.Note = IRD.Note;
                entity.BeginDateAcutely = IRD.BeginDateAcutely;
                entity.BeginDateExpected = IRD.BeginDateExpected;
                entity.EndDateAcutely = IRD.EndDateAcutely;
                entity.EndDateExpected = IRD.EndDateExpected;
                //entity.BeginDateAcutely = DateTime.Now;
                //entity.BeginDateExpected = DateTime.Now;
                //entity.EndDateAcutely = DateTime.Now;
                //entity.EndDateExpected = DateTime.Now;                
                entity.IsDeleted = false;
                entity.StageId = IRD.Stage.id;
                entity.MainItemDetailId = IRD.MianItems.ID;
                if (HttpContext.Current.Session["ItemInventoryMasterId"] != null) { 
                entity.OrderJobId = (int)HttpContext.Current.Session["ItemInventoryMasterId"];

                    if (entity.StageId == null)
                    {
                        entity.StageId = 1;
                    }

                    //if (IRD.Stage != null)
                    //{
                    //    entity.StageId = IRD.Stage.id;
                    //}

                    if (entity.MainItemDetailId == null)
                    {
                        entity.MainItemDetailId = 1;
                    }

                    //if (IRD.MianItems != null)
                    //{
                    //    entity.MainItemDetailId = IRD.MianItems.ID;
                    //}
                    db.OrderJobDetails.Add(entity);
                db.SaveChanges();

                IRD.Id = entity.Id;
                }         
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
                    //target.BeginDateExpected = IRD.BeginDateExpected;
                    //target.BeginDateAcutely = IRD.BeginDateAcutely;
                    //target.EndDateAcutely = IRD.EndDateAcutely;
                    //target.EndDateExpected = IRD.EndDateExpected;
                    target.Cost = IRD.Cost;
                    target.IsDeleted = false;
                    //if (IRD.itmId == null)
                    //{
                    //    IRD.itmId = 1;
                    //}

                    //if (IRD.Items != null)
                    //{
                    //    IRD.itmId = IRD.Items.id;
                    //}
                    //else
                    //{
                    //    IRD.Items = new ItemsViewModel()
                    //    {
                    //        id = (int)IRD.itmId,
                    //        arName = db.Items.Where(s => s.id == IRD.itmId).Select(s => s.arName).First()
                    //    };
                    //}

                    //target.itmId = IRD.itmId;
                    //target.Items = IRD.Items;

                    //if (IRD.STOREID == null)
                    //{
                    //    IRD.STOREID = 1;
                    //}

                    //if (IRD.LKStore != null)
                    //{
                    //    IRD.STOREID = IRD.LKStore.Id;
                    //}
                    //else
                    //{
                    //    IRD.LKStore = new LKStoreViewModel()
                    //    {
                    //        Id = (int)IRD.STOREID,
                    //        ARName = db.LKStores.Where(s => s.Id == IRD.STOREID).Select(s => s.ARName).First()
                    //    };
                    //}

                    //target.STOREID = IRD.STOREID;
                    //target.LKStore = IRD.LKStore;
                }
            }
            else
            {
                var entity = new OrderJobDetails();
                entity.OrderJobId = (int)HttpContext.Current.Session["BillSalesMasterId"];
                entity.Id = IRD.Id;
                entity.Cost = IRD.Cost;
                entity.ExecutionTime = IRD.ExecutionTime;
                entity.Note = IRD.Note;
                //entity.BeginDateAcutely = IRD.BeginDateAcutely;
                //entity.BeginDateExpected = IRD.BeginDateExpected;
                //entity.EndDateAcutely = IRD.EndDateAcutely;
                //entity.EndDateExpected = IRD.EndDateExpected;
                entity.IsDeleted = false;

                db.OrderJobDetails.Attach(entity);
                db.Entry(entity).State = EntityState.Modified;
                db.SaveChanges();
            }
        }

        public void Destroy(OrderJobDetailsViewModel IRD)
        {
            if (!UpdateDatabase)
            {
                var target = GetAll().FirstOrDefault(p => p.Id == IRD.Id);
                if (target != null)
                {
                    GetAll().Remove(target);
                }
            }
            else
            {
                var entity = new OrderJobDetails();

                entity.Id = IRD.Id;
                entity.IsDeleted = true;

                db.OrderJobDetails.Attach(entity);

                db.OrderJobDetails.Remove(entity);

                db.SaveChanges();
            }
        }

        public OrderJobDetailsViewModel One(Func<OrderJobDetailsViewModel, bool> predicate)
        {
            return GetAll().FirstOrDefault(predicate);
        }

        public void Dispose()
        {
            db.Dispose();
        }

        public void createMaster(OrderJob OrderJob)
        {
            OrderJob.IsDeleted = false;
            db.OrderJob.Add(OrderJob);
            db.SaveChanges();
        }
    }
}