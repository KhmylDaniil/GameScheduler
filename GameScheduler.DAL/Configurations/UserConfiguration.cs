using GameScheduler.BLL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GameScheduler.DAL.Configurations
{
    internal class UserConfiguration : EntityBaseConfiguration<User>
    {
        public override void ConfigureChild(EntityTypeBuilder<User> builder)
        {
            builder.Property(x => x.Name).IsRequired();
            builder.Property(x => x.PasswordHash).IsRequired();
            builder.Property(x => x.RoleId).IsRequired();

            builder.HasMany(u => u.Games)
                .WithMany(g => g.Users)
                .UsingEntity(x => x.ToTable("UserGames"));
        }
    }
}
