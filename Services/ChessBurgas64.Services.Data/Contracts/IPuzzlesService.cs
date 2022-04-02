namespace ChessBurgas64.Services.Data.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using ChessBurgas64.Data.Models;
    using ChessBurgas64.Web.ViewModels.Puzzles;

    public interface IPuzzlesService
    {
        Task<Puzzle> CreateAsync(PuzzleInputModel input, string imagePath);

        Task DeleteAsync(int id);

        IEnumerable<T> GetAll<T>(int page, int itemsPerPage);

        T GetById<T>(int id);

        Puzzle GetById(int id);

        int GetCount();

        void InitializePuzzlePoints(Puzzle puzzle);

        Task<Puzzle> UpdateAsync(int id, PuzzleInputModel input);
    }
}
