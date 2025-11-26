using Domain.Entities;

namespace Analysis.Application.Interfaces
{
    public interface IContentHashRepository
    {
        Task<ContentHashEntry?> FindEarlierSubmissionAsync(
            string hash,
            string excludingStudentId,
            DateTime submittedAt);
    
        Task AddAsync(ContentHashEntry entry);
    }
}