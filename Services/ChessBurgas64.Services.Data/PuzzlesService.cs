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
            var puzzle = this.puzzlesRepository.All().FirstOrDefault(x => x.Id == id);
            this.puzzlesRepository.Delete(puzzle);
            await this.puzzlesRepository.SaveChangesAsync();
        }

        public IEnumerable<T> GetAll<T>(int page, int itemsPerPage)
        {
            var puzzles = this.puzzlesRepository.AllAsNoTracking()
                .OrderBy(x => x.Number)
                .Skip((page - 1) * itemsPerPage).Take(itemsPerPage)
                .To<T>()
                .ToList();

            return puzzles;
        }

        public T GetById<T>(int id)
        {
            var puzzle = this.puzzlesRepository.AllAsNoTracking().Where(x => x.Id == id)
                .To<T>().FirstOrDefault();

            return puzzle;
        }

        public Puzzle GetById(int id)
        {
            var puzzle = this.puzzlesRepository
                .AllAsNoTracking()
                .Where(x => x.Id == id)
                .FirstOrDefault();

            return puzzle;
        }

        public int GetCount()
        {
            return this.puzzlesRepository.AllAsNoTracking().Count();
        }

        public void InitializePuzzlePoints(Puzzle puzzle)
        {
            var puzzlePoints = (PuzzleDifficulty)Enum.Parse(typeof(PuzzleDifficulty), puzzle.Difficulty);
            puzzle.Points = (int)puzzlePoints * GlobalConstants.PuzzlePointsMultiplier;
        }

        public async Task<Puzzle> UpdateAsync(int id, PuzzleInputModel input)
        {
            var puzzle = this.puzzlesRepository.All().FirstOrDefault(x => x.Id == id);
            puzzle.CategoryId = input.CategoryId;
            puzzle.Difficulty = input.Difficulty.ToString();
            puzzle.Number = input.Number;
            puzzle.Objective = input.Objective;
            puzzle.Solution = input.Solution;

            this.InitializePuzzlePoints(puzzle);

            await this.puzzlesRepository.SaveChangesAsync();

            return puzzle;
        }
    }
}
