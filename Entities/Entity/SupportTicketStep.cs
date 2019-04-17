using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities.Entity
{
    public class SupportTicketStep:BaseEntity
    {
        [MaxLength(1000)]
        public string Content { get; set; }

        [ForeignKey("SupportTicket")]
        public int SupportTicketId { get; set; }
        public virtual SupportTicket SupportTicket { get; set; }

        [ForeignKey("AnsweredUser")]
        public string AnsweredUserId { get; set; }
        public virtual AppUser AnsweredUser { get; set; }
    }
}
