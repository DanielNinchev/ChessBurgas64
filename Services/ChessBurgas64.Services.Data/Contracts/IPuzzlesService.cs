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

        ICollection<T> GetAll<T>(int page, int itemsPerPage);

        T GetById<T>(int id);

        Puzzle GetById(int id);

        ICollection<T> GetSearched<T>(IEnumerable<int> categoryIds, string searchText);

        int GetCount();

        void InitializePuzzlePoints(Puzzle puzzle);

        Task<Puzzle> UpdateAsync(int id, PuzzleInputModel input);
    }
}
