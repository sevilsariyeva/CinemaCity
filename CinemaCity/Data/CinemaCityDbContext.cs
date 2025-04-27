using CinemaCity.Models;
using Microsoft.EntityFrameworkCore;

namespace CinemaCity.Data
{
    public partial class CinemaCityDbContext : DbContext
    {
        public CinemaCityDbContext()
        {
        }

        public CinemaCityDbContext(DbContextOptions<CinemaCityDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Actor> Actors { get; set; }

        public virtual DbSet<Admin> Admins { get; set; }

        public virtual DbSet<Author> Authors { get; set; }

        public virtual DbSet<Basket> Baskets { get; set; }
        public DbSet<BasketDetail> BasketDetails { get; set; }

        public virtual DbSet<Film> Films { get; set; }

        public virtual DbSet<FilmActor> FilmActors { get; set; }

        public virtual DbSet<FilmGenre> FilmGenres { get; set; }

        public virtual DbSet<Genre> Genres { get; set; }

        public virtual DbSet<Session> Sessions { get; set; }

        public virtual DbSet<Ticket> Tickets { get; set; }

        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseSqlServer("Server=DESKTOP-3F2PTRN\\SQLEXPRESS;Database=CinemaCityDB;Trusted_Connection=True;Encrypt=False");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Actor>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__actors__3213E83F09352FA3");
            });

            modelBuilder.Entity<Admin>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__admin__3213E83F7F713B7B");
            });

            modelBuilder.Entity<Author>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__authors__3213E83FEF87B555");
            });

            modelBuilder.Entity<Basket>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__baskets__3213E83F29B75742");

                entity.HasOne(d => d.User).WithMany(p => p.Baskets).HasConstraintName("FK__baskets__user_id__4BAC3F29");
            });

            modelBuilder.Entity<Film>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__films__3213E83FF13C125D");

                entity.HasOne(d => d.Author).WithMany(p => p.Films).HasConstraintName("FK__films__author_id__4AB81AF0");
            });

            modelBuilder.Entity<FilmActor>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__film_act__3213E83F7B1F4DF1");

                entity.HasOne(d => d.Actor).WithMany(p => p.FilmActors).HasConstraintName("FK__film_acto__actor__534D60F1");

                entity.HasOne(d => d.Film).WithMany(p => p.FilmActors).HasConstraintName("FK__film_acto__film___52593CB8");
            });

            modelBuilder.Entity<FilmGenre>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__film_gen__3213E83F95F17413");

                entity.HasOne(d => d.Film).WithMany(p => p.FilmGenres).HasConstraintName("FK__film_genr__film___5070F446");

                entity.HasOne(d => d.Genre).WithMany(p => p.FilmGenres).HasConstraintName("FK__film_genr__genre__5165187F");
            });

            modelBuilder.Entity<Genre>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__genres__3213E83FDF87D713");
            });

            modelBuilder.Entity<Session>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__sessions__3213E83F64BCDE8E");

                entity.HasOne(d => d.Film).WithMany(p => p.Sessions).HasConstraintName("FK__sessions__film_i__4CA06362");
            });
            modelBuilder.Entity<BasketDetail>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__basket_d__3213E83F1C116F22");

                entity.Property(e => e.MovieJson).HasColumnName("movie_json").HasColumnType("nvarchar(max)");
                entity.Property(e => e.SessionTime).HasColumnName("session_time").HasMaxLength(100);
                entity.Property(e => e.Theater).HasColumnName("theater").HasMaxLength(255);
                entity.Property(e => e.Date).HasColumnName("date").HasMaxLength(100);
                entity.Property(e => e.TotalPrice).HasColumnName("total_price").HasColumnType("decimal(18,2)");
                entity.Property(e => e.CreatedAt).HasColumnName("created_at").HasDefaultValueSql("getdate()");

                entity.HasOne<Basket>()
                      .WithMany()
                      .HasForeignKey(e => e.BasketId)
                      .HasConstraintName("FK_basket_details_basket_id");
            });

            modelBuilder.Entity<Ticket>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__tickets__3213E83F87E09935");

                entity.HasOne(d => d.Basket).WithMany(p => p.Tickets).HasConstraintName("FK__tickets__basket___4F7CD00D");

                entity.HasOne(d => d.Session).WithMany(p => p.Tickets).HasConstraintName("FK__tickets__session__4E88ABD4");

                entity.HasOne(d => d.User).WithMany(p => p.Tickets).HasConstraintName("FK__tickets__user_id__4D94879B");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__users__3213E83FB558B3A0");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
