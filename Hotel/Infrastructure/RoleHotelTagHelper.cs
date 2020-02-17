using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hotel.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Razor.TagHelpers;


namespace Hotel.Infrastructure
{
    /// <summary>
    /// Хелпер для вывода списка пользователей, принадлежащих к роли.
    /// </summary>
    [HtmlTargetElement("td", Attributes = "identity-role")]
    public class RoleHotelTagHelper : TagHelper
    {
        //Менеджеры через инверсию зависимостей
        private UserManager<AppUser> userManager;
        private RoleManager<IdentityRole> roleManager;

        public RoleHotelTagHelper(UserManager<AppUser> usermgr,
                                  RoleManager<IdentityRole> rolemgr)
        {
            userManager = usermgr;
            roleManager = rolemgr;
        }

        [HtmlAttributeName("identity-role")]
        public string Role { get; set; }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            var names = new List<string>();

            IdentityRole role = await roleManager.FindByIdAsync(Role);

            if (role != null)
            {
                foreach (var user in userManager.Users)
                {
                    if (user != null && await userManager.IsInRoleAsync(user, role.Name))
                    {
                        names.Add(user.UserName);
                    }
                }
            }

            output.Content.SetContent(names.Count == 0 ? "Таких крутых ребят еще нет" : string.Join(", ", names));
        }
    }
}
