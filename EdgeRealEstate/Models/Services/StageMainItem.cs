using EdgeRealEstate.Entities;
using EdgeRealEstate.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace EdgeRealEstate.Models.Services
{
    public class StageMainItem /*: IDisposable*/
    {
        //private static bool UpdateDatabase = true;

        //private ApplicationDbContext entities;

        //public StageMainItem(ApplicationDbContext entities)
        //{
        //    this.entities = entities;
        //}



        //public IList<StageVM> GetAll()
        //{
        //    var result = HttpContext.Current.Session["Products"] as IList<StageVM>;

        //    if (result == null || UpdateDatabase)
        //    {
        //        result = entities.Stages.Select(product => new StageVM
        //        {
        //            id = product.id,
        //            Aname = product.Aname,
        //            Ename = product.Ename,
        //            code = product.code,
        //            IsHaveItem = product.IsHaveItem,
        //            BeginDateAcutely = product.BeginDateAcutely,
        //            EndDateAcutely = product.EndDateAcutely,
        //            lastCost = product.lastCost,
        //            nots = product.nots,
        //            MainItemID = product.MainItemId,
        //            Main = new MainItemViewModel()
        //            {
        //                ID = product.MianItems.Id,
        //                Name = product.MianItems.Ename
        //            },
        //        }).ToList();

        //        HttpContext.Current.Session["Products"] = result;
        //    }

        //    return result;
        //}

        //public IEnumerable<StageVM> Read()
        //{
        //    return GetAll();
        //}

        //public void Create(StageVM product)
        //{

        //    if (!UpdateDatabase)
        //    {
        //        var first = GetAll().OrderByDescending(e => e.id).FirstOrDefault();
        //        var id = (first != null) ? first.id : 0;

        //        product.id = id + 1;

        //        if (product.MainItemID == null)
        //        {
        //            product.MainItemID = 1;
        //        }

        //        if (product.Main == null)
        //        {
        //            product.Main = new MainItemViewModel() { ID = 1, Name = "Beverages" };
        //        }

        //        GetAll().Insert(0, product);
        //    }
        //    else
        //    {
        //        var entity = new Stage();

        //        entity.Aname = product.Aname;
        //        entity.Ename = product.Ename;
        //        entity.IsHaveItem = product.IsHaveItem;
        //        entity.code = product.code;
        //        entity.BeginDateAcutely = product.BeginDateAcutely;
        //        entity.EndDateAcutely = product.EndDateAcutely;
        //        entity.lastCost = product.lastCost;
        //        entity.nots = product.nots;
        //        entity.MainItemId = product.MainItemID.Value;

        //        if (entity.MainItemId == null)
        //        {
        //            entity.MainItemId = 1;
        //        }

        //        if (product.Main != null)
        //        {
        //            entity.MainItemId = product.Main.ID;
        //        }

        //        entities.Stages.Add(entity);
        //        entities.SaveChanges();

        //        product.id = entity.id;
        //    }

        //}

        //public void Update(StageVM product)
        //{
        //    if (!UpdateDatabase)
        //    {
        //        var target = One(e => e.MainItemID == product.MainItemID);

        //        if (target != null)
        //        {
        //            target.Aname = product.Aname;
        //            target.Ename = product.Ename;
        //            target.code = product.code;
        //            target.IsHaveItem = product.IsHaveItem;
        //            target.BeginDateAcutely = product.BeginDateAcutely;
        //            target.EndDateAcutely = product.EndDateAcutely;
        //            target.lastCost = product.lastCost;
        //            target.nots = product.nots;

        //            if (product.MainItemID == null)
        //            {
        //                product.MainItemID = 1;
        //            }

        //            if (product.Main != null)
        //            {
        //                product.MainItemID = product.Main.ID;
        //            }
        //            else
        //            {
        //                product.Main = new MainItemViewModel()
        //                {
        //                    ID = (int)product.MainItemID,
        //                    Name = entities.MianItemss.Where(s => s.Id == product.MainItemID).Select(s => s.Ename).First()
        //                };
        //            }

        //            target.MainItemID = product.MainItemID;
        //            target.Main = product.Main;
        //        }
        //    }
        //    else
        //    {
        //        var entity = new Stage();

        //        entity.id = product.id;
        //        entity.Aname = product.Aname;
        //        entity.Ename = product.Ename;
        //        entity.code = product.code;
        //        entity.IsHaveItem = product.IsHaveItem;
        //        entity.BeginDateAcutely = product.BeginDateAcutely;
        //        entity.EndDateAcutely = product.EndDateAcutely;
        //        entity.lastCost = product.lastCost;
        //        entity.nots = product.nots;
        //        entity.MainItemId = product.MainItemID.Value;

        //        if (product.Main != null)
        //        {
        //            entity.MainItemId = product.Main.ID;
        //        }

        //        entities.Stages.Attach(entity);
        //        entities.Entry(entity).State = EntityState.Modified;
        //        entities.SaveChanges();
        //    }
        //}

        //public void Destroy(StageVM product)
        //{
        //    if (!UpdateDatabase)
        //    {
        //        var target = GetAll().FirstOrDefault(p => p.id == product.id);
        //        if (target != null)
        //        {
        //            GetAll().Remove(target);
        //        }
        //    }
        //    else
        //    {
        //        //var entity = new Stage();

        //        //entity.id = product.id;

        //        Stage engennering = entities.Stages.Find(product.id);

        //        entities.Stages.Remove(engennering);

        //        //entities.Stages.Attach(entity);

        //        //entities.Stages.Remove(entity);

        //        //var orderDetails = entities.Order_Details.Where(pd => pd.id == entity.id);

        //        //foreach (var orderDetail in orderDetails)
        //        //{
        //        //    entities.Order_Details.Remove(orderDetail);
        //        //}

        //        entities.SaveChanges();
        //    }
        //}

        //public StageVM One(Func<StageVM, bool> predicate)
        //{
        //    return GetAll().FirstOrDefault(predicate);
        //}

        //public void Dispose()
        //{
        //    entities.Dispose();
        //}




    }
}