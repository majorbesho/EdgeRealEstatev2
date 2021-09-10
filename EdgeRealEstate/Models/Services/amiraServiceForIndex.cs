
using EdgeRealEstate.Entities;
using EdgeRealEstate.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace EdgeRealEstate.Models.Services
{
    public class amiraServiceForIndex
    {
        private static bool UpdateDatabase = true;
        private ApplicationDbContext db;

        public amiraServiceForIndex(ApplicationDbContext db)
        {
            this.db = db;
        }

        public IList<amiraDetailsViewModel> GetAllFromIndex()
        {
            var result = HttpContext.Current.Session["BillSalesDetails"] as IList<amiraDetailsViewModel>;
            int BillSalesMasterId = 0;
            if (HttpContext.Current.Session["MasterIdFromIndex"] != null)
            {
                BillSalesMasterId = (int)HttpContext.Current.Session["MasterIdFromIndex"];
            }

            if (result == null || UpdateDatabase)
            {
                result = db.amiraDetails
                    .Where(x => x.MasterId == BillSalesMasterId && x.IsDeleted == false)
                    .Select(IRD => new amiraDetailsViewModel
                    {
                        id = IRD.id,
                        name = IRD.name,
                        //price = IRD.price,
                        //rowTotal = IRD.rowTotal,
                        //disPer = IRD.disPer,
                        //disNo = IRD.disNo,
                        //tax = IRD.tax,
                        //Items = new ItemsViewModel()
                        //{
                        //    id = IRD.Items.id,
                        //    arName = IRD.Items.arName
                        //}
                    }).ToList();

                HttpContext.Current.Session["BillSalesDetails"] = result;
            }

            return result;
        }

        public IEnumerable<amiraDetailsViewModel> Read()
        {
            return GetAllFromIndex();
        }
        public void Create(amiraDetailsViewModel IRD)
        {
            if (!UpdateDatabase)
            {
                var first = GetAllFromIndex().OrderByDescending(e => e.id).FirstOrDefault();
                var id = (first != null) ? first.id : 0;

                IRD.id = id + 1;
                //if (IRD.itmId == null)
                //{
                //    IRD.itmId = 1;
                //}

                //if (IRD.Items == null)
                //{
                //    IRD.Items = new ItemsViewModel();
                //}
                //if (IRD.STOREID == null)
                //{
                //    IRD.STOREID = 1;
                //}

                //if (IRD.LKStore == null)
                //{
                //    IRD.LKStore = new LKStoreViewModel();
                //}
                GetAllFromIndex().Insert(0, IRD);
            }
            else
            {
                var entity = new amiraDetails();
                entity.id = IRD.id;
                entity.name = IRD.name;
                //entity.price = IRD.price;
                //entity.rowTotal = IRD.rowTotal;
                //entity.disPer = IRD.disPer;
                //entity.disNo = IRD.disNo;
                //entity.tax = IRD.tax;
                entity.IsDeleted = false;
                //entity.itmId = IRD.itmId;
                //entity.STOREID = IRD.STOREID;
                entity.MasterId = (int)HttpContext.Current.Session["MasterIdFromIndex"];

                //if (entity.itmId == null)
                //{
                //    entity.itmId = 1;
                //}

                //if (IRD.Items != null)
                //{
                //    entity.itmId = IRD.Items.id;
                //}
                //if (entity.STOREID == null)
                //{
                //    entity.STOREID = 1;
                //}

                //if (IRD.LKStore != null)
                //{
                //    entity.STOREID = IRD.LKStore.Id;
                //}
                db.amiraDetails.Add(entity);
                db.SaveChanges();

                IRD.id = entity.id;
                //StoreTransaction storeTransaction = new StoreTransaction();
                //storeTransaction.itmId = entity.itmId;
                //storeTransaction.Quan = (int?)IRD.Qu;
                //if (HttpContext.Current.Session["refType"] != null)
                //{
                //    storeTransaction.refType = HttpContext.Current.Session["refType"].ToString();
                //}
                //if (HttpContext.Current.Session["refNo"] != null)
                //{
                //    storeTransaction.Refid = HttpContext.Current.Session["refNo"].ToString();
                //}
                //storeTransaction.ISadd = false;
                //storeTransaction.isDeleted = false;
                //storeTransaction.billId = (int)HttpContext.Current.Session["BillSalesMasterId"];
                //storeTransaction.indate = (DateTime)HttpContext.Current.Session["addDate"];
                //db.StoreTransactions.Add(storeTransaction);
                //db.SaveChanges();
            }
        }
        public void Update(amiraDetailsViewModel IRD)
        {
            if (!UpdateDatabase)
            {
                var target = One(e => e.id == IRD.id);

                if (target != null)
                {
                    target.name = IRD.name;
                    //target.price = IRD.price;
                    //target.rowTotal = IRD.rowTotal;
                    //target.disPer = IRD.disPer;
                    //target.disNo = IRD.disNo;
                    //target.tax = IRD.tax;
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
                var entity = new amiraDetails();
                entity.MasterId = (int)HttpContext.Current.Session["MasterIdFromIndex"];
                entity.id = IRD.id;
                entity.name = IRD.name;
                //entity.price = IRD.price;
                //entity.rowTotal = IRD.rowTotal;
                //entity.disPer = IRD.disPer;
                //entity.disNo = IRD.disNo;
                //entity.tax = IRD.tax;
                entity.IsDeleted = false;
                //entity.itmId = IRD.itmId;
                //entity.STOREID = IRD.STOREID;
                //if (IRD.Items != null)
                //{
                //    entity.itmId = IRD.Items.id;
                //}
                //if (IRD.LKStore != null)
                //{
                //    entity.STOREID = IRD.LKStore.Id;
                //}
                db.amiraDetails.Attach(entity);
                db.Entry(entity).State = EntityState.Modified;
                db.SaveChanges();


                //StoreTransaction storeTransaction = db.StoreTransactions.Where(x => x.billId == entity.billId && x.itmId == entity.itmId).FirstOrDefault();
                //if (storeTransaction != null)
                //{
                //    storeTransaction.Quan = (int?)IRD.Qu;
                //    if (HttpContext.Current.Session["refType"] != null)
                //    {
                //        storeTransaction.refType = HttpContext.Current.Session["refType"].ToString();
                //    }
                //    if (HttpContext.Current.Session["refNo"] != null)
                //    {
                //        storeTransaction.Refid = HttpContext.Current.Session["refNo"].ToString();
                //    }
                //    storeTransaction.ISadd = false;
                //    storeTransaction.isDeleted = false;
                //    db.StoreTransactions.Attach(storeTransaction);
                //    db.Entry(storeTransaction).State = EntityState.Modified;
                //    db.SaveChanges();
                //}
            }
        }

        public void Destroy(amiraDetailsViewModel IRD)
        {
            if (!UpdateDatabase)
            {
                var target = GetAllFromIndex().FirstOrDefault(p => p.id == IRD.id);
                if (target != null)
                {
                    GetAllFromIndex().Remove(target);
                }
            }
            else
            {
                var entity = new amiraDetails();
                entity.MasterId = (int)HttpContext.Current.Session["MasterIdFromIndex"];
                entity.id = IRD.id;
                entity.IsDeleted = true;
                //entity.itmId = IRD.Items.id;
                entity.name = IRD.name;
                //entity.price = IRD.price;
                //entity.rowTotal = IRD.rowTotal;
                //entity.disPer = IRD.disPer;
                //entity.disNo = IRD.disNo;
                //entity.tax = IRD.tax;
                //entity.STOREID = IRD.STOREID;
                db.amiraDetails.Attach(entity);
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

        public amiraDetailsViewModel One(Func<amiraDetailsViewModel, bool> predicate)
        {
            return GetAllFromIndex().FirstOrDefault(predicate);
        }

        public void Dispose()
        {
            db.Dispose();
        }

        public void updateMaster(amiraMaster amiraMaster)
        {
            amiraMaster.IsDeleted = false;
            db.Entry(amiraMaster).State = EntityState.Modified;
            db.SaveChanges();
        }
    }
}