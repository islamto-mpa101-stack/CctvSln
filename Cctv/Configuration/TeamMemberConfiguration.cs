using Cctv.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cctv.Configuration
{
    public class TeamMemberConfiguration : IEntityTypeConfiguration<TeamMember>
    {
        public void Configure(EntityTypeBuilder<TeamMember> builder)
        {
            builder.Property(x => x.Fullname).IsRequired().HasMaxLength(256);

            builder.Property(x=>x.Designation).IsRequired().HasMaxLength(256);

            builder.Property(x => x.ImagePath).IsRequired();
        }
    }
}
