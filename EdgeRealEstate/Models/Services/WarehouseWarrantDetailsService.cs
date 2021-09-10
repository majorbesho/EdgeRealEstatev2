using EdgeRealEstate.Entities;
using EdgeRealEstate.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace EdgeRealEstate.Models.Services
{
    public class WarehouseWarrantDetailsService
    {
        private static bool UpdateDatabase = true;
        private ApplicationDbContext db;

        public WarehouseWarrantDetailsService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public IList<WarehouseWarrantDetailsViewModel> GetAll()
        {

            var result = HttpContext.Current.Session["BillSalesDetails"] as IList<WarehouseWarrantDetailsViewModel>;
            int BillSalesMasterId = 0;
            if (HttpContext.Current.Session["BillSalesMasterId"] != null)
            {
                BillSalesMasterId = (int)HttpContext.Current.Session["BillSalesMasterId"];
            }
            if (result == null || UpdateDatabase)
            {
                result = db.WarehouseWarrantDetails
                    //.Where(x => x.billId == BillSalesMasterId)
                    .Select(IRD => new WarehouseWarrantDetailsViewModel
                    {
                        Qu = IRD.Qu,
                        price = IRD.price,
                        rowTotal = IRD.rowTotal,
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

        public IEnumerable<WarehouseWarrantDetailsViewModel> Read()
        {
            return GetAll();
        }
        public void Create(WarehouseWarrantDetailsViewModel IRD)
        {
            if (!UpdateDatabase)
            {
                var first = GetAll().OrderByDescending(e => e.id).FirstOrDefault();
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
                GetAll().Insert(0, IRD);
            }
            else
            {
                var entity = new WarehouseWarrantDetails();
                entity.Qu = IRD.Qu;
                entity.price = IRD.price;
                entity.rowTotal = IRD.rowTotal;
                entity.isDeleted = false;
                entity.ConstructionMaterialId = IRD.ConstructionMaterialId;
                entity.STOREID = IRD.STOREID;
                entity.billId = (int)HttpContext.Current.Session["BillSalesMasterId"];

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
                    storeTransaction.refType = HttpContext.Current.Session["refType"].ToString();
                    storeTransaction.Refid = HttpContext.Current.Session["refNo"].ToString();
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
                entity.billId = (int)HttpContext.Current.Session["BillSalesMasterId"];
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
                    .Where(x => x.ConstructionMaterialId == entity.ConstructionMaterialId 
                    && x.billId == entity.billId).FirstOrDefault();
                    storeTransaction.Quan = (int?)IRD.Qu;
                    db.StoreTransactions.Attach(storeTransaction);
                    db.Entry(storeTransaction).State = EntityState.Modified;
                    db.SaveChanges();
            }
        }

        public void Destroy(WarehouseWarrantDetailsViewModel IRD)
        {
            if (!UpdateDatabase)
            {
                var target = GetAll().FirstOrDefault(p => p.id == IRD.id);
                if (target != null)
                {
                    GetAll().Remove(target);
                }
            }
            else
            {
                var entity = new WarehouseWarrantDetails();

                entity.id = IRD.id;
                entity.isDeleted = true;

                db.WarehouseWarrantDetails.Attach(entity);

                db.WarehouseWarrantDetails.Remove(entity);

                db.SaveChanges();

                    StoreTransaction storeTransaction = db.StoreTransactions
                    .Where(x => x.ConstructionMaterialId == entity.ConstructionMaterialId 
                    && x.billId == entity.billId).FirstOrDefault();
                    storeTransaction.isDeleted = true;
                    db.StoreTransactions.Attach(storeTransaction);
                    db.Entry(storeTransaction).State = EntityState.Modified;
                    db.SaveChanges();
            }
        }

        public WarehouseWarrantDetailsViewModel One(Func<WarehouseWarrantDetailsViewModel, bool> predicate)
        {
            return GetAll().FirstOrDefault(predicate);
        }

        public void Dispose()
        {
            db.Dispose();
        }

        public void createMaster(WarehouseWarrantMaster WarehouseWarrantMaster)
        {
            WarehouseWarrantMaster.isDeleted = false;
            db.WarehouseWarrantMaster.Add(WarehouseWarrantMaster);
            db.SaveChanges();
        }
    }
}