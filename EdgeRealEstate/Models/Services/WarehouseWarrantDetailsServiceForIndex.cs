using EdgeRealEstate.Entities;
using EdgeRealEstate.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace EdgeRealEstate.Models.Services
{
    public class WarehouseWarrantDetailsServiceForIndex
    {
        private static bool UpdateDatabase = true;
        private ApplicationDbContext db;

        public WarehouseWarrantDetailsServiceForIndex(ApplicationDbContext db)
        {
            this.db = db;
        }

        public IList<WarehouseWarrantDetailsViewModel> GetAllFromIndex()
        {
            var result = HttpContext.Current.Session["BillSalesDetails"] as IList<WarehouseWarrantDetailsViewModel>;
            int BillSalesMasterId = 0;
            if (HttpContext.Current.Session["MasterIdFromIndex"] != null)
            {
                BillSalesMasterId = (int)HttpContext.Current.Session["MasterIdFromIndex"];
            }

            if (result == null || UpdateDatabase)
            {
                result = db.WarehouseWarrantDetails
                    .Where(x => x.billId == BillSalesMasterId && x.isDeleted == false)
                    .Select(IRD => new WarehouseWarrantDetailsViewModel
                    {
                        id = IRD.id,
                        Qu = IRD.Qu,
                        price = IRD.price,
                        rowTotal = IRD.rowTotal,
                        ConstructionMaterial = new ConstructionMaterialViewModel()
                        {
                            ID = IRD.ConstructionMaterial.ID,
                            MaterialName = IRD.ConstructionMaterial.MaterialName
                        }
                    }).ToList();

                HttpContext.Current.Session["BillSalesDetails"] = result;
            }

            return result;
        }

        public IEnumerable<WarehouseWarrantDetailsViewModel> Read()
        {
            return GetAllFromIndex();
        }
        public void Create(WarehouseWarrantDetailsViewModel IRD)
        {
            if (!UpdateDatabase)
            {
                var first = GetAllFromIndex().OrderByDescending(e => e.id).FirstOrDefault();
                var id = (first != null) ? first.id : 0;

                IRD.id = id + 1;
                if (IRD.ConstructionMaterialId == null)
                {
                    IRD.ConstructionMaterialId = 1;
                }

                if (IRD.ConstructionMaterial == null)
                {
                    IRD.ConstructionMaterial = new ConstructionMaterialViewModel();
                }
                if (IRD.STOREID == null)
                {
                    IRD.STOREID = 1;
                }

                if (IRD.LKStore == null)
                {
                    IRD.LKStore = new LKStoreViewModel();
                }
                GetAllFromIndex().Insert(0, IRD);
            }
            else
            {
                var entity = new WarehouseWarrantDetails();
                entity.id = IRD.id;
                entity.Qu = IRD.Qu;
                entity.price = IRD.price;
                entity.rowTotal = IRD.rowTotal;
                entity.isDeleted = false;
                entity.ConstructionMaterialId = IRD.ConstructionMaterialId;
                entity.STOREID = IRD.STOREID;
                entity.billId = (int)HttpContext.Current.Session["MasterIdFromIndex"];

                if (entity.ConstructionMaterialId == null)
                {
                    entity.ConstructionMaterialId = 1;
                }

                if (IRD.ConstructionMaterial != null)
                {
                    entity.ConstructionMaterialId = IRD.ConstructionMaterial.ID;
                }
                if (entity.STOREID == null)
                {
                    entity.STOREID = 1;
                }

                if (IRD.LKStore != null)
                {
                    entity.STOREID = IRD.LKStore.Id;
                }
                db.WarehouseWarrantDetails.Add(entity);
                db.SaveChanges();

                IRD.id = entity.id;
                StoreTransaction storeTransaction = new StoreTransaction();
                storeTransaction.ConstructionMaterialId = entity.ConstructionMaterialId;
                storeTransaction.Quan = (int?)IRD.Qu;
                if (HttpContext.Current.Session["refType"] != null)
                {
                    storeTransaction.refType = HttpContext.Current.Session["refType"].ToString();
                }
                if (HttpContext.Current.Session["refNo"] != null)
                {
                    storeTransaction.Refid = HttpContext.Current.Session["refNo"].ToString();
                }
                storeTransaction.ISadd = false;
                storeTransaction.isDeleted = false;
                storeTransaction.billId = (int)HttpContext.Current.Session["BillSalesMasterId"];
                storeTransaction.indate = (DateTime)HttpContext.Current.Session["addDate"];
                db.StoreTransactions.Add(storeTransaction);
                db.SaveChanges();
            }
        }
        public void Update(WarehouseWarrantDetailsViewModel IRD)
        {
            if (!UpdateDatabase)
            {
                var target = One(e => e.id == IRD.id);

                if (target != null)
                {
                    target.Qu = IRD.Qu;
                    target.price = IRD.price;
                    target.rowTotal = IRD.rowTotal;
                    target.isDeleted = false;
                    if (IRD.ConstructionMaterialId == null)
                    {
                        IRD.ConstructionMaterialId = 1;
                    }

                    if (IRD.ConstructionMaterial != null)
                    {
                        IRD.ConstructionMaterialId = IRD.ConstructionMaterial.ID;
                    }
                    else
                    {
                        IRD.ConstructionMaterial = new ConstructionMaterialViewModel()
                        {
                            ID = (int)IRD.ConstructionMaterialId,
                            MaterialName = db.ConstructionMaterials
                            .Where(s => s.ID == IRD.ConstructionMaterialId).Select(s => s.MaterialName).First()
                        };
                    }

                    target.ConstructionMaterialId = IRD.ConstructionMaterialId;
                    target.ConstructionMaterial = IRD.ConstructionMaterial;

                    if (IRD.STOREID == null)
                    {
                        IRD.STOREID = 1;
                    }

                    if (IRD.LKStore != null)
                    {
                        IRD.STOREID = IRD.LKStore.Id;
                    }
                    else
                    {
                        IRD.LKStore = new LKStoreViewModel()
                        {
                            Id = (int)IRD.STOREID,
                            ARName = db.LKStores.Where(s => s.Id == IRD.STOREID).Select(s => s.ARName).First()
                        };
                    }

                    target.STOREID = IRD.STOREID;
                    target.LKStore = IRD.LKStore;
                }
            }
            else
            {
                var entity = new WarehouseWarrantDetails();
                entity.billId = (int)HttpContext.Current.Session["MasterIdFromIndex"];
                entity.id = IRD.id;
                entity.Qu = IRD.Qu;
                entity.price = IRD.price;
                entity.rowTotal = IRD.rowTotal;
                entity.isDeleted = false;
                entity.ConstructionMaterialId = IRD.ConstructionMaterialId;
                entity.STOREID = IRD.STOREID;
                if (IRD.ConstructionMaterial != null)
                {
                    entity.ConstructionMaterialId = IRD.ConstructionMaterial.ID;
                }
                if (IRD.LKStore != null)
                {
                    entity.STOREID = IRD.LKStore.Id;
                }
                db.WarehouseWarrantDetails.Attach(entity);
                db.Entry(entity).State = EntityState.Modified;
                db.SaveChanges();


                StoreTransaction storeTransaction = db.StoreTransactions
                    .Where(x => x.billId == entity.billId && x.ConstructionMaterialId == entity.ConstructionMaterialId).FirstOrDefault();
                if (storeTransaction != null)
                {
                    storeTransaction.Quan = (int?)IRD.Qu;
                    if (HttpContext.Current.Session["refType"] != null)
                    {
                        storeTransaction.refType = HttpContext.Current.Session["refType"].ToString();
                    }
                    if (HttpContext.Current.Session["refNo"] != null)
                    {
                        storeTransaction.Refid = HttpContext.Current.Session["refNo"].ToString();
                    }
                    storeTransaction.ISadd = false;
                    storeTransaction.isDeleted = false;
                    db.StoreTransactions.Attach(storeTransaction);
                    db.Entry(storeTransaction).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }
        }

        public void Destroy(WarehouseWarrantDetailsViewModel IRD)
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
                var entity = new WarehouseWarrantDetails();
                entity.billId = (int)HttpContext.Current.Session["MasterIdFromIndex"];
                entity.id = IRD.id;
                entity.isDeleted = true;
                entity.ConstructionMaterialId = IRD.ConstructionMaterial.ID;
                entity.Qu = IRD.Qu;
                entity.price = IRD.price;
                entity.rowTotal = IRD.rowTotal;
                entity.STOREID = IRD.STOREID;
                db.WarehouseWarrantDetails.Attach(entity);
                db.Entry(entity).State = EntityState.Modified;
                db.SaveChanges();


                int billid = (int)HttpContext.Current.Session["BillSalesMasterId"];
                StoreTransaction storeTransaction = db.StoreTransactions
                    .Where(x => x.billId == billid && x.ConstructionMaterialId == IRD.ConstructionMaterial.ID).FirstOrDefault();
                storeTransaction.isDeleted = true;
                db.StoreTransactions.Attach(storeTransaction);
                db.Entry(storeTransaction).State = EntityState.Modified;
                db.SaveChanges();
            }
        }

        public WarehouseWarrantDetailsViewModel One(Func<WarehouseWarrantDetailsViewModel, bool> predicate)
        {
            return GetAllFromIndex().FirstOrDefault(predicate);
        }

        public void Dispose()
        {
            db.Dispose();
        }

        public void updateMaster(WarehouseWarrantMaster WarehouseWarrantMaster)
        {
            WarehouseWarrantMaster.isDeleted = false;
            db.Entry(WarehouseWarrantMaster).State = EntityState.Modified;
            db.SaveChanges();
        }
    }
}