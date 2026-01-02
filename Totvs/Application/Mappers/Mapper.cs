using Totvs.Application.DTOs.In;
using Totvs.Domain.Entities;
using Totvs.Domain.ValueObjects;

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
                Resume = ResumeToResumeResponseDTO(candidate.Resume),
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

        public static ResumeResponseDTO ResumeToResumeResponseDTO(Resume resume)
        {
            if (resume == null)
                return null;

            return new ResumeResponseDTO
            {
                Description = resume.Description
            };
        }
    }
}
