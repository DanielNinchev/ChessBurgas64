namespace ChessBurgas64.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    using AutoMapper;
    using ChessBurgas64.Common;
    using ChessBurgas64.Data.Common.Repositories;
    using ChessBurgas64.Data.Models;
    using ChessBurgas64.Data.Models.Enums;
    using ChessBurgas64.Services.Data.Contracts;
    using ChessBurgas64.Services.Mapping;
    using ChessBurgas64.Web.ViewModels.Trainers;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc.Rendering;

    public class TrainersService : ITrainersService
    {
        private readonly IDeletableEntityRepository<Image> imagesRepository;
        private readonly IDeletableEntityRepository<ApplicationUser> usersRepository;
        private readonly IDeletableEntityRepository<Trainer> trainersRepository;
        private readonly IMapper mapper;

        public TrainersService(
            IDeletableEntityRepository<Image> imagesRepository,
            IDeletableEntityRepository<ApplicationUser> usersRepository,
            IDeletableEntityRepository<Trainer> trainersRepository,
            IMapper mapper)
        {
            this.imagesRepository = imagesRepository;
            this.usersRepository = usersRepository;
            this.trainersRepository = trainersRepository;
            this.mapper = mapper;
        }

        public async Task<Trainer> CreateAsync(TrainerInputModel input, string userId, string imagePath)
        {
            var trainer = this.mapper.Map<Trainer>(input);
            trainer.UserId = userId;

            await this.trainersRepository.AddAsync(trainer);

            Directory.CreateDirectory(imagePath);

            trainer.Image = await this.InitializeTrainerImage(input.ProfilePicture, trainer, imagePath);

            await this.trainersRepository.AddAsync(trainer);
            await this.trainersRepository.SaveChangesAsync();

            return trainer;
        }

        public IEnumerable<SelectListItem> GetAllTrainersInSelectList()
        {
            var trainers = this.usersRepository.AllAsNoTracking()
                .Where(u => u.ClubStatus.Equals(ClubStatus.Треньор) && u.TrainerId != null)
                .ToList()
                .Select(tr => new
                {
                    tr.TrainerId,
                    Name = $"{tr.FirstName} {tr.LastName}",
                })
                .OrderBy(x => x.Name)
                .ToList()
                .Select(x => new SelectListItem(x.Name, x.TrainerId));

            return trainers;
        }

        public IEnumerable<T> GetAllTrainersForPublicView<T>(int page, int itemsPerPage)
        {
            var trainers = this.trainersRepository.AllAsNoTracking()
                .OrderByDescending(x => x.User.FideRating)
                .Skip((page - 1) * itemsPerPage).Take(itemsPerPage)
                .To<T>()
                .ToList();

            return trainers;
        }

        public T GetById<T>(string id)
        {
            var trainer = this.trainersRepository.AllAsNoTracking()
                .Where(x => x.UserId == id)
                .To<T>().FirstOrDefault();

            return trainer;
        }

        public int GetCount()
        {
            return this.trainersRepository.AllAsNoTracking().Count();
        }

        public async Task<Image> InitializeTrainerImage(IFormFile image, Trainer trainer, string imagePath)
        {
            var extension = Path.GetExtension(image.FileName);

            if (!GlobalConstants.AllowedImageExtensions.Any(x => extension.ToLower().EndsWith(x)))
            {
                throw new Exception($"{ErrorMessages.InvalidImageExtension}{extension}");
            }

            var dbImage = new Image
            {
                TrainerId = trainer.Id,
                Trainer = trainer,
                Extension = extension,
            };

            var physicalPath = $"{imagePath}{dbImage.Id}{extension}";

            using Stream fileStream = new FileStream(physicalPath, FileMode.Create);

            await image.CopyToAsync(fileStream);

            dbImage.ImageUrl = $"{GlobalConstants.TrainerImagesPath}{dbImage.Id}{extension}";

            if (trainer.ImageId != null)
            {
                var oldImage = this.imagesRepository.All().FirstOrDefault(x => x.Id == trainer.ImageId);
                this.imagesRepository.HardDelete(oldImage);
            }

            await this.imagesRepository.AddAsync(dbImage);
            await this.imagesRepository.SaveChangesAsync();

            return dbImage;
        }

        public async Task UpdateAsync(string id, TrainerInputModel input, string imagePath)
        {
            var user = this.usersRepository.All().FirstOrDefault(x => x.Id == id);

            if (user.TrainerId == null)
            {
                user.Trainer = await this.CreateAsync(input, id, imagePath);
            }
            else
            {
                user.Trainer = this.trainersRepository.All().FirstOrDefault(x => x.UserId == id);
                user.Trainer.Image = await this.InitializeTrainerImage(input.ProfilePicture, user.Trainer, imagePath);
            }

            user.Trainer.DateOfLastAttendance = DateTime.Parse(input.DateOfLastAttendance);

            await this.trainersRepository.SaveChangesAsync();
            await this.usersRepository.SaveChangesAsync();
        }
    }
}
