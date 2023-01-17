using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Project3Api.Core.Entities;

namespace Project3Api.Data.EntityConfiguration
{
    public class ResourceAllocationConfiguration : IEntityTypeConfiguration<ResourceAllocation>
    {
        public void Configure(EntityTypeBuilder<ResourceAllocation> builder)
        {
            // primary key
            builder.HasKey(x => x.Id);

            // not null
            builder.Property(x => x.AllocatedFrom).IsRequired();
        }
    }
}
