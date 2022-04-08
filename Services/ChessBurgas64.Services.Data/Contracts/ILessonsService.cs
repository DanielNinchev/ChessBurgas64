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

        T GetById<T>(int id);

        IEnumerable<T> GetAllLessonsTableData<T>(string sortColumn, string sortColumnDirection, string searchValue);

        IEnumerable<T> GetGroupLessonsTableData<T>(string groupId, string sortColumn, string sortColumnDirection, string searchValue);

        List<T> GetLessonGroupMembers<T>(int id);

        IEnumerable<T> GetTrainerLessonsTableData<T>(string userId, string sortColumn, string sortColumnDirection, string searchValue);

        IQueryable<Lesson> GetUserLessonsTableData(string userId);

        Task MarkLessonMemberAttendance(int id, GroupMemberCheckboxModel model);

        Task UpdateAsync(int id, LessonInputModel input);
    }
}
