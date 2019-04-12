using GamePlatform.Entities.Entity;
using System.Collections.Generic;

namespace GamePlatform.Personnel.Web.UI.Areas.Editor.Models {
    public class GameAddGetVm {
        public List<Company> Companies { get; set; }
        public List<Category> Categories { get; set; }
    }

    public class GameAddPostVm {
        public string GameName { get; set; }
        public decimal Price { get; set; }
        public bool IsSafeContent { get; set; }
        public int DeveloperId { get; set; }
        public int PublisherId { get; set; }
        public List<int> Categories { get; set; }
    }
}
