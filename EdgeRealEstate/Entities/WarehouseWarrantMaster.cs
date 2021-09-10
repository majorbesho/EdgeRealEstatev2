using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EdgeRealEstate.Entities
{
    public class WarehouseWarrantMaster
    {
        public WarehouseWarrantMaster()
        {
            this.WarehouseWarrantDetails = new HashSet<WarehouseWarrantDetails>();
        }
        public int id { get; set; }
        public Nullable<int> empID { get; set; }
        [ForeignKey("contractor")]
        public Nullable<int> contractorId { get; set; }
        public Nullable<int> EngenneringId { get; set; }
        public DateTime? addDate { get; set; }
        public string refNo { get; set; }
        public string refType { get; set; }
        public Nullable<decimal> total { get; set; }
        public string hashCol { get; set; }
        public Nullable<bool> isDeleted { get; set; }
        public virtual contractor contractor { get; set; }
        public virtual Employee Employee { get; set; }
        public virtual Engennering Engennering { get; set; }

        public virtual ICollection<WarehouseWarrantDetails> WarehouseWarrantDetails { get; set; }
    }
}
