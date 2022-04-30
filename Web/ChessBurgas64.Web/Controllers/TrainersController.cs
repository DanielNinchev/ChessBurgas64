namespace ChessBurgas64.Web.Controllers
{
    using ChessBurgas64.Common;
    using ChessBurgas64.Services.Data.Contracts;
    using ChessBurgas64.Web.ViewModels.Trainers;
    using Ganss.XSS;
    using Microsoft.AspNetCore.Mvc;

    public class TrainersController : Controller
    {
        private readonly IHtmlSanitizer sanitizer;
        private readonly ITrainersService trainersService;

        public TrainersController(
            IHtmlSanitizer sanitizer,
            ITrainersService trainersService)
        {
            this.sanitizer = sanitizer;
            this.trainersService = trainersService;
        }

        public IActionResult All(int id = 1)
        {
            if (id <= 0)
            {
                return this.NotFound();
            }

            var viewModel = new TrainersListViewModel
            {
                ItemsPerPage = GlobalConstants.TrainersPerPage,
                PageNumber = id,
                Trainers = this.trainersService.GetAllTrainersForPublicView<TrainerViewModel>(id, GlobalConstants.AnnouncementsPerPage),
            };

            return this.View(viewModel);
        }

        public IActionResult ById(string id)
        {
            var trainer = this.trainersService.GetById<TrainerViewModel>(id);
            trainer.UserDescription = this.sanitizer.Sanitize(trainer.UserDescription);

            return this.View(trainer);
        }
    }
}
