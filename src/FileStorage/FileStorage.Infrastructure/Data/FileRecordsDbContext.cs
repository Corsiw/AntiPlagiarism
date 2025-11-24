using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FileStorage.Infrastructure.Data
{
    public class FileRecordsDbContext(DbContextOptions<FileRecordsDbContext> options) : DbContext(options)
    {
        public DbSet<FileRecord> FileRecords => Set<FileRecord>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<FileRecord>(entity =>
            {
                entity.HasKey(f => f.FileId);

                entity.Property(f => f.FileName)
                      .IsRequired();

                entity.Property(f => f.ContentType)
                      .IsRequired();

                entity.Property(f => f.Size)
                      .IsRequired();

                entity.Property(f => f.StoragePath)
                      .IsRequired();

                // Индексы
                entity.HasIndex(f => f.FileId);
            });
        }
    }
}
