using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EdgeRealEstate.Entities
{
    public class MainLandAttachment
    {
        public int ID { get; set; }
        public string Path { get; set; }
        public int MainLandId { get; set; }
        [ForeignKey(nameof(MainLandId))]
        public MainLand MainLands { get; set; }
    }
}