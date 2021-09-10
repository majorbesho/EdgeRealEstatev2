using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EdgeRealEstate.Entities
{
    public class BillPurchasesMaster
    {
        public BillPurchasesMaster()
        {
            this.BillPurchasesDetails = new HashSet<BillPurchasesDetails>();
        }
        public int id { get; set; }
        public Nullable<int> empID { get; set; }
        [ForeignKey("Customer")]
        public Nullable<int> customersId { get; set; }
        public Nullable<int> salesManId { get; set; }
        public DateTime? addDate { get; set; }
        public string refNo { get; set; }
        public string refType { get; set; }
        public string billType { get; set; }
        public string paidType { get; set; }
        public Nullable<decimal> total { get; set; }
        public Nullable<decimal> disMoney { get; set; }
        public Nullable<double> disPer { get; set; }
        public Nullable<decimal> paid { get; set; }
        public Nullable<decimal> net { get; set; }
        public bool isApproval { get; set; }
        public string hashCol { get; set; }
        public Nullable<bool> isDeleted { get; set; }
        public Nullable<decimal> tax { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual Employee Employee { get; set; }
        public virtual Employee Employee1 { get; set; }

        public virtual ICollection<BillPurchasesDetails> BillPurchasesDetails { get; set; }
    }
}
