using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EdgeRealEstate.Entities
{
    public class CashReceiptFromShareholder
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Cost { get; set; }
        public DateTime Date { get; set; }
        //جارى – مخصص لمشروع التوجيه
        public bool Guiding { get; set; }
        public string Statement { get; set; }
    }
}