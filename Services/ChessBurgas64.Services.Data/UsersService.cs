namespace ChessBurgas64.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Dynamic.Core;
    using System.Threading.Tasks;

    using AutoMapper;
    using ChessBurgas64.Data.Common.Repositories;
    using ChessBurgas64.Data.Models;
    using ChessBurgas64.Data.Models.Enums;
    using ChessBurgas64.Services.Data.Contracts;
    using ChessBurgas64.Services.Mapping;
    using ChessBurgas64.Web.ViewModels.Users;

    public class UsersService : IUsersService
    {
        private readonly IDeletableEntityRepository<ApplicationUser> usersRepository;

        public UsersService(
            IDeletableEntityRepository<ApplicationUser> usersRepository)
        {
            this.usersRepository = usersRepository;
        }

        public async Task ChangeClubStatusAsync(string id, string clubStatus)
        {
            var user = this.usersRepository.All().FirstOrDefault(x => x.Id == id);
            user.ClubStatus = (ClubStatus)Enum.Parse(typeof(ClubStatus), clubStatus);

            await this.usersRepository.SaveChangesAsync();
        }

        public T GetById<T>(string id)
        {
            var user = this.usersRepository.AllAsNoTracking()
                .Where(x => x.Id == id)
                .To<T>().FirstOrDefault();

            return user;
        }

        public IEnumerable<T> GetTableData<T>(string sortColumn, string sortColumnDirection, string searchValue)
        {
            var users = this.usersRepository.All();
            var userData = from user in users select user;

            if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
            {
                userData = userData.OrderBy(sortColumn + " " + sortColumnDirection);
            }

            if (!string.IsNullOrEmpty(searchValue))
            {
                userData = userData.Where(s => s.FirstName.Contains(searchValue)
                                    || s.MiddleName.Contains(searchValue)
                                    || s.LastName.Contains(searchValue)
                                    || s.PhoneNumber.Contains(searchValue)
                                    || s.Email.Contains(searchValue));
            }

            return userData.To<T>().ToList();
        }

        public async Task UpdateAsync(string id, UserInputModel input)
        {
            var user = this.usersRepository.All().FirstOrDefault(x => x.Id == id);
            user.ClubStatus = (ClubStatus)Enum.Parse(typeof(ClubStatus), input.ClubStatus);
            user.FideTitle = (FideTitle)Enum.Parse(typeof(FideTitle), input.FideTitle);
            user.FideRating = input.FideRating;

            await this.usersRepository.SaveChangesAsync();
        }
    }
}
