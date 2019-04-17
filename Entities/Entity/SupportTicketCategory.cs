using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entities.Entity
{
    public class SupportTicketCategory:BaseEntity
    {
        [MaxLength(50)]
        public string CategoryName { get; set; }
        [MaxLength(7)]
        public string ColorHex { get; set; }
    }
}
