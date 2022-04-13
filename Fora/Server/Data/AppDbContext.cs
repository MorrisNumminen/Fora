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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            // Seed Interest data to database
            modelBuilder.Entity<InterestModel>()
                .HasData(
                new InterestModel() { Id = 1, Name = "Games" },
                new InterestModel() { Id = 2, Name = "Sports" },
                new InterestModel() { Id = 3, Name = "Politics" },
                new InterestModel() { Id = 4, Name = "Religion" },
                new InterestModel() { Id = 5, Name = "Design" },
                new InterestModel() { Id = 6, Name = "Garden" },
                new InterestModel() { Id = 7, Name = "Technology" },
                new InterestModel() { Id = 8, Name = "Pets" });

 

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
                new ThreadModel() { Id = 1, Name = "Introduce yourself!" },
                new ThreadModel() { Id = 2, Name = "DS3 Cheat codes plz" },
                new ThreadModel() { Id = 3, Name = "How to get rich in sims 66" },
                new ThreadModel() { Id = 4, Name = "Why is my game lagging???" },
                new ThreadModel() { Id = 5, Name = "How to git gud" },
                new ThreadModel() { Id = 6, Name = "New Lego City Speedrun Record!" },
                new ThreadModel() { Id = 7, Name = "GTA hydra abuse" },
                new ThreadModel() { Id = 8, Name = "Tetris laggy. What is my bottleneck??? help" }
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
