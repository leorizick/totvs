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
    public class VacancyServiceTests
    {
        private readonly Mock<IVacancyRepository> _mock;
        private readonly VacancyService _service;

        public VacancyServiceTests()
        {
            _mock = new Mock<IVacancyRepository>();
            _service = new VacancyService(_mock.Object);
        }

        [Fact]
        public async Task CreateAsync_ShouldCreateVacancy_AndReturnResponse()
        {
            var request = new VacancyRequestDTO
            {
                Name = "Backend Developer",
                Description = "C# and .NET"
            };

            var result = await _service.CreateAsync(request);

            result.Should().NotBeNull();
            result.Name.Should().Be("Backend Developer");
            result.Description.Should().Be("C# and .NET");

            _mock.Verify(
                r => r.CreateAsync(It.IsAny<Vacancy>()),
                Times.Once);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnAllVacancies()
        {
            var vacancies = new List<Vacancy>
            {
                new Vacancy("Backend", "C#"),
                new Vacancy("Frontend", "Angular")
            };

            _mock
                .Setup(r => r.GetAllAsync())
                .ReturnsAsync(vacancies);

            var result = await _service.GetAllAsync();

            result.Should().NotBeNull();
            result.Should().HaveCount(2);

            result.First().Name.Should().Be("Backend");
            result.Last().Name.Should().Be("Frontend");

            _mock.Verify(
                r => r.GetAllAsync(),
                Times.Once);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldThrowEntityNotFound_WhenVacancyDoesNotExist()
        {
            _mock
                .Setup(r => r.GetByIdAsync(It.IsAny<string>()))
                .ReturnsAsync((Vacancy)null);

            var act = async () => await _service.GetByIdAsync("123");

            await act.Should()
                .ThrowAsync<EntityNotFoundException>();
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnVacancy_WhenExists()
        {
            var vacancy = new Vacancy("Backend", "C#");

            _mock
                .Setup(r => r.GetByIdAsync("123"))
                .ReturnsAsync(vacancy);

            var result = await _service.GetByIdAsync("123");

            result.Name.Should().Be("Backend");
            result.Description.Should().Be("C#");
        }
        [Fact]
        public async Task UpdateAsync_ShouldUpdateVacancy_WhenExists()
        {
            var vacancy = new Vacancy("Backend", "C#");

            _mock
                .Setup(r => r.GetByIdAsync("123"))
                .ReturnsAsync(vacancy);

            var request = new VacancyRequestDTO
            {
                Name = "Backend",
                Description = "C# and MongoDB"
            };

            await _service.UpdateAsync("123", request);

            vacancy.Name.Should().Be("Backend");
            vacancy.Description.Should().Be("C# and MongoDB");

            _mock.Verify(
                r => r.UpdateAsync("123", vacancy),
                Times.Once);
        }

        [Fact]
        public async Task UpdateAsync_ShouldThrowEntityNotFound_WhenVacancyDoesNotExist()
        {
            _mock
                .Setup(r => r.GetByIdAsync(It.IsAny<string>()))
                .ReturnsAsync((Vacancy)null);

            var request = new VacancyRequestDTO
            {
                Name = "Backend",
                Description = "C#"
            };

            var act = async () => await _service.UpdateAsync("123", request);

            await act.Should()
                .ThrowAsync<EntityNotFoundException>();
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
