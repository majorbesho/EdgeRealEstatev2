using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace EdgeRealEstate.Models.ViewModels
{
    public class ItemMoveViewModel
    {
        public int id { get; set; }
        //[ForeignKey("Items")]
        [DisplayName("كود الصنف")]
        public Nullable<int> itmId { get; set; }

        [DisplayName("اسم الصنف")]
        public string arName { get; set; }
        [DisplayName("الباركود")]
        public string barCodeUnv { get; set; }
        [DisplayName("رقم الفاتورة")]
        public Nullable<int> billId { get; set; }

        [DisplayName("الكمية")]
        [DefaultValue(0)]
        public Nullable<int> Qu { get; set; }
        [DisplayName("السعر")]
        [DefaultValue(0)]
        public Nullable<decimal> price { get; set; }
        [DisplayName("الاجمالى")]
        [DefaultValue(0)]
        public Nullable<decimal> rowTotal { get; set; }
        [DisplayName("العميل / المورد")]
        public string CustARName { get; set; }
        [DisplayName("نوع الحركة")]
        public string MoveType { get; set; }
        [DisplayName("تاريخ الفاتورة")]
        public DateTime? addDate { get; set; }

        public int? custId { get; set; }

        [DisplayName("الخصم")]
        public Nullable<decimal> disNo { get; set; }

        [DisplayName("الضرائب")]
        public Nullable<decimal> tax { get; set; }
        [DisplayName("الصافى")]
        public Nullable<decimal> net { get; set; }
        [DisplayName("حد الائتمان وقت")]
        public int timeLimit { get; set; }
        [DisplayName("حد الائتمان فلوس")]
        public decimal moneyLimit { get; set; }
        [DisplayName("اخر حركة ")]
        public int lastMove { get; set; }
        public Nullable<int> salesManId { get; set; }

        [DisplayName("المندوب")]
        public string salesManARName { get; set; }

    }
}