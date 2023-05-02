using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using GameScheduler.BLL.Entities;

namespace GameScheduler.DAL.Configurations
{
    public abstract class EntityBaseConfiguration<TEntity> : IEntityTypeConfiguration<TEntity>
        where TEntity : EntityBase
    {
        public virtual void Configure(EntityTypeBuilder<TEntity> builder)
        {
            ConfigureId(builder);
            ConfigureBase(builder);
            ConfigureChild(builder);
        }

        public virtual void ConfigureId(EntityTypeBuilder<TEntity> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(r => r.Id).IsRequired();
        }

        public virtual void ConfigureBase(EntityTypeBuilder<TEntity> builder)
        {
            builder.Property(r => r.CreatedOn).IsRequired();
            builder.Property(r => r.UpdatedOn).IsRequired();
            builder.Property(r => r.CreatedByUserId).IsRequired();
            builder.Property(r => r.UpdatedByUserId).IsRequired();
        }

        public abstract void ConfigureChild(EntityTypeBuilder<TEntity> builder);
    }
}
