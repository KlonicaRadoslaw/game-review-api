using GameReviewApp.Models;
using Microsoft.EntityFrameworkCore;

namespace GameReviewApp.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) 
        {
            
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Producer> Producers { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<GameProducer> GamesProducers { get; set; }
        public DbSet<GameCategory> GamesCategories { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Reviewer> Reviewer { get; set;}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GameCategory>()
                .HasKey(gc => new { gc.GameId, gc.CategoryId });
            modelBuilder.Entity<GameCategory>()
                .HasOne(g => g.Game)
                .WithMany(gc => gc.GameCategories)
                .HasForeignKey(c => c.GameId);
            modelBuilder.Entity<GameCategory>()
                .HasOne(g => g.Category)
                .WithMany(gc => gc.GameCategories)
                .HasForeignKey(c => c.CategoryId);

            modelBuilder.Entity<GameProducer>()
                .HasKey(gp => new { gp.GameId, gp.ProducerId });
            modelBuilder.Entity<GameProducer>()
                .HasOne(g => g.Game)
                .WithMany(gp => gp.GameProducers)
                .HasForeignKey(c => c.GameId);
            modelBuilder.Entity<GameProducer>()
                .HasOne(p => p.Producer)
                .WithMany(pc => pc.GameProducers)
                .HasForeignKey(c => c.ProducerId);
        }

    }
}
