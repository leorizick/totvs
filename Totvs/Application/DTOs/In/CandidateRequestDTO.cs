using System.ComponentModel.DataAnnotations;

namespace Totvs.Application.DTOs.In
{
    public class CandidateRequestDTO
    {
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "E-mail is required")]
        [EmailAddress]
        public string Email { get; set; }
    }
}
