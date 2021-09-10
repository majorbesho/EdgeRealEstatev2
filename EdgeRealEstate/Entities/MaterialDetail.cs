using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EdgeRealEstate.Entities
{
    public class MaterialDetail
    {
        public int ID { get; set; }
        public int Quantity { get; set; }
        public Nullable<decimal> price { get; set; }

        //Strat Relations
        public int ConstructionMaterialId { get; set; }
        [ForeignKey(nameof(ConstructionMaterialId))]
        public virtual ConstructionMaterial ConstructionMaterial { get; set; }

        public int MianItemsId { get; set; }
        [ForeignKey(nameof(MianItemsId))]
        public virtual MianItems MianItems { get; set; }

    }
}