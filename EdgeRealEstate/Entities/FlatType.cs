using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EdgeRealEstate.Entities
{
    [Table("FlatType")]
    public class FlatType
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        
        [MaxLength(50)]
        [DisplayName("نوع الشقه عربي")]
        public string Aname { get; set; }
        [DisplayName("نوع الشقه لاتينى")]
        public string Ename { get; set; }
        [DisplayName("الوصف ")]
        public string Description { get; set; }
        public bool IsDeleted { get; set; }
      public ICollection<Flat> Flats { get; set; }



    }
}