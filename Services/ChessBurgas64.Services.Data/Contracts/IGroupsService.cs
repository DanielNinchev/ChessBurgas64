namespace ChessBurgas64.Services.Data.Contracts
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using ChessBurgas64.Data.Models;
    using ChessBurgas64.Web.ViewModels.Groups;
    using Microsoft.AspNetCore.Mvc.Rendering;

    public interface IGroupsService
    {
        Task CreateAsync(GroupInputModel input);

        Task DeleteAsync(string id);

        IEnumerable<SelectListItem> GetAllTrainerGroups(string trainerId);

        T GetById<T>(string id);

        IEnumerable<T> GetTableData<T>(string sortColumn, string sortColumnDirection, string searchValue);

        IQueryable<Group> GetUserGroupsTableData(string userId);

        Task InitializeGroupProperties(string groupId);

        void InitializeHighestClubRatingInGroup(List<GroupMember> groupMembers, Group group);

        void InitializeLowestClubRatingInGroup(List<GroupMember> groupMembers, Group group);

        Task UpdateAsync(string id, GroupInputModel input);
    }
}
