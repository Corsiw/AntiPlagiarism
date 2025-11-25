using Microsoft.EntityFrameworkCore;

namespace Analysis.Infrastructure.Data
{
    public class AnalysisDbContext(DbContextOptions<AnalysisDbContext> options) : DbContext(options)
    {
        public DbSet<Entities.Analysis> Analysis => Set<Entities.Analysis>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Entities.Analysis>(entity =>
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
