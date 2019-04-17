using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entities.Entity
{
    public class Category:BaseEntity
    {
        [MaxLength(255)]
        public string CategoryName { get; set; }

        //İlişkiler
        public virtual ICollection<GameCategory> GameCategories { get; set; }

    }
}
