using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EdgeRealEstate.Entities
{
    [Table("Engennering")]

    public class Engennering
    {
        public Engennering()
        {
            this.OrderJob = new HashSet<OrderJob>();
            //this.OrderJob1 = new HashSet<OrderJob>();
        }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        [Required]
        [MaxLength(50)]

        public string Aname { get; set; }
        public string Ename { get; set; }
        public string Type { get; set; }
        public string ResEmp { get; set; }
        public string tele1 { get; set; }
        public string Tele2 { get; set; }
        public string Adress { get; set; }
        public int LikeNo { get; set; }
        public string EngCV { get; set; }
        public string limitation { get; set; }
        public string limitationTime { get; set; }
        public string CrruntyCompletionRate { get; set; }
        public string imgLogo { get; set; }
        public string BankAccount1Name { get; set; }
        public string BankAccount1 { get; set; }
        public string BankAccountName2 { get; set; }
        public string BankAccount2 { get; set; }
        public string BankAccountName3 { get; set; }
        public string BankAccount3 { get; set; }
        public string nots { get; set; }

        ////////Startt Relations/////////////

        public ICollection<EngSupPro> EngSupPro { get; set; }
        public ICollection<OrderJob> OrderJob { get; set; }
        //public ICollection<OrderJob> OrderJob1 { get; set; }

    }
}