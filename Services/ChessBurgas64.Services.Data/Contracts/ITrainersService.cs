namespace ChessBurgas64.Services.Data.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using ChessBurgas64.Web.ViewModels.Trainers;
    using Microsoft.AspNetCore.Mvc.Rendering;

    public interface ITrainersService
    {
        IEnumerable<SelectListItem> GetAllTrainers();

        T GetById<T>(string id);

        Task UpdateAsync(string id, TrainerInputModel input);
    }
}
