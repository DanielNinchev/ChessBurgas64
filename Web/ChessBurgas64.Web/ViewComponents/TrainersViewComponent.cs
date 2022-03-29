namespace ChessBurgas64.Web.ViewComponents
{
    using ChessBurgas64.Services.Data.Contracts;
    using ChessBurgas64.Web.ViewModels.Groups;
    using Microsoft.AspNetCore.Mvc;

    public class TrainersViewComponent : ViewComponent
    {
        private readonly ITrainersService trainersService;

        public TrainersViewComponent(ITrainersService trainersService)
        {
            this.trainersService = trainersService;
        }

        public IViewComponentResult Invoke(string title)
        {
            var trainers = this.trainersService.GetAllTrainersInSelectList();
            var viewModel = new GroupInputModel
            {
                Name = title,
                Trainers = trainers,
            };

            return this.View(viewModel);
        }
    }
}
