using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Application.DTOs.Navigation;
using Infrastructure.Authentication.Contexts;
using Infrastructure.Authentication.Models.Identity;
using Infrastructure.Shared.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication.ViewComponents
{
    public class DashboardNavigationViewComponent : ViewComponent
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly AuthenticationDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public DashboardNavigationViewComponent(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, AuthenticationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var user = await _userManager.FindByIdAsync(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));

            var roleId = _context.UserRoles.Where(x => x.UserId == user.Id).ToList()[0].RoleId;
            var roleClaims = _context.RoleClaims.Where(x => x.RoleId == roleId).ToList();
            var rolePermission = _context.RolePermission.ToList().Where(x => x.IsActive);

            var claims = new HashSet<string>(roleClaims.Select(claim => claim.ClaimValue));
            rolePermission = rolePermission.Where(x => claims.Contains(x.Slug)).ToList();

            var menuGroups = rolePermission.OrderBy(x => x.DisplayOrder).GroupBy(g => g.Group).ToList();
            var menuList = new List<NavigationViewModel>();
            foreach (var menuGroup in menuGroups)
            {
                var menu = new NavigationViewModel
                {
                    Group = TextUtility.CamelCaseTranslation(menuGroup.Select(x => x.Group).FirstOrDefault()),
                    DisplayOrder = menuGroup.Select(y => y.DisplayOrder).FirstOrDefault(),
                    Icon = menuGroup.Select(y => y.Icon).FirstOrDefault(),
                    GroupIcon = menuGroup.Select(y => y.GroupIcon).FirstOrDefault()
                };
                var menuItemList = new List<NavigationItems>();
                if (menuGroup.Any())
                {

                    foreach (var menuLists in menuGroup)
                    {
                        var navigationItems = new NavigationItems
                        {
                            MenuName = menuLists.MenuName,
                            ItemIcons = menuLists.Icon,
                            Links = menuLists.Link,
                            SubGroup = menuLists.SubGroup,
                            SubGroupIcon = menuLists.GroupIcon
                        };
                        menuItemList.Add(navigationItems);
                    }
                }

                menu.MenuItems = menuItemList;
                menuList.Add(menu);
            }
            menuList = menuList.OrderBy(x => x.DisplayOrder).ToList();
            return View(menuList);
        }
    }
}
