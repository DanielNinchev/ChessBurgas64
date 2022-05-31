namespace ChessBurgas64.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;

    using ChessBurgas64.Data.Common.Repositories;
    using ChessBurgas64.Data.Models;
    using ChessBurgas64.Data.Models.Enums;
    using ChessBurgas64.Services.Data.Contracts;
    using Microsoft.AspNetCore.Mvc.Rendering;

    public class TrainersService : ITrainersService
    {
        private readonly IDeletableEntityRepository<ApplicationUser> usersRepository;

        public TrainersService(IDeletableEntityRepository<ApplicationUser> usersRepository)
        {
            this.usersRepository = usersRepository;
        }

        public IEnumerable<SelectListItem> GetAllTrainersInSelectList()
        {
            var trainers = this.usersRepository.AllAsNoTracking()
                .Where(u => u.ClubStatus.Equals(ClubStatus.Треньор.ToString()) && u.TrainerId != null)
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
    }
}
