using GameScheduler.BLL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GameScheduler.DAL.Configurations
{
    internal class GameConfiguration : EntityBaseConfiguration<Game>
    {
        public override void ConfigureChild(EntityTypeBuilder<Game> builder)
        {
            builder.Property(x => x.Name).IsRequired();
            builder.Property(x => x.DateTime).IsRequired();
            builder.Property(x => x.Description);

            builder.HasMany(g => g.Users)
                .WithMany(u => u.Games)
                .UsingEntity(x => x.ToTable("UserGames"));
        }
    }
}
