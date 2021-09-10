using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EdgeRealEstate.Entities
{
    public class MianItemsAttachment
    {
        public int ID { get; set; }
        public string Path { get; set; }
        public int MianItemsId { get; set; }
        [ForeignKey(nameof(MianItemsId))]
        public MianItems MianItems { get; set; }
    }
}