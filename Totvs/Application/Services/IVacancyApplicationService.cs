using Totvs.Application.DTOs.In;
using Totvs.Domain.Entities;

namespace Totvs.Application.Services
{
    public interface IVacancyApplicationService
    {
        Task<IEnumerable<CandidateResponseDTO>> GetCandidatesAppliedToVacancyAsync(string vacancyId);
        Task ApplyCandidateAsync(string vacancyId, string candidateId);
    }
}
