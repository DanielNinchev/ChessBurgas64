namespace ChessBurgas64.Services.Data.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using ChessBurgas64.Web.ViewModels.Videos;

    public interface IVideosService
    {
        Task CreateAsync(VideoInputModel input);

        Task DeleteAsync(int id);

        Task<IEnumerable<T>> GetAllAsync<T>(int page, int itemsPerPage);

        Task<T> GetByIdAsync<T>(int id);

        Task<int> GetCountAsync();

        Task<IEnumerable<T>> GetSearchedAsync<T>(IEnumerable<int> categoryIds, string searchText);

        Task UpdateAsync(int id, VideoInputModel input);
    }
}
