using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EdgeRealEstate.Models.ViewModels
{
    public class MaterialsDetailsVM
    {
        public int ID { get; set; }
        [Display(Name = "الكمية")]

        public int Quantity { get; set; }

        [UIHint("MaterialDroList")]
        [Display(Name = "مادة الانشاء")]
        public int MaterialID { get; set; }

        [UIHint("MaterialCombo")]
        [DisplayName("مادة البناء")]
        public virtual MaterialVM Material { get; set; }

        [DisplayName("الكمية")]
        [UIHint("QuChange")]
        public Nullable<int> Qu { get; set; }
        [DisplayName("السعر")]
        public Nullable<decimal> price { get; set; }
        [DisplayName("الاجمالى")]
        public Nullable<decimal> rowTotal { get; set; }
    }
}