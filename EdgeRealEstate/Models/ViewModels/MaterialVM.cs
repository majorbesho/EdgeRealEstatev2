using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EdgeRealEstate.Models.ViewModels
{
    public class MaterialVM
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
        [UIHint("FileUpload")]
        [Required]
        public string Image { get; set; }
        public int Quantity { get; set; }
        public int Total { get; set; }
    }
}