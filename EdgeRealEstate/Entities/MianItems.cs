using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EdgeRealEstate.Entities
{
    [Table("MianItems")]
    public class MianItems
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Aname { get; set; }
        public string Ename { get; set; }
        public int NoOfDaies { get; set; }
        public string Description { get; set; }
        public string AttachedId { get; set; }
        public int DegreeOfExcellenceId { get; set; }
        public bool HaveItem { get; set; }
        public string Nots { get; set; }

        ////Relations//////
        public virtual ICollection<Stage> Stages { get; set; }

        //public ICollection<ConstructionMaterial> ConstructionMaterials { get; set; }
        public ICollection<MianItemsAttachment> MianItemsAttachments { get; set; }
        public ICollection<MaterialDetail> MaterialDetails { get; set; }

        //public virtual ICollection<MianItems> MianItemss { get; set; }


    }
}