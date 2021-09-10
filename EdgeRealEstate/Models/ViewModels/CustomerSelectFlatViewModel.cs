using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EdgeRealEstate.Models.ViewModels
{
    public class CustomerSelectFlatViewModel
    {
        [Key]
        public int id { get; set; }
        public int CustomerId { get; set; }

        //[DisplayName("اسم العميل ")]
        public string CustomerName { get; set; }
        public int FlatId { get; set; }
        //[DisplayName("اسم الشقة ")]
        public string Flatname { get; set; }

        public int BuildingId { get; set; }
        //[DisplayName("كود العمارة")]
        public string BuildingCode { get; set; }

        public int Projectid { get; set; }
  
        //[DisplayName("الاسم بالعربى")]
        public string ProjectName { get; set; }
        
        //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime CurrntlyDate { get; set; }

        //[DisplayName(" المبلغ")]
        public decimal CostMony { get; set; }
  
    }
}