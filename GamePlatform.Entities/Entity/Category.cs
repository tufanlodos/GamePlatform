using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GamePlatform.Entities.Entity {
    public class Category : BaseEntity {
        [MaxLength(255)]
        public string CategoryName { get; set; }

        public virtual ICollection<GameCategory> GameCategories { get; set; }
    }
}
