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
using CrystalDecisions.CrystalReports.Engine;
using System.IO;
using Kendo.Mvc;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;

namespace EdgeRealEstate.Controllers
{
    public class CustomerSelectFlatReportController : Controller
    {
         ApplicationDbContext db = new ApplicationDbContext();
        private CustomerSelectFlatReportService CustomerSelectFlatReportService;
        public ActionResult Index()
        {
            ViewBag.ProjectsFrom = new SelectList(db.Projectes, "Id", "Aname");
            ViewBag.ProjectsTo = new SelectList(db.Projectes, "Id", "Aname");

            return View();
        }
        public ActionResult Index2(int? FromProID)
        {
            ViewBag.ProjectsFrom = new SelectList(db.Projectes, "Id", "Aname");
            ViewBag.ProjectsTo = new SelectList(db.Projectes, "Id", "Aname");
            var FromPro = FromProID;

            if (FromPro == null)
            {
                FromPro = db.Projectes.Select(c => c.id).First();
            }
            //var FromPro = db.Projectes.Select(c => c.id).First();

            var result = (from i in db.CustomerSelectFlat
                              // join cust in db.Customers on i.CustomerId equals cust.Id
                              // join f in db.Flats.DefaultIfEmpty() on i.FlatId equals f.id
                          where i.Flat.ProjectsId == FromPro /*&& i.Flat.ProjectsId >= ToProID*/
                          select new
                          {
                              CustomerName = i.Customer.ARName,//cust.ARName,
                              Flatname = i.Flat.Aname,//f.Aname
                              BuildingCode = i.Flat.Building.Code,
                              ProjectName = i.Flat.Projects.Aname,
                              CurrntlyDate = (DateTime)i.CurrntlyDate,
                              CostMony = i.CostMony
                          }).ToList().Select(x => new CustomerSelectFlatViewModel
                          {
                              CustomerName = x.CustomerName,// cust.ARName,
                              Flatname = x.Flatname,//f.Aname//,
                              BuildingCode = x.BuildingCode,
                              ProjectName = x.ProjectName,
                              CurrntlyDate = (DateTime)x.CurrntlyDate,
                              CostMony = x.CostMony
                          }).ToList();
            return View(result.ToList());
           // return View();
        }
        // GET: CustomerSelectFlatReport
        //public async Task<ActionResult> Index()
        //{
        //    return View(await db.CustomerSelectFlatViewModels.ToListAsync());
        //}

        // GET: CustomerSelectFlatReport/Details/5
        
        // GET: CustomerSelectFlatReport/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CustomerSelectFlatReport/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
      
        // GET: CustomerSelectFlatReport/Edit/5
        
        // POST: CustomerSelectFlatReport/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
       
        // POST: CustomerSelectFlatReport/Delete/5
     
        public ActionResult Download_PDF(int? FromProID, int? ToProID)
        {
            var FromPro = FromProID;
            var ToPro = ToProID;
            if (FromPro == null)
            {
                FromPro = db.Projectes.Select(c => c.id).First();
            }
            if (ToPro == null)
            {
                ToPro =db.Projectes.OrderByDescending(c => c.id).First().id;//.OrderByDescending();//.Last();//.OrderByDescending(c.id).SingleOrDefault();
            }
            ReportDocument rd = new ReportDocument();

            rd.Load(Path.Combine(Server.MapPath("~/Reports"), "CustomerSelectFlatReport.rpt"));
            //List cust >
            var result = (from i in db.CustomerSelectFlat
                         // join cust in db.Customers on i.CustomerId equals cust.Id
                         // join f in db.Flats.DefaultIfEmpty() on i.FlatId equals f.id
                         where i.Flat.ProjectsId <= FromProID && i.Flat.ProjectsId >= ToProID
                          select new {
                              CustomerName = i.Customer.ARName,//cust.ARName,
                              Flatname = i.Flat.Aname,//f.Aname
                              BuildingCode = i.Flat.Building.Code,
                              ProjectName = i.Flat.Projects.Aname,
                              CurrntlyDate = (DateTime)i.CurrntlyDate,
                              CostMony = i.CostMony
                          }).ToList().Select(x=> new CustomerSelectFlatViewModel
                          {
                              CustomerName = x.CustomerName,// cust.ARName,
                              Flatname = x.Flatname,//f.Aname//,
                              BuildingCode = x.BuildingCode,
                              ProjectName = x.ProjectName,
                              CurrntlyDate = (DateTime)x.CurrntlyDate,
                              CostMony = x.CostMony
                          }).ToList();

            rd.SetDataSource(result);

            string FromProval, ToProval;
            FromProval = FromPro.ToString();
            ToProval = ToPro.ToString();
            rd.SetParameterValue("FromPro", FromProval);
            rd.SetParameterValue("ToPro", ToProval);

            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();

            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);

            return File(stream, "application/pdf", "تقرير سندات صرف نقدية عميل.pdf");
        }
        public ActionResult ReadingData( int? FromProID, int? ToProID)
        {
            var FromPro = FromProID;
            var ToPro = ToProID;
            if (FromPro == null)
            {
                FromPro = db.Projectes.Select(c => c.id).First();
            }
            //if (ToPro == null)
            //{
            //    ToPro = db.Projectes.OrderByDescending(c => c.id).First().id;//.OrderByDescending();//.Last();//.OrderByDescending(c.id).SingleOrDefault();
            //}
          //  ReportDocument rd = new ReportDocument();

          //  rd.Load(Path.Combine(Server.MapPath("~/Reports"), "CustomerSelectFlatReport.rpt"));
            //List cust >
            var result = (from i in db.CustomerSelectFlat
                              // join cust in db.Customers on i.CustomerId equals cust.Id
                              // join f in db.Flats.DefaultIfEmpty() on i.FlatId equals f.id
                          where i.Flat.ProjectsId == FromProID /*&& i.Flat.ProjectsId >= ToProID*/
                          select new
                          {
                              CustomerName = i.Customer.ARName,//cust.ARName,
                              Flatname = i.Flat.Aname,//f.Aname
                              BuildingCode = i.Flat.Building.Code,
                              ProjectName = i.Flat.Projects.Aname,
                              CurrntlyDate = (DateTime)i.CurrntlyDate,
                              CostMony = i.CostMony
                          }).ToList().Select(x => new CustomerSelectFlatViewModel
                          {
                              CustomerName = x.CustomerName,// cust.ARName,
                              Flatname = x.Flatname,//f.Aname//,
                              BuildingCode = x.BuildingCode,
                              ProjectName = x.ProjectName,
                              CurrntlyDate = (DateTime)x.CurrntlyDate,
                              CostMony = x.CostMony
                          }).ToList();
            return View (result.ToList());
     
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
