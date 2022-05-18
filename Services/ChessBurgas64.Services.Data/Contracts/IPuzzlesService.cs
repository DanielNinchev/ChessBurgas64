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

        Task<ICollection<T>> GetAllAsync<T>(int page, int itemsPerPage);

        Task<Puzzle> GetByIdAsync(int id);

        Task<T> GetByIdAsync<T>(int id);

        Task<int> GetCountAsync();

        Task<ICollection<T>> GetSearchedAsync<T>(IEnumerable<int> categoryIds, string searchText);

        Task<Puzzle> UpdateAsync(int id, PuzzleInputModel input);
    }
}
