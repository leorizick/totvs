using Totvs.Application.DTOs.In;
using Totvs.Domain.Entities;

namespace Totvs.Application.Mappers
{
    public static class Mapper
    {
        public static CandidateResponseDTO CandidateToCandidateResponseDTO(Candidate candidate)
        {
            return new CandidateResponseDTO
            {
                Id = candidate.Id,
                Email = candidate.Email,
                Name = candidate.Name,
            };
        }

        public static VacancyResponseDTO VacancyToVacancyResponseDTO(Vacancy vacancy)
        {
            return new VacancyResponseDTO
            {
                Id = vacancy.Id,
                Name = vacancy.Name,
                Description = vacancy.Description,
            };
        }
    }
}
