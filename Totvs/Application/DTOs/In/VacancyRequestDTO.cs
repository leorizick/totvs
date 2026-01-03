using System.ComponentModel.DataAnnotations;

namespace Totvs.Application.DTOs.In
{
    public class VacancyRequestDTO
    {
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }
    }
}
