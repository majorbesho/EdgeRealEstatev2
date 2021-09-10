using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EdgeRealEstate.Models.ViewModels
{
    public class MainItemMaterialMaster
    {
        public int ID { get; set; }
        public MItemVM MainItem { get; set; }
        public ICollection<MaterialsDetailsVM> MaterialsDetails { get; set; }
    }
}