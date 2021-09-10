using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using EdgeRealEstate.Entities;

namespace EdgeRealEstate.Entities
{
    public class contractor
    {
        public contractor()
        {
            this.contractorPaperReceipts = new HashSet<contractorPaperReceipt>();
            this.contractorPaperPayments = new HashSet<contractorPaperPayment>();
            this.OrderJob = new HashSet<OrderJob>();
        }
        public int Id { get; set; }
        [Required(ErrorMessage = "يجب ادخال اسم")]
        public string ARName { get; set; }

        public string ENName { get; set; }
        [Required(ErrorMessage = " يجب اختيار النوع")]
        public bool Type { get; set; }

        public bool isActive { get; set; }

        public int salesManID { get; set; }
        [ForeignKey("LKAccount")]
        [Required(ErrorMessage = " يجب اختيار حساب")]
        public int AccountId { get; set; }
        public string managerName { get; set; }
        [Required(ErrorMessage = " يجب اختيار دولة")]
        public int LKCountryId { get; set; }
        [Required(ErrorMessage = " يجب اختيار مدينة")]
        public int LKCityId { get; set; }
        public string Address { get; set; }
        public string tele1 { get; set; }
        public string tele2 { get; set; }
        public string Fax { get; set; }
        public string Site { get; set; }
        public string Email { get; set; }
        public string Mailbox { get; set; }
        public bool isCredit { get; set; }
        public int timeLimit { get; set; }
        public decimal moneyLimit { get; set; }
        public double discountPer { get; set; }
        public bool autoDiscount { get; set; }
        public decimal intialValue { get; set; }
        public decimal? currentValue { get; set; }
        public string hashCol { get; set; }
        public bool IsDeleted { get; set; }
        public string Notes { get; set; }
        //[Required(ErrorMessage = " يجب اختيار نظام تسعير")]
        public int pricePlanId { get; set; }
        public virtual City LKCity { get; set; }

        public virtual LKCountry LKCountry { get; set; }
        public virtual LKAccount LKAccount { get; set; }
        public virtual ICollection<contractorPaperReceipt> contractorPaperReceipts { get; set; }
        public virtual ICollection<contractorPaperPayment> contractorPaperPayments { get; set; }
        public virtual ICollection<OrderJob> OrderJob { get; set; }
        
    }
}