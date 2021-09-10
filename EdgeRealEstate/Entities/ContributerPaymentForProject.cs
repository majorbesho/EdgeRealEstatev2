using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace EdgeRealEstate.Entities
{
    public class ContributerPaymentForProject
    {
        public int id { get; set; }
        public int ContributorId { get; set; }
        public int ProjectId { get; set; }
        [DisplayName("المبلغ")]
        public decimal paid { get; set; }
        public decimal Actuallypaid { get; set; }
        public string notes { get; set; }
      
        public int salesManId { get; set; }
        [DisplayName("التاريخ")]
        public DateTime indate { get; set; }
        public int empId { get; set; }
        public string paidMethod { get; set; }
        [DisplayName("المندوب")]
        public string salesManName { get; set; }

        public int payType { get; set; }

    }
}