using FiapCloudGamesWebAPI.Application.DTOs.Game;
using FiapCloudGameWebAPI.Domain.Interfaces.Repositories;
using FiapCloudGameWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiapCloudGamesWebAPI.Application.Services
{
    public class GameService
    {
        private readonly IGameRepository _gameRepository;

        public GameService(IGameRepository gameRepository)
        {
            _gameRepository = gameRepository;
        }

        public async Task<List<GameResponseDto>> GetAllAsync()
        {
            var games = await _gameRepository.GetByConditionAsync(g => true);

            return games.Select(g => new GameResponseDto
            {
                Id = g.Id,
                Name = g.Name,
                Gender = g.Gender,
                ReleaseDate = g.ReleaseDate
            }).ToList();
        }

        public async Task<GameResponseDto?> GetByIdAsync(int id)
        {
            var game = await _gameRepository.GetAsync(id);
            if (game == null) return null;

            return new GameResponseDto
            {
                Id = game.Id,
                Name = game.Name,
                Gender = game.Gender,
                ReleaseDate = game.ReleaseDate
            };
        }

        public async Task<GameResponseDto> CreateAsync(GameCreateDto dto)
        {
            var game = new GameModel
            {
                Name = dto.Name,
                Gender = dto.Gender,
                ReleaseDate = dto.ReleaseDate
            };

            await _gameRepository.AddAsync(game);

            return new GameResponseDto
            {
                Id = game.Id,
                Name = game.Name,
                Gender = game.Gender,
                ReleaseDate = game.ReleaseDate
            };
        }

        public async Task<bool> UpdateAsync(int id, GameUpdateDto dto)
        {
            var game = await _gameRepository.GetAsync(id);
            if (game == null) return false;

            game.Name = dto.Name;
            game.Gender = dto.Gender;
            game.ReleaseDate = dto.ReleaseDate;

            await _gameRepository.UpdateAsync(game);
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _gameRepository.DeleteAsync(id);
        }
    }
}
