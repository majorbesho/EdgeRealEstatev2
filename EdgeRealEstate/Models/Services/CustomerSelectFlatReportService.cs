using EdgeRealEstate.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EdgeRealEstate.Models.Services
{
    public class CustomerSelectFlatReportService
    {
        private static bool UpdateDatabase = true;
        private ApplicationDbContext db;
        public CustomerSelectFlatReportService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public IList<CustomerSelectFlatViewModel> GetAll(int? FromProID, int? ToProID)
        {
           // DateTime vFromDate = new DateTime(DateTime.Now.Year, 1, 1);
            var result = HttpContext.Current.Session["CustomerFlat"] as IList<CustomerSelectFlatViewModel>;
            //if (result == null || UpdateDatabase)
            //{
                //if (FromProID == null && ToProID == null)
                //{



                result = (from i in db.CustomerSelectFlat
                          where i.Flat.ProjectsId <= FromProID && i.Flat.ProjectsId >= ToProID
                          select new CustomerSelectFlatViewModel
                          {
                              CustomerName = i.Customer.ARName,
                              Flatname = i.Flat.Aname,
                              BuildingCode = i.Flat.Building.Code,
                              ProjectName = i.Flat.Projects.Aname,
                              CurrntlyDate =(DateTime)i.CurrntlyDate,
                              CostMony = i.CostMony
                          }).ToList();
               // return result;



                //}
                //else if (FromDate != null && toDate != null)
                //{
                //    result = (from i in db.ContPaperPayments
                //              where i.isDeleted == false
                //              //&& i.customerId >= FromCust && i.customerId <= toCust
                //              && i.indate >= FromDate && i.indate <= toDate
                //              //&& i.salesManId >= FromSalesMan && i.salesManId <= toSalesMan
                //              select new ContPaperPaymentViewModel
                //              {
                //                  //indate = (DateTime)i.indate,
                //                  //ContributorId = (int)i.ContributorId,
                //                  ////salesManId = (int)i.salesManId,
                //                  //paid = (decimal)i.paid,
                //                  //ContributorName = i.Contributor.ARName,
                //                  //salesManName = i.Employee1.ARName,
                //                  //FromDate = (DateTime)FromDate,
                //                  //toDate = (DateTime)toDate
                //              }).ToList();
                //    return result;
                //}
            //}
            return result;
        }


        public IEnumerable<CustomerSelectFlatViewModel> Read(int? FromProID, int? ToProID)
        {
            return GetAll(FromProID, ToProID);
        }
        public void Dispose()
        {
            db.Dispose();
        }



    }
}