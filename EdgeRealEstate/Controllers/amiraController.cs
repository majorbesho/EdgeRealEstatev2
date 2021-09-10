using EdgeRealEstate.Entities;
using EdgeRealEstate.Models;
using EdgeRealEstate.Models.Services;
using EdgeRealEstate.Models.ViewModels;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace PointOfSaleApp.Controllers
{
    public class amiraController : Controller
    {
        // GET: amira
        ApplicationDbContext db = new ApplicationDbContext();
        private amiraDetailsService amiraDetailsService;
        private amiraServiceForIndex amiraServiceForIndex;
        public amiraController()
        {
            this.amiraDetailsService = new amiraDetailsService(db);
            this.amiraServiceForIndex = new amiraServiceForIndex(db);
        }
        // GET: MasterDetailsTry
        public ActionResult Index(int? page)
        {
            var BillSalesMasters = db.amiraMaster.Where(x => x.IsDeleted == false)
            .OrderByDescending(x => x.id).ToList().ToPagedList(page ?? 1, 1);
            if (BillSalesMasters.Count() != 0)
            {
                Session["MasterIdFromIndex"] = BillSalesMasters.FirstOrDefault().id;
            }
            else
            {
                Session["MasterIdFromIndex"] = 1;
            }

            return View(BillSalesMasters);
        }

        // GET: BillSalesMaster/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            amiraMaster BillSalesMasters = db.amiraMaster.Find(id);
            if (BillSalesMasters == null)
            {
                return HttpNotFound();
            }
            return View(BillSalesMasters);
        }

        // POST: BillSalesMaster/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            amiraMaster BillSalesMasters = db.amiraMaster.Find(id);
            List<amiraDetails> BillSalesDetails = db.amiraDetails.Where(x => x.MasterId == id).ToList();
            foreach (amiraDetails i in BillSalesDetails)
            {
                i.IsDeleted = true;             
            }
            BillSalesMasters.IsDeleted = true;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        /************ create ***********/
        public ActionResult Create(string ARname, string ENname)
        {
            amiraMaster amiraMaster = new amiraMaster();
            amiraMaster.ARname = ARname;
            amiraMaster.ENname = ENname;
            amiraDetailsService.createMaster(amiraMaster);
            Session["BillSalesMasterId"] = amiraMaster.id;

            return View();
        }
        public ActionResult Editing_Custom()
        {
            return View();
        }
        public ActionResult EditingCustom_Read([DataSourceRequest] DataSourceRequest request)
        {
            return Json(request);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EditingCustom_Update([DataSourceRequest] DataSourceRequest request,
            [Bind(Prefix = "models")]IEnumerable<amiraDetailsViewModel> ItemRequests)
        {
            if (ItemRequests != null && ModelState.IsValid)
            {
                foreach (var ItemRequest in ItemRequests)
                {
                    amiraDetailsService.Update(ItemRequest);
                }
            }

            return Json(ItemRequests.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EditingCustom_Create([DataSourceRequest] DataSourceRequest request,
            [Bind(Prefix = "models")]IEnumerable<amiraDetailsViewModel> ItemRequests)
        {
            var results = new List<amiraDetailsViewModel>();

            if (ItemRequests != null && ModelState.IsValid)
            {
                foreach (var ItemRequest in ItemRequests)
                {
                    amiraDetailsService.Create(ItemRequest);
                    results.Add(ItemRequest);
                    db.SaveChanges();
                }
            }

            return Json(results.ToDataSourceResult(request, ModelState));
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EditingCustom_Destroy([DataSourceRequest] DataSourceRequest request,
            [Bind(Prefix = "models")]IEnumerable<amiraDetailsViewModel> ItemRequests)
        {
            foreach (var ItemRequest in ItemRequests)
            {
                amiraDetailsService.Destroy(ItemRequest);
            }

            return Json(ItemRequests.ToDataSourceResult(request, ModelState));
        }


        /*************************Index****************************/
        public ActionResult Editing_CustomForIndex()
        {
            return View();
        }
        public ActionResult EditingCustom_ReadForIndex([DataSourceRequest] DataSourceRequest request)
        {
            return Json(amiraServiceForIndex.Read().ToDataSourceResult(request));
        }

        /********************Edit ********************/
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Edit(int? id, string ARname, string ENname)
        {
            amiraMaster amiraMaster = db.amiraMaster.Find(id);
            amiraMaster.ARname = ARname;
            amiraMaster.ENname = ENname;
            amiraServiceForIndex.updateMaster(amiraMaster);
            Session["BillSalesMasterId"] = amiraMaster.id;
            ViewBag.msgg = 1;
            return View(amiraMaster);
        }
        public ActionResult Editing_CustomForEdit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            amiraMaster amiraMaster = db.amiraMaster.Find(id);
            ViewBag.ARname = amiraMaster.ARname;
            ViewBag.ENname = amiraMaster.ENname;
            if (amiraMaster == null)
            {
                return HttpNotFound();
            }
            return View(amiraMaster);
        }

        public ActionResult EditingCustom_ReadForEdit([DataSourceRequest] DataSourceRequest request)
        {
            return Json(amiraServiceForIndex.Read().ToDataSourceResult(request));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EditingCustom_UpdateForEdit([DataSourceRequest] DataSourceRequest request,
            [Bind(Prefix = "models")]IEnumerable<amiraDetailsViewModel> ItemRequests)
        {
            if (ItemRequests != null && ModelState.IsValid)
            {
                foreach (var ItemRequest in ItemRequests)
                {
                    amiraServiceForIndex.Update(ItemRequest);
                }
            }

            return Json(ItemRequests.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EditingCustom_CreateForEdit([DataSourceRequest] DataSourceRequest request,
                [Bind(Prefix = "models")]IEnumerable<amiraDetailsViewModel> ItemRequests)
        {
            var results = new List<amiraDetailsViewModel>();

            if (ItemRequests != null && ModelState.IsValid)
            {
                foreach (var ItemRequest in ItemRequests)
                {
                    amiraServiceForIndex.Create(ItemRequest);
                    results.Add(ItemRequest);
                    db.SaveChanges();
                }
            }

            return Json(results.ToDataSourceResult(request, ModelState));
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EditingCustom_DestroyForEdit([DataSourceRequest] DataSourceRequest request,
            [Bind(Prefix = "models")]IEnumerable<amiraDetailsViewModel> ItemRequests)
        {
            foreach (var ItemRequest in ItemRequests)
            {
                amiraServiceForIndex.Destroy(ItemRequest);
            }

            return Json(ItemRequests.ToDataSourceResult(request, ModelState));
        }
        /**********************Delete view************************/
        public ActionResult Editing_CustomForDelete()
        {
            return View();
        }

        public ActionResult EditingCustom_ReadForDelete([DataSourceRequest] DataSourceRequest request)
        {
            return Json(amiraServiceForIndex.Read().ToDataSourceResult(request));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EditingCustom_UpdateForDelete([DataSourceRequest] DataSourceRequest request,
    [Bind(Prefix = "models")]IEnumerable<amiraDetailsViewModel> ItemRequests)
        {
            if (ItemRequests != null && ModelState.IsValid)
            {
                foreach (var ItemRequest in ItemRequests)
                {
                    amiraServiceForIndex.Update(ItemRequest);
                }
            }

            return Json(ItemRequests.ToDataSourceResult(request, ModelState));
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EditingCustom_DestroyForDelete([DataSourceRequest] DataSourceRequest request,
            [Bind(Prefix = "models")]IEnumerable<amiraDetailsViewModel> ItemRequests)
        {
            foreach (var ItemRequest in ItemRequests)
            {
                amiraServiceForIndex.Destroy(ItemRequest);
            }

            return Json(ItemRequests.ToDataSourceResult(request, ModelState));
        }
 
    }
}