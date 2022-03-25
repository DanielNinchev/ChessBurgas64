namespace ChessBurgas64.Services.Data.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using ChessBurgas64.Web.ViewModels.GroupMembers;
    using ChessBurgas64.Web.ViewModels.Lessons;

    public interface ILessonsService
    {
        Task CreateAsync(LessonInputModel input, string userId);

        Task DeleteAsync(int id);

        T GetById<T>(int id);

        List<T> GetLessonGroupMembers<T>(int id);

        IEnumerable<T> GetTableData<T>(string userId, string sortColumn, string sortColumnDirection, string searchValue);

        Task MarkLessonMemberAttendance(int id, GroupMemberCheckboxModel model);

        Task UpdateAsync(int id, LessonInputModel input);
    }
}
