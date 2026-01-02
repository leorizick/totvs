using Totvs.Application.DTOs.In;
using Totvs.Application.Mappers;
using Totvs.Domain.Entities;
using Totvs.Domain.Exceptions;
using Totvs.Infrastructure.Repositories;

namespace Totvs.Application.Services
{
    public class VacancyService : IVacancyService
    {
        private readonly IVacancyRepository _vacancyRepository;

        public VacancyService(IVacancyRepository vacansyRepository)
        {
            _vacancyRepository = vacansyRepository;
        }

        public async Task<VacancyResponseDTO> CreateAsync(VacancyRequestDTO requestDTO)
        {
            var vacancy = new Vacancy(requestDTO.Name, requestDTO.Description);
            await _vacancyRepository.CreateAsync(vacancy);
            return Mapper.VacancyToVacancyResponseDTO(vacancy);
        }

        public async Task DeleteAsync(string id)
        {
            await _vacancyRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<VacancyResponseDTO>> GetAllAsync()
        {
            var candidates = await _vacancyRepository.GetAllAsync();
            return candidates.Select(c => Mapper.VacancyToVacancyResponseDTO(c));
        }

        public async Task<VacancyResponseDTO> GetByIdAsync(string id)
        {
            var vacancy = await _vacancyRepository.GetByIdAsync(id);
            return Mapper.VacancyToVacancyResponseDTO(vacancy);
        }

        public async Task UpdateAsync(string id, VacancyRequestDTO requestDTO)
        {
            var vacancy = await _vacancyRepository.GetByIdAsync(id);
            if (vacancy == null)
                throw new EntityNotFoundException("Vacancy", id);

            vacancy.Update(requestDTO.Name, requestDTO.Description);

            await _vacancyRepository.UpdateAsync(id, vacancy);
        }
    }
}
