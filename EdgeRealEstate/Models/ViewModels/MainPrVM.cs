using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace EdgeRealEstate.Models.ViewModels
{
    public class MainPrVM
    {
        public int id { get; set; }
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
        public string MaxLevelForContributions { get; set; }
        [DisplayName("الصورة")]
        public string ImgURL { get; set; }
        [DisplayName("رقم المشروع")]
        public string LandNo { get; set; }
        [DisplayName("الملاحظات")]
        public string nots { get; set; }
    }
}