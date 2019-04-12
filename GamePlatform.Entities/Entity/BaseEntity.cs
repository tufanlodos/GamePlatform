using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GamePlatform.Entities.Entity {
    public class BaseEntity {
        [Key, Column(Order = 1)]
        public int Id { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? AddedDate { get; set; }

        public bool? IsActive { get; set; }
    }
}
