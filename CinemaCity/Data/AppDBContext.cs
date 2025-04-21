using CinemaCity.Models;
using Microsoft.EntityFrameworkCore;

namespace CinemaCity.Data
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Actor> Actors { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<Film> Films { get; set; }
        public DbSet<Author> Authors { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Film>()
                .HasMany(f => f.Genres)
                .WithMany(g => g.Films)
                .UsingEntity(j => j.ToTable("FilmGenres"));

            modelBuilder.Entity<Film>()
                .HasMany(f => f.Actors)
                .WithMany(a => a.Films)
                .UsingEntity(j => j.ToTable("FilmActors"));

            modelBuilder.Entity<Session>()
                .HasOne(s => s.Film)
                .WithMany(f => f.Sessions)
                .HasForeignKey(s => s.FilmId);

            modelBuilder.Entity<Film>()
            .HasOne(f => f.Author)
            .WithMany(a => a.Films)
            .HasForeignKey(f => f.AuthorId);

            modelBuilder.Entity<Film>()
            .Property(f => f.Price)
            .HasPrecision(10, 2);
        }
    }
}
