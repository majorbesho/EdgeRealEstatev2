using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EdgeRealEstate.Entities
{
    [Table("MainProject")]
    public class MainProject
    {
        public MainProject()
        {
            this.OrderJob = new HashSet<OrderJob>();
        }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        [Required]
        [MaxLength(250)]
        [DisplayName("الاسم العربية")]
        public string Aname { get; set; }
        [DisplayName("الاسم الانجليزية")]
        public string Ename { get; set; }
        [DisplayName("الحجم الكلى")]
        public string TotalLandSize { get; set; }
        [DisplayName("نوع الاستثمار")]
        public string TypesInvestment { get; set; }
        [DisplayName("المرفقات")]
        public int attachedFileAndPic { get; set; }
        [DisplayName("الحد الاقصى للمساهمات ")]
        public string MaxLevelForContributions  { get; set; }
        [DisplayName("الصورة")]
        [MaxLength(2048)]
        public string ImgURL { get; set; }
        [DisplayName("رقم المشروع")]
        public string LandNo { get; set; }
        [DisplayName("الملاحظات")]
        [MaxLength(2048)]
        public string nots { get; set; }

        //////////Start Relations///////////////
        [DisplayName("الارض الرئيسية")]
        public int MainLandId { get; set; }

        [ForeignKey(nameof(MainLandId))]
        [DisplayName("الارض الرئيسية")]
        public MainLand MainLand { get; set; }
        [DisplayName("المشاريع")]
        public ICollection<Projects> Projects { get; set; }

        public ICollection<Flat> Flats { get; set; } //wrong relation
        [DisplayName("اوامر الشغل")]
        public virtual ICollection<OrderJob> OrderJob { get; set; }
    }
}