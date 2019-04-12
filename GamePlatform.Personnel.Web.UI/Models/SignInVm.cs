using System.ComponentModel.DataAnnotations;

namespace GamePlatform.Personnel.Web.UI.Models {
    public class SignInVm {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
