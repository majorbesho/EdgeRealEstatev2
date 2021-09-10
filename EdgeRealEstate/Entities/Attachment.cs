using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EdgeRealEstate.Entities
{
    public class Attachment
    {
        public int ID { get; set; }
        public string Path { get; set; }
        public int ConstructionMaterialId { get; set; }
        public ConstructionMaterial ConstructionMaterial { get; set; }
    }
}