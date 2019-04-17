using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities.Entity
{
    public class Comment:BaseEntity
    {
        [MaxLength(1000)]
        public string Content { get; set; }

        [ForeignKey("Game")]
        public int GameId { get; set; }
        public virtual Game Game { get; set; }

        public int? LikeCount { get; set; }
        public int? DislikeCount { get; set; }
        public int? FunnyCount { get; set; }

        [MaxLength(255)]
        public string Author { get; set; }
    }
}
