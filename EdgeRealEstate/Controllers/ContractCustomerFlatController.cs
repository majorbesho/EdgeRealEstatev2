using EdgeRealEstate.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EdgeRealEstate.Entities;
using Kendo.Mvc.Extensions;

namespace EdgeRealEstate.Controllers
{
    public class ContractCustomerFlatController : Controller
    {

        private ApplicationDbContext db = new ApplicationDbContext();



        // GET: ContractCustomerFlat
        public ActionResult Index()
        {
            return View();
        }

        // GET: ContractCustomerFlat/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ContractCustomerFlat/Create
        public ActionResult Create()
        {
            ViewBag.MainProjects = db.MainProjects.ToList();
            return View();
        }

        // POST: ContractCustomerFlat/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: ContractCustomerFlat/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ContractCustomerFlat/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: ContractCustomerFlat/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ContractCustomerFlat/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }


        //ContractCustomerFlat/GetProjectByMainProjectId/5
        public JsonResult GetProjectByMainProjectId(int? id)
        {

            var Projects = db.Projectes.Where(a => a.MainProjectId == id ).ToList();

            if (Projects == null)
            {
                Projects = new List<Projects>();

            }

            return Json(Projects.Select(a => new { a.id, a.Aname }), JsonRequestBehavior.AllowGet);
        }


        //ContractCustomerFlat/GetFlatByProjectId/5
        public JsonResult GetFlatByProjectId(int? id, int? mainProjectId)
        {
            //  all type   0  1 2 

            var Flat = db.Flats.Include("Building").Include("Projects.MainProject").Where(a=>a.FlatTypeId==3).ToList();
            if (id != null)
            {
               
                Flat = Flat.Where(a => a.ProjectsId == id).ToList();
            }
            if (mainProjectId != null)
            {
                Flat = Flat.Where(a => a.Projects?.MainProjectId == mainProjectId ).ToList();
            }
            if (Flat == null)
            {
                Flat = new List<Flat>();
            }
            return Json(Flat.Select(a => new { a.id, a.Aname, a.code, a.Level, a.BuildingId, BuildingCode = a.Building?.Code, a.FlotSize, a.BedroomNo, a.DegreeOfExcellence, a.EndDateExpected ,a.FlatTypeId}), JsonRequestBehavior.AllowGet);
        }

        //ContractCustomerFlat/GetFlatByProjectId/5
        public JsonResult GetFlat()
        {
            //  all type   0  1 2 
            var Flat = db.Flats.Include("Building").Where(a=>a.FlatTypeId==3);
            var x = Flat.ToList();

            return Json(Flat.Select(a => new { a.id, a.Aname, a.code, a.Level, a.BuildingId, BuildingCode = a.Building.Code, a.FlotSize, a.BedroomNo, a.DegreeOfExcellence, a.EndDateExpected,a.FlatTypeId }), JsonRequestBehavior.AllowGet);
        }

        // GET: ContractCustomerFlat/Details/5
        public ActionResult Details(string FlatCode)
        {
            var findFlat = db.Flats.Where(a => a.code == FlatCode);
            return View(findFlat);
        }
    }
}
