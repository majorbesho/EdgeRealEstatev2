using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EdgeRealEstate.Models.ViewModels
{
    public class MessageVM
    {

        public int ID { get; set; }
        [AllowHtml]
        public string MessageContent { get; set; }
        public bool Seen { get; set; }
        public string Sender { get; set; } 
        public string ReceiverId { get; set; }       
        public DateTime SentAt { get; set; }
    }
}