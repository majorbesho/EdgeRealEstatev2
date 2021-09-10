using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EdgeRealEstate.Entities
{
    public partial class EmployeeAttach
    {
        public int id { get; set; }
        public byte[] attachFile { get; set; }
        public string AttachPath { get; set; }
        public string FileName { get; set; }
        [ForeignKey("Employee")]
        public int EmpId { get; set; }
        public string hashcol { get; set; }
        public bool isDeleted { get; set; }
        public virtual Employee Employee { get; set; }

    }
}