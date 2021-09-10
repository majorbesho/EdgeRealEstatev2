using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EdgeRealEstate.Entities
{
    public class StageMaster
    {
        public int ID { get; set; }
        [DisplayName("التكلفة")]
        public string Cost { get; set; }
        [DisplayName("الفترة")]
        public string Duration { get; set; }
        [DisplayName("اسم المرحلة")]
        public int StageId { get; set; }

        [ForeignKey(nameof(StageId))]
        public Stage Stage { get; set; }

        //public IEnumerable<StageMainItemMaster> StageMainItemMasters { get; set; }

        public ICollection<MainItemDetail> MainItemDetails { get; set; }

    }
}