using FootballersCatalog.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballersCatalog.Persistence
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) 
            : base(options) { }

        public DbSet<TeamEntity> Teams { get; set; }

        public DbSet<FootballerEntity> Footballers { get; set; }
    }
}
