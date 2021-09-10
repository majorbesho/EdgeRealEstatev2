using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EdgeRealEstate.Entities
{
    public class AssignedSupplier
    {
        public int ID { get; set; }

        public int OrderNumber { get; set; }
        //رقم المقاول
        public string ContractorNumber { get; set; }
        public string SupervisingEngineerNumber { get; set; }
        public string SupervisingEngineer { get; set; }
        public string ReceivingEngineer { get; set; }
        public string SpecialSpecifications { get; set; }
        public string Notes { get; set; }
        public bool CreateContract{ get; set; }
        public string MainProjectNumber { get; set; }
        public string SubprojectNumber{ get; set; }

    }
}