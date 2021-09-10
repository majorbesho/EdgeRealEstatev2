using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EdgeRealEstate.Entities
{[Table("Stage")]
    public class Stage
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        [Required]
        [MaxLength(50)]
        [DisplayName("الاسم بالعربى")]
        public string Aname { get; set; }
        [DisplayName("الاسم باللاتينى")]
        public string Ename { get; set; }
        [DisplayName("الكود")]
        public string code { get; set; }
        [DisplayName("تمتلك بنود")]
        public bool IsHaveItem { get; set; }
        [DisplayName("تاريخ البدأ الفعلى")]

        public string BeginDateAcutely { get; set; }
        [DisplayName("تاريخ الانتهاء الفعلى")]

        public string EndDateAcutely { get; set; }
        [DisplayName("اخر سعر")]

        public float lastCost { get; set; }
        [DisplayName("الملاحظات")]
        public string nots { get; set; }

        //////////Start Relations///////////////
        //[ForeignKey("MianItems")]
        //public int MainItemId { get; set; }
        ////public virtual MianItems MianItems { get; set; }
        public ICollection<ProjectStage> ProjectStages { get; set; }

    }
}