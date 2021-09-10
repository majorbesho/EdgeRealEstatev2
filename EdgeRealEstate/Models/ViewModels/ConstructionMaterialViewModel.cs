using EdgeRealEstate.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EdgeRealEstate.Models.ViewModels
{
    public class ConstructionMaterialViewModel
    {
        public int ID { get; set; }
        public string MaterialName { get; set; }
        public string MasuringUnit { get; set; }
        public string Length { get; set; }
        public string Width { get; set; }
        public decimal Price { get; set; }
        public string Brand { get; set; }
        public string Type { get; set; }
        public bool effective { get; set; }
        public string ProcessingTime { get; set; }
        public string Specifications { get; set; }
        public string Notes { get; set; }
        public string Image { get; set; }
        ///
        /// Start Relations
        ///
        public int MianItemsId { get; set; }
        public MianItems MianItems { get; set; }

        //public ICollection<MianItemsConstructionMaterial> MianItemsConstructionMaterials { get; set; }

        public ICollection<ConstructionMaterialPriceVariable> ConstructionMaterialPriceVariables { get; set; }

        public ICollection<Attachment> Attachments { get; set; }
    }
}