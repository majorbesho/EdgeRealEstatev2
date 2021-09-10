using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace EdgeRealEstate.Models.ViewModels
{
    public class OrderJobDetailsVMReport
    {
        public int Id { get; set; }
        //[ForeignKey("OrderJob")]
        public int OrderJobId { get; set; }
        public int StageId { get; set; }
        public int MianItemsId { get; set; }
        [DisplayName("التكلفة التقديرية")]

        public decimal Cost { get; set; }

        public DateTime BeginDateAcutely { get; set; }


        public DateTime BeginDateExpected { get; set; }

        public DateTime EndDateAcutely { get; set; }

        public DateTime EndDateExpected { get; set; }

        [DisplayName("زمن التنفيذ")]
        public int ExecutionTime { get; set; }
        [DisplayName("ملاحظات")]
        public string Note { get; set; }
        [DisplayName("نسبة الانجاز")]
        public int CompletionRate { get; set; }




        public string contractorName { get; set; }
        public string SupervisorEngenneringName { get; set; }
        public string MainProjectName { get; set; }
        public string StageName { get; set; }
        public string MianItemsName { get; set; }
    }
}