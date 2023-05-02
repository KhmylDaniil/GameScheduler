using GameScheduler.BLL.Abstractions;
using GameScheduler.BLL.Entities;
using Microsoft.EntityFrameworkCore;

namespace GameScheduler.DAL
{
    public class AppDbContext : DbContext, IAppDbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<Game> Games { get; set; }

        private readonly IUserContext _userContext;
        private readonly IDateTimeProvider _dateTimeProvider;

        public AppDbContext(
            DbContextOptions<AppDbContext> dbContextOptions,
            IUserContext userContext,
            IDateTimeProvider dateTimeProvider)
            : base(dbContextOptions)
        {
            _userContext = userContext;
            _dateTimeProvider = dateTimeProvider;
        }

        protected AppDbContext()
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entries = ChangeTracker.Entries()
                .Where(e => e.Entity is EntityBase && (
                        e.State == EntityState.Added
                        || e.State == EntityState.Modified));

            foreach (var entityEntry in entries)
                if (entityEntry.Entity is EntityBase entityBase)
                    if (entityEntry.State == EntityState.Added)
                    {
                        entityBase.Id = Guid.NewGuid();
                        entityBase.CreatedByUserId = _userContext.CurrentUserId;
                        entityBase.CreatedOn = _dateTimeProvider.Now;
                        entityBase.UpdatedOn = _dateTimeProvider.Now;
                        entityBase.UpdatedByUserId = _userContext.CurrentUserId;

                    }
                    else
                    {
                        entityBase.UpdatedOn = _dateTimeProvider.Now;
                        entityBase.UpdatedByUserId = _userContext.CurrentUserId;
                    }

            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}
