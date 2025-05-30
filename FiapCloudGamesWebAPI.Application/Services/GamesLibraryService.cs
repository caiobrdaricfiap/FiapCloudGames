using FiapCloudGamesWebAPI.Application.DTOs.GamesLibrary;
using FiapCloudGameWebAPI.Domain.Interfaces.Repositories;
using FiapCloudGameWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiapCloudGamesWebAPI.Application.Services
{
    public class GamesLibraryService
    {
        private readonly IGamesLibraryRepository _libraryRepository;

        public GamesLibraryService(IGamesLibraryRepository libraryRepository)
        {
            _libraryRepository = libraryRepository;
        }

        public async Task<List<GamesLibraryResponseDto>> GetAllAsync()
        {
            var items = await _libraryRepository.GetByConditionAsync(x => true);
            return items.Select(item => new GamesLibraryResponseDto
            {
                Id = item.Id,
                UserId = item.UserId,
                GameId = item.GameId,
                AcquisitionDate = item.AcquisitionDate
            }).ToList();
        }

        public async Task<GamesLibraryResponseDto> AddAsync(GamesLibraryCreateDto dto)
        {
            var model = new GamesLibraryModel
            {
                UserId = dto.UserId,
                GameId = dto.GameId,
                AcquisitionDate = dto.AcquisitionDate ?? DateTime.UtcNow
            };

            await _libraryRepository.AddAsync(model);

            return new GamesLibraryResponseDto
            {
                Id = model.Id,
                UserId = model.UserId,
                GameId = model.GameId,
                AcquisitionDate = model.AcquisitionDate
            };
        }

        public async Task<List<GamesLibraryResponseDto>> GetByUserAsync(int userId)
        {
            var items = await _libraryRepository.GetByConditionAsync(x => x.UserId == userId);
            return items.Select(item => new GamesLibraryResponseDto
            {
                Id = item.Id,
                UserId = item.UserId,
                GameId = item.GameId,
                AcquisitionDate = item.AcquisitionDate
            }).ToList();
        }

        public async Task<List<GamesLibraryResponseDto>> GetByGameAsync(int gameId)
        {
            var items = await _libraryRepository.GetByConditionAsync(x => x.GameId == gameId);
            return items.Select(item => new GamesLibraryResponseDto
            {
                Id = item.Id,
                UserId = item.UserId,
                GameId = item.GameId,
                AcquisitionDate = item.AcquisitionDate
            }).ToList();
        }
    }
}
