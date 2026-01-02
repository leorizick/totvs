using Totvs.Application.DTOs.In;

namespace Totvs.Application.Services
{
    public interface IVacancyService
    {
        Task<VacancyResponseDTO> GetByIdAsync(string id);
        Task<IEnumerable<VacancyResponseDTO>> GetAllAsync();
        Task<VacancyResponseDTO> CreateAsync(VacancyRequestDTO requestDTO);
        Task UpdateAsync(string id, VacancyRequestDTO requestDTO);
        Task DeleteAsync(string id);
    }
}
