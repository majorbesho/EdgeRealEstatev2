using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EdgeRealEstate.Entities
{
    [Table("City")]
    public class City
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        [DisplayName("الاسم ")]
        public string Aname { get; set; }
        [DisplayName("الاسم الاجنبى ")]
        public string Ename { get; set; }
        [DisplayName("ملاحظات ")]
        public  string Notss { get; set; }     
        

        public ICollection<MainLand> MainLands { get; set; }
        //public ICollection<MainLand> MainLands { get; set; }
        //public ICollection<MainLand> MainLands { get; set; }

    }
}