using MongoDB.Driver;
using Totvs.Application.DTOs.In;
using Totvs.Application.Mappers;
using Totvs.Domain.Entities;
using Totvs.Domain.Exceptions;
using Totvs.Infrastructure.Repositories;

namespace Totvs.Application.Services
{
    public class VacancyApplicationService : IVacancyApplicationService
    {
        private readonly ICandidateRepository _candidateRepository;
        private readonly IVacancyRepository _vacancyRepository;

        public VacancyApplicationService(ICandidateRepository candidateRepository, IVacancyRepository vacancyRepository)
        {
            _candidateRepository = candidateRepository;
            _vacancyRepository = vacancyRepository;
        }

        public async Task ApplyCandidateAsync(string vacancyId, string candidateId)
        {
            var vacancy = await _vacancyRepository.GetByIdAsync(vacancyId);

            if (vacancy == null)
                throw new EntityNotFoundException("Vacancy", vacancyId);

            var candidate = await _candidateRepository.GetByIdAsync(candidateId);

            if (candidate == null)
                throw new EntityNotFoundException("Candidate", candidateId);

            await _vacancyRepository.ApplyCandidateAsync(vacancyId, candidateId);
        }

        public async Task<IEnumerable<CandidateResponseDTO>> GetCandidatesAppliedToVacancyAsync(string vacancyId)
        {
            var vacancy = await _vacancyRepository.GetByIdAsync(vacancyId);
            if (vacancy == null)
                throw new EntityNotFoundException("Vacancy", vacancyId);

            if (!vacancy.CandidateIds.Any())
                return [];

            var candidates = await _candidateRepository.GetManyByIdsAsync(vacancy.CandidateIds);
            return candidates.Select(Mapper.CandidateToCandidateResponseDTO);
        }
    }
}
