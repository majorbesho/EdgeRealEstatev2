using EdgeRealEstate.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EdgeRealEstate.Models.ViewModels
{
    public class PricesOffersDetailsViewModel
    {
        public int id { get; set; }
        [ForeignKey("ConstructionMaterial")]
        [DisplayName("كود الصنف")]
        //[UIHint("ItemCode")]
        public Nullable<int> ConstructionMaterialId { get; set; }
        [ForeignKey("PricesOffersMaster")]
        public Nullable<int> billId { get; set; }
        [DisplayName("الكمية")]
        [UIHint("QuChange")]
        public Nullable<int> Qu { get; set; }
        [DisplayName("السعر")]
        public Nullable<decimal> price { get; set; }
        [DisplayName("الاجمالى")]
        public Nullable<decimal> rowTotal { get; set; }
        [DisplayName("نسبة الخصم")]
        [UIHint("disPercalc")]
        public Nullable<double> disPer { get; set; }
        [DisplayName("الخصم")]
        [UIHint("disNocalc")]
        public Nullable<decimal> disNo { get; set; }
        public string hashCol { get; set; }
        public Nullable<bool> isDeleted { get; set; }
        [DisplayName("الضرائب")]
        [UIHint("taxcalc")]
        public Nullable<decimal> tax { get; set; }
        [ForeignKey("LKStore")]
        public Nullable<int> STOREID { get; set; }

        [UIHint("ConMaterialAutoComplete")]
        [DisplayName("الصنف")]
        public ConstructionMaterialViewModel ConstructionMaterial { get; set; }
        [UIHint("ClientLKStore")]
        [DisplayName("المخزن")]
        public LKStoreViewModel LKStore
        {
            get;
            set;
        }
        public virtual PricesOffersMaster PricesOffersMaster { get; set; }
    }
}