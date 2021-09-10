using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EdgeRealEstate.Models.ViewModels
{
    public class ItemVMCreate
    {
        public int ID { get; set; }

        [UIHint("MainDroList")]
        [Display(Name = "البند")]
        public int MainID { get; set; }

        [Display(Name = "Main Item")]
        [UIHint("MainItemList")]
        public MainItemViewModel MainItem { get; set; }
        [Display(Name = "التكلفة")]
        public string Cost { get; set; }
        [Display(Name = "المدة")]
        public string Duration { get; set; }
    }
}