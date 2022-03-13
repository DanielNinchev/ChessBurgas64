namespace ChessBurgas64.Services.Data.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using ChessBurgas64.Web.ViewModels.Lessons;

    public interface ILessonsService
    {
        Task CreateAsync(LessonInputModel input, string groupId);

        Task DeleteAsync(int id);

        T GetById<T>(int id);

        IEnumerable<T> GetTableData<T>(string groupId, string sortColumn, string sortColumnDirection, string searchValue);

        Task UpdateAsync(int id, LessonInputModel input);
    }
}
