namespace ChessBurgas64.Services.Data.Contracts
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using ChessBurgas64.Data.Models;
    using ChessBurgas64.Web.ViewModels.GroupMembers;
    using ChessBurgas64.Web.ViewModels.Lessons;

    public interface ILessonsService
    {
        Task CreateAsync(LessonInputModel input, string userId);

        Task DeleteAsync(int id);

        Task<T> GetByIdAsync<T>(int id);

        Task<IEnumerable<T>> GetAllLessonsTableDataAsync<T>(string sortColumn, string sortColumnDirection, string searchValue);

        Task<IEnumerable<T>> GetGroupLessonsTableDataAsync<T>(string groupId, string sortColumn, string sortColumnDirection, string searchValue);

        Task<IEnumerable<T>> GetLessonGroupMembersAsync<T>(int id);

        Task<IEnumerable<T>> GetUserLessonsTableDataAsync<T>(string userId, string sortColumn, string sortColumnDirection, string searchValue);

        IQueryable<Lesson> GetUserLessonsTableData(string userId);

        Task MarkLessonMemberAttendanceAsync(int id, GroupMemberCheckboxModel model);

        Task UpdateAsync(int id, LessonInputModel input);
    }
}
