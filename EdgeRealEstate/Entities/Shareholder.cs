using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EdgeRealEstate.Entities
{
    public class Shareholder
    {
        public int ID { get; set; }
        public string ShareholderName{ get; set; }
        public string Address { get; set; }
        public string BankAccount { get; set; }
        public string Phone { get; set; }
    }
}