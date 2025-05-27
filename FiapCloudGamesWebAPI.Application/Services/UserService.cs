using FiapCloudGames.Domain.Enums;
using FiapCloudGamesWebAPI.Application.DTOs.User;
using FiapCloudGameWebAPI.Domain.Interfaces.Repositories;
using FiapCloudGameWebAPI.Domain.Utils;
using FiapCloudGameWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiapCloudGamesWebAPI.Application.Services
{
    public class UserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserModel> RegisterAsync(UserRegisterDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Email) || !dto.Email.Contains("@"))
                throw new ArgumentException("E-mail inválido.");

            if (!IsPasswordStrong(dto.Password))
                throw new ArgumentException("Senha fraca. A senha deve ter no mínimo 8 caracteres, incluir números, letras e caracteres especiais.");

            var exists = await _userRepository.GetByEmailAsync(dto.Email);
            if (exists != null)
                throw new InvalidOperationException("E-mail já cadastrado.");

            if (!Enum.IsDefined(typeof(UserRole), dto.Role))
                throw new ArgumentException("Tipo de usuário inválido.");

            // Gera o salt e hash usando CriptografiaUtils
            var salt = CriptografiaUtils.GeraSalt(12);
            var hashPassword = CriptografiaUtils.HashPassWord(dto.Password, salt);

            var role = UserRole.User;
            var active = true;
            var registerDate = DateTimeHelper.BrasiliaTime();

            var user = new UserModel(
                name: dto.Name,
                email: dto.Email,
                hashPassword: hashPassword,
                active: active,
                role: dto.Role,
                registerDate: registerDate,
                games: null 
            )
            {
                Salt = salt
            };
            await _userRepository.AddAsync(user);

            return user;
        }

        public async Task<List<UserResponseDto>> GetAllAsync()
        {
            var users = await _userRepository.GetAllAsync();
            return users.Select(u => new UserResponseDto
            {
                Id = u.Id,
                Name = u.Name,
                Email = u.Email,
                Active = u.Active,
                Role = u.Role,
                RegisterDate = u.RegisterDate
            }).ToList();
        }

        public async Task<UserResponseDto?> GetByIdAsync(int id)
        {
            var user = await _userRepository.GetDetailsByIdAsync(id);
            if (user == null) return null;

            return new UserResponseDto
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Active = user.Active,
                Role = user.Role,
                RegisterDate = user.RegisterDate
            };
        }

        public async Task<UserResponseDto?> UpdateAsync(int id, UserUpdateDto dto)
        {
            var user = await _userRepository.GetDetailsByIdAsync(id);
            if (user == null) return null;

            string? hashPassword = null, salt = null;
            if (!string.IsNullOrWhiteSpace(dto.Password))
            {
                salt = CriptografiaUtils.GeraSalt(12);
                hashPassword = CriptografiaUtils.HashPassWord(dto.Password, salt);
            }

            user.Update(dto.Name, dto.Email, dto.Active, dto.Role, hashPassword, salt);

            await _userRepository.UpdateAsync(user, id); // agora só salva, não altera nada!

            return new UserResponseDto
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Active = user.Active,
                Role = user.Role,
                RegisterDate = user.RegisterDate
            };
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var user = await _userRepository.GetDetailsByIdAsync(id);
            if (user == null)
                return false;

            return await _userRepository.DeleteAsync(id);
        }

        private bool IsPasswordStrong(string password)
        {
            return password.Length >= 8 &&
                   password.Any(char.IsUpper) &&
                   password.Any(char.IsLower) &&
                   password.Any(char.IsDigit) &&
                   password.Any(ch => "!@#$%^&*()_+-=,./?><".Contains(ch));
        }
    }
}
