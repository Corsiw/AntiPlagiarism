using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Analysis.Infrastructure.Data
{
    public class AnalysisDbContext(DbContextOptions<AnalysisDbContext> options) : DbContext(options)
    {
        public DbSet<WorkAnalysis> Analysis => Set<WorkAnalysis>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<WorkAnalysis>(entity =>
            {
                entity.HasKey(a => a.AnalysisId);

                entity.Property(a => a.WorkId)
                    .HasMaxLength(64)
                    .IsRequired();

                entity.Property(a => a.FileId)
                    .HasMaxLength(64)
                    .IsRequired();

                entity.Property(a => a.ReportFileId)
                    .HasMaxLength(64);

                entity.Property(a => a.CreatedAt)
                    .IsRequired();

                entity.Property(a => a.PlagiarismFlag);

                // Индексы
                entity.HasIndex(a => a.AnalysisId);
                entity.HasIndex(a => a.WorkId);
            });
        }
    }
}
