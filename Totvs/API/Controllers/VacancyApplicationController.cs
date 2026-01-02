using Microsoft.AspNetCore.Mvc;
using Totvs.Application.DTOs.In;
using Totvs.Application.Services;
using Totvs.Domain.Entities;

namespace Totvs.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VacancyApplicationController : ControllerBase
    {
        private readonly IVacancyApplicationService _service;

        public VacancyApplicationController(IVacancyApplicationService vacancyApplicationService)
        {
            _service = vacancyApplicationService;
        }


        [HttpPost("{vacancyId}/apply/{candidateId}")]
        public async Task<IActionResult> Apply(string vacancyId, string candidateId)
        {
            await _service.ApplyCandidateAsync(vacancyId, candidateId);

            return Ok(new
            {
                message = "Applied successfully"
            });
        }

        [HttpGet("{vacancyId}/candidates")]
        public async Task<ActionResult<IEnumerable<CandidateResponseDTO>>> GetCandidates(string vacancyId)
        {
            var candidates = await _service.GetCandidatesAppliedToVacancyAsync(vacancyId);

            return Ok(candidates);
        }
    }
}
