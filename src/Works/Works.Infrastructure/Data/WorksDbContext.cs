using Microsoft.EntityFrameworkCore;
using Domain.Entities;

namespace Infrastructure.Data
{
    public class WorksDbContext(DbContextOptions<WorksDbContext> options) : DbContext(options)
    {
        public DbSet<Work> Works => Set<Work>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Work>(entity =>
            {
                entity.HasKey(w => w.WorkId);

                entity.Property(w => w.WorkId)
                      .IsRequired();

                entity.Property(w => w.StudentId)
                      .HasMaxLength(64)
                      .IsRequired();

                entity.Property(w => w.AssignmentId)
                      .HasMaxLength(64)
                      .IsRequired();

                entity.Property(w => w.SubmissionTime)
                      .IsRequired();

                entity.Property(w => w.FileId)
                      .HasMaxLength(256);

                entity.Property(w => w.Status)
                      .HasConversion<int>()   // enum → int
                      .IsRequired();

                // Индексы
                entity.HasIndex(w => w.AssignmentId);

                entity.HasIndex(w => w.StudentId);

                entity.HasIndex(w => w.Status);
            });
        }
    }
}
