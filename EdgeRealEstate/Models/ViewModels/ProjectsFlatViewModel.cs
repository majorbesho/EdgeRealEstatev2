using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EdgeRealEstate.Models.ViewModels
{
    public class ProjectsFlatViewModel
    {
        [Key]
        public int id { get; set; }
        public int ProjectID { get; set; }
        public string ProjectName { get; set; }
        public DateTime BeginDateAcutely { get; set; }
        public DateTime EndDateAcutely { get; set; }
        public int TotalFlatNO { get; set; }
        public int UnderpreparingFlat { get; set; }
        public int BookedFlat { get; set; }
        public int SoledFlat { get; set; }
        public int ReadyFlat { get; set; }
    }
}