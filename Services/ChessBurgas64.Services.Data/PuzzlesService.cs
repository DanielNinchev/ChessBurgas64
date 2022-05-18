namespace ChessBurgas64.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    using AutoMapper;
    using ChessBurgas64.Common;
    using ChessBurgas64.Data.Common.Repositories;
    using ChessBurgas64.Data.Models;
    using ChessBurgas64.Data.Models.Enums;
    using ChessBurgas64.Services.Data.Contracts;
    using ChessBurgas64.Services.Mapping;
    using ChessBurgas64.Web.ViewModels.Puzzles;
    using Microsoft.EntityFrameworkCore;

    public class PuzzlesService : IPuzzlesService
    {
        private readonly IDeletableEntityRepository<Puzzle> puzzlesRepository;
        private readonly IMapper mapper;

        public PuzzlesService(
            IDeletableEntityRepository<Puzzle> puzzlesRepository,
            IMapper mapper)
        {
            this.puzzlesRepository = puzzlesRepository;
            this.mapper = mapper;
        }

        public async Task<Puzzle> CreateAsync(PuzzleInputModel input, string imagePath)
        {
            var puzzle = this.mapper.Map<Puzzle>(input);
            this.InitializePuzzlePoints(puzzle);

            Directory.CreateDirectory(imagePath);

            await this.puzzlesRepository.AddAsync(puzzle);
            await this.puzzlesRepository.SaveChangesAsync();

            return puzzle;
        }

        public async Task DeleteAsync(int id)
        {
            var puzzle = await this.puzzlesRepository
                .All()
                .FirstOrDefaultAsync(x => x.Id == id);

            this.puzzlesRepository.Delete(puzzle);
            await this.puzzlesRepository.SaveChangesAsync();
        }

        public async Task<ICollection<T>> GetAllAsync<T>(int page, int itemsPerPage)
        {
            var puzzles = await this.puzzlesRepository
                .AllAsNoTracking()
                .OrderBy(x => x.Number)
                .Skip((page - 1) * itemsPerPage).Take(itemsPerPage)
                .To<T>()
                .ToListAsync();

            return puzzles;
        }

        public async Task<T> GetByIdAsync<T>(int id)
        {
            var puzzle = await this.puzzlesRepository
                .AllAsNoTracking()
                .Where(x => x.Id == id)
                .To<T>()
                .FirstOrDefaultAsync();

            return puzzle;
        }

        public async Task<Puzzle> GetByIdAsync(int id)
        {
            var puzzle = await this.puzzlesRepository
                .AllAsNoTracking()
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();

            return puzzle;
        }

        public async Task<int> GetCountAsync()
        {
            return await this.puzzlesRepository.AllAsNoTracking().CountAsync();
        }

        public async Task<ICollection<T>> GetSearchedAsync<T>(IEnumerable<int> categoryIds, string searchText)
        {
            var puzzles = this.puzzlesRepository
                .All()
                .OrderByDescending(x => x.CreatedOn)
                .AsQueryable();

            if (categoryIds.Any() && searchText != null)
            {
                foreach (var categoryId in categoryIds)
                {
                    puzzles = puzzles.Where(x => categoryIds.Any(id => id == x.CategoryId)
                                                        && (x.Number.Equals(searchText)
                                                            || searchText.ToLower().Contains(x.Category.Name.ToLower())
                                                            || searchText.ToLower().Contains(x.Difficulty.ToLower())
                                                            || searchText.ToLower().Contains(x.Objective.ToLower())
                                                            || searchText.ToLower().Contains(x.Solution.ToLower())
                                                            || x.Category.Name.ToLower().Contains(searchText.ToLower())
                                                            || x.Difficulty.ToLower().Contains(searchText.ToLower())
                                                            || x.Objective.ToLower().Contains(searchText.ToLower())
                                                            || x.Solution.ToLower().Contains(searchText.ToLower())));
                }
            }
            else if (!categoryIds.Any() && searchText != null)
            {
                puzzles = puzzles.Where(x => x.Number.Equals(searchText)
                                                           || searchText.ToLower().Contains(x.Category.Name.ToLower())
                                                           || searchText.ToLower().Contains(x.Difficulty.ToLower())
                                                           || searchText.ToLower().Contains(x.Objective.ToLower())
                                                           || searchText.ToLower().Contains(x.Solution.ToLower())
                                                           || x.Category.Name.ToLower().Contains(searchText.ToLower())
                                                           || x.Difficulty.ToLower().Contains(searchText.ToLower())
                                                           || x.Objective.ToLower().Contains(searchText.ToLower())
                                                           || x.Solution.ToLower().Contains(searchText.ToLower()));
            }
            else if (categoryIds.Any() && searchText == null)
            {
                foreach (var categoryId in categoryIds)
                {
                    puzzles = puzzles.Where(x => categoryIds.Any(id => id == x.CategoryId));
                }
            }
            else
            {
                return null;
            }

            return await puzzles.To<T>().ToListAsync();
        }

        public async Task<Puzzle> UpdateAsync(int id, PuzzleInputModel input)
        {
            var puzzle = await this.puzzlesRepository
                .All()
                .FirstOrDefaultAsync(x => x.Id == id);

            puzzle.CategoryId = input.CategoryId;
            puzzle.Difficulty = input.Difficulty.ToString();
            puzzle.Number = input.Number;
            puzzle.Objective = input.Objective;
            puzzle.Solution = input.Solution;

            this.InitializePuzzlePoints(puzzle);

            await this.puzzlesRepository.SaveChangesAsync();

            return puzzle;
        }

        private void InitializePuzzlePoints(Puzzle puzzle)
        {
            var puzzlePoints = (PuzzleDifficulty)Enum.Parse(typeof(PuzzleDifficulty), puzzle.Difficulty);
            puzzle.Points = (int)puzzlePoints * GlobalConstants.PuzzlePointsMultiplier;
        }
    }
}
