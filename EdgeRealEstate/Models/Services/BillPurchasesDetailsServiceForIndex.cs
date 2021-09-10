using EdgeRealEstate.Entities;
using EdgeRealEstate.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace EdgeRealEstate.Models.Services
{
    public class BillPurchasesDetailsServiceForIndex
    {
        private static bool UpdateDatabase = true;
        private ApplicationDbContext db;

        public BillPurchasesDetailsServiceForIndex(ApplicationDbContext db)
        {
            this.db = db;
        }

        public IList<BillPurchasesDetailsViewModel> GetAllFromIndex(int? id)
        {
            var result = HttpContext.Current.Session["BillPurchasesDetails"] as IList<BillPurchasesDetailsViewModel>;
            int BillPurchasesMasterId = 0;
            if (HttpContext.Current.Session["MasterIdFromIndex"] != null)
            {
                BillPurchasesMasterId = (int)HttpContext.Current.Session["MasterIdFromIndex"];
            }
            if (result == null || UpdateDatabase)
            {
                if (id == null)
                {
                    result = db.BillPurchasesDetails
                        .Where(x => x.billId == BillPurchasesMasterId && x.isDeleted == false)
                        .Select(IRD => new BillPurchasesDetailsViewModel
                        {
                            id = IRD.id,
                            Qu = IRD.Qu,
                            price = IRD.price,
                            rowTotal = IRD.rowTotal,
                            disPer = IRD.disPer,
                            disNo = IRD.disNo,
                            tax = IRD.tax,
                            ConstructionMaterial = new ConstructionMaterialViewModel()
                            {
                                ID = IRD.ConstructionMaterial.ID,
                                MaterialName = IRD.ConstructionMaterial.MaterialName
                            }
                        }).ToList();

                    HttpContext.Current.Session["BillPurchasesDetails"] = result;
                }
                else
                {
                    result = db.PricesOffersDetails
                      .Where(x => x.billId == id && x.isDeleted == false)
                      .Select(IRD => new BillPurchasesDetailsViewModel
                      {
                          id = IRD.id,
                          Qu = IRD.Qu,
                          price = IRD.price,
                          rowTotal = IRD.rowTotal,
                          disPer = IRD.disPer,
                          disNo = IRD.disNo,
                          tax = IRD.tax,
                          ConstructionMaterial = new ConstructionMaterialViewModel()
                          {
                              ID = IRD.ConstructionMaterial.ID,
                              MaterialName = IRD.ConstructionMaterial.MaterialName
                          }
                      }).ToList();

                    HttpContext.Current.Session["BillPurchasesDetails"] = result;
                }
            }

            return result;
        }

        public IEnumerable<BillPurchasesDetailsViewModel> Read(int? id)
        {
            return GetAllFromIndex(id);
        }
        public void Create(BillPurchasesDetailsViewModel IRD,int? PriceOfferid)
        {
            if (!UpdateDatabase)
            {
                var first = GetAllFromIndex(PriceOfferid).OrderByDescending(e => e.id).FirstOrDefault();
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
                GetAllFromIndex(PriceOfferid).Insert(0, IRD);
            }
            else
            {
                var entity = new BillPurchasesDetails();
                entity.id = IRD.id;
                entity.Qu = IRD.Qu;
                entity.price = IRD.price;
                entity.rowTotal = IRD.rowTotal;
                entity.disPer = IRD.disPer;
                entity.disNo = IRD.disNo;
                entity.tax = IRD.tax;
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
                db.BillPurchasesDetails.Add(entity);
                db.SaveChanges();

                IRD.id = entity.id;


                StoreTransaction storeTransaction = new StoreTransaction();
                storeTransaction.ConstructionMaterialId = entity.ConstructionMaterialId;
                storeTransaction.Quan = (int?)IRD.Qu;
                storeTransaction.refType = HttpContext.Current.Session["refType"].ToString();
                storeTransaction.Refid = HttpContext.Current.Session["refNo"].ToString();
                storeTransaction.ISadd = true;
                storeTransaction.isDeleted = false;
                storeTransaction.billId = (int)HttpContext.Current.Session["BillPurchasesMasterId"];
                storeTransaction.indate = (DateTime)HttpContext.Current.Session["addDate"];
                db.StoreTransactions.Add(storeTransaction);
                db.SaveChanges();

                // ItemQuanPrice
                //var ItemQuanPricequery = db.ItemQuanPrice
                //    .Where(x => x.itmId == entity.itmId).ToList();
                //if (ItemQuanPricequery.Count() == 0)
                //{
                //    ItemQuanPrice ItemQuanPrice = new ItemQuanPrice();
                //    ItemQuanPrice.itmId = entity.itmId;
                //    ItemQuanPrice.Qu = IRD.Qu;
                //    ItemQuanPrice.price = IRD.price;
                //    ItemQuanPrice.changeDate = DateTime.Now;
                //    db.ItemQuanPrice.Add(ItemQuanPrice);
                //    db.SaveChanges();
                //}
                //else
                //{
                //    ItemQuanPrice itemQuanPrice = ItemQuanPricequery.FirstOrDefault();
                //    itemQuanPrice.Qu += IRD.Qu;
                //    itemQuanPrice.changeDate = DateTime.Now;
                //    itemQuanPrice.price = ((itemQuanPrice.Qu * itemQuanPrice.price)
                //        + (IRD.Qu * IRD.price)) / (itemQuanPrice.Qu + IRD.Qu);
                //    db.ItemQuanPrice.Attach(itemQuanPrice);
                //    db.Entry(itemQuanPrice).State = EntityState.Modified;
                //    db.SaveChanges();
                //}
            }
        }
        public void Update(BillPurchasesDetailsViewModel IRD,int? PriceOfferid)
        {
            if (!UpdateDatabase)
            {
                var target = One(e => e.id == IRD.id, PriceOfferid);

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

                    if (IRD.BillPurchasesMaster != null)
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
                int? billId = (int)HttpContext.Current.Session["MasterIdFromIndex"];

                var entity = new BillPurchasesDetails();
                entity.billId = (int)HttpContext.Current.Session["MasterIdFromIndex"];
                entity.id = IRD.id;
                entity.Qu = IRD.Qu;
                entity.price = IRD.price;
                entity.rowTotal = IRD.rowTotal;
                entity.disPer = IRD.disPer;
                entity.disNo = IRD.disNo;
                entity.tax = IRD.tax;
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
                db.BillPurchasesDetails.Attach(entity);
                db.Entry(entity).State = EntityState.Modified;
                db.SaveChanges();


                // BillPurchasesDetailsHistory

                //var History = db.BillPurchasesDetailsHistory
                //    .Where(x => x.billId == entity.billId && x.itmId == entity.itmId).FirstOrDefault();
                //History.oldPrice = History.newPrice;
                //History.oldQu = History.newQu;
                //History.newPrice = entity.price;
                //History.newQu = entity.Qu;
                //History.changeDate = DateTime.Now;
                //db.BillPurchasesDetailsHistory.Attach(History);
                //db.Entry(History).State = EntityState.Modified;
                //db.SaveChanges();

                ///////
                StoreTransaction storeTransaction = db.StoreTransactions.Where(x => x.billId == entity.billId && x.ConstructionMaterialId == entity.ConstructionMaterialId).FirstOrDefault();
                storeTransaction.Quan = (int?)IRD.Qu;
                storeTransaction.refType = HttpContext.Current.Session["refType"].ToString();
                storeTransaction.Refid = HttpContext.Current.Session["refNo"].ToString();
                storeTransaction.ISadd = true;
                storeTransaction.isDeleted = false;
                db.StoreTransactions.Attach(storeTransaction);
                db.Entry(storeTransaction).State = EntityState.Modified;
                db.SaveChanges();

                // ItemQuanPrice
                //                var ItemQuanPricequery = db.ItemQuanPrice
                //    .Where(x => x.itmId == IRD.Items.id).ToList();
                //                int? itemId = ItemQuanPricequery.FirstOrDefault().itmId;
                //                int lastid = db.BillPurchasesDetailsHistory
                //    .Where(x => x.itmId == itemId).Max(x => x.id);
                //                int beforelast =
                //                    db.BillPurchasesDetailsHistory
                //    .Where(x => x.itmId == itemId).Max(x => x.id - 1);
                //                var getlast = db.BillPurchasesDetailsHistory
                //    .Where(x => x.id == lastid
                //    && x.itmId == itemId).FirstOrDefault();
                //                var getbeforelast = db.BillPurchasesDetailsHistory
                //.Where(x => x.id == beforelast
                //&& x.itmId == itemId).FirstOrDefault();
                //                if (ItemQuanPricequery.Count() == 0)
                //                {
                //                    ItemQuanPrice ItemQuanPrice = new ItemQuanPrice();
                //                    ItemQuanPrice.itmId = entity.itmId;
                //                    ItemQuanPrice.Qu = IRD.Qu;
                //                    ItemQuanPrice.price = IRD.price;
                //                    ItemQuanPrice.changeDate = DateTime.Now;
                //                    db.ItemQuanPrice.Add(ItemQuanPrice);
                //                    db.SaveChanges();
                //                }
                //                else
                //                {

                //                    ItemQuanPrice itemQuanPrice = ItemQuanPricequery.FirstOrDefault();
                //                    //int? old = itemQuanPrice.Qu - oldQu;
                //                    //itemQuanPrice.Qu = old + IRD.Qu;
                //                    itemQuanPrice.changeDate = DateTime.Now;
                //                    //itemQuanPrice.price = ((itemQuanPrice.Qu * itemQuanPrice.price)
                //                    //    + (IRD.Qu * IRD.price)) / (itemQuanPrice.Qu + IRD.Qu);
                //                    itemQuanPrice.price = ((getbeforelast.newQu * getbeforelast.newPrice)
                //                        + (getlast.newQu * getlast.oldPrice)) / (getbeforelast.newQu + getlast.oldPrice);
                //                    db.ItemQuanPrice.Attach(itemQuanPrice);
                //                    db.Entry(itemQuanPrice).State = EntityState.Modified;
                //                    db.SaveChanges();
                //                }
            }
        }

        public void Destroy(BillPurchasesDetailsViewModel IRD,int? PriceOfferid)
        {
            if (!UpdateDatabase)
            {
                var target = GetAllFromIndex(PriceOfferid).FirstOrDefault(p => p.id == IRD.id);
                if (target != null)
                {
                    GetAllFromIndex(PriceOfferid).Remove(target);
                }
            }
            else
            {
                var entity = new BillPurchasesDetails();
                entity.billId = (int)HttpContext.Current.Session["MasterIdFromIndex"];
                entity.id = IRD.id;
                entity.isDeleted = true;
                entity.ConstructionMaterialId = IRD.ConstructionMaterial.ID;
                entity.Qu = IRD.Qu;
                entity.price = IRD.price;
                entity.rowTotal = IRD.rowTotal;
                entity.disPer = IRD.disPer;
                entity.disNo = IRD.disNo;
                entity.tax = IRD.tax;
                entity.STOREID = IRD.STOREID;
                db.BillPurchasesDetails.Attach(entity);
                db.Entry(entity).State = EntityState.Modified;
                //db.BillPurchasesDetails.Remove(entity);
                db.SaveChanges();


                int billid = (int)HttpContext.Current.Session["BillPurchasesMasterId"];
                StoreTransaction storeTransaction = db.StoreTransactions.Where(x => x.billId == billid 
                && x.ConstructionMaterialId == IRD.ConstructionMaterial.ID).FirstOrDefault();
                storeTransaction.isDeleted = true;
                db.StoreTransactions.Attach(storeTransaction);
                db.Entry(storeTransaction).State = EntityState.Modified;
                db.SaveChanges();

                // ItemQuanPrice
                //var ItemQuanPricequery = db.ItemQuanPrice
                //    .Where(x => x.itmId == entity.itmId).ToList();
                //if (ItemQuanPricequery.Count() == 0)
                //{
                //    ItemQuanPrice ItemQuanPrice = new ItemQuanPrice();
                //    ItemQuanPrice.itmId = entity.itmId;
                //    ItemQuanPrice.Qu = IRD.Qu;
                //    ItemQuanPrice.price = IRD.price;
                //    ItemQuanPrice.changeDate = DateTime.Now;
                //    db.ItemQuanPrice.Add(ItemQuanPrice);
                //    db.SaveChanges();
                //}
                //else
                //{
                //    ItemQuanPrice itemQuanPrice = ItemQuanPricequery.FirstOrDefault();
                //    itemQuanPrice.Qu -= IRD.Qu;
                //    itemQuanPrice.changeDate = DateTime.Now;
                //    itemQuanPrice.price = ((itemQuanPrice.Qu * itemQuanPrice.price)
                //        + (IRD.Qu * IRD.price)) / (itemQuanPrice.Qu + IRD.Qu);
                //    db.ItemQuanPrice.Attach(itemQuanPrice);
                //    db.Entry(itemQuanPrice).State = EntityState.Modified;
                //    db.SaveChanges();
                //}
            }
        }

        public BillPurchasesDetailsViewModel One(Func<BillPurchasesDetailsViewModel, bool> predicate,int? PriceOfferid)
        {
            return GetAllFromIndex(PriceOfferid).FirstOrDefault(predicate);
        }

        public void Dispose()
        {
            db.Dispose();
        }
        public void updateMaster(BillPurchasesMaster BillPurchasesMasters)
        {
            BillPurchasesMasters.isDeleted = false;
            db.Entry(BillPurchasesMasters).State = EntityState.Modified;
            db.SaveChanges();
        }
    }
}