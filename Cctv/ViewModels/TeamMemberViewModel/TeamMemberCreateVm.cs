using System.ComponentModel.DataAnnotations;

namespace Cctv.ViewModels.TeamMemberViewModel
{
    public class TeamMemberCreateVm
    {
        [Required, MaxLength(256), MinLength(3)]
        public string Fullname { get; set; }
        [Required, MaxLength(256), MinLength(3)]
        public string Designation { get; set; }
        [Required]
        public IFormFile Image { get; set; }
    }
}
