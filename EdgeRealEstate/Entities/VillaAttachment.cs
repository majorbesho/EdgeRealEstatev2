using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EdgeRealEstate.Entities
{
    public class VillaAttachment
    {
        public int ID { get; set; }
        public string Path { get; set; }
        public int VillaId { get; set; }
        [ForeignKey(nameof(VillaId))]
        public Villa Villas { get; set; }
        public ICollection<VillaAttachment> VillaAttachments { get; set; }

    }
}