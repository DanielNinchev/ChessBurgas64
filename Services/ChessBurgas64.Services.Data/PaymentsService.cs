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
    using ChessBurgas64.Services.Data.Contracts;
    using ChessBurgas64.Services.Mapping;
    using ChessBurgas64.Web.ViewModels.Payments;
    using Microsoft.EntityFrameworkCore;

    public class PaymentsService : IPaymentsService
    {
        private readonly IDeletableEntityRepository<Payment> paymentsRepository;
        private readonly IMapper mapper;

        public PaymentsService(IDeletableEntityRepository<Payment> paymentsRepository, IMapper mapper)
        {
            this.paymentsRepository = paymentsRepository;
            this.mapper = mapper;
        }

        public async Task CreateAsync(PaymentInputModel input, string userId)
        {
            var payment = this.mapper.Map<Payment>(input);

            payment.UserId = userId;

            await this.paymentsRepository.AddAsync(payment);
            await this.paymentsRepository.SaveChangesAsync();
        }

        public async Task DeleteAsync(string id)
        {
            var payment = await this.paymentsRepository
                .All()
                .FirstOrDefaultAsync(x => x.Id == id);

            this.paymentsRepository.Delete(payment);
            await this.paymentsRepository.SaveChangesAsync();
        }

        public async Task<T> GetByIdAsync<T>(string id)
        {
            var payment = await this.paymentsRepository
                .AllAsNoTracking()
                .Where(x => x.Id == id)
                .To<T>()
                .FirstOrDefaultAsync();

            return payment;
        }

        public async Task<IEnumerable<T>> GetTableData<T>(string userId, string sortColumn, string sortColumnDirection, string searchValue)
        {
            var payments = this.paymentsRepository.AllAsNoTracking().Where(p => p.UserId == userId);
            var paymentData = from payment in payments select payment;

            if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
            {
                paymentData = paymentData.OrderBy(sortColumn + " " + sortColumnDirection);
            }

            if (!string.IsNullOrEmpty(searchValue))
            {
                // TODO: add search by date and amount
                paymentData = paymentData.Where(p => p.Description.Contains(searchValue));
            }

            return await paymentData.To<T>().ToListAsync();
        }

        public IQueryable<Payment> GetUserPaymentsTableData(string userId)
        {
            var payment = this.paymentsRepository
                .AllAsNoTracking()
                .Where(x => x.UserId == userId);

            return payment;
        }

        public async Task UpdateAsync(string id, PaymentInputModel input)
        {
            var payment = await this.paymentsRepository
                .All()
                .FirstOrDefaultAsync(x => x.Id == id);

            payment.Amount = input.Amount;
            payment.DateOfPayment = DateTime.Parse(input.DateOfPayment);
            payment.Description = input.Description;
            payment.UserId = input.UserId;

            await this.paymentsRepository.SaveChangesAsync();
        }
    }
}
