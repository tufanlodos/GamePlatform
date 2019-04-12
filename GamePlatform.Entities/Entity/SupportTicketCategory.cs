using System.ComponentModel.DataAnnotations;

namespace GamePlatform.Entities.Entity {
    public class SupportTicketCategory : BaseEntity {
        [MaxLength(50)]
        public string CategoryName { get; set; }
        [MaxLength(7)]
        public string ColorHex { get; set; }
    }
}
