using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EdgeRealEstate.Entities
{
    public class CustomerSelectFlat
    {
        public int id { get; set; }
        [DisplayName("اسم العميل ")]
        public int CustomerId { get; set; }

        [DisplayName("اسم الشقة ")]
        public int FlatId { get; set; }
        [DisplayName("التاريخ")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? CurrntlyDate { get; set; }
        [DisplayName(" سند القبض ")]

        public int CustomerPaiedId { get; set; }
        [DisplayName(" المبلغ")]
        public decimal CostMony { get; set; }
        [DisplayName("الضريبه")]
        public decimal Vet { get; set; }
        public bool? IsSolded { get; set; }

        //************Relation******
        public Flat Flat { get; set; }
        public Customer Customer { get; set; }
        public CustPaperPayment CustPaperPayment  { get; set; }

    }
}