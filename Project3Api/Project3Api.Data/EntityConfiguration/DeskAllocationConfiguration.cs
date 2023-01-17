using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Project3Api.Core.Entities;

namespace Project3Api.Data.EntityConfiguration
{
    public class DeskAllocationConfiguration : IEntityTypeConfiguration<DeskAllocation>
    {
        public void Configure(EntityTypeBuilder<DeskAllocation> builder)
        {
            // primary key
            builder.HasKey(x => x.Id);

            // not null
            builder.Property(x => x.ReservedFrom).IsRequired();
            builder.Property(x => x.ReservedUntil).IsRequired();
        }
    }
}
