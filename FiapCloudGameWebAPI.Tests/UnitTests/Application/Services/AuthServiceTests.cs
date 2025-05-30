using Xunit;
using Moq;
using FluentAssertions;
using System.Threading.Tasks;
using FiapCloudGamesWebAPI.Application.DTOs.Auth;
using FiapCloudGamesWebAPI.Application.Services;
using FiapCloudGameWebAPI.Domain.Interfaces.Repositories;
using FiapCloudGameWebAPI.Models;
using Microsoft.Extensions.Configuration;
using FiapCloudGames.Domain.Enums;
using FiapCloudGameWebAPI.Domain.Utils;

namespace FiapCloudGameWebAPI.Tests.UnitTests.Application.Services
{
    public class AuthServiceTests
    {
        [Fact(DisplayName = "Deve autenticar usuário com sucesso e retornar token")]
        public async Task Login_DeveAutenticarUsuarioValido()
        {
            var mockRepo = new Mock<IUserRepository>();
            var mockConfig = new Mock<IConfiguration>();

            var salt = CriptografiaUtils.GeraSalt(12);
            var hash = CriptografiaUtils.HashPassWord("Senha@123", salt);

            var user = new UserModel
            {
                Id = 1,
                Name = "Teste",
                Email = "teste@email.com",
                Salt = salt,
                Role = UserRole.User,
                HashPassword = hash
            };

            mockRepo.Setup(r => r.GetByEmailAsync(user.Email)).ReturnsAsync(user);
            mockConfig.Setup(c => c["Jwt:Key"]).Returns("12345678901234567890123456789012");
            mockConfig.Setup(c => c["Jwt:Issuer"]).Returns("IssuerTest");
            mockConfig.Setup(c => c["Jwt:Audience"]).Returns("AudienceTest");

            var service = new AuthService(mockRepo.Object, mockConfig.Object);
            var dto = new LoginRequestDto { Email = user.Email, Password = "Senha@123" };

            // Act
            var result = await service.LoginAsync(dto);

            // Assert
            result.Should().NotBeNull();
            result.Token.Should().NotBeNullOrWhiteSpace();
            result.Name.Should().Be(user.Name);
            result.Email.Should().Be(user.Email);
            result.Role.Should().Be(user.Role.ToString());
        }

        [Fact(DisplayName = "Não deve autenticar usuário não existente")]
        public async Task Login_DeveFalhar_UsuarioNaoEncontrado()
        {
            var mockRepo = new Mock<IUserRepository>();
            var mockConfig = new Mock<IConfiguration>();

            mockRepo.Setup(r => r.GetByEmailAsync(It.IsAny<string>())).ReturnsAsync((UserModel?)null);

            var service = new AuthService(mockRepo.Object, mockConfig.Object);
            var dto = new LoginRequestDto { Email = "naoexiste@email.com", Password = "Senha@123" };

            // Act
            var act = async () => await service.LoginAsync(dto);

            // Assert
            await act.Should().ThrowAsync<UnauthorizedAccessException>()
                .WithMessage("Usuário ou senha inválidos.");
        }

        [Fact(DisplayName = "Não deve autenticar usuário com senha incorreta")]
        public async Task Login_DeveFalhar_SenhaIncorreta()
        {
            var mockRepo = new Mock<IUserRepository>();
            var mockConfig = new Mock<IConfiguration>();

            var salt = CriptografiaUtils.GeraSalt(12);
            var hash = CriptografiaUtils.HashPassWord("Senha@123", salt);

            var user = new UserModel
            {
                Id = 1,
                Name = "Teste",
                Email = "teste@email.com",
                Salt = salt,
                Role = UserRole.User,
                HashPassword = hash
            };

            mockRepo.Setup(r => r.GetByEmailAsync(user.Email)).ReturnsAsync(user);

            var service = new AuthService(mockRepo.Object, mockConfig.Object);
            var dto = new LoginRequestDto { Email = user.Email, Password = "SenhaErrada" };

            // Act
            var act = async () => await service.LoginAsync(dto);

            // Assert
            await act.Should().ThrowAsync<UnauthorizedAccessException>()
                .WithMessage("Usuário ou senha inválidos.");
        }
    }
}
