using System;
using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace GamePlatform.Entities.Entity {
    public class Game : BaseEntity {
        [MaxLength(255)]
        public string GameName { get; set; }
        public decimal Price { get; set; }
        public decimal? DiscountPrice { get; set; }
        public bool IsSafeContent { get; set; }
        [MaxLength(500)]
        public string CoverPhoto { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? PublishDate { get; set; }

        // relations
        [ForeignKey("Developer")]
        public int? DeveloperId { get; set; }
        public virtual Company Developer { get; set; }

        [ForeignKey("Publisher")]
        public int? PublisherId { get; set; }
        public virtual Company Publisher { get; set; }

        public virtual ICollection<GameCategory> GameCategories { get; set; }

        public virtual ICollection<UserGame> UserGames { get; set; }
    }
}
