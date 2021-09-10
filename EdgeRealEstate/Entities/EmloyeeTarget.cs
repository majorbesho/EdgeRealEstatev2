using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EdgeRealEstate.Entities
{
    public partial class EmloyeeTarget
    {
        public int id { get; set; }

        [ForeignKey("Employee")]
        public int salesManId { get; set; }
        public string typeOfCommission { get; set; }
        public decimal fromMoney { get; set; }
        public decimal toMoney { get; set; }
        public double precent { get; set; }
        public string hashCol { get; set; }
        public bool isDeleted { get; set; }
        public int ItmId { get; set; }
        public string typeOfOperation { get; set; }

        public virtual Employee Employee { get; set; }
    }
}