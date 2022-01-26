using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace WpfApp2
{
    public partial class CourseProj2Context : DbContext
    {
        public CourseProj2Context()
        {
        }

        public CourseProj2Context(DbContextOptions<CourseProj2Context> options)
            : base(options)
        {
        }

        public virtual DbSet<Album> Albums { get; set; }
        public virtual DbSet<AlbumTrackList> AlbumTrackLists { get; set; }
        public virtual DbSet<Artist> Artists { get; set; }
        public virtual DbSet<Group> Groups { get; set; }
        public virtual DbSet<Performer> Performers { get; set; }
        public virtual DbSet<Track> Tracks { get; set; }
        public virtual DbSet<TrackPerformer> TrackPerformers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=EXCLUSIVEDE-101\\SQLEXPRESS;Database=CourseProj2;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Cyrillic_General_CI_AS");

            modelBuilder.Entity<Album>(entity =>
            {
                entity.ToTable("Album");

                entity.Property(e => e.AlbumId)
                    .ValueGeneratedNever()
                    .HasColumnName("AlbumID");

                entity.Property(e => e.Cover)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ReleaseDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Release_Date");

                entity.Property(e => e.Title)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.TotalLength).HasColumnName("Total_Length");

                entity.Property(e => e.TrackCount).HasColumnName("Track_count");
            });

            modelBuilder.Entity<AlbumTrackList>(entity =>
            {
                entity.HasKey(e => new { e.AlbumId, e.TrackId })
                    .HasName("XPKAlbum_Track_List");

                entity.ToTable("Album_Track_List");

                entity.Property(e => e.AlbumId).HasColumnName("AlbumID");

                entity.Property(e => e.TrackId).HasColumnName("TrackID");

                entity.HasOne(d => d.Album)
                    .WithMany(p => p.AlbumTrackLists)
                    .HasForeignKey(d => d.AlbumId)
                    .HasConstraintName("R_5");

                entity.HasOne(d => d.Track)
                    .WithMany(p => p.AlbumTrackLists)
                    .HasForeignKey(d => d.TrackId)
                    .HasConstraintName("R_6");
            });

            modelBuilder.Entity<Artist>(entity =>
            {
                entity.ToTable("Artist");

                entity.Property(e => e.ArtistId)
                    .ValueGeneratedNever()
                    .HasColumnName("ArtistID");

                entity.Property(e => e.ArtistName)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("Artist_Name");

                entity.Property(e => e.Country)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.DateOfBirth)
                    .HasColumnType("datetime")
                    .HasColumnName("Date_Of_Birth");
            });

            modelBuilder.Entity<Group>(entity =>
            {
                entity.HasKey(e => new { e.PerformerId, e.ArtistId, e.DateStart })
                    .HasName("XPKGroup");

                entity.ToTable("Group");

                entity.Property(e => e.PerformerId).HasColumnName("PerformerID");

                entity.Property(e => e.ArtistId).HasColumnName("ArtistID");

                entity.Property(e => e.DateStart).HasColumnType("datetime");

                entity.Property(e => e.DateEnd).HasColumnType("datetime");

                entity.HasOne(d => d.Artist)
                    .WithMany(p => p.Groups)
                    .HasForeignKey(d => d.ArtistId)
                    .HasConstraintName("R_2");

                entity.HasOne(d => d.Performer)
                    .WithMany(p => p.Groups)
                    .HasForeignKey(d => d.PerformerId)
                    .HasConstraintName("R_1");
            });

            modelBuilder.Entity<Performer>(entity =>
            {
                entity.ToTable("Performer");

                entity.Property(e => e.PerformerId)
                    .ValueGeneratedNever()
                    .HasColumnName("PerformerID");

                entity.Property(e => e.Logo)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PerformerName)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("Performer_Name");
            });

            modelBuilder.Entity<Track>(entity =>
            {
                entity.ToTable("Track");

                entity.Property(e => e.TrackId)
                    .ValueGeneratedNever()
                    .HasColumnName("TrackID");

                entity.Property(e => e.Lenght)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Rating)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Title)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.TrackFile)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Track_File");

                entity.Property(e => e.TrackLength).HasColumnName("Track_Length");
            });

            modelBuilder.Entity<TrackPerformer>(entity =>
            {
                entity.HasKey(e => new { e.TrackId, e.PerformerId })
                    .HasName("XPKTrack_Performer");

                entity.ToTable("Track_Performer");

                entity.Property(e => e.TrackId).HasColumnName("TrackID");

                entity.Property(e => e.PerformerId).HasColumnName("PerformerID");

                entity.HasOne(d => d.Performer)
                    .WithMany(p => p.TrackPerformers)
                    .HasForeignKey(d => d.PerformerId)
                    .HasConstraintName("R_9");

                entity.HasOne(d => d.Track)
                    .WithMany(p => p.TrackPerformers)
                    .HasForeignKey(d => d.TrackId)
                    .HasConstraintName("R_8");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
