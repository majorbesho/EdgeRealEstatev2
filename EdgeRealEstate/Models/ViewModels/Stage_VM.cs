using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EdgeRealEstate.Models.ViewModels
{
    public class Stage_VM
    {
        public int ID { get; set; }
        [Display(Name = "المرحلة")]

        public int StageId { get; set; }
        [Display(Name = "اسم المرحلة")]

        public string StageName { get; set; }
        [Display(Name = "التكلفة")]
        public string Cost { get; set; }
        [Display(Name = "المدة")]
        public string Duration { get; set; }
    }
}