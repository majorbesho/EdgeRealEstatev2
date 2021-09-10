using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EdgeRealEstate.Entities
{[Table("DegreeOfExcellence")]
    public class DegreeOfExcellence
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        //new ttest
        [Display(Name = " الاسم عربى")]
        public string   Aname { get; set; }
        [Display(Name = " الاسم لاتيني")]
        public string   Ename { get; set; }    

    }
}