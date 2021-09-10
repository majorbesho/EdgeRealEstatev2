using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EdgeRealEstate.Entities
{
    public class StoreTransaction
    {
        public int id { get; set; }
        [ForeignKey("LKStore")]
        public Nullable<int> storied { get; set; }
        public Nullable<int> IndexId { get; set; }
        [ForeignKey("ConstructionMaterial")]
        public Nullable<int> ConstructionMaterialId { get; set; }
        public Nullable<bool> ISadd { get; set; }
        public Nullable<int> Quan { get; set; }
        public Nullable<int> unitId { get; set; }
        public string Refid { get; set; }
        public string refType { get; set; }
        public string hashCol { get; set; }
        public Nullable<bool> isDeleted { get; set; }
        public string notes { get; set; }
        public int? billId { get; set; }
        public DateTime? indate { get; set; }
        public virtual ConstructionMaterial ConstructionMaterial { get; set; }
        public virtual LKStore LKStore { get; set; }
    }
}