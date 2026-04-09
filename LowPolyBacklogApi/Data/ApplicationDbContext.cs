using LowPolyBacklogApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace LowPolyBacklogApi.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Game> Games { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<BacklogEntry> BacklogEntries { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<BacklogEntry>()
                .ToTable(b => b.HasCheckConstraint("CK_BacklogEntry_Rating", "\"Rating\" >= 1 AND \"Rating\" <= 10"));

            
            modelBuilder.Entity<Game>()
                .ToTable(g => g.HasCheckConstraint("CK_Game_ReleaseYear", "\"ReleaseYear\" >= 1994 AND \"ReleaseYear\" <= 2006"));

            
            modelBuilder.Entity<BacklogEntry>()
                .HasIndex(b => b.GameId)
                .IsUnique();
        }
    }
}
