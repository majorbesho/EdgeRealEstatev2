using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EdgeRealEstate.Entities
{[Table("Villa")]
    public class Villa
    {
        public Villa()
        {
            this.EmployeeSales = new HashSet<EmployeeSales>();
        }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        [Required]
        [MaxLength(50)]
        [DisplayName("الاسم بالعربى")]
        public string Aname { get; set; }
        [DisplayName("الاسم باللاتينى")]
        public string Ename { get; set; }
        [DisplayName("الكود")]
        public string code { get; set; }
        [DisplayName("FlotSize")]
        public int FlotSize { get; set; }
        [DisplayName("تاريخ البدأ الفعلى")]
        public DateTime BeginDateAcutely { get; set; }
        [DisplayName("تاريخ البدأ المتوقع")]
        public DateTime BeginDateExpected { get; set; }
        [DisplayName("تاريخ الانتهاء الفعلى")]
        public DateTime EndDateAcutely { get; set; }
        [DisplayName("تاريخ الانتهاء المتوقع")]
        public DateTime EndDateExpected { get; set; }
        [DisplayName("الصورة")]
        public string ImgUrl { get; set; }
        [DisplayName("المرفقات")]
        public string AttachedId { get; set; }
        //[DisplayName("الارض الرئيسية")]
        //public int MainlandId { get; set; }
        //[DisplayName("المشروع الرئيسى")]
        //public int MainProjectId { get; set; }

        //  public int ProjectId { get; set; }
        [DisplayName("عدد غرب النوم")]
        public int BedroomNo { get; set; }
        [DisplayName("عدد الريسيبشنات")]
        public int resptionNo { get; set; }
        [DisplayName("الملاحظات")]
        public string Nots { get; set; }

        //////////Start Relations///////////////
        [DisplayName("المشروع")]
        public int ProjectId { get; set; }

        [ForeignKey(nameof(ProjectId))]
        public Projects Project { get; set; }
        public virtual ICollection<EmployeeSales> EmployeeSales { get; set; }


        public virtual DegreeOfExcellence DegreeOfExcellence { get; set; }
        [DisplayName("درجة التميز")]
        public int DegreeOfExcellenceId { get; set; }
    }
}