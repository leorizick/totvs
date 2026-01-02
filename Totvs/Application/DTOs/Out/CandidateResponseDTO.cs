namespace Totvs.Application.DTOs.In
{
    public class CandidateResponseDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public ResumeResponseDTO? Resume { get; set; }
    }
}
