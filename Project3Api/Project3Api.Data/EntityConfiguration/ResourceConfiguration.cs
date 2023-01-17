using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Project3Api.Core.Configuration;
using Project3Api.Core.Entities;

namespace Project3Api.Data.EntityConfiguration
{
    public class ResourceConfiguration : IEntityTypeConfiguration<Resource>
    {
        public void Configure(EntityTypeBuilder<Resource> builder)
        {
            // primary key
            builder.HasKey(x => x.Id);

            // unique name
            builder.HasIndex(x => new { x.Name }).IsUnique();

            // not null
            builder.Property(x => x.Name).IsRequired();
            builder.Property(x => x.Type).IsRequired();

            // max length
            builder.Property(x => x.Name).HasMaxLength(EntityHelperConstants.RESOURCE_NAME_MAX_LENGTH);
            builder.Property(x => x.Type).HasMaxLength(EntityHelperConstants.RESOURCE_TYPE_MAX_LENGTH);
        }
    }
}
