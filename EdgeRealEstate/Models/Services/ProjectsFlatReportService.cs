using EdgeRealEstate.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EdgeRealEstate.Models.Services
{
    public class ProjectsFlatReportService
    {
        private ApplicationDbContext db;
        public ProjectsFlatReportService(ApplicationDbContext db)
        {
            this.db = db;
        }
        public IList<ProjectsFlatViewModel> GetAll(int? FromProID, int? ToProID)
        {
            var result = HttpContext.Current.Session["ProjectFlat"] as IList<ProjectsFlatViewModel>;
      


            result = (from i in db.Projectes
                      where i.id <= FromProID && i.id >= ToProID
                      select new ProjectsFlatViewModel
                      {
                          ProjectName = i.Aname,
                          BeginDateAcutely = (DateTime)i.BeginDateAcutely,
                          EndDateAcutely = (DateTime)i.EndDateAcutely, 
                          TotalFlatNO = i.FlatNO,
                          UnderpreparingFlat = i.Flats.Where(c=>c.FlatTypeId==2).Count(),
                          BookedFlat = i.Flats.Where(c => c.FlatTypeId == 4).Count(),
                          ReadyFlat = i.Flats.Where(c => c.FlatTypeId == 3).Count(),
                          SoledFlat = i.Flats.Where(c => c.FlatTypeId == 5).Count()
                      }).ToList();   
            return result;
        }


        public IEnumerable<ProjectsFlatViewModel> Read(int? FromProID, int? ToProID)
        {
            return GetAll(FromProID, ToProID);
        }
        public void Dispose()
        {
            db.Dispose();
        }
    }
}