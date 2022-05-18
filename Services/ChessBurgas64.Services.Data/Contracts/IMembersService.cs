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

        IEnumerable<SelectListItem> GetAllGroupMembersInSelectList(string groupId);

        IEnumerable<SelectListItem> GetAllMembersInSelectList();

        Task<Member> GetMemberByIdAsync(string memberId);

        IEnumerable<SelectListItem> GetNecessaryMembersInSelectList(string groupId);

        Task<T> GetByUserIdAsync<T>(string userId);

        Task<IEnumerable<T>> GetTableData<T>(string groupId, string sortColumn, string sortColumnDirection, string searchValue);

        Task SaveMemberChangesAsync(Member member);

        Task UpdateAsync(string userId, MemberInputModel input);
    }
}
