using FiapCloudGames.Domain.Enums;
using FiapCloudGamesWebAPI.Application.DTOs.User;
using FiapCloudGamesWebAPI.Application.Services;
using FiapCloudGameWebAPI.Domain.Interfaces.Repositories;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiapCloudGameWebAPI.Tests.UnitTests.Application.Services
{
    public class UserServiceTests
    {
        [Fact(DisplayName = "Não deve permitir cadastro com e-mail inválido")]
        public async Task NaoDeveCadastrarUsuarioComEmailInvalido()
        {
            var repoMock = new Mock<IUserRepository>();
            var service = new UserService(repoMock.Object);
            var dto = new UserRegisterDto { Name = "Teste", Email = "invalido", Password = "Senha@123", Role = UserRole.User };

            var ex = await Assert.ThrowsAsync<ArgumentException>(() => service.RegisterAsync(dto));
            Assert.Equal("E-mail inválido.", ex.Message);
        }

        [Fact(DisplayName = "Não deve permitir cadastro de e-mail já existente")]
        public async Task NaoDeveCadastrarUsuarioComEmailDuplicado()
        {
            var repoMock = new Mock<IUserRepository>();
            repoMock.Setup(r => r.GetByEmailAsync(It.IsAny<string>())).ReturnsAsync(new Models.UserModel());
            var service = new UserService(repoMock.Object);
            var dto = new UserRegisterDto { Name = "Teste", Email = "jaexiste@email.com", Password = "Senha@123", Role = UserRole.User };

            var ex = await Assert.ThrowsAsync<InvalidOperationException>(() => service.RegisterAsync(dto));
            Assert.Equal("E-mail já cadastrado.", ex.Message);
        }

        [Fact(DisplayName = "Deve cadastrar usuário válido com sucesso")]
        public async Task DeveCadastrarUsuarioValido()
        {
            var repoMock = new Mock<IUserRepository>();
            repoMock.Setup(r => r.GetByEmailAsync(It.IsAny<string>())).ReturnsAsync((Models.UserModel?)null);
            repoMock.Setup(r => r.AddAsync(It.IsAny<Models.UserModel>()))
                .ReturnsAsync((Models.UserModel u) => u);

            var service = new UserService(repoMock.Object);
            var dto = new UserRegisterDto { Name = "Teste", Email = "valido@email.com", Password = "Senha@123", Role = UserRole.User };

            var result = await service.RegisterAsync(dto);

            Assert.NotNull(result);
            Assert.Equal(dto.Email, result.Email);
            Assert.Equal(dto.Name, result.Name);
            Assert.True(result.Active);
            Assert.Equal(UserRole.User, result.Role);
        }

        [Fact(DisplayName = "Dado um usuário com senha fraca, deve lançar exceção")]
        public async Task CadastroComSenhaFraca_DeveFalhar()
        {
            var repoMock = new Mock<IUserRepository>();
            var service = new UserService(repoMock.Object);
            var dto = new UserRegisterDto { Name = "Teste", Email = "email@email.com", Password = "abc", Role = UserRole.User };

            var ex = await Assert.ThrowsAsync<ArgumentException>(() => service.RegisterAsync(dto));
            Assert.Equal("Senha fraca. A senha deve ter no mínimo 8 caracteres, incluir números, letras e caracteres especiais.", ex.Message);
        }
    }
}
