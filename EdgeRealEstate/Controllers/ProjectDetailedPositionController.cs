using EdgeRealEstate.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EdgeRealEstate.Models.ViewModels;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;

namespace EdgeRealEstate.Controllers
{
    public class ProjectDetailedPositionController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: 
        public ActionResult Index()
        {
            ViewBag.Projects = new SelectList(db.Projectes, "id", "Aname");

            ViewBag.FlatType = new SelectList(db.Flats, "id", "Aname");
            return View();
        }

        public JsonResult GetMainLands(string text)
        {
            var MainLands = db.MainLands
                .Select(i => new MainLandDropVM
                {
                    Id = i.id,
                    ARName = i.Aname,
                });
            return Json(MainLands, JsonRequestBehavior.AllowGet);
        }
        public ActionResult HierarchyBinding_MainProjects([DataSourceRequest] DataSourceRequest request, int? MainLand_Id)
        {

            if (MainLand_Id != null)
            {
                var res = db.MainProjects.Where(m => m.MainLandId == MainLand_Id).Select(x => new MainProjectGridVM
                {
                    id = x.id,
                    LandNo = x.LandNo,
                    Aname = x.Aname,
                    Ename = x.Ename,
                    MaxLevelForContributions = x.MaxLevelForContributions,
                    nots = x.nots,
                    TypesInvestment = x.TypesInvestment

                }).ToList();
                return Json(res.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);

            }
            var result = db.MainProjects.Select(x => new MainProjectGridVM
            {
                id = x.id,
                LandNo = x.LandNo,
                Aname = x.Aname,
                Ename = x.Ename,
                MaxLevelForContributions = x.MaxLevelForContributions,
                nots = x.nots,
                TypesInvestment = x.TypesInvestment

            }).ToList();
            return Json(result.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);

        }

        public ActionResult HierarchyBinding_Projects(int id, [DataSourceRequest] DataSourceRequest request)
        {
            var result = db.Projectes.Where(i => i.MainProjectId == id).Select(x => new ProjectGridVM
            {
                id = x.id,
                Aname = x.Aname,
                MaxLevelForContributions = x.MaxLevelForContributions,
                LandSize = x.LandSize,
                VellaNO = x.VellaNO,
                FlatNO = x.FlatNO,

            }).ToList();
            return Json(result.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }
    }
}