using EdgeRealEstate.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EdgeRealEstate.Models.ViewModels
{
    public class MainItmsVM
    {
        [Required]
        public MianItems MianItems { get; set; }
        [Display(Name = "Construction Materials")]
        [Required]
        public List<int> SelectedMatrialsIds { get; set; }


    }
}