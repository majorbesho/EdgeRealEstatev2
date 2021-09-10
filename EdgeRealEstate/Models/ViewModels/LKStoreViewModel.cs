using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EdgeRealEstate.Models.ViewModels
{
    public class LKStoreViewModel
    {
        public int Id { get; set; }
        public string ARName { get; set; }
        public string ENName { get; set; }
        public bool IsDeleted { get; set; }
        public string HashCol { get; set; }
        public string Description { get; set; }
    }
}