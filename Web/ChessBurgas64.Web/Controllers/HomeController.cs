namespace ChessBurgas64.Web.Controllers
{
    using System.Diagnostics;

    using ChessBurgas64.Data.Common.Repositories;
    using ChessBurgas64.Data.Models;
    using ChessBurgas64.Web.ViewModels;

    using Microsoft.AspNetCore.Mvc;

    public class HomeController : BaseController
    {
        private readonly IDeletableEntityRepository<Announcement> announcementsRepository;
        private readonly IDeletableEntityRepository<Category> categoriesRepository;
        private readonly IDeletableEntityRepository<Group> groupsRepository;
        private readonly IDeletableEntityRepository<Image> imagesRepository;
        private readonly IDeletableEntityRepository<Lesson> lessonsRepository;
        private readonly IDeletableEntityRepository<Member> membersRepository;
        private readonly IDeletableEntityRepository<Payment> paymentsRepository;
        private readonly IDeletableEntityRepository<Puzzle> puzzlesRepository;
        private readonly IDeletableEntityRepository<Tournament> tournamentsRepository;
        private readonly IDeletableEntityRepository<Trainer> trainersRepository;

        public HomeController(
            IDeletableEntityRepository<Announcement> announcementsRepository,
            IDeletableEntityRepository<Category> categoriesRepository,
            IDeletableEntityRepository<Group> groupsRepository,
            IDeletableEntityRepository<Image> imagesRepository,
            IDeletableEntityRepository<Lesson> lessonsRepository,
            IDeletableEntityRepository<Member> membersRepository,
            IDeletableEntityRepository<Payment> paymentsRepository,
            IDeletableEntityRepository<Puzzle> puzzlesRepository,
            IDeletableEntityRepository<Tournament> tournamentsRepository,
            IDeletableEntityRepository<Trainer> trainersRepository)
        {
            this.announcementsRepository = announcementsRepository;
            this.categoriesRepository = categoriesRepository;
            this.groupsRepository = groupsRepository;
            this.imagesRepository = imagesRepository;
            this.lessonsRepository = lessonsRepository;
            this.membersRepository = membersRepository;
            this.paymentsRepository = paymentsRepository;
            this.puzzlesRepository = puzzlesRepository;
            this.tournamentsRepository = tournamentsRepository;
            this.trainersRepository = trainersRepository;
        }

        public IActionResult Index()
        {
            return this.View();
        }

        public IActionResult Privacy()
        {
            return this.View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return this.View(
                new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }
    }
}
