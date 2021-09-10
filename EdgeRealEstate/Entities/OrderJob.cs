using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EdgeRealEstate.Entities
{
    public class OrderJob
    {
        public OrderJob()
        {
            this.OrderJobDetails = new HashSet<OrderJobDetails>();
        }
        public int Id { get; set; }
        public string OrderJobCode { get; set; }
        public int? contractorId { get; set; }
        public int? SupervisorEngenneringId { get; set; }
        public int? receiptEngenneringId { get; set; }
        public int? MainProjectId { get; set; }
        public int? ProjectsId { get; set; }
        public string SpecialNote { get; set; }
        public string Note { get; set; }
        public bool IsCreateContract { get; set; }
        public bool IsDeleted { get; set; }

        public virtual contractor contractor { get; set; }
        public virtual Engennering Engennering { get; set; }
        public virtual Engennering Engennering1 { get; set; }
        public virtual MainProject MainProject { get; set; }
        public virtual Projects Projects { get; set; }

        public virtual ICollection<OrderJobDetails> OrderJobDetails { get; set; }

    }
}