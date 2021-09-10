using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EdgeRealEstate.Entities
{
    public class Supplier
    {
        public int ID { get; set; }

        public string SupplierName { get; set; }

        //الكيان (شركة – فرد
        public string Company { get; set; }

        //المسئول 
        public string Administrator { get; set; }

        public string Phone { get; set; }
        public string Address { get; set; }
        public string Rate { get; set; }
        public string CV { get; set; }
        public string FinancialInformation { get; set; }
        public string Creditlimit { get; set; }
        public string Completionrate { get; set; }
        public string Attachments { get; set; }
        public string Logo { get; set; }
        public string BankAccount1 { get; set; }
        public string BankAccount2 { get; set; }
        public string BankAccount3 { get; set; }
    }
}