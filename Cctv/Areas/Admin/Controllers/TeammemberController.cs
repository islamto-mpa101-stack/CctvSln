using Cctv.Context;
using Cctv.Helpers;
using Cctv.Models;
using Cctv.ViewModels.TeamMemberViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Cctv.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TeamMemberController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly string _folderPath;

        public TeamMemberController(AppDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
            _folderPath = Path.Combine(_webHostEnvironment.WebRootPath, "img");
        }

        public async Task<IActionResult> Update(int id)
        {
            var member = await _context.TeamMembers
                                    .Select(x=> new TeamMemberUpdateVm
                                    {
                                        Id = id,
                                        Fullname = x.Fullname,
                                        Designation = x.Designation,
                                    })
                                    .FirstOrDefaultAsync(x=>x.Id == id);

            return View(member);
        }

        [HttpPost]
        public async Task<IActionResult> Update(TeamMemberUpdateVm vm)
        {
            if(!ModelState.IsValid)
                return View(vm);

            if (!vm.Image.CheckSize(2))
            {
                ModelState.AddModelError("", "Image in size-i 2 mbdan cox olmamalidir");
                return View(vm);
            }

            if (!vm.Image.CheckType())
            {
                ModelState.AddModelError("", "Ancaq image tipinde olmalidir.");
                return View(vm);
            }

            TeamMember member = await _context.TeamMembers.FirstOrDefaultAsync(x => x.Id == vm.Id);

            if (vm.Image is { })
            {
                string path = Path.Combine(_folderPath, member.ImagePath);

                if(System.IO.File.Exists(path))
                    System.IO.File.Delete(path);

                string uniqueFileName = await vm.Image.SaveFileAsync(_folderPath);

                member.ImagePath = uniqueFileName;
            }

            member.Fullname = vm.Fullname;
            member.Designation = vm.Designation;

            _context.TeamMembers.Update(member);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");

        }

        public async Task<IActionResult> Delete(int id)
        {
            var member = await _context.TeamMembers.FindAsync(id);

            if (member == null)
                return NotFound();

            _context.TeamMembers.Remove(member);
            await _context.SaveChangesAsync();

            string path = Path.Combine(_folderPath, member.ImagePath);

            if (System.IO.File.Exists(path))
                System.IO.File.Delete(path);

            return RedirectToAction("Index");

        }

        public async Task<IActionResult> Index()
        {
            var teamMembers = await _context.TeamMembers
                                            .Select(x => new TeamMemberGetVm
                                            {
                                                Id = x.Id,
                                                Fullname = x.Fullname,
                                                Designation = x.Designation,
                                                ImagePath = x.ImagePath,
                                            })
                                            .ToListAsync();

            return View(teamMembers);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(TeamMemberCreateVm vm)
        {
            if (!ModelState.IsValid)
                return View(vm);

            if (!vm.Image.CheckSize(2))
            {
                ModelState.AddModelError("", "Image in size i 2 mbdan cox olmamalidir");
                return View(vm);
            }

            if (!vm.Image.CheckType())
            {
                ModelState.AddModelError("", "Ancaq image tipinde olmalidir.");
                return View(vm);
            }

            string uniqueFileName = await vm.Image.SaveFileAsync(_folderPath);

            TeamMember teamMember = new()
            {
                Fullname = vm.Fullname,
                Designation = vm.Designation,
                ImagePath = uniqueFileName,
            };

            await _context.TeamMembers.AddAsync(teamMember);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");

        }

    }
}
