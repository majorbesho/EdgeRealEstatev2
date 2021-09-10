using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EdgeRealEstate.Entities
{
    public class Contributor
    {
        public Contributor()
        {
            this.ContPaperReceipts = new HashSet<ContPaperReceipt>();
            this.ContPaperPayments = new HashSet<ContPaperPayment>();
        }
        public int id { get; set; }
        [DisplayName("الاسم عربى")]
        public string ARName { get; set; }
        [DisplayName(" الاسم لاتينى ")]
        public string EName { get; set; }
        [DisplayName("العنوان")]
        public string address { get; set; }
        [DisplayName("الحساب البنكى ")]
        public string BankAccount { get; set; }
        [DisplayName(" التقيم")]
        [Range(0,100)]
        public int Evaluation { get; set; }
        [DisplayName(" المندوب")]
        public int salesManID { get; set; }
        public bool IsDeleted { get; set; }
        [DisplayName(" البريد الاليكترونى")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string Email { get; set; }
        [DisplayName("جوال")]
        public string Tele1 { get; set; }
        [DisplayName("هاتف")]
        public string Tele2 { get; set; }
        [DisplayName("موقع اليكتروني")]
      
        [DataType(DataType.Url)]
        [Url]
        public string WebSite { get; set; }


        public virtual ICollection<ContPaperReceipt> ContPaperReceipts { get; set; }
        public virtual ICollection<ContPaperPayment> ContPaperPayments { get; set; }
    }
}