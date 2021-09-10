using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EdgeRealEstate.Entities
{
    [Table("Supplieres")]

    public class Supplieres
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [Required]
        [MaxLength(50)]

        [DisplayName("الاسم بالعربى")]
        public string Aname { get; set; }
        [DisplayName("الاسم باللاتينى")]
        public string Ename { get; set; }
        [DisplayName("النوع")]
        public string Type { get; set; }
        [DisplayName("RESEMP")]
        public string ResEmp { get; set; }
        [DisplayName("رقم الهاتف 1")]
        public string tele1 { get; set; }
        [DisplayName("رقم الهاتف 2")]
        public string Tele2 { get; set; }
        [DisplayName("العنوان")]
        public string Adress { get; set; }
        [DisplayName("LikeNo")]
        public int LikeNo { get; set; }
        [DisplayName("CompanyCV")]
        public string CompanyCV { get; set; }
        [DisplayName("limitation")]
        public string limitation { get; set; }
        [DisplayName("limitationTime")]
        public string limitationTime { get; set; }
        [DisplayName("CrruntyCompletionRate")]
        public string CrruntyCompletionRate { get; set; }
        [DisplayName("اللوجو")]
        public string imgLogo { get; set; }
        [DisplayName("اسم الحساب البنكى 1")]
        public string BankAccount1Name { get; set; }
        [DisplayName("رقم الحساب البنكى 1")]
        public string BankAccount1 { get; set; }
        [DisplayName("اسم الحساب البنكى 2")]
        public string BankAccountName2 { get; set; }
        [DisplayName("رقم الحساب البنكى 2")]
        public string BankAccount2 { get; set; }
        [DisplayName("اسم الحساب البنكى 3")]
        public string BankAccountName3 { get; set; }
        [DisplayName("رقم الحساب البنكى 3")]
        public string BankAccount3 { get; set; }
        [DisplayName("ملاحظات")]
        public string nots { get; set; }

        //////////Start Relations///////////

        public ICollection<EngSupPro> EngSupPro { get; set; }


    }
}