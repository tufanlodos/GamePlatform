using Entities.Entity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Context
{
    public class GamePlatformDbContext : IdentityDbContext<AppUser, AppRole, string>
    {
        public GamePlatformDbContext(DbContextOptions<GamePlatformDbContext> options) : base(options)
        {

        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<GameCategory> GameCategories { get; set; }
        public DbSet<GamePhoto> GamePhotos { get; set; }
        public DbSet<UserGame> UserGames { get; set; }
        public DbSet<SupportTicketCategory> SupportTicketCategories { get; set; }
        public DbSet<SupportTicket> SupportTicket { get; set; }
        public DbSet<SupportTicketStep> SupportTicketSteps { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<GameCategory>()
                .HasKey(k => new { k.GameId, k.CategoryId });

            builder.Entity<GameCategory>()
                .HasOne(g => g.Game)
                .WithMany(x => x.GameCategories)
                .HasForeignKey(fk => fk.GameId);

            builder.Entity<GameCategory>()
                .HasOne(c => c.Category)
                .WithMany(x => x.GameCategories)
                .HasForeignKey(fk => fk.CategoryId);

            builder.Entity<UserGame>()
                .HasKey(k => new { k.AppUserId, k.GameId });

            builder.Entity<UserGame>()
                .HasOne(u => u.AppUser)
                .WithMany(x => x.UserGames)
                .HasForeignKey(fk => fk.AppUserId);

            builder.Entity<UserGame>()
                .HasOne(g => g.Game)
                .WithMany(x => x.UserGames)
                .HasForeignKey(fk => fk.GameId);
            base.OnModelCreating(builder);
        }
    }
}
