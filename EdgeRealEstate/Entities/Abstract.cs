using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EdgeRealEstate.Entities
{
    public class Abstract
    {


        public int ID { get; set; }
        public string WorkOrderNumber { get; set; }
        public string Cost { get; set; }
        [DisplayName(" العقد")]
        public string Contractor { get; set; }
        //من مشروع – بند –مرحله
        [DisplayName("اسم المشروع")]
        public string Project { get; set; }
        [DisplayName("اسم المشروع")]
       
        public DateTime Date { get; set; }
        public bool EngineerApproval { get; set; }
        public string CompletionRate { get; set; }
    }
}