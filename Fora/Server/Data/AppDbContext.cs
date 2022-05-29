using Microsoft.EntityFrameworkCore;

namespace Fora.Server.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<InterestModel> Interests { get; set; }
        public DbSet<ThreadModel> Threads { get; set; }
        public DbSet<MessageModel> Messages { get; set; }
        public DbSet<UserModel> Users { get; set; }
        public DbSet<UserInterestModel> UserInterests { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            // Seed Interest data to database
            modelBuilder.Entity<InterestModel>()
                .HasData(
                new InterestModel() { Id = 1, Name = "Games", UserId = 1 },
                new InterestModel() { Id = 2, Name = "Sports", UserId = 1 },
                new InterestModel() { Id = 3, Name = "Politics", UserId = 1 },
                new InterestModel() { Id = 4, Name = "Religion", UserId = 1 },
                new InterestModel() { Id = 5, Name = "Design", UserId = 1 },
                new InterestModel() { Id = 6, Name = "Garden", UserId = 1 },
                new InterestModel() { Id = 7, Name = "Technology", UserId = 1 },
                new InterestModel() { Id = 8, Name = "Pets", UserId = 1 });

            modelBuilder.Entity<UserModel>()
                .HasData(
                new UserModel() { Id = 1, 
                    Username = "Casper", 
                    Banned = false, 
                    Deleted = false 
                });

            // Many to many (users can have many interests that in turns have many users)
            modelBuilder.Entity<UserInterestModel>()
                .HasKey(ui => new { ui.UserId, ui.InterestId });
            modelBuilder.Entity<UserInterestModel>()
                .HasOne(ui => ui.User)
                .WithMany(u => u.UserInterests)
                .HasForeignKey(ui => ui.UserId);
            modelBuilder.Entity<UserInterestModel>()
                .HasOne(ui => ui.Interest)
                .WithMany(i => i.UserInterests)
                .HasForeignKey(ui => ui.InterestId);

            // Restrict deletion of interest on user delete (set user to null instead)
            modelBuilder.Entity<InterestModel>()
                .HasOne(i => i.User)
                .WithMany(u => u.Interests)
                .HasForeignKey(i => i.UserId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.SetNull);

            // Restrict deletion of thread on user delete (set user to null instead)
            modelBuilder.Entity<ThreadModel>()
                .HasOne(i => i.User)
                .WithMany(u => u.Threads)
                .HasForeignKey(i => i.UserId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.SetNull);

            // Seed Threads data to database
            modelBuilder.Entity<ThreadModel>()
                .HasData(
                new ThreadModel() { Id = 1, Name = "Introduce yourself!", UserId = 1 },
                new ThreadModel() { Id = 2, Name = "DS3 Cheat codes plz", UserId = 1 },
                new ThreadModel() { Id = 3, Name = "How to get rich in sims 66", UserId = 1 },
                new ThreadModel() { Id = 4, Name = "Why is my game lagging???", UserId = 1 },
                new ThreadModel() { Id = 5, Name = "How to git gud", UserId = 1 },
                new ThreadModel() { Id = 6, Name = "New Lego City Speedrun Record!", UserId = 1 },
                new ThreadModel() { Id = 7, Name = "GTA hydra abuse", UserId = 1 },
                new ThreadModel() { Id = 8, Name = "Tetris laggy. What is my bottleneck??? help", UserId = 1 }
                );


            // Restrict deletion of thread on message delete (set user to null instead)
            modelBuilder.Entity<MessageModel>()
                .HasOne(i => i.User)
                .WithMany(u => u.Messages)
                .HasForeignKey(i => i.UserId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
