using EdgeRealEstate.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EdgeRealEstate.Models.ViewModels
{
    public class OrderJobDetailsGridVM
    {
        public int Id { get; set; }

        public int? OrderJobId { get; set; }
        public int? StageId { get; set; }
        public int? MianItemsId { get; set; }
        [DisplayName("التكلفة التقديرية")]

        public decimal? Cost { get; set; }


        [DisplayName("تاريخ البداية الفعلى")]
        public string BeginDateAcutely { get; set; }

        [DisplayName("تاريخ البداية المتوقع")]
        public string BeginDateExpected { get; set; }

        [DisplayName("تاريخ النهاية الفعلى")]
        public string EndDateAcutely { get; set; }


        [DisplayName("تاريخ النهاية المتوقع")]
        public string EndDateExpected { get; set; }

        [DisplayName("زمن التنفيذ")]
        public int? ExecutionTime { get; set; }
        [DisplayName("ملاحظات")]
        public string Note { get; set; }
        [DisplayName("نسبة الانجاز")]
        public int? CompletionRate { get; set; }

        public int MainIttemID { get; set; }
        [DisplayName("البند")]
        public string MainIttemName { get; set; }
        public int StageID { get; set; }
        [DisplayName("المرحلة")]
        public string StageName { get; set; }

        public virtual OrderJob OrderJob { get; set; }


    }
}