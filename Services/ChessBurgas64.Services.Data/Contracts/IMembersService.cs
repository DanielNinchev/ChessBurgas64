namespace ChessBurgas64.Services.Data.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using ChessBurgas64.Data.Models;
    using ChessBurgas64.Web.ViewModels.GroupMembers;
    using ChessBurgas64.Web.ViewModels.Members;
    using Microsoft.AspNetCore.Mvc.Rendering;

    public interface IMembersService
    {
        Task AddMemberToGroupAsync(string groupId, GroupMemberInputModel input);

        Task DeleteGroupMemberAsync(string groupId, string memberId);

        IEnumerable<SelectListItem> GetAllGroupMembers(string groupId);

        IEnumerable<SelectListItem> GetAllMembers();

        IEnumerable<SelectListItem> GetAllMembersWhichAreNotInCurrentGroup(string groupId);

        Member GetMemberById(string memberId);

        IEnumerable<SelectListItem> GetNecessaryMembers(string groupId);

        T GetByUserId<T>(string userId);

        IEnumerable<T> GetTableData<T>(string groupId, string sortColumn, string sortColumnDirection, string searchValue);

        Task SaveMemberChangesAsync(Member member);

        Task UpdateAsync(string userId, MemberInputModel input);
    }
}
