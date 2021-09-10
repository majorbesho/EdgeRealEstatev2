using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EdgeRealEstate.Entities
{
    [Table("units")]
    public class units
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        [DisplayName("الاسم بالعربى")]
        public string Aname { get; set; }
        [DisplayName("الاسم باللاتينى")]
        public string Ename { get; set; }
        [DisplayName("RelatedWithUniteNo")]
        public int RelatedWithUniteNo { get; set; }
        [DisplayName("RateOfTrans")]
        public float RateOfTrans { get; set; }
        [DisplayName("IsTransation")]
        public bool IsTransation { get; set; }
        [DisplayName("ItemsId")]

        public int ItemsId { get; set; }
        public Items Items { get; set; }

    }
}