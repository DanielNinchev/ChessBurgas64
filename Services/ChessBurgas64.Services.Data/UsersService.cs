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
        private readonly IMapper mapper;

        public UsersService(IDeletableEntityRepository<ApplicationUser> usersRepository, IMapper mapper)
        {
            this.usersRepository = usersRepository;
            this.mapper = mapper;
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

        public async Task UpdateAsync(string id, EditUserInputModel input)
        {
            var user = this.usersRepository.All().FirstOrDefault(x => x.Id == id);
            user.ClubStatus = (ClubStatus)Enum.Parse(typeof(ClubStatus), input.ClubStatus);

            if (user.Member != null)
            {
                if (user.ClubStatus != ClubStatus.Изчакващ)
                {
                    user.Member = this.mapper.Map<Member>(input);
                }
            }

            await this.usersRepository.SaveChangesAsync();
        }
    }
}
