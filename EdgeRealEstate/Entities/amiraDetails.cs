using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EdgeRealEstate.Entities
{
    public class amiraDetails
    {
        public int id { get; set; }
        //[ForeignKey("Items")]
        //public Nullable<int> itmId { get; set; }
        [ForeignKey("amiraMaster")]
        public Nullable<int> MasterId { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        [DisplayName("الاسم")]
       
        public string name { get; set; }

        public virtual amiraMaster amiraMaster { get; set; }

        //[UIHint("ClientItemsAutoComplete")]
        //public virtual Items Items { get; set; }
    }
}