using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EdgeRealEstate.Entities
{
    [Table("MianLandContract")]

    public class MianLandContract
    {

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Aname { get; set; }
        public string Ename { get; set; }
        public int SupplireId { get; set; }
        public int MainLandId { get; set; }
        public int MyProperty { get; set; }
    }
}