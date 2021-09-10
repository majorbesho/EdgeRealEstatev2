using EdgeRealEstate.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EdgeRealEstate.Models.ViewModels
{
    public class StageMainItemMasterVM
    {
        public int ID { get; set; }
        public Stage_VM Stage { get; set; }
        public ICollection<ItemVMCreate> Items { get; set; }
    }
}