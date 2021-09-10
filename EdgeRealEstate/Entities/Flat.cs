using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using EdgeRealEstate.Models;

namespace EdgeRealEstate.Entities
{[Table("Flat")]
    public class Flat
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
       // [ForeignKey("DegreeOfExcellence")]
        public int id { get; set; }
        [Required]
        [MaxLength(50)]
        [DisplayName("الاسم عربى ")]
        public string Aname { get; set; }
        [DisplayName("الاسم لاتيني ")]
        public string Ename { get; set; }
        [DisplayName("الكود")]
        public string code { get; set; }
        public int FlotSize { get; set; }
        public DateTime BeginDateAcutely { get; set; }
        public DateTime BeginDateExpected { get; set; }
        public DateTime EndDateAcutely { get; set; }
        public DateTime EndDateExpected { get; set; }
        public string ImgUrl { get; set; }
        public string AttachedId { get; set; }
        public int Level { get; set; }
        public int BedroomNo { get; set; }
        public int resptionNo { get; set; }
        public string Nots { get; set; }
        public string  NewType { get; set; }

        //////////Start Relations///////////////

        public int FlatTypeId { get; set; }


        public FlatType FlatTypes { get; set; }

        [DisplayName("كود العمارة ")]

        public int BuildingId { get; set; }

        [ForeignKey(nameof(BuildingId))]
        public Buildings Building { get; set; }

        public int ProjectsId { get; set; }

        [ForeignKey(nameof(ProjectsId))]
        public Projects Projects { get; set; }
        public virtual DegreeOfExcellence DegreeOfExcellence { get; set; }
        public int DegreeOfExcellenceId { get; set; }
        public virtual ICollection<EmployeeSales> EmployeeSales { get; set; }
        public virtual ICollection<FlatAttachment> FlatAttachments { get; set; }

    }
}