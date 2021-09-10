using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
//using EdgeRealEstate.Migrations;

namespace EdgeRealEstate.Entities
{
    /// <summary>
    ///  جدول العمارات
    /// لربط العمارات بالمشاريع وتخليق اكواد  للشقق
    /// حيث ان الشقق تنتمنى للعمارات وتنمتمى ايضا 
    /// </summary>
    [Table("Buildings")]
    public class Buildings
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [DisplayName("كود العمارة")]
        public string Code { get; set; }
        [DisplayName("رقم")]
        public string Level { get; set; }
        [DisplayName("ملاحظات ")]
        public String Nots { get; set; }
        public ICollection<Flat> Flats { get; set; }

        
       

    }
}