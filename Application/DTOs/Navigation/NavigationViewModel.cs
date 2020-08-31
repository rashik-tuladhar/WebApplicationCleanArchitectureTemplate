using System.Collections.Generic;

namespace Application.DTOs.Navigation
{
    public class NavigationViewModel
    {
        public int DisplayOrder { get; set; }
        public string Group { get; set; }
        public string Icon { get; set; }
        public string GroupIcon { get; set; }
        public List<NavigationItems> MenuItems { get; set; }
    }
}
