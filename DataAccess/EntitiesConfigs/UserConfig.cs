using BusinessLogic.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;


namespace DataAccess.EntitiesConfigs
{
    public class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("id");
            builder.Property(x => x.Username).HasColumnName("username");
            builder.Property(x => x.Surname).HasColumnName("surname");
            builder.Property(x => x.Email).HasColumnName("email");
            builder.Property(x => x.Birthdate).HasColumnName("birthdate");
            builder.Property(x => x.Image).HasColumnName("image");
            builder.Property(x => x.IsAccountNonExpired).HasColumnName("is_account_non_expired");
            builder.Property(x => x.IsAccountNonLocked).HasColumnName("is_account_non_locked");
            builder.Property(x => x.IsCredentialsNonExpired).HasColumnName("is_credentials_non_expired");
            builder.Property(x => x.IsEnabled).HasColumnName("is_enabled");
            builder.Property(x => x.Name).HasColumnName("name");
            builder.Property(x => x.Password).HasColumnName("password");
            builder.HasMany(x => x.FavoriteProducts)
                .WithMany(x => x.FavoriteInUsers);
            builder.HasMany(u => u.Roles)
                   .WithMany(r => r.Users)
                   .UsingEntity<Dictionary<string, object>>(
                        "tbl_user_user_roles",
                         j => j.HasOne<UserRole>()
                               .WithMany()
                               .HasForeignKey("role_id"),
                         j => j.HasOne<User>()
                               .WithMany()
                               .HasForeignKey("user_id"));
            builder.ToTable("tbl_users");
        }

    }
}
