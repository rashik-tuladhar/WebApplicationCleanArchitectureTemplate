using System.Collections.Generic;

namespace Application.DTOs.Navigation
{
    public class NavigationItems
    {
        public string ItemIcons { get; set; }
        public string SubGroup { get; set; }
        public string MenuName { get; set; }
        public string Links { get; set; }
        public string SubGroupIcon { get; set; }
        public List<NavigationItems> SubGroupMenuItems { get; set; }
    }
}
