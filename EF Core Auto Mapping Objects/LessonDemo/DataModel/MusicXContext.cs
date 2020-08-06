using Microsoft.EntityFrameworkCore;

namespace DataModel
{
    using Models;

    public partial class MusicXContext : DbContext
    {
        public MusicXContext()
        {
        }

        public MusicXContext(DbContextOptions<MusicXContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ArtistMetadata> ArtistMetadata { get; set; }
        public virtual DbSet<Artist> Artists { get; set; }
        public virtual DbSet<SongArtist> SongArtists { get; set; }
        public virtual DbSet<SongMetadata> SongMetadata { get; set; }
        public virtual DbSet<Songs> Songs { get; set; }
        public virtual DbSet<Source> Sources { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer(Configuration.ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ArtistMetadata>(entity =>
            {
                entity.HasIndex(e => e.ArtistId);

                entity.HasIndex(e => e.IsDeleted);

                entity.HasIndex(e => e.SourceId);

                entity.HasOne(d => d.Artist)
                    .WithMany(p => p.ArtistMetadata)
                    .HasForeignKey(d => d.ArtistId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.Source)
                    .WithMany(p => p.ArtistMetadata)
                    .HasForeignKey(d => d.SourceId);
            });

            modelBuilder.Entity<Artist>(entity =>
            {
                entity.HasIndex(e => e.IsDeleted);
            });

            modelBuilder.Entity<SongArtist>(entity =>
            {
                entity.HasIndex(e => e.ArtistId);

                entity.HasIndex(e => e.IsDeleted);

                entity.HasIndex(e => e.SongId);

                entity.HasOne(d => d.Artist)
                    .WithMany(p => p.SongArtists)
                    .HasForeignKey(d => d.ArtistId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.Song)
                    .WithMany(p => p.SongArtists)
                    .HasForeignKey(d => d.SongId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<SongMetadata>(entity =>
            {
                entity.HasIndex(e => e.IsDeleted);

                entity.HasIndex(e => e.SongId);

                entity.HasIndex(e => e.SourceId);

                entity.HasOne(d => d.Song)
                    .WithMany(p => p.SongMetadata)
                    .HasForeignKey(d => d.SongId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.Source)
                    .WithMany(p => p.SongMetadata)
                    .HasForeignKey(d => d.SourceId);
            });

            modelBuilder.Entity<Songs>(entity =>
            {
                entity.HasIndex(e => e.IsDeleted);

                entity.HasIndex(e => e.SourceId);

                entity.Property(e => e.Name).IsRequired();

                entity.HasOne(d => d.Source)
                    .WithMany(p => p.Songs)
                    .HasForeignKey(d => d.SourceId);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
