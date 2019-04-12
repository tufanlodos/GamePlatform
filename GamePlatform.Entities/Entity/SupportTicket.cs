using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GamePlatform.Entities.Entity {
    public class SupportTicket : BaseEntity {
        [MaxLength(2500)]
        public string Content { get; set; }
        public bool? IsSolved { get; set; }

        [ForeignKey("IssuedUser"), MaxLength(450)]
        public string IssuedUserId { get; set; }
        public virtual AppUser IssuedUser { get; set; }

        [ForeignKey("FixedUser"), MaxLength(450)]
        public string FixedUserId { get; set; }
        public virtual AppUser FixedUser { get; set; }

        [ForeignKey("SupportTicketCategory")]
        public int SupportTicketCategoryId { get; set; }
        public virtual SupportTicketCategory SupportTicketCategory { get; set; }
    }
}
