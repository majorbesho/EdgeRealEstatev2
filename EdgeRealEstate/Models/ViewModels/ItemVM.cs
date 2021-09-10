using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EdgeRealEstate.Models.ViewModels
{
    public class ItemVM
    {
        public int ID { get; set; }
        [Display(Name = "البند")]
        [UIHint("MainDroList")]
        public int MainID { get; set; }

        [Display(Name = "البند")]
        //[UIHint("MainItemList")]
        public MainItemViewModel MainItem { get; set; }
        [Display(Name = "التكلفة")]
        public string Cost { get; set; }
        [Display(Name = "المدة")]
        public string Duration { get; set; }
    }
}