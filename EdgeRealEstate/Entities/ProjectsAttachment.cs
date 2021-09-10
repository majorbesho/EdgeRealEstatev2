using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EdgeRealEstate.Entities
{
    public class ProjectsAttachment
    {
        public int ID { get; set; }
        public string Path { get; set; }
        public int ProjectsId { get; set; }
        [ForeignKey(nameof(ProjectsId))]
        public Projects Projects { get; set; }
    }
}