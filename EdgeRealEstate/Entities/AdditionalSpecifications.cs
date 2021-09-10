using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EdgeRealEstate.Entities
{
    public class AdditionalSpecifications
    {
        public int ID { get; set; }
        [DisplayName("المواصفات")]
        public string Specification { get; set; }
        [DisplayName("التكلفه")]
        public string Cost { get; set; }
        [DisplayName("تاريخ البدايه")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime StartDate { get; set; }
        [DisplayName("تاريخ  النهاية")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime EndDate { get; set; }
        public string ExecutionDuration { get; set; }
        public string Connected { get; set; }
    }
}