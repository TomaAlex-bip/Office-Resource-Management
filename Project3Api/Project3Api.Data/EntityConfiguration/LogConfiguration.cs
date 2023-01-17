using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Project3Api.Core.Entities;

namespace Project3Api.Data.EntityConfiguration
{
    public class LogConfiguration : IEntityTypeConfiguration<Log>
    {
        public void Configure(EntityTypeBuilder<Log> builder)
        {
            // primary key
            builder.HasKey(x => x.Id);

            // not null
            builder.Property(x => x.DateTime).IsRequired();
            builder.Property(x => x.Message).IsRequired();

            // default date
            builder.Property(o => o.DateTime).HasDefaultValueSql("getdate()");
        }
    }
}
