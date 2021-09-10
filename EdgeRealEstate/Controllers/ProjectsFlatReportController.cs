using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EdgeRealEstate.Models;
using EdgeRealEstate.Models.ViewModels;
using EdgeRealEstate.Models.Services;

namespace EdgeRealEstate.Controllers
{
    public class ProjectsFlatReportController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        private ProjectsFlatReportService ProjectsFlatReportService;

        // GET: ProjectsFlatReport
        public ActionResult Index(int? FromProID, int? ToProID)
        {
            ViewBag.ProjectsFrom = new SelectList(db.Projectes, "Id", "Aname");
            ViewBag.ProjectsTo = new SelectList(db.Projectes, "Id", "Aname");
            var FromPro = FromProID;
            var ToPro = ToProID;
            if (FromPro == null)
            {
                FromPro = db.Projectes.Select(c => c.id).First();
            }
            if (ToPro == null)
            {
                ToPro = db.Projectes.OrderByDescending(c => c.id).First().id;
            }
            //var FromPro = db.Projectes.Select(c => c.id).First();
            var result = (from i in db.Projectes
                      where i.id <= FromPro && i.id >= ToPro
                          select new 
                      {
                          ProjectName = i.Aname,
                          BeginDateAcutely = (DateTime)i.BeginDateAcutely,
                          EndDateAcutely = (DateTime)i.EndDateAcutely,
                          TotalFlatNO = i.FlatNO,
                          UnderpreparingFlat = i.Flats.Where(c => c.FlatTypeId == 2).Count(),
                          BookedFlat = i.Flats.Where(c => c.FlatTypeId == 4).Count(),
                          ReadyFlat = i.Flats.Where(c => c.FlatTypeId == 3).Count(),
                          SoledFlat = i.Flats.Where(c => c.FlatTypeId == 5).Count()
                      }).ToList().Select(x=> new ProjectsFlatViewModel 
                      {
                          ProjectName = x.ProjectName,
                          BeginDateAcutely = x.BeginDateAcutely,
                          EndDateAcutely = x.EndDateAcutely,
                          TotalFlatNO = x.TotalFlatNO,
                          UnderpreparingFlat = x.UnderpreparingFlat,
                          BookedFlat = x.BookedFlat,
                          ReadyFlat = x.ReadyFlat,
                          SoledFlat = x.SoledFlat
                      }).ToList();
           // return result;
            //var result = (from i in db.CustomerSelectFlat
            //                  // join cust in db.Customers on i.CustomerId equals cust.Id
            //                  // join f in db.Flats.DefaultIfEmpty() on i.FlatId equals f.id
            //              where i.Flat.ProjectsId == FromPro /*&& i.Flat.ProjectsId >= ToProID*/
            //              select new
            //              {
            //                  CustomerName = i.Customer.ARName,//cust.ARName,
            //                  Flatname = i.Flat.Aname,//f.Aname
            //                  BuildingCode = i.Flat.Building.Code,
            //                  ProjectName = i.Flat.Projects.Aname,
            //                  CurrntlyDate = (DateTime)i.CurrntlyDate,
            //                  CostMony = i.CostMony
            //              }).ToList().Select(x => new CustomerSelectFlatViewModel
            //              {
            //                  CustomerName = x.CustomerName,// cust.ARName,
            //                  Flatname = x.Flatname,//f.Aname//,
            //                  BuildingCode = x.BuildingCode,
            //                  ProjectName = x.ProjectName,
            //                  CurrntlyDate = (DateTime)x.CurrntlyDate,
            //                  CostMony = x.CostMony
            //              }).ToList();
            return View(result.ToList());
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
