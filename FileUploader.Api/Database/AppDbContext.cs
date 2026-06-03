using FileUploader.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace FileUploader.Api.Database
{
    public sealed class AppDbContext(DbContextOptions options) : DbContext(options)
    {
        public DbSet<StoredFile> StoredFiles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<StoredFile>().HasKey(sf => sf.Id);

            modelBuilder.Entity<StoredFile>().Property(sf => sf.Id)
                .HasMaxLength(500);

            modelBuilder.Entity<StoredFile>().Property(sf => sf.OriginalFileName)
                .IsRequired()
                .HasMaxLength(500);

            modelBuilder.Entity<StoredFile>().Property(sf => sf.BlobName)
                .IsRequired()
                .HasMaxLength(500);

            modelBuilder.Entity<StoredFile>().Property(sf => sf.ContentType)
                .HasMaxLength(50);

            modelBuilder.Entity<StoredFile>().Property(sf => sf.FileSize)
                .HasPrecision(10);
        }
    }
}
