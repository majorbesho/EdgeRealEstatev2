using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EdgeRealEstate.Entities
{
    public class OrderJobDetails
    {
        public int Id { get; set; }
        public int? OrderJobId { get; set; }
        public int? StageId { get; set; }
        //public int? MianItemsId { get; set; }
        public int? MainItemDetailId { get; set; }

        public decimal? Cost { get; set; }
        public DateTime? BeginDateAcutely { get; set; }
        public DateTime? BeginDateExpected { get; set; }
        public DateTime? EndDateAcutely { get; set; }
        public DateTime? EndDateExpected { get; set; }
        public int? ExecutionTime { get; set; }
        public string Note { get; set; }
        public bool? IsDeleted { get; set; }
        public int? CompletionRate { get; set; }
        public virtual OrderJob OrderJob { get; set; }
        public virtual StageMaster Stage { get; set; }
        //public virtual StageMainItemMaster MianItems { get; set; }
        //[ForeignKey(nameof(MainItemDetailId))]
        public virtual MainItemDetail MainItemDetail { get; set; }
    }
}