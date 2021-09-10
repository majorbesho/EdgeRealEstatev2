using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EdgeRealEstate.Entities
{
    [Table("ProjectStatu")]
    public class ProjectStatu
    { 
        
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        [DisplayName("الاسم بالعربى")]
        public string Aname { get; set; }
        [DisplayName("الاسم باللاتينى")]
        public string Ename { get; set; }

        
    }
}