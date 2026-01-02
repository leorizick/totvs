using Totvs.Application.DTOs.In;

namespace Totvs.Application.Services
{
    public interface ICandidateService
    {
        Task<CandidateResponseDTO> GetByIdAsync(string id);
        Task<IEnumerable<CandidateResponseDTO>> GetAllAsync();
        Task<CandidateResponseDTO> CreateAsync(CandidateRequestDTO requestDTO);
        Task UpdateAsync(string id, CandidateRequestDTO requestDTO);
        Task DeleteAsync(string id);
    }
}
