using AutoMapper;
using LowPolyBacklogApi.Data;
using LowPolyBacklogApi.DTOs.Game;
using LowPolyBacklogApi.DTOs.Igdb;
using LowPolyBacklogApi.Entities;
using LowPolyBacklogApi.Repositories.Interfaces;
using LowPolyBacklogApi.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LowPolyBacklogApi.Services.Implementations
{
    public class GameService : IGameService
    {
        private readonly IGameRepository _gameRepository;
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IImageService _imageService;

        private static readonly Dictionary<string, string> _genreMapper = new(StringComparer.OrdinalIgnoreCase)
        {
            { "Role-playing (RPG)", "RPG" },
            { "Platform", "Platformer" },
            { "Sport", "Sports" }
        };

        public GameService(IGameRepository gameRepository, ApplicationDbContext context, IMapper mapper, IImageService imageService)
        {
            _gameRepository = gameRepository;
            _context = context;
            _mapper = mapper;
            _imageService = imageService;
        }

        public async Task<(IEnumerable<GameResponseDto> items, int totalCount)> GetAllGamesAsync(GameQueryParameters parameters)
        {
            var (games, totalCount) = await _gameRepository.GetAllAsync(parameters);

            var gamesDto = _mapper.Map<IEnumerable<GameResponseDto>>(games);

            return (gamesDto, totalCount);
        }

        public async Task<GameDetailsResponseDto?> GetGameByIdAsync(int id)
        {
            var game = await _gameRepository.GetByIdAsync(id);

            if (game == null) return null;

            return _mapper.Map<GameDetailsResponseDto>(game);
        }

        public async Task<GameResponseDto> CreateGameAsync(GameCreateDto game)
        {
            var newGame = _mapper.Map<Game>(game);

            var realGenres = await _context.Genres
                .Where(g => game.GenreIds
                .Contains(g.Id))
                .ToListAsync();

            newGame.Genres = realGenres;

            await _gameRepository.AddAsync(newGame);

            return _mapper.Map<GameResponseDto>(newGame);

        }
        public async Task<GameResponseDto> UpdateAsync(GameUpdateDto game, int id)
        {
            var existingGame = await _gameRepository.GetByIdAsync(id);
            if (existingGame == null)
            {
                throw new KeyNotFoundException($"The Game with the ID: {id} does not exist.");
            }

            _mapper.Map(game, existingGame);


            var newRealGenres = await _context.Genres
                .Where(g => game.GenreIds
                .Contains(g.Id))
                .ToListAsync();

            existingGame.Genres = newRealGenres;

            await _gameRepository.UpdateAsync(existingGame);

            return _mapper.Map<GameResponseDto>(existingGame);

        }

        public async Task DeleteAsync(int id)
        {
            var game = await _gameRepository.GetByIdAsync(id);

            if (game == null)
            {
                throw new KeyNotFoundException($"The Game with the ID: {id} does not exist.");
            }

            await _gameRepository.DeleteAsync(game);
        }

        public async Task<GameResponseDto> ImportFromIgdbAsync(IgdbSearchResultDto igdbGame)
        {
            var exists = await _gameRepository.ExistsByIgdbIdAsync(igdbGame.IgdbId);
            if (exists)
            {
                throw new InvalidOperationException($"El juego '{igdbGame.Title}' ya se encuentra en tu catálogo.");
            }

            string? localCloudinaryUrl = null;

            if (!string.IsNullOrEmpty(igdbGame.CoverImageUrl))
            {
                localCloudinaryUrl = await _imageService.UploadImageFromUrlAsync(igdbGame.CoverImageUrl);
            }

            var localGenreIds = new List<int>();

            if (igdbGame.Genres != null && igdbGame.Genres.Any())
            {

                var translatedGenres = igdbGame.Genres
                    .Select(g => _genreMapper.TryGetValue(g, out var localName) ? localName : g)
                    .ToList();

                localGenreIds = await _context.Genres
                    .Where(g => translatedGenres.Contains(g.Name))
                    .Select(g => g.Id)
                    .ToListAsync();
            }

            var createDto = new GameCreateDto
            {
                IgdbId = igdbGame.IgdbId,
                Title = igdbGame.Title,
                Synopsis = igdbGame.Synopsis ?? "Synopsis Unavailable",
                ReleaseYear = igdbGame.ReleaseYear ?? 0,
                Developer = igdbGame.Developer ?? "Unknown",
                DiscCount = igdbGame.DiscCount,

                CoverImageUrl = localCloudinaryUrl,

                GenreIds = localGenreIds

            };

            return await CreateGameAsync(createDto);
        }

        public async Task<IEnumerable<GenreResponseDto>> GetAllGenresAsync()
        {
            var genres = await _gameRepository.GetAllGenresAsync();
            return _mapper.Map<IEnumerable<GenreResponseDto>>(genres);
        }



    }
}
