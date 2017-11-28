using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace SL.WebExperience.Test.Web.Models
{
    public partial class AssetDbContext : DbContext
    {
        public virtual DbSet<Asset> Asset { get; set; }
        public virtual DbSet<Country> Country { get; set; }
        public virtual DbSet<MimeType> MimeType { get; set; }

        public AssetDbContext(DbContextOptions<AssetDbContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer(@"Server=tcp:rh-main.database.windows.net,1433;Initial Catalog=slweb;Persist Security Info=False;User ID=;Password=;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Asset>(entity =>
            {
                entity.Property(e => e.AssetId).ValueGeneratedNever();

                entity.Property(e => e.AssetKey).HasDefaultValueSql("(newid())");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedWhen)
                    .HasColumnType("datetimeoffset(5)")
                    .HasDefaultValueSql("(sysdatetimeoffset())");

                entity.Property(e => e.Description).HasMaxLength(500);

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.FileName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedWhen)
                    .HasColumnType("datetimeoffset(5)")
                    .HasDefaultValueSql("(sysdatetimeoffset())");

                entity.Property(e => e.Version)
                    .IsRequired()
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.Asset)
                    .HasForeignKey(d => d.CountryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Asset_Country");

                entity.HasOne(d => d.MimeType)
                    .WithMany(p => p.Asset)
                    .HasForeignKey(d => d.MimeTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Asset_MimeType");
            });

            modelBuilder.Entity<Country>(entity =>
            {
                entity.Property(e => e.CountryId).ValueGeneratedNever();

                entity.Property(e => e.CreatedWhen)
                    .HasColumnType("datetimeoffset(5)")
                    .HasDefaultValueSql("(sysdatetimeoffset())");

                entity.Property(e => e.CountryKey).HasDefaultValueSql("(newid())");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedWhen)
                    .HasColumnType("datetimeoffset(5)")
                    .HasDefaultValueSql("(sysdatetimeoffset())");

                entity.Property(e => e.Version)
                    .IsRequired()
                    .HasMaxLength(25)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<MimeType>(entity =>
            {
                entity.Property(e => e.MimeTypeId).ValueGeneratedNever();

                entity.Property(e => e.CreatedWhen)
                    .HasColumnType("datetimeoffset(5)")
                    .HasDefaultValueSql("(sysdatetimeoffset())");

                entity.Property(e => e.MimeTypeKey).HasDefaultValueSql("(newid())");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedWhen)
                    .HasColumnType("datetimeoffset(5)")
                    .HasDefaultValueSql("(sysdatetimeoffset())");

                entity.Property(e => e.Version)
                    .IsRequired()
                    .HasMaxLength(25)
                    .IsUnicode(false);
            });
        }
    }
}
