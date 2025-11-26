using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Analysis.Infrastructure.Data
{
    public class AnalysisDbContext(DbContextOptions<AnalysisDbContext> options) : DbContext(options)
    {
        public DbSet<AnalysisRecord> Analysis => Set<AnalysisRecord>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<AnalysisRecord>(entity =>
            {
                entity.HasKey(a => a.AnalysisRecordId);

                entity.Property(a => a.WorkId)
                    .HasMaxLength(64)
                    .IsRequired();

                entity.Property(a => a.FileId)
                    .HasMaxLength(64)
                    .IsRequired();
                
                entity.Property(a => a.StudentId)
                    .HasMaxLength(64)
                    .IsRequired();
                
                entity.Property(a => a.AssignmentId)
                    .HasMaxLength(64)
                    .IsRequired();
                
                entity.Property(a => a.SubmittedAt)
                    .IsRequired();
                
                entity.Property(a => a.Status)
                    .HasConversion<int>()   // enum → int
                    .IsRequired();
                
                entity.Property(a => a.ReportId)
                    .HasMaxLength(64);

                // Индексы
                entity.HasIndex(a => a.AnalysisRecordId);
                entity.HasIndex(a => a.WorkId);
            });

            modelBuilder.Entity<Report>(entity =>
            {
                entity.HasKey(a => a.ReportId);

                entity.Property(a => a.AnalysisRecordId)
                    .HasMaxLength(64)
                    .IsRequired();

                entity.Property(a => a.FileId)
                    .HasMaxLength(64)
                    .IsRequired();
                
                entity.Property(a => a.IsPlagiarism)
                    .IsRequired();

                entity.Property(a => a.GeneratedAt)
                    .IsRequired();

                // Индексы
                entity.HasIndex(a => a.ReportId);
                entity.HasIndex(a => a.AnalysisRecordId);
            });
            
            modelBuilder.Entity<ContentHashEntry>(entity =>
            {
                entity.HasKey(a => a.HashEntryId);

                entity.Property(a => a.AnalysisRecordId)
                    .HasMaxLength(64)
                    .IsRequired();

                entity.Property(a => a.StudentId)
                    .HasMaxLength(64)
                    .IsRequired();
                
                entity.Property(a => a.SubmittedAt)
                    .IsRequired();

                // Индексы
                entity.HasIndex(a => a.HashEntryId);
                entity.HasIndex(a => a.AnalysisRecordId);
            });
        }
    }
}
