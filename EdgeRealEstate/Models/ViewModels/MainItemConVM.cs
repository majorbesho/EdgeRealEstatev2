using EdgeRealEstate.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EdgeRealEstate.Models.ViewModels
{
    public class MainItemConVM
    {
        public MianItems MainItem { get; set; }
        public ICollection<ConstructionMaterial> ConstructionMaterial { get; set; }
    }
}