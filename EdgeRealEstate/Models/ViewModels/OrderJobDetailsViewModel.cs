using EdgeRealEstate.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EdgeRealEstate.Models.ViewModels
{
    public class OrderJobDetailsViewModel
    {
        public int Id { get; set; }
        //[ForeignKey("OrderJob")]
        public int? OrderJobId { get; set; }
        public int? StageId { get; set; }
        public int? MianItemsId { get; set; }
        [DisplayName("التكلفة التقديرية")]

        public decimal? Cost { get; set; }

        [UIHint("BeginDateAcutely")]
        [DisplayName("تاريخ البداية الفعلى")]
        [DisplayFormat(DataFormatString = "{dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? BeginDateAcutely { get; set; }

        [UIHint("BeginDateExpected")]
        [DisplayName("تاريخ البداية المتوقع")]
        [DisplayFormat(DataFormatString = "{dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? BeginDateExpected { get; set; }

        [UIHint("EndDateAc")]
        [DisplayName("تاريخ النهاية الفعلى")]
        [DisplayFormat(DataFormatString = "{dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? EndDateAcutely { get; set; }


        [UIHint("EndDateEx")]
        [DisplayName("تاريخ النهاية المتوقع")]
        [DisplayFormat(DataFormatString = "{dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? EndDateExpected { get; set; }
        [Range(0, 365)]
        [DisplayName("زمن التنفيذ")]
        public int? ExecutionTime { get; set; }
        [DisplayName("ملاحظات")]
        public string Note { get; set; }
        public bool? IsDeleted { get; set; }
        [DisplayName("نسبة الانجاز")]
         [Range(0,100)]
        public int? CompletionRate { get; set; }
        [UIHint("StageCombo")]
        [DisplayName("المرحلة")]
        public virtual StageVM Stage { get; set; }
        [UIHint("MianItemsCombo")]
        [DisplayName("البند")]
        public virtual MainItemViewModel MianItems { get; set; }

        public virtual OrderJob OrderJob { get; set; }

        public int? contractorId { get; set; }
        public string contractorName { get; set; }
        public int? SupervisorEngenneringId { get; set; }
        public string SupervisorEngenneringName { get; set; }
        public int? MainProjectId { get; set; }
        public string MainProjectName { get; set; }
        public string StageName { get; set; }
        public string MianItemsName { get; set; }

    }
}