using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EdgeRealEstate.Entities
{
    public class FlatAttachment
    {
        public int ID { get; set; }
        public string Path { get; set; }
        public int FlatId { get; set; }
        [ForeignKey(nameof(FlatId))]
        public Flat Flat { get; set; }
    }
}