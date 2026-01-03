using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Totvs.Application.Services;
using Totvs.Domain.Entities;
using Totvs.Domain.Exceptions;
using Totvs.Infrastructure.Repositories;

namespace Tests
{
    public class VacancyApplicationServiceTests
    {
        private readonly Mock<ICandidateRepository> _candidateRepositoryMock;
        private readonly Mock<IVacancyRepository> _vacancyRepositoryMock;
        private readonly VacancyApplicationService _service;

        public VacancyApplicationServiceTests()
        {
            _candidateRepositoryMock = new Mock<ICandidateRepository>();
            _vacancyRepositoryMock = new Mock<IVacancyRepository>();

            _service = new VacancyApplicationService(
                _candidateRepositoryMock.Object,
                _vacancyRepositoryMock.Object);
        }

        [Fact]
        public async Task ApplyCandidateAsync_ShouldApplyCandidate_WhenVacancyAndCandidateExist()
        {
            var vacancy = new Vacancy("Backend", "C#");
            var candidate = new Candidate("Test123", "Test123@email.com");

            _vacancyRepositoryMock
                .Setup(r => r.GetByIdAsync("vacancy-id"))
                .ReturnsAsync(vacancy);

            _candidateRepositoryMock
                .Setup(r => r.GetByIdAsync("candidate-id"))
                .ReturnsAsync(candidate);

            await _service.ApplyCandidateAsync("vacancy-id", "candidate-id");

            _vacancyRepositoryMock.Verify(
                r => r.ApplyCandidateAsync("vacancy-id", "candidate-id"),
                Times.Once);
        }

        [Fact]
        public async Task ApplyCandidateAsync_ShouldThrowEntityNotFound_WhenVacancyDoesNotExist()
        {
            _vacancyRepositoryMock
                .Setup(r => r.GetByIdAsync(It.IsAny<string>()))
                .ReturnsAsync((Vacancy)null);

            var act = async () =>
                await _service.ApplyCandidateAsync("vacancy-id", "candidate-id");

            await act.Should()
                .ThrowAsync<EntityNotFoundException>()
                .WithMessage("*Vacancy*");
        }
        [Fact]
        public async Task ApplyCandidateAsync_ShouldThrowEntityNotFound_WhenCandidateDoesNotExist()
        {
            var vacancy = new Vacancy("Backend", "C#");

            _vacancyRepositoryMock
                .Setup(r => r.GetByIdAsync("vacancy-id"))
                .ReturnsAsync(vacancy);

            _candidateRepositoryMock
                .Setup(r => r.GetByIdAsync(It.IsAny<string>()))
                .ReturnsAsync((Candidate)null);

            var act = async () =>
                await _service.ApplyCandidateAsync("vacancy-id", "candidate-id");

            await act.Should()
                .ThrowAsync<EntityNotFoundException>()
                .WithMessage("*Candidate*");
        }
        [Fact]
        public async Task GetCandidatesAppliedToVacancyAsync_ShouldThrowEntityNotFound_WhenVacancyDoesNotExist()
        {
            _vacancyRepositoryMock
                .Setup(r => r.GetByIdAsync(It.IsAny<string>()))
                .ReturnsAsync((Vacancy)null);

            var act = async () =>
                await _service.GetCandidatesAppliedToVacancyAsync("vacancy-id");

            await act.Should()
                .ThrowAsync<EntityNotFoundException>();
        }

        [Fact]
        public async Task GetCandidatesAppliedToVacancyAsync_ShouldReturnEmpty_WhenNoCandidatesApplied()
        {
            var vacancy = new Vacancy("Backend", "C#");

            _vacancyRepositoryMock
                .Setup(r => r.GetByIdAsync("vacancy-id"))
                .ReturnsAsync(vacancy);

            var result = await _service.GetCandidatesAppliedToVacancyAsync("vacancy-id");

            result.Should().NotBeNull();
            result.Should().BeEmpty();

            _candidateRepositoryMock.Verify(
                r => r.GetManyByIdsAsync(It.IsAny<IEnumerable<string>>()),
                Times.Never);
        }

        [Fact]
        public async Task GetCandidatesAppliedToVacancyAsync_ShouldReturnCandidates_WhenExist()
        {
            var vacancy = new Vacancy("Backend", "C#");
            vacancy.CandidateIds.Add("c1");
            vacancy.CandidateIds.Add("c2");

            var candidates = new List<Candidate>
            {
                new Candidate("Test123", "Test123@email.com"),
                new Candidate("Candidate2", "Candidate2@email.com")
            };

            _vacancyRepositoryMock
                .Setup(r => r.GetByIdAsync("vacancy-id"))
                .ReturnsAsync(vacancy);

            _candidateRepositoryMock
                .Setup(r => r.GetManyByIdsAsync(vacancy.CandidateIds))
                .ReturnsAsync(candidates);

            var result = await _service.GetCandidatesAppliedToVacancyAsync("vacancy-id");

            result.Should().HaveCount(2);
            result.First().Name.Should().Be("Test123");
            result.Last().Name.Should().Be("Candidate2");

            _candidateRepositoryMock.Verify(
                r => r.GetManyByIdsAsync(vacancy.CandidateIds),
                Times.Once);
        }
    }
}
