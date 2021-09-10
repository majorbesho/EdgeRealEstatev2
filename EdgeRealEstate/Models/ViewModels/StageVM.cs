using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EdgeRealEstate.Models.ViewModels
{
    public class StageVM
    {
        public int id { get; set; }
        public string Aname { get; set; }
        public string Ename { get; set; }
        public string code { get; set; }
        public bool IsHaveItem { get; set; }

        [UIHint("_DatePickerTemp")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime BeginDateAcutely { get; set; }

        [UIHint("_DatePickerTemp")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime EndDateAcutely { get; set; }

        public float lastCost { get; set; }
        public string nots { get; set; }

        public Nullable<int> MainItemID { get; set; }
        [Display(Name ="Main Item")]
        [UIHint("MainItemList")]
        public MainItemViewModel Main { get; set; }

    }
}