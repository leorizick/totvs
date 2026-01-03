using System.ComponentModel.DataAnnotations;

namespace Totvs.Application.DTOs.In
{
    public class ResumeRequestDTO
    {
        [Required(ErrorMessage = "Resume is required")]
        public string Description { get; set; }
    }
}
