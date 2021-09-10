using EdgeRealEstate.Entities;
using EdgeRealEstate.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace EdgeRealEstate.Models.Services
{
    public class PricesOffersDetailsService
    {
        private static bool UpdateDatabase = true;
        private ApplicationDbContext db;

        public PricesOffersDetailsService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public IList<PricesOffersDetailsViewModel> GetAll()
        {
            var result = HttpContext.Current.Session["BillPurchasesDetails"] as IList<PricesOffersDetailsViewModel>;
            int BillPurchasesMasterId = 0;

            if (HttpContext.Current.Session["BillPurchasesMasterId"] != null)
            {
                BillPurchasesMasterId = (int)HttpContext.Current.Session["BillPurchasesMasterId"];
            }
            if (result == null || UpdateDatabase)
            {
                result = db.PricesOffersDetails
                    .Where(x => x.billId == BillPurchasesMasterId)
                    .Select(IRD => new PricesOffersDetailsViewModel
                    {
                        Qu = IRD.Qu,
                        price = IRD.price,
                        rowTotal = IRD.rowTotal,

                        disPer = IRD.disPer,
                        disNo = IRD.disNo,
                        tax = IRD.tax,
                        //itmId = IRD.itmId,
                        //Storeid = IRD.Storeid,
                        ConstructionMaterial = new ConstructionMaterialViewModel()
                        {
                            ID = IRD.ConstructionMaterial.ID,
                            MaterialName = IRD.ConstructionMaterial.MaterialName
                        },
                        LKStore = new LKStoreViewModel()
                        {
                            Id = IRD.LKStore.Id,
                            ARName = IRD.LKStore.ARName
                        }
                    }).ToList();

                HttpContext.Current.Session["BillPurchasesDetails"] = result;
            }

            return result;
        }

        public IEnumerable<PricesOffersDetailsViewModel> Read()
        {
            return GetAll();
        }
        public void Create(PricesOffersDetailsViewModel IRD)
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
                var entity = new PricesOffersDetails();
                entity.ConstructionMaterialId = IRD.ConstructionMaterialId;
                entity.Qu = IRD.Qu;
                entity.price = IRD.price;
                entity.rowTotal = IRD.rowTotal;
                entity.disPer = IRD.disPer;
                entity.disNo = IRD.disNo;
                entity.tax = IRD.tax;
                entity.isDeleted = false;
                entity.ConstructionMaterialId = IRD.ConstructionMaterialId;
                entity.STOREID = IRD.STOREID;
                entity.billId = (int)HttpContext.Current.Session["BillPurchasesMasterId"];

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

                db.PricesOffersDetails.Add(entity);
                db.SaveChanges();
                IRD.id = entity.id;

                // BillPurchasesDetailsHistory

                //BillPurchasesDetailsHistory History = new BillPurchasesDetailsHistory();
                //History.itmId = entity.itmId;
                //History.billId = entity.billId;
                //History.newPrice = entity.price;
                //History.newQu = entity.Qu;
                //History.changeDate = DateTime.Now;
                //db.BillPurchasesDetailsHistory.Add(History);
                //db.SaveChanges();

                ///////
                //Items query = db.Items.Find(entity.itmId);
                //bool IsService = query.IsService;
                //if (IsService == false)
                //{
                //StoreTransaction storeTransaction = new StoreTransaction();
                //storeTransaction.ConstructionMaterialId = entity.ConstructionMaterialId;
                //storeTransaction.Quan = (int?)IRD.Qu;
                //storeTransaction.refType = HttpContext.Current.Session["refType"].ToString();
                //storeTransaction.Refid = HttpContext.Current.Session["refNo"].ToString();
                //storeTransaction.ISadd = true;
                //storeTransaction.isDeleted = false;
                //storeTransaction.billId = (int)HttpContext.Current.Session["BillPurchasesMasterId"];
                //storeTransaction.indate = (DateTime)HttpContext.Current.Session["addDate"];
                //db.StoreTransactions.Add(storeTransaction);
                //db.SaveChanges();

                //    // ItemQuanPrice
                //    var ItemQuanPricequery = db.ItemQuanPrice
                //        .Where(x => x.itmId == entity.itmId).ToList();
                //    if (ItemQuanPricequery.Count() == 0)
                //    {
                //        ItemQuanPrice ItemQuanPrice = new ItemQuanPrice();
                //        ItemQuanPrice.itmId = entity.itmId;
                //        ItemQuanPrice.Qu = IRD.Qu;
                //        ItemQuanPrice.price = IRD.price;
                //        ItemQuanPrice.changeDate = DateTime.Now;
                //        db.ItemQuanPrice.Add(ItemQuanPrice);
                //        db.SaveChanges();
                //    }
                //    else
                //    {
                //        ItemQuanPrice itemQuanPrice = ItemQuanPricequery.FirstOrDefault();
                //        itemQuanPrice.changeDate = DateTime.Now;
                //        itemQuanPrice.price = ((itemQuanPrice.Qu * itemQuanPrice.price)
                //            + (IRD.Qu * IRD.price)) / (itemQuanPrice.Qu + IRD.Qu);
                //        itemQuanPrice.Qu += IRD.Qu;
                //        db.ItemQuanPrice.Attach(itemQuanPrice);
                //        db.Entry(itemQuanPrice).State = EntityState.Modified;
                //        db.SaveChanges();
                //    }
                //}
            }
        }
        public void Update(PricesOffersDetailsViewModel IRD)
        {
            if (!UpdateDatabase)
            {
                var target = One(e => e.id == IRD.id);

                if (target != null)
                {

                    target.Qu = IRD.Qu;
                    target.price = IRD.price;
                    target.rowTotal = IRD.rowTotal;
                    target.disPer = IRD.disPer;
                    target.disNo = IRD.disNo;
                    target.tax = IRD.tax;
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
                            MaterialName = db.ConstructionMaterials.Where(s => s.ID == IRD.ConstructionMaterialId).Select(s => s.MaterialName).First()
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
                var entity = new PricesOffersDetails();
                entity.billId = (int)HttpContext.Current.Session["BillPurchasesMasterId"];
                entity.id = IRD.id;
                entity.Qu = IRD.Qu;
                entity.price = IRD.price;
                entity.rowTotal = IRD.rowTotal;
                entity.disPer = IRD.disPer;
                entity.disNo = IRD.disNo;
                entity.tax = IRD.tax;
                //entity.hashCol = IRD.hashCol;
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
                db.PricesOffersDetails.Attach(entity);
                db.Entry(entity).State = EntityState.Modified;
                db.SaveChanges();

                // BillPurchasesDetailsHistory

                //var History = db.BillPurchasesDetailsHistory
                //    .Where(x => x.billId == entity.billId && x.itmId == entity.itmId).FirstOrDefault();
                //History.oldPrice = entity.price;
                //History.oldQu = entity.Qu;
                //History.changeDate = DateTime.Now;
                //db.BillPurchasesDetailsHistory.Attach(History);
                //db.Entry(History).State = EntityState.Modified;
                //db.SaveChanges();

                ///////
                //  Items query = db.Items.Find(entity.itmId);
                //  bool IsService = query.IsService;
                //  if (IsService == false)
                //  {
          //      StoreTransaction storeTransaction = db.StoreTransactions
          //.Where(x => x.ConstructionMaterialId == entity.ConstructionMaterialId && x.billId == entity.billId).FirstOrDefault();
          //      storeTransaction.Quan = (int?)IRD.Qu;
          //      db.StoreTransactions.Attach(storeTransaction);
          //      db.Entry(storeTransaction).State = EntityState.Modified;
          //      db.SaveChanges();

                //      // ItemQuanPrice
                //      var ItemQuanPricequery = db.ItemQuanPrice
                //          .Where(x => x.itmId == entity.itmId).ToList();
                //      if (ItemQuanPricequery.Count() == 0)
                //      {
                //          ItemQuanPrice ItemQuanPrice = new ItemQuanPrice();
                //          ItemQuanPrice.itmId = entity.itmId;
                //          ItemQuanPrice.Qu = IRD.Qu;
                //          ItemQuanPrice.price = IRD.price;
                //          ItemQuanPrice.changeDate = DateTime.Now;
                //          db.ItemQuanPrice.Add(ItemQuanPrice);
                //          db.SaveChanges();
                //      }
                //      else
                //      {
                //          ItemQuanPrice itemQuanPrice = ItemQuanPricequery.FirstOrDefault();
                //          int? old = itemQuanPrice.Qu - entity.Qu;
                //          itemQuanPrice.Qu = old + IRD.Qu;
                //          itemQuanPrice.changeDate = DateTime.Now;
                //          itemQuanPrice.price = ((itemQuanPrice.Qu * itemQuanPrice.price)
                //              + (IRD.Qu * IRD.price)) / (itemQuanPrice.Qu + IRD.Qu);
                //          db.ItemQuanPrice.Attach(itemQuanPrice);
                //          db.Entry(itemQuanPrice).State = EntityState.Modified;
                //          db.SaveChanges();
                //      }
                //  }
            }
        }

        public void Destroy(PricesOffersDetailsViewModel IRD)
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
                var entity = new PricesOffersDetails();

                entity.id = IRD.id;
                entity.isDeleted = true;

                db.PricesOffersDetails.Attach(entity);

                db.PricesOffersDetails.Remove(entity);

                db.SaveChanges();

                //Items query = db.Items.Find(entity.itmId);
                //bool IsService = query.IsService;
                //if (IsService == false)
                //{
             //   StoreTransaction storeTransaction = db.StoreTransactions
             //.Where(x => x.ConstructionMaterialId == entity.ConstructionMaterialId && x.billId == entity.billId).FirstOrDefault();
             //   storeTransaction.isDeleted = true;
             //   db.StoreTransactions.Attach(storeTransaction);
             //   db.Entry(storeTransaction).State = EntityState.Modified;
             //   db.SaveChanges();

                //    // ItemQuanPrice
                //    var ItemQuanPricequery = db.ItemQuanPrice
                //        .Where(x => x.itmId == entity.itmId).ToList();
                //    if (ItemQuanPricequery.Count() == 0)
                //    {
                //        ItemQuanPrice ItemQuanPrice = new ItemQuanPrice();
                //        ItemQuanPrice.itmId = entity.itmId;
                //        ItemQuanPrice.Qu = IRD.Qu;
                //        ItemQuanPrice.price = IRD.price;
                //        ItemQuanPrice.changeDate = DateTime.Now;
                //        db.ItemQuanPrice.Add(ItemQuanPrice);
                //        db.SaveChanges();
                //    }
                //    else
                //    {
                //        ItemQuanPrice itemQuanPrice = ItemQuanPricequery.FirstOrDefault();
                //        itemQuanPrice.Qu -= IRD.Qu;
                //        itemQuanPrice.changeDate = DateTime.Now;
                //        itemQuanPrice.price = ((itemQuanPrice.Qu * itemQuanPrice.price)
                //            + (IRD.Qu * IRD.price)) / (itemQuanPrice.Qu + IRD.Qu);
                //        db.ItemQuanPrice.Attach(itemQuanPrice);
                //        db.Entry(itemQuanPrice).State = EntityState.Modified;
                //        db.SaveChanges();
                //    }
                //}
            }
        }

        public PricesOffersDetailsViewModel One(Func<PricesOffersDetailsViewModel, bool> predicate)
        {
            return GetAll().FirstOrDefault(predicate);
        }

        public void Dispose()
        {
            db.Dispose();
        }

        public void createMaster(PricesOffersMaster PricesOffersMaster)
        {
            PricesOffersMaster.isDeleted = false;
            db.PricesOffersMaster.Add(PricesOffersMaster);
            db.SaveChanges();
        }
    }
}