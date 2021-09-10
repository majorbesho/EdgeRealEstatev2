using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EdgeRealEstate.Entities
{
    public class ConstructionMaterialPriceVariable
    {
        public int ID { get; set; }
        public string lastPrice { get; set; }
        public string lastSupplier { get; set; }
        public string ExecutiveEngineerPrice { get; set; }
        public string Marketprice { get; set; }

        ///
        /// Start Relations
        ///

        public int ConstructionMaterialId { get; set; }
        //public ConstructionMaterial ConstructionMaterial { get; set; }
    }
}