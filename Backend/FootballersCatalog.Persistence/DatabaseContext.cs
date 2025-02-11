using FootballersCatalog.Persistence.Entities;
using Microsoft.EntityFrameworkCore;

namespace FootballersCatalog.Persistence
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) 
            : base(options) { }

        public DbSet<TeamEntity> Teams { get; set; }

        public DbSet<FootballerEntity> Footballers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<FootballerEntity>()
                .HasOne(f => f.Team)
                .WithMany(t => t.Footballers)
                .HasForeignKey(f => f.TeamId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
