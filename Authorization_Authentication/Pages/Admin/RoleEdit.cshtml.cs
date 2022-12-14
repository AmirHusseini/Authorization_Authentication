using Authorization_Authentication.Model;
using Authorization_Authentication.Models.Custom_Fields;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Authorization_Authentication.Pages.Admin
{
    public class RoleEditModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;


        public RoleEditModel(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;

        }
        public List<UserRolesView> userRolesView = new List<UserRolesView>();
        public async Task<IActionResult> OnGet()
        {
            var users = await _userManager.Users.ToListAsync();

            foreach (ApplicationUser user in users)
            {
                var thisviewModel = new UserRolesView();
                thisviewModel.UserId = user.Id;
                thisviewModel.UserName = user.UserName;
                thisviewModel.Email = user.Email;
                thisviewModel.FirstName = user.FirstName;
                thisviewModel.LastName = user.LastName;
                thisviewModel.Roles = await GetUserRoles(user);
                userRolesView.Add(thisviewModel);

            }

            return Page();
        }
        private async Task<List<string>> GetUserRoles(ApplicationUser user)
        {
            return new List<string>(await _userManager.GetRolesAsync(user));
        }

    }
}
