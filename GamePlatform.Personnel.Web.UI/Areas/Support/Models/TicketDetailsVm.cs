using GamePlatform.Entities.Entity;
using System.Collections.Generic;

namespace GamePlatform.Personnel.Web.UI.Areas.Support.Models {
    public class TicketDetailsVm {
        public List<SupportTicketStep> SupportTicketSteps { get; set; }
        public SupportTicket SupportTicket { get; set; }
    }
}
