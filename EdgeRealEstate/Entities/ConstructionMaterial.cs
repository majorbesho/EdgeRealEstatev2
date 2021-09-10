using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EdgeRealEstate.Entities
{
    public class ConstructionMaterial
    {
        public int ID { get; set; }
        [Display(Name="الاسم")]
        public string MaterialName { get; set; }
        [Display(Name = "وحدة القياس")]
        public string MasuringUnit { get; set; }
        [Display(Name = "الطول")]
        public string Length { get; set; }
        [Display(Name = "العرض")]
        public string Width { get; set; }
        [Display(Name = "السعر")]
        public decimal Price { get; set; }
        [Display(Name = "العلامة التجارية")]
        public string Brand { get; set; }
        [Display(Name = "النوع")]
        public string Type { get; set; }
        [Display(Name = "مؤثر")]
        public bool effective { get; set; }
        [Display(Name = "وقت التنفيذ")]
        public string ProcessingTime { get; set; }
        [Display(Name = "المواصفات")]
        public string Specifications { get; set; }
        [Display(Name = "الملاحظات")]
        public string Notes { get; set; }
        [Display(Name = "الصورة")]
        public string Image { get; set; }
        [Display(Name = "الكمية")]
        public int Quantity { get; set; }
        [Display(Name = "المجموع")]
        public int Total { get; set; }
        [Display(Name = "البند")]
        ///
        /// Start Relations
        ///
        //public int MianItemsId { get; set; }
        //public MianItems MianItems { get; set; }

        //public ICollection<MianItemsConstructionMaterial> MianItemsConstructionMaterials { get; set; }
        public virtual ICollection<MaterialDetail> MaterialDetails { get; set; }
        public ICollection<ConstructionMaterialPriceVariable> ConstructionMaterialPriceVariables { get; set; }

        public ICollection<Attachment> Attachments { get; set; }

    }
}