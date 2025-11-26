using Analysis.Application.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Analysis.Infrastructure.Repositories
{
    public class ContentHashRepository : IContentHashRepository
    {
        private readonly DbContext _context;
        private readonly DbSet<ContentHashEntry> _dbSet;

        public ContentHashRepository(DbContext context)
        {
            _context = context;
            _dbSet = _context.Set<ContentHashEntry>();
        }

        public async Task<ContentHashEntry?> FindEarlierSubmissionAsync(string hash, string excludingStudentId,
            DateTime submittedAt)
        {
            return await _dbSet.Where(e =>
                    e.Hash == hash
                    && e.SubmittedAt < submittedAt
                    && e.StudentId != excludingStudentId)
                .FirstOrDefaultAsync();
        }

        public async Task AddAsync(ContentHashEntry entry)
        {
            await _dbSet.AddAsync(entry);
            await _context.SaveChangesAsync();
        }
    }
}