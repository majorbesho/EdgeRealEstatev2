using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EdgeRealEstate.Models.ViewModels
{
    public class StageGridCreate
    {
        public int ID { get; set; }
        [Required]
        //[UIHint("_DatePickerTemp")]
        //[DataType(DataType.Date)]
        //[DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name ="تاريخ البدأ")]
        public string StartDate { get; set; }
        [Required]
        //[UIHint("DatePickerTemp")]
        //[DataType(DataType.Date)]
        //[DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "تاريخ الانتهاء")]
        public string EndDate { get; set; }
        [Display(Name = "المدة")]
        public string Duration { get; set; }

        [UIHint("StageDroList")]
        [Display(Name ="المرحلة")]
        public int StageID { get; set; }

        //[UIHint("StageList")]

        //public StageListVM Stage { get; set; }

        //[UIHint("StageCombo")]
        //public virtual StageVM Stage { get; set; }
    }
}