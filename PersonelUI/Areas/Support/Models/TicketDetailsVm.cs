using Entities.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonelUI.Areas.Support.Models
{
    public class TicketDetailsVm
    {
        public List<SupportTicketStep> SupportTicketSteps { get; set; }
        public SupportTicket SupportTicket { get; set; }
    }
}
