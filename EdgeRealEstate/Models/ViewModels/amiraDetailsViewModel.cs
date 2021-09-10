using EdgeRealEstate.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EdgeRealEstate.Models.ViewModels
{
    public class amiraDetailsViewModel
    {
        public int id { get; set; }
        //[ForeignKey("Items")]
        //[DisplayName("كود الصنف")]
        //public Nullable<int> itmId { get; set; }
        [ForeignKey("amiraMaster")]
        public Nullable<int> MasterId { get; set; }
       
        public Nullable<bool> IsDeleted { get; set; }
        public string name { get; set; }

        //[UIHint("ClientItemsAutoComplete")]
        //[DisplayName("الصنف")]
        //public ItemsViewModel Items
        //{
        //    get;
        //    set;
        //}

        public virtual amiraMaster amiraMaster { get; set; }

    }
}