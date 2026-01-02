using Microsoft.AspNetCore.Mvc;
using Totvs.Application.DTOs.In;
using Totvs.Application.Services;
using Totvs.Domain.Entities;
using Totvs.Infrastructure.Repositories;

namespace Totvs.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CandidateController : ControllerBase
    {

        private readonly ICandidateService _service;

        public CandidateController(ICandidateService candidateService)
        {
            _service = candidateService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var response = await _service.GetAllAsync();
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var response = await _service.GetByIdAsync(id);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CandidateRequestDTO request)
        {
            var response = await _service.CreateAsync(request);
            return CreatedAtAction(nameof(GetById), new { id = response.Id }, response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] CandidateRequestDTO request)
        {
            await _service.UpdateAsync(id, request);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _service.DeleteAsync(id);

            return NoContent();
        }
    }
}
