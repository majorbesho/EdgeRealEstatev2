using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EdgeRealEstate.Entities;

namespace EdgeRealEstate.Entities
{
    public class EmployeeSales
    {
        public int id { get; set; }
        public int ProjectId { get; set; }
      //  [ForeignKey("Employee")]
        public int EmpId { get; set; }
        public int FlatId { get; set; }
        public int VillaId { get; set; }
        public string FlatCode { get; set; }
        public string VillaCode { get; set; }
        [ForeignKey("Customer")]
        public int CustId { get; set; }
        public decimal Paid { get; set; }
        [ForeignKey("PaymentSystems")]
        public int PaymentSystemId { get; set; }
        public bool IsPrint { get; set; }
        [AllowHtml]
        public string Details { get; set; }
        public string Note { get; set; }
        public decimal AdvancePayment { get; set; }
        public bool IsDeleted { get; set; }

        public virtual Customer Customer { get; set; }
       public virtual Employee Employee { get; set; }
        public virtual Flat Flat { get; set; }
        public virtual PaymentSystems PaymentSystems { get; set; }
        public virtual Projects Project { get; set; }
        public virtual Villa Villa { get; set; }
    }
}