using Totvs.Domain.Entities;

namespace Totvs.Infrastructure.Repositories
{
    public interface ICandidateRepository
    {
        Task<Candidate?> GetByIdAsync(string id);
        Task<IEnumerable<Candidate>> GetAllAsync();
        Task<Candidate?> CreateAsync(Candidate candidate);
        Task UpdateAsync(string  id, Candidate candidate);
        Task DeleteAsync(string id);
    }
}
