using Totvs.Domain.Entities;

namespace Totvs.Infrastructure.Repositories
{
    public interface IVacancyRepository
    {
        Task<Vacancy?> GetByIdAsync(string id);
        Task<IEnumerable<Vacancy>> GetAllAsync();
        Task<Vacancy?> CreateAsync(Vacancy vacancy);
        Task UpdateAsync(string  id, Vacancy vacancy);
        Task DeleteAsync(string id);
        Task ApplyCandidateAsync(string vacancyId, string candidateId);
    }
}
