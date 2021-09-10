using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EdgeRealEstate.Entities
{
    public class EngineerReceiveFromContractor
    {
        public int ID { get; set; }
        public string WorkOrderNumber { get; set; }
        public string ContractorNumber { get; set; }
        public string Specifications { get; set; }
        public string Notes { get; set; }
        public bool Identical { get; set; }
        public string CompletionRate { get; set; }
        public bool Finished { get; set; }
        public string ReceiptNotes { get; set; }
        public string Recommendations { get; set; }
    }
}