using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Totvs.Application.DTOs.In;
using Totvs.Application.Services;
using Totvs.Domain.Entities;
using Totvs.Domain.Exceptions;
using Totvs.Infrastructure.Repositories;

namespace Tests
{
    public class CandidateServiceTests
    {
        private readonly Mock<ICandidateRepository> _mock;
        private readonly CandidateService _service;

        public CandidateServiceTests()
        {
            _mock = new Mock<ICandidateRepository>();
            _service = new CandidateService(_mock.Object);
        }


        [Fact]
        public async Task CreateAsync_ShouldCreateCandidate_AndReturnResponse()
        {
            var request = new CandidateRequestDTO
            {
                Name = "Teste123",
                Email = "Teste123@email.com"
            };

            var result = await _service.CreateAsync(request);

            result.Should().NotBeNull();
            result.Name.Should().Be("Teste123");
            result.Email.Should().Be("Teste123@email.com");

            _mock.Verify(
                r => r.CreateAsync(It.IsAny<Candidate>()),
                Times.Once);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldThrowEntityNotFound_WhenCandidateDoesNotExist()
        {
            _mock
                .Setup(r => r.GetByIdAsync(It.IsAny<string>()))
                .ReturnsAsync((Candidate)null);

            var act = async () => await _service.GetByIdAsync("123");

            await act.Should()
                .ThrowAsync<EntityNotFoundException>();
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnCandidate_WhenExists()
        {
            var candidate = new Candidate("Teste123", "Teste123@email.com");

            _mock
                .Setup(r => r.GetByIdAsync(It.IsAny<string>()))
                .ReturnsAsync(candidate);

            var result = await _service.GetByIdAsync("123");

            result.Name.Should().NotBeNull(result.Name);
            result.Name.Should().Be("Teste123");
            result.Email.Should().NotBeNull(result.Email);
            result.Email.Should().Be("Teste123@email.com");
        }

        [Fact]
        public async Task UpdateAsync_ShouldUpdateCandidate_WhenExists()
        {
            var candidate = new Candidate("Teste123", "Teste123@email.com");

            _mock
                .Setup(r => r.GetByIdAsync("123"))
                .ReturnsAsync(candidate);

            var request = new CandidateRequestDTO
            {
                Name = "New Name",
                Email = "new@email.com"
            };

            await _service.UpdateAsync("123", request);

            _mock.Verify(
                r => r.UpdateAsync("123", candidate),
                Times.Once);
        }

        [Fact]
        public async Task UpdateAsync_ShouldThrowEntityNotFound_WhenCandidateDoesNotExist()
        {
            _mock
                .Setup(r => r.GetByIdAsync(It.IsAny<string>()))
                .ReturnsAsync((Candidate)null);

            var request = new CandidateRequestDTO
            {
                Name = "Name",
                Email = "email@email.com"
            };

            var act = async () => await _service.UpdateAsync("123", request);

            await act.Should()
                .ThrowAsync<EntityNotFoundException>();
        }

        [Fact]
        public async Task UpdateResumeAsync_ShouldUpdateResume_WhenCandidateExists()
        {
            var candidate = new Candidate("John", "john@email.com");

            _mock
                .Setup(r => r.GetByIdAsync("123"))
                .ReturnsAsync(candidate);

            var request = new ResumeRequestDTO
            {
                Description = "My resume"
            };

            await _service.UpdateResumeAsync("123", request);

            candidate.Resume.Should().NotBeNull();
            candidate.Resume.Description.Should().Be("My resume");

            _mock.Verify(
                r => r.UpdateAsync("123", candidate),
                Times.Once);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnAllCandidates()
        {
            var candidates = new List<Candidate>
                {
                    new Candidate("Test123", "Test123@email.com"),
                    new Candidate("Candidate2", "Candidate2@email.com")
                };

            _mock
                .Setup(r => r.GetAllAsync())
                .ReturnsAsync(candidates);

            var result = await _service.GetAllAsync();

            result.Should().NotBeNull();
            result.Should().HaveCount(2);

            result.First().Name.Should().Be("Test123");
            result.Last().Name.Should().Be("Candidate2");

            _mock.Verify(
                r => r.GetAllAsync(),
                Times.Once);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnEmptyList_WhenNoCandidatesExist()
        {
            _mock
                .Setup(r => r.GetAllAsync())
                .ReturnsAsync(new List<Candidate>());

            var result = await _service.GetAllAsync();

            result.Should().NotBeNull();
            result.Should().BeEmpty();

            _mock.Verify(
                r => r.GetAllAsync(),
                Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_ShouldCallRepositoryDelete()
        {
            var id = "123";

            await _service.DeleteAsync(id);

            _mock.Verify(
                r => r.DeleteAsync(id),
                Times.Once);
        }




    }
}
