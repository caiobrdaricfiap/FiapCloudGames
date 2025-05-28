using FiapCloudGamesWebAPI.Application.DTOs.Game;
using FiapCloudGamesWebAPI.Application.Services;
using FiapCloudGameWebAPI.Domain.Interfaces.Repositories;
using FiapCloudGameWebAPI.Models;
using Moq;
using Xunit;
using FluentAssertions;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;


namespace FiapCloudGameWebAPI.Tests
{
    public class GameServiceTests
    {
        [Fact(DisplayName = "Deve cadastrar um jogo válido com sucesso")]
        public async Task DeveCadastrarJogoValido()
        {
            var repoMock = new Mock<IGameRepository>();
            repoMock.Setup(r => r.AddAsync(It.IsAny<GameModel>()))
                .ReturnsAsync((GameModel g) => { g.Id = 1; return g; });

            var service = new GameService(repoMock.Object);
            var dto = new GameCreateDto
            {
                Name = "Tom Clancy's Rainbow Six Siege",
                Gender = "FPS",
                ReleaseDate = new DateTime(2015, 5, 19)
            };

            var result = await service.CreateAsync(dto);

            result.Should().NotBeNull();
            result.Id.Should().Be(1);
            result.Name.Should().Be("Tom Clancy's Rainbow Six Siege");
            result.Gender.Should().Be("FPS");
            result.ReleaseDate.Should().Be(new DateTime(2015, 5, 19));
        }

        [Fact(DisplayName = "Deve retornar null ao buscar jogo inexistente")]
        public async Task GetById_JogoNaoExiste_DeveRetornarNull()
        {
            var repoMock = new Mock<IGameRepository>();
            repoMock.Setup(r => r.GetAsync(It.IsAny<int>())).ReturnsAsync((GameModel?)null);

            var service = new GameService(repoMock.Object);

            var result = await service.GetByIdAsync(99);

            result.Should().BeNull();
        }

        [Fact(DisplayName = "Deve atualizar um jogo existente com sucesso")]
        public async Task DeveAtualizarJogoValido()
        {
            var repoMock = new Mock<IGameRepository>();
            var existing = new GameModel { Id = 2, Name = "Old Name", Gender = "Old", ReleaseDate = new DateTime(2020, 1, 1) };
            repoMock.Setup(r => r.GetAsync(existing.Id)).ReturnsAsync(existing);
            repoMock.Setup(r => r.UpdateAsync(It.IsAny<GameModel>())).ReturnsAsync((GameModel g) => g);

            var service = new GameService(repoMock.Object);
            var dto = new GameUpdateDto
            {
                Name = "New Name",
                Gender = "New",
                ReleaseDate = new DateTime(2021, 2, 2)
            };

            var updated = await service.UpdateAsync(existing.Id, dto);

            updated.Should().BeTrue();
            existing.Name.Should().Be("New Name");
            existing.Gender.Should().Be("New");
            existing.ReleaseDate.Should().Be(new DateTime(2021, 2, 2));
        }

        [Fact(DisplayName = "Deve retornar false ao tentar atualizar jogo inexistente")]
        public async Task Atualizar_JogoNaoExiste_DeveRetornarFalse()
        {
            var repoMock = new Mock<IGameRepository>();
            repoMock.Setup(r => r.GetAsync(It.IsAny<int>())).ReturnsAsync((GameModel?)null);

            var service = new GameService(repoMock.Object);
            var dto = new GameUpdateDto { Name = "Any", Gender = "Any", ReleaseDate = DateTime.Now };

            var updated = await service.UpdateAsync(999, dto);

            updated.Should().BeFalse();
        }
    }
}
