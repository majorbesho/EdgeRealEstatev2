using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EdgeRealEstate.Entities
{
    public class PricesOffersMaster
    {
        public PricesOffersMaster()
        {
            this.PricesOffersDetails = new HashSet<PricesOffersDetails>();
        }
        public int id { get; set; }
        [DisplayName("الموظف")]
        public Nullable<int> empID { get; set; }
        [ForeignKey("Customer")] // Wrong Declartion
        [DisplayName("العميل")]
        public Nullable<int> customersId { get; set; }
        [DisplayName("البائع")]
        public Nullable<int> salesManId { get; set; }
        [DisplayName("تاريخ الاضافة")]
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

        public virtual ICollection<PricesOffersDetails> PricesOffersDetails { get; set; }
    }
}