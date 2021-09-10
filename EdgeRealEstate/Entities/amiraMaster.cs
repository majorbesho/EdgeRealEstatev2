using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EdgeRealEstate.Entities
{
    public class amiraMaster
    {
        public amiraMaster()
        {
            this.amiraDetails = new HashSet<amiraDetails>();
        }
        public int id { get; set; }
        [DisplayName("الاسم عربى")]
        public string ARname { get; set; }
        [DisplayName("الاسم اجنبى")]
        public string ENname { get; set; }
        public Nullable<bool> IsDeleted { get; set; }

        public virtual ICollection<amiraDetails> amiraDetails { get; set; }
    }
}