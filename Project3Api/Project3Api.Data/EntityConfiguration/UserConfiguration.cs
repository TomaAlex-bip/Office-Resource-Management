using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Project3Api.Core.Configuration;
using Project3Api.Core.Entities;

namespace Project3Api.Data.EntityConfiguration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            // primary key
            builder.HasKey(x => x.Id);

            // unique username
            builder.HasIndex(u => u.Username).IsUnique();

            // not null
            builder.Property(u => u.Username).IsRequired();
            builder.Property(u => u.Password).IsRequired();
            builder.Property(u => u.Role).IsRequired();

            // max lenght
            builder.Property(u => u.Username).HasMaxLength(EntityHelperConstants.USERNAME_MAX_LENGTH);
            builder.Property(u => u.Password).HasMaxLength(EntityHelperConstants.PASSWORD_MAX_LENGTH);

            // set check for role
            builder.HasCheckConstraint("CK_Users_Role",
                $"[Role] >= {EntityHelperConstants.USER_ROLE_MIN} AND " +
                $"[Role] <= {EntityHelperConstants.USER_ROLE_MAX}", 
                x => x.HasName("CK_Users_Role"));
        }
    }
}
