using FiapCloudGamesWebAPI.Application.DTOs.GamesLibrary;
using FiapCloudGamesWebAPI.Application.Services;
using FiapCloudGameWebAPI.Domain.Interfaces.Repositories;
using FiapCloudGameWebAPI.Models;
using Xunit;
using Moq;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiapCloudGameWebAPI.Tests
{
    public class GamesLibraryServiceTests
    {
        [Fact(DisplayName = "Deve adicionar jogo à biblioteca com sucesso")]
        public async Task DeveAdicionarJogoNaBiblioteca()
        {
            var repoMock = new Mock<IGamesLibraryRepository>();
            repoMock.Setup(r => r.AddAsync(It.IsAny<GamesLibraryModel>()))
                .ReturnsAsync((GamesLibraryModel g) => { g.Id = 10; return g; });

            var service = new GamesLibraryService(repoMock.Object);
            var dto = new GamesLibraryCreateDto
            {
                UserId = 1,
                GameId = 2,
                AcquisitionDate = new DateTime(2024, 5, 26)
            };

            // Act
            var result = await service.AddAsync(dto);

            // Assert
            result.Should().NotBeNull();
            result.Id.Should().Be(10);
            result.UserId.Should().Be(1);
            result.GameId.Should().Be(2);
            result.AcquisitionDate.Should().Be(new DateTime(2024, 5, 26));
        }

        [Fact(DisplayName = "Deve retornar biblioteca vazia se usuário não possui jogos")]
        public async Task GetByUser_UsuarioSemJogos_DeveRetornarListaVazia()
        {
            var repoMock = new Mock<IGamesLibraryRepository>();
            repoMock.Setup(r => r.GetByConditionAsync(It.IsAny<System.Linq.Expressions.Expression<Func<GamesLibraryModel, bool>>>()))
                .ReturnsAsync(new List<GamesLibraryModel>());
            var service = new GamesLibraryService(repoMock.Object);

            // Act
            var result = await service.GetByUserAsync(99);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeEmpty();
        }

        [Fact(DisplayName = "Deve retornar todos os jogos de um usuário")]
        public async Task GetByUser_DeveRetornarJogosDoUsuario()
        {
            var repoMock = new Mock<IGamesLibraryRepository>();
            var list = new List<GamesLibraryModel>
        {
            new GamesLibraryModel { Id = 1, UserId = 1, GameId = 5, AcquisitionDate = DateTime.UtcNow },
            new GamesLibraryModel { Id = 2, UserId = 1, GameId = 7, AcquisitionDate = DateTime.UtcNow }
        };
            repoMock.Setup(r => r.GetByConditionAsync(It.IsAny<System.Linq.Expressions.Expression<Func<GamesLibraryModel, bool>>>()))
                .ReturnsAsync(list);
            var service = new GamesLibraryService(repoMock.Object);

            // Act
            var result = await service.GetByUserAsync(1);

            // Assert
            result.Should().HaveCount(2);
            result[0].UserId.Should().Be(1);
            result[1].UserId.Should().Be(1);
        }

        [Fact(DisplayName = "Deve retornar todos os usuários de um jogo")]
        public async Task GetByGame_DeveRetornarUsuariosDoJogo()
        {
            var repoMock = new Mock<IGamesLibraryRepository>();
            var list = new List<GamesLibraryModel>
        {
            new GamesLibraryModel { Id = 1, UserId = 1, GameId = 5, AcquisitionDate = DateTime.UtcNow },
            new GamesLibraryModel { Id = 2, UserId = 2, GameId = 5, AcquisitionDate = DateTime.UtcNow }
        };
            repoMock.Setup(r => r.GetByConditionAsync(It.IsAny<System.Linq.Expressions.Expression<Func<GamesLibraryModel, bool>>>()))
                .ReturnsAsync(list);
            var service = new GamesLibraryService(repoMock.Object);

            // Act
            var result = await service.GetByGameAsync(5);

            // Assert
            result.Should().HaveCount(2);
            result[0].GameId.Should().Be(5);
            result[1].GameId.Should().Be(5);
        }
    }
}
