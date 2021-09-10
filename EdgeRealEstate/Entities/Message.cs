using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EdgeRealEstate.Entities
{
    public class Message
    {
        public int ID { get; set; }
        [Required]
        [AllowHtml]
        [Display(Name = "Message")]
        public string MessageContent { get; set; }
        public bool Seen { get; set; }
        public string SenderId { get; set; } // message sender
        [Display(Name ="Send To")]
        public string ReceiverId { get; set; } //message receiver   
        public string SenderUsername { get; set; } 
        public string ReceiverUsername { get; set; } 
        public DateTime SentAt  { get; set; }
    }
}