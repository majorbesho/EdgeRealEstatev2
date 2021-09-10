using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EdgeRealEstate.Entities
{
    public class MainItemDetail
    {
        public int Id { get; set; }

        [DisplayName("التكلفة")]
        public string Cost { get; set; }
        [DisplayName("الفترة")]
        public string Duration { get; set; }

        [DisplayName("اسم البند")]
        public int MainItemID { get; set; }

        [ForeignKey(nameof(MainItemID))]
        public MianItems MainItem { get; set; }

        [DisplayName("اسم المرحلة")]
        public int StageMasterID { get; set; }

        [ForeignKey(nameof(StageMasterID))]
        public StageMaster StageMaster { get; set; }
    }
}