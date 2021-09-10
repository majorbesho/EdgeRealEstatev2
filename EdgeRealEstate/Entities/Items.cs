using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EdgeRealEstate.Entities
{
    [Table("Items")]
    public class Items
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
        public int UnitsId { get; set; }
        public float  Long { get; set; }
        public float width { get; set; }
        public float price { get; set; }

        public string brand { get; set; }

        public string types { get; set; }
        
        public int RelatedWithSuppelyId { get; set; }

        //////////Start Relations///////////////

        public int StageId { get; set; }
        public Stage Stage { get; set; }

        public ICollection<units> units { get; set; }

    }
}