using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EdgeRealEstate.Entities
{
    public class PricesOffersDetails
    {
        public int id { get; set; }
        [ForeignKey("ConstructionMaterial")]
        [DisplayName("كود الصنف")]
        public Nullable<int> ConstructionMaterialId { get; set; }
        [ForeignKey("PricesOffersMaster")]
        public Nullable<int> billId { get; set; }
        [DisplayName("الكمية")]
        public Nullable<int> Qu { get; set; }
        [DisplayName("السعر")]
        public Nullable<decimal> price { get; set; }
        [DisplayName("الاجمالى")]
        public Nullable<decimal> rowTotal { get; set; }
        [DisplayName("نسبة الخصم")]
        public Nullable<double> disPer { get; set; }
        [DisplayName("الخصم")]
        public Nullable<decimal> disNo { get; set; }
        public string hashCol { get; set; }
        public Nullable<bool> isDeleted { get; set; }
        [DisplayName("الضرائب")]
        public Nullable<decimal> tax { get; set; }
        [ForeignKey("LKStore")]
        public Nullable<int> STOREID { get; set; }

        public virtual PricesOffersMaster PricesOffersMaster { get; set; }
        public virtual ConstructionMaterial ConstructionMaterial { get; set; }
        public virtual LKStore LKStore { get; set; }
    }
}