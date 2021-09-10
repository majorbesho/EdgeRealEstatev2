using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EdgeRealEstate.Models.ViewModels
{
    public class ProjectGridVM
    {
        public int id { get; set; }
        [Required]
        [DisplayName("الاسم بالعربى")]
        public string Aname { get; set; }
        [DisplayName("الاسم باللاتينى")]
        public string Ename { get; set; }
        [DisplayName(" كود المشروع  ")]
        public string LandNo { get; set; }
        [DisplayName("حجم الارض")]
        public string LandSize { get; set; }
        [DisplayName("المرفقات")]
        public int attachedFileAndPic { get; set; }
        [DisplayName("MaxLevelForContributions")]
        public string MaxLevelForContributions { get; set; }
        [DisplayName("الصورة")]
        [MaxLength(2048)]
        public string ImgURL { get; set; }
        [DisplayName("تاريخ البدأ الفعلى")]
        public DateTime BeginDateAcutely { get; set; }
        [DisplayName("تاريخ البدأ المتوقع")]
        public DateTime BeginDateExpected { get; set; }
        [DisplayName("تاريخ الانتهاء الفعلى")]
        public DateTime EndDateAcutely { get; set; }
        [DisplayName("تاريخ الانتهاء المتوقع")]
        public DateTime EndDateExpected { get; set; }
        [DisplayName("IsExtand")]

        public bool IsExtand { get; set; }
        [DisplayName("عدد الشقق")]
        //nomber  of flat الشقق 
        public int FlatNO { get; set; }
        [DisplayName("عدد الروف المتاح للايجار")]
        // nomber of Rove الروف المتاح للايجار
        public int RoveNO { get; set; }
        [DisplayName("عدد المحلات")]
        // nommber of shops  المحلات
        public int ShopNO { get; set; }
        [DisplayName("عدد الادوار")]
        //nomber of level عدد الادوار 
        public int LevelNO { get; set; }
        [DisplayName("عدد الفلل")]
        // nomber of Vella  عدد الفيلا
        public int VellaNO { get; set; }
        [DisplayName("AdminstrationLevelNO")]

        public int AdminstrationLevelNO { get; set; }
        [DisplayName("ProjectDescriptionID")]
        public int ProjectDescriptionID { get; set; }
        [DisplayName("الملاحظات")]

        [MaxLength(2048)]
        public string nots { get; set; }
    }
}