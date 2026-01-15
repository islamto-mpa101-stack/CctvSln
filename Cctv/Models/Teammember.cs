using Cctv.Models.Common;

namespace Cctv.Models
{
    public class TeamMember : BaseEntity
    {
        public string Fullname { get; set; }
        public string Designation { get; set; }
        public string ImagePath { get; set; }

    }
}
