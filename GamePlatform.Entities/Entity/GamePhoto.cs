using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GamePlatform.Entities.Entity {
    public class GamePhoto : BaseEntity {
        [MaxLength(500)]
        public string PhotoUrl { get; set; }

        [ForeignKey("Game")]
        public int GameId { get; set; }
        public virtual Game Game { get; set; }
    }
}
