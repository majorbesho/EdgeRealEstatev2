using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace EdgeRealEstate.Models.ViewModels
{
    public class contractorPaperPaymentViewModel
    {
        public int id { get; set; }
        public int contractorId { get; set; }
        [DisplayName("المبلغ")]
        public decimal paid { get; set; }
        public string notes { get; set; }
        public string refType { get; set; }
        public int refID { get; set; }
        public int salesManId { get; set; }
        [DisplayName("التاريخ")]
        public DateTime indate { get; set; }
        public int empId { get; set; }
        public string hashCol { get; set; }
        public bool isDeleted { get; set; }
        public int billId { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime toDate { get; set; }
        [DisplayName("المقاول")]
        public string contractorName { get; set; }
        public string paidMethod { get; set; }
        [DisplayName("المندوب")]
        public string salesManName { get; set; }
    }
}