using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EdgeRealEstate.Entities
{
    public partial class CustPaperReceipt
    {
        public int id { get; set; }

        [ForeignKey("Customer")]
        public int customerId { get; set; }
        public decimal paid { get; set; }
        public string notes { get; set; }
        public string refType { get; set; }
        public int refID { get; set; }
        public string paidMethod { get; set; }

        [ForeignKey("Employee1")]
        public int salesManId { get; set; }
        public DateTime indate { get; set; }

        [ForeignKey("Employee")]
        public int empId { get; set; }
        public string hashCol { get; set; }
        public bool isDeleted { get; set; }
        public int billId { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual Employee Employee { get; set; }
        public virtual Employee Employee1 { get; set; }
    }
}