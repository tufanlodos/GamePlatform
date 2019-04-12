using System;

namespace GamePlatform.Personnel.Web.UI.Areas.Admin.Models {
    public class UserAddVm {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public DateTime BirthDate { get; set; }
        public string RoleName { get; set; }
    }
}
