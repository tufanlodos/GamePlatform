using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities.Entity
{
    public class AppUser:IdentityUser
    {
        [MaxLength(100)]
        public string FirstName { get; set; }
        [MaxLength(100)]
        public string LastName { get; set; }
        [Column(TypeName ="datetime2")]
        public DateTime BirthDay { get; set; }
        [MaxLength(30)]
        public string NickName { get; set; }
        [MaxLength(500)]
        public string AvatarPath { get; set; }

        //İlişkiler
        public virtual ICollection<UserGame> UserGames { get; set; }
    }
}
