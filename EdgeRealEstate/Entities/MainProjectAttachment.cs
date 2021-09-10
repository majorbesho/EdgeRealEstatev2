using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EdgeRealEstate.Entities
{
    public class MainProjectAttachment
    {
        public int ID { get; set; }
        public string Path { get; set; }
        public int MainProjectId { get; set; }
        [ForeignKey(nameof(MainProjectId))]
        public MainProject MainProject { get; set; }
    }
}