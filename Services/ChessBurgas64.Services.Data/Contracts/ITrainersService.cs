namespace ChessBurgas64.Services.Data.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using ChessBurgas64.Data.Models;
    using ChessBurgas64.Web.ViewModels.Trainers;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc.Rendering;

    public interface ITrainersService
    {
        Task<Trainer> CreateAsync(TrainerInputModel input, string userId, string imagePath);

        IEnumerable<SelectListItem> GetAllTrainersInSelectList();

        IEnumerable<T> GetAllTrainersForPublicView<T>(int page, int itemsPerPage);

        T GetById<T>(string id);

        int GetCount();

        Task UpdateAsync(string id, TrainerInputModel input, string imagePath);
    }
}
