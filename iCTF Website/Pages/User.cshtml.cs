using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using iCTF_Shared_Resources.Models;
using iCTF_Shared_Resources;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using iCTF_Shared_Resources.Managers;
using static iCTF_Shared_Resources.Managers.SharedStatsManager;
using Microsoft.AspNetCore.Identity;

namespace iCTF_Website.Pages
{
    public class UserModel : PageModel
    {
        public string Name { get; set; }
        public ApplicationUser AppUser { get; set; }
        public List<string> Roles { get; set; } = new List<string>();
        public User Player { get; set; }
        public Stats PlayerStats { get; set; }

        private readonly DatabaseContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public UserModel(DatabaseContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Player = await _context.Users.Include(x => x.WebsiteUser).Include(x => x.Solves).ThenInclude(x => x.Challenge).Include(x => x.Team).FirstOrDefaultAsync(x => x.Id == id);
            if (Player == null) { 
                return NotFound();
            }

            AppUser = Player.WebsiteUser;
            if (AppUser != null)
            {
                Roles = (await _userManager.GetRolesAsync(AppUser)).ToList();
            }
            PlayerStats = await GetStats(_context, Player);

            Name = AppUser?.UserName ?? Player.DiscordUsername;

            return Page();
        }
    }
}
