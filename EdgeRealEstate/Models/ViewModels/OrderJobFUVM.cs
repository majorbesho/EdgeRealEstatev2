using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EdgeRealEstate.Models.ViewModels
{
    public class OrderJobFUVM
    {
        public int Id { get; set; }
        [Display(Name ="الكود")]
        public string Code { get; set; }
        [Display(Name = "المقاول")]
        public string Contractor { get; set; }
        public int? ContractorId { get; set; }
        [Display(Name = "المشروع الرئيسى")]
        public string MainProject { get; set; }
        public int? MainProjectId { get; set; }
        [Display(Name = "المهندس المستلم")]
        public string ReceiptEngennering { get; set; }
        [Display(Name = "المهندس المشرف")]
        public string SupervisorEngennering { get; set; }
        public int? SupervisorEngenneringId { get; set; }
        [Display(Name = "المشروع الفرعى")]
        public string Project { get; set; }
        public int? ProjectId { get; set; }
        [Display(Name = "ملاحظات خاصة")]
        public string SpecialNote { get; set; }
        [Display(Name = "ملاحظات")]
        public string Note { get; set; }
    }
}