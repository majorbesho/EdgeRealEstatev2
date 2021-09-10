using EdgeRealEstate.Entities;
using EdgeRealEstate.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace EdgeRealEstate.Models.Services
{
    public class amiraDetailsService
    {

        private static bool UpdateDatabase = true;
        private ApplicationDbContext db;

        public amiraDetailsService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public IList<amiraDetailsViewModel> GetAll()
        {

            var result = HttpContext.Current.Session["BillSalesDetails"] as IList<amiraDetailsViewModel>;
            int BillSalesMasterId = 0;
            if (HttpContext.Current.Session["BillSalesMasterId"] != null)
            {
                BillSalesMasterId = (int)HttpContext.Current.Session["BillSalesMasterId"];
            }
            if (result == null || UpdateDatabase)
            {
                result = db.amiraDetails
                    //.Where(x => x.billId == BillSalesMasterId)
                    .Select(IRD => new amiraDetailsViewModel
                    {
                        name = IRD.name,
                     
                    }).ToList();

                HttpContext.Current.Session["BillSalesDetails"] = result;
            }

            return result;
        }

        public IEnumerable<amiraDetailsViewModel> Read()
        {
            return GetAll();
        }
        public void Create(amiraDetailsViewModel IRD)
        {
            if (!UpdateDatabase)
            {
                var first = GetAll().OrderByDescending(e => e.id).FirstOrDefault();
                var id = (first != null) ? first.id : 0;

                IRD.id = id + 1;        
                GetAll().Insert(0, IRD);
            }
            else
            {
                var entity = new amiraDetails();
                entity.name = IRD.name;
                entity.IsDeleted = false;
                entity.MasterId = (int)HttpContext.Current.Session["BillSalesMasterId"];
              
                db.amiraDetails.Add(entity);
                db.SaveChanges();

                IRD.id = entity.id;                
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
                    target.IsDeleted = false;
                   
                }
            }
            else
            {
                var entity = new amiraDetails();
                entity.MasterId = (int)HttpContext.Current.Session["BillSalesMasterId"];
                entity.id = IRD.id;
                entity.name = IRD.name;
                entity.IsDeleted = false;               
                db.amiraDetails.Attach(entity);
                db.Entry(entity).State = EntityState.Modified;
                db.SaveChanges();
            }
        }

        public void Destroy(amiraDetailsViewModel IRD)
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
                var entity = new amiraDetails();

                entity.id = IRD.id;
                entity.IsDeleted = true;

                db.amiraDetails.Attach(entity);

                db.amiraDetails.Remove(entity);

                db.SaveChanges();

            }
        }

        public amiraDetailsViewModel One(Func<amiraDetailsViewModel, bool> predicate)
        {
            return GetAll().FirstOrDefault(predicate);
        }

        public void Dispose()
        {
            db.Dispose();
        }

        public void createMaster(amiraMaster amiraMaster)
        {
            amiraMaster.IsDeleted = false;
            db.amiraMaster.Add(amiraMaster);
            db.SaveChanges();
        }
    }
}