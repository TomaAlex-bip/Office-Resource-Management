using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Project3Api.Core.Configuration;
using Project3Api.Core.Entities;

namespace Project3Api.Data.EntityConfiguration
{
    public class DeskConfiguration : IEntityTypeConfiguration<Desk>
    {
        public void Configure(EntityTypeBuilder<Desk> builder)
        {
            // primary key
            builder.HasKey(x => x.Id);

            // unique name
            builder.HasIndex(x => new { x.Name }).IsUnique();

            // not null
            builder.Property(x => x.Name).IsRequired();
            builder.Property(x => x.GridPositionX).IsRequired();
            builder.Property(x => x.GridPositionY).IsRequired();
            builder.Property(x => x.Orientation).IsRequired();

            // max length
            builder.Property(x => x.Name).HasMaxLength(EntityHelperConstants.DESK_NAME_MAX_LENGTH);

            // set check for role
            builder.HasCheckConstraint("CK_Desk_Orientation",
                $"[Orientation] >= {EntityHelperConstants.DESK_ORIENTATION_MIN} AND " +
                $"[Orientation] <= {EntityHelperConstants.DESK_ORIENTATION_MAX}",
                x => x.HasName("CK_Desk_Orientation"));
        }
    }
}
