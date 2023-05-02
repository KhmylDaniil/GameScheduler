using GameScheduler.BLL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameScheduler.BLL.Abstractions
{
    public interface IAppDbContext
    {
        DbSet<User> Users { get; }

        DbSet<Game> Games { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
