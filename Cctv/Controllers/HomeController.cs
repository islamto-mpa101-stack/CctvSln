using System.Diagnostics;
using System.Threading.Tasks;
using Cctv.Context;
using Cctv.Models;
using Cctv.ViewModels.TeamMemberViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Cctv.Controllers
{
    public class HomeController(AppDbContext context) : Controller
    {
        [Authorize(Roles ="Member")]
        public async Task<IActionResult> Index()
        {
            var members = await context.TeamMembers
			.Select(x => new TeamMemberGetVm
			{
				Id = x.Id,
				Fullname = x.Fullname,
				Designation = x.Designation,
				ImagePath = x.ImagePath,
			})
												.ToListAsync();

			return View(members);
        }

    }
}
