using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EdgeRealEstate.Entities
{
    public class EngSupPro
    {
        public int Id { get; set; }
        [ForeignKey("Projects")]
        public int ProjectsId { get; set; }   // FK property

        [ForeignKey("Supplieres")]
        public int SupplieresId { get; set; }  // FK property

        [ForeignKey("Engennering")]
        public int EngenneringId { get; set; }   // FK property

        public Projects Projects { get; set; }
        public Supplieres Supplieres { get; set; }
        public Engennering Engennering { get; set; }
    }
}