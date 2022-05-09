namespace ChessBurgas64.Services.Data.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using ChessBurgas64.Web.ViewModels.Videos;

    public interface IVideosService
    {
        Task CreateAsync(VideoInputModel input);

        Task DeleteAsync(int id);

        IEnumerable<T> GetAll<T>(int page, int itemsPerPage);

        T GetById<T>(int id);

        int GetCount();

        IEnumerable<T> GetSearched<T>(IEnumerable<int> categoryIds, string searchText);

        Task UpdateAsync(int id, VideoInputModel input);
    }
}
