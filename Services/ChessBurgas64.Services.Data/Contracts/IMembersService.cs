namespace ChessBurgas64.Services.Data.Contracts
{
    using System.Threading.Tasks;

    using ChessBurgas64.Web.ViewModels.Members;

    public interface IMembersService
    {
        T GetById<T>(string id);

        Task UpdateAsync(string id, MemberInputModel input);
    }
}
