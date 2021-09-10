using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace EdgeRealEstate.Models.ViewModels
{
    public class MItemVM
    {
        public int Id { get; set; }
        [DisplayName("الاسم العربى")]
        public string Aname { get; set; }
        [DisplayName("الاسم اللاتينى")]
        public string Ename { get; set; }
        [DisplayName("عدد الايام")]
        public int NoOfDaies { get; set; }
        [DisplayName(" الوصف")]
        public string Description { get; set; }
        [DisplayName(" المرفقات")]
        public string AttachedId { get; set; }
        [DisplayName("درجة التميز ")]
        public int DegreeOfExcellenceId { get; set; }
        [DisplayName("يحتوى على مواد")]
        public bool HaveItem { get; set; }
        [DisplayName(" ملاحظات")]
        public string Nots { get; set; }
    }
}