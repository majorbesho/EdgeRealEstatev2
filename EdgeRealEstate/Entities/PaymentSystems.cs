using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EdgeRealEstate.Entities
{
    public class PaymentSystems
    {
        public PaymentSystems()
        {
            this.EmployeeSales = new HashSet<EmployeeSales>();
        }

        public int id { get; set; }
        public string ARName { get; set; }
        public string ENName { get; set; }
        public bool IsDeleted { get; set; }
        public virtual ICollection<EmployeeSales> EmployeeSales { get; set; }
    }
}