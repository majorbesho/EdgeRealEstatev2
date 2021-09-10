using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace EdgeRealEstate.Models.ViewModels
{
    public class CustPaperPaymentViewModel
    {
        public int id { get; set; }
        public int customerId { get; set; }
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
        [DisplayName(" رقم السند")]
        public int billId { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime toDate { get; set; }
        [DisplayName("العميل / المورد")]
        public string custName { get; set; }
        [DisplayName("نوع السند")]
        public string paidMethod { get; set; }
        [DisplayName("المندوب")]
        public string salesManName { get; set; }
    }
}