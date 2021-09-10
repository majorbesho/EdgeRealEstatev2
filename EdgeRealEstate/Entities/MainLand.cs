using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EdgeRealEstate.Entities
{
    [Table("MainLand")]
    public class MainLand
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        [Required]
        [MaxLength(250)]
        [DisplayName("اسم الارض ")]
        public string Aname { get; set; }
        [DisplayName("اسم الارص لاتينيى ")]
        public string Ename { get; set; }
        [DisplayName("  مساحة الارض ")]
        public string TypesInvestmentTag { get; set; }
        [DisplayName("المالك الاول ")]
        

        public string TypesInvestmentTag1 { get; set; }
        [DisplayName(" وصف الارض ")]

        public string TypesInvestmentTag2 { get; set; }
        [DisplayName(" سعر الارض ")]

        public string TypesInvestmentTag3 { get; set; }

        [DisplayName("الاجمالى")]
        public string TypesInvestmentTag4 { get; set; }
        [DisplayName(" تاريخ الشراء   ")]
        //[DataType(DataType.Date)]
        //[DisplayFormat(DataFormatString ="{0/yyyy/MM/dd}",ApplyFormatInEditMode =true)]
        public DateTime? TypesInvestmentTag5 { get; set; }

        [DisplayName("الصورة")]
        [MaxLength(2048)]
        public string ImgURL { get; set; }
        [DisplayName("خط الطول")]
        public string longitude { get; set; }
        [DisplayName("خط العرض")]
        public string Latitude { get; set; }
        [DisplayName("رقم الارض")]
        public string LandNo { get; set; }
        [MaxLength(2048)]
        [DisplayName("الملاحظات")]
        public string nots { get; set; }
        [DisplayName("المرفقات")]
        public int Attached { get; set; }


        //////////Start Relations///////////////
        [DisplayName("الشارع")]
        public int StreetID { get; set; }
        [DisplayName("الشارع")]
        [ForeignKey(nameof(StreetID))]
        public Street Street { get; set; }
        [DisplayName("الحى")]
        public int DistrictID { get; set; }

        [DisplayName("الحى")]
        [ForeignKey(nameof(DistrictID))]
        public District District { get; set; }

        [DisplayName("المدينة")]
        public int CitiesID { get; set; }

        [ForeignKey(nameof(CitiesID))]
        [DisplayName("المدينة")]
        public City Cities { get; set; }
        [DisplayName("المشاريع الرئيسيى")]
        public ICollection<MainProject> MainProjects { get; set; }
        [DisplayName("الشقق")]
        public ICollection<Flat> Flats { get; set; }

        [DisplayName("المرفقات")]
        public ICollection<MainLandAttachment> MainLandAttachments { get; set; }


    }
}