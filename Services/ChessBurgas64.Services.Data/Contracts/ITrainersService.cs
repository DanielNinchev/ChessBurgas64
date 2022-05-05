namespace ChessBurgas64.Services.Data.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using ChessBurgas64.Data.Models;
    using ChessBurgas64.Web.ViewModels.Trainers;
    using Microsoft.AspNetCore.Mvc.Rendering;

    public interface ITrainersService
    {
        Task<Trainer> CreateAsync(TrainerInputModel input, string userId, string imagePath);

        IEnumerable<SelectListItem> GetAllTrainersInSelectList();

        T GetById<T>(string id);

        Task<Trainer> UpdateAsync(string id, TrainerInputModel input, string imagePath);
    }
}
