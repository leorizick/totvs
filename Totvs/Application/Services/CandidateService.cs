using Totvs.Application.DTOs.In;
using Totvs.Application.Mappers;
using Totvs.Domain.Entities;
using Totvs.Domain.Exceptions;
using Totvs.Infrastructure.Repositories;

namespace Totvs.Application.Services
{
    public class CandidateService : ICandidateService
    {
        private readonly ICandidateRepository _candidateRepository;

        public CandidateService(ICandidateRepository candidateRepository) { _candidateRepository = candidateRepository; }

        public async Task<CandidateResponseDTO> CreateAsync(CandidateRequestDTO requestDTO)
        {
            var candidate = new Candidate(requestDTO.Name, requestDTO.Email);
            await _candidateRepository.CreateAsync(candidate);
            return Mapper.CandidateToCandidateResponseDTO(candidate);
        }

        public async Task DeleteAsync(string id)
        {
            await _candidateRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<CandidateResponseDTO>> GetAllAsync()
        {
            var candidates = await _candidateRepository.GetAllAsync();
            return candidates.Select(c => Mapper.CandidateToCandidateResponseDTO(c));
        }

        public async Task<CandidateResponseDTO> GetByIdAsync(string id)
        {
            var candidate = await _candidateRepository.GetByIdAsync(id);
            return Mapper.CandidateToCandidateResponseDTO(candidate);
        }

        public async Task UpdateAsync(string id, CandidateRequestDTO requestDTO)
        {
            var candidate = await _candidateRepository.GetByIdAsync(id);
            if (candidate == null)
                throw new EntityNotFoundException("Candidate", id);

            candidate.Update(requestDTO.Name, requestDTO.Email);

            await _candidateRepository.UpdateAsync(id, candidate);
        }
    }
}
