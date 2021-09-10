using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EdgeRealEstate.Entities
{
    [Table("District")]
    public class District
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        [DisplayName("الاسم ")]
        public string Aname { get; set; }
        [DisplayName("الاسم اجنبى ")]
        public string Ename { get; set; }
        public ICollection<MainLand> MainLands { get; set; }
    }
}