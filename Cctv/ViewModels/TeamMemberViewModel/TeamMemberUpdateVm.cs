using System.ComponentModel.DataAnnotations;

namespace Cctv.ViewModels.TeamMemberViewModel
{
    public class TeamMemberUpdateVm
    {
        public int Id { get; set; }
        [Required, MaxLength(256), MinLength(3)]
        public string Fullname { get; set; }
        [Required, MaxLength(256), MinLength(3)]
        public string Designation { get; set; }
        [Required]
        public IFormFile? Image { get; set; }
    }
}
