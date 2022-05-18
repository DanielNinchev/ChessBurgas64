namespace ChessBurgas64.Services.Data
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    using AutoMapper;
    using ChessBurgas64.Data.Common.Repositories;
    using ChessBurgas64.Data.Models;
    using ChessBurgas64.Services.Data.Contracts;
    using ChessBurgas64.Services.Mapping;
    using ChessBurgas64.Web.ViewModels.NotableMembers;
    using Microsoft.EntityFrameworkCore;

    public class NotableMembersService : INotableMembersService
    {
        private readonly IDeletableEntityRepository<NotableMember> notableMembersRepository;
        private readonly IMapper mapper;

        public NotableMembersService(IDeletableEntityRepository<NotableMember> notableMembersRepository, IMapper mapper)
        {
            this.notableMembersRepository = notableMembersRepository;
            this.mapper = mapper;
        }

        public async Task<NotableMember> CreateAsync(NotableMemberInputModel input, string imagePath)
        {
            var notableMember = this.mapper.Map<NotableMember>(input);

            Directory.CreateDirectory(imagePath);

            await this.notableMembersRepository.AddAsync(notableMember);
            await this.notableMembersRepository.SaveChangesAsync();

            return notableMember;
        }

        public async Task DeleteAsync(int id)
        {
            var notableMember = await this.notableMembersRepository
                .All()
                .FirstOrDefaultAsync(x => x.Id == id);

            this.notableMembersRepository.Delete(notableMember);
            await this.notableMembersRepository.SaveChangesAsync();
        }

        public async Task<ICollection<T>> GetAllInGovernanceAsync<T>()
        {
            var notableMembers = await this.notableMembersRepository
                .AllAsNoTracking()
                .Where(x => x.IsPartOfGovernance)
                .OrderBy(x => x.ListIndex)
                .To<T>()
                .ToListAsync();

            return notableMembers;
        }

        public async Task<ICollection<T>> GetAllPlayersAsync<T>()
        {
            var notableMembers = await this.notableMembersRepository
                .AllAsNoTracking()
                .Where(x => !x.IsPartOfGovernance)
                .OrderBy(x => x.ListIndex)
                .To<T>()
                .ToListAsync();

            return notableMembers;
        }

        public async Task<T> GetByIdAsync<T>(int id)
        {
            var notableMember = await this.notableMembersRepository
                .AllAsNoTracking()
                .Where(x => x.Id == id)
                .To<T>()
                .FirstOrDefaultAsync();

            return notableMember;
        }

        public async Task<NotableMember> GetByIdAsync(int id)
        {
            var notableMember = await this.notableMembersRepository
                .AllAsNoTracking()
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();

            return notableMember;
        }

        public async Task<int> GetCountAsync()
        {
            return await this.notableMembersRepository.AllAsNoTracking().CountAsync();
        }

        public async Task<NotableMember> UpdateAsync(int id, NotableMemberInputModel input)
        {
            var notableMember = await this.notableMembersRepository
                .All()
                .FirstOrDefaultAsync(x => x.Id == id);

            notableMember.Description = input.Description;
            notableMember.FideTitle = input.FideTitle.ToString();
            notableMember.IsPartOfGovernance = input.IsPartOfGovernance;
            notableMember.ListIndex = input.ListIndex;
            notableMember.Name = input.Name;

            await this.notableMembersRepository.SaveChangesAsync();

            return notableMember;
        }
    }
}
