using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EdgeRealEstate.Models.ViewModels
{
    public class ProjectStageMaster
    {
        public int ID { get; set; }
        public ProjectVM Project { get; set; }
        public ICollection<StageGridCreate> Stages { get; set; }
    }
}