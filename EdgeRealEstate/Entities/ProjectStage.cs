using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EdgeRealEstate.Entities
{
    public class ProjectStage
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int ID { get; set; }
        //[DataType(DataType.Date)]
        //[DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [DisplayName("تاريخ البدأ")]
        public string StartDate { get; set; }
        [DisplayName("تاريخ الانتهاء")]
        //[DataType(DataType.Date)]
        //[DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public string EndDate { get; set; }
        [DisplayName("الفترة")]
        public string Duration { get; set; }
        [DisplayName("المشروع")]
        public int ProjectID { get; set; }
        [DisplayName("المرحلة")]
        public int StageID { get; set; }

        [ForeignKey(nameof(ProjectID))]

        public Projects Project { get; set; }

        [ForeignKey(nameof(StageID))]

        public Stage Stage { get; set; }
    }
}