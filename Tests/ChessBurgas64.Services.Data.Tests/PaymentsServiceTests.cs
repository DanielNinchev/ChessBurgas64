namespace ChessBurgas64.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Threading.Tasks;

    using AutoMapper;
    using ChessBurgas64.Data.Common.Repositories;
    using ChessBurgas64.Data.Models;
    using ChessBurgas64.Services.Mapping;
    using ChessBurgas64.Web.ViewModels;
    using ChessBurgas64.Web.ViewModels.Payments;
    using Moq;
    using Xunit;

    public class PaymentsServiceTests
    {
        private readonly IMapper mapper;

        [Fact]
        public async Task ShouldReturnTableDataWhenAllParametersAreGivenCorrectly()
        {
            string testUserId = "testUserId";

            var mockPayment = new Mock<Payment>();
            var mockPayment2 = new Mock<Payment>();
            var mockPayment3 = new Mock<Payment>();
            var testUser = new Mock<ApplicationUser>();

            testUser.Setup(x => x.Id).Returns(testUserId);
            mockPayment.Object.UserId = testUserId;
            mockPayment2.Object.UserId = testUserId;
            mockPayment3.Object.UserId = "someOtherId";

            mockPayment.Object.Description = "Месечна такса";
            mockPayment2.Object.Description = "Картотека";
            mockPayment3.Object.Description = "Седмично заплащане";

            var paymentsMockRepo = new Mock<IDeletableEntityRepository<Payment>>();
            var paymentsList = new List<Payment>();

            paymentsMockRepo.Setup(x => x.AllAsNoTracking()).Returns(paymentsList.AsQueryable());
            paymentsMockRepo.Setup(x => x.AddAsync(It.IsAny<Payment>())).Callback(
                (Payment payment) => paymentsList.Add(payment));

            var usersMockRepo = new Mock<IDeletableEntityRepository<ApplicationUser>>();
            var usersList = new List<ApplicationUser>();
            usersMockRepo.Setup(x => x.AddAsync(It.IsAny<ApplicationUser>())).Callback(
                (ApplicationUser user) => usersList.Add(user));

            await usersMockRepo.Object.AddAsync(testUser.Object);
            await paymentsMockRepo.Object.AddAsync(mockPayment.Object);
            await paymentsMockRepo.Object.AddAsync(mockPayment2.Object);
            await paymentsMockRepo.Object.AddAsync(mockPayment3.Object);

            string sortColumn = "Description";
            string sortColumnDirection = "asc";
            string searchValue = null;

            var paymentsService = this.InitializeService(paymentsMockRepo);

            var payments = await paymentsService.GetTableData<PaymentViewModel>(testUserId, sortColumn, sortColumnDirection, searchValue);

            var paymentViewModel = new PaymentViewModel()
            {
                Id = mockPayment.Object.Id,
                UserId = mockPayment.Object.UserId,
                Description = mockPayment.Object.Description,
            };

            var paymentViewModel2 = new PaymentViewModel()
            {
                Id = mockPayment2.Object.Id,
                UserId = mockPayment2.Object.UserId,
                Description = mockPayment2.Object.Description,
            };

            var expectedResult = new List<PaymentViewModel>()
            {
                paymentViewModel2,
                paymentViewModel,
            };

            List<PaymentViewModel> someList = new List<PaymentViewModel>();

            Assert.Equal(expectedResult[0].Id, payments.ToList()[0].Id);
            Assert.Equal(expectedResult[1].Id, payments.ToList()[1].Id);
            Assert.Equal(expectedResult.Count, payments.Count());
        }

        [Fact]
        public async Task GetTableDataShouldReturnEmptyListWhenUserHasNoPaymentsRegistered()
        {
            string testUserId = "testUserId";
            var testUser = new Mock<ApplicationUser>();
            var mockPayment = new Mock<Payment>();

            testUser.Setup(x => x.Id).Returns(testUserId);
            mockPayment.Object.UserId = "someOtherUserId";

            var paymentsMockRepo = new Mock<IDeletableEntityRepository<Payment>>();
            var paymentsList = new List<Payment>();

            paymentsMockRepo.Setup(x => x.AllAsNoTracking()).Returns(paymentsList.AsQueryable());
            paymentsMockRepo.Setup(x => x.AddAsync(It.IsAny<Payment>())).Callback(
                (Payment payment) => paymentsList.Add(payment));

            var usersMockRepo = new Mock<IDeletableEntityRepository<ApplicationUser>>();
            var usersList = new List<ApplicationUser>();
            usersMockRepo.Setup(x => x.AddAsync(It.IsAny<ApplicationUser>())).Callback(
                (ApplicationUser user) => usersList.Add(user));

            await usersMockRepo.Object.AddAsync(testUser.Object);
            await paymentsMockRepo.Object.AddAsync(mockPayment.Object);

            string sortColumn = "Description";
            string sortColumnDirection = "asc";
            string searchValue = null;

            var paymentsService = this.InitializeService(paymentsMockRepo);
            var payments = await paymentsService.GetTableData<PaymentViewModel>(testUserId, sortColumn, sortColumnDirection, searchValue);

            Assert.Empty(payments);
        }

        [Fact]
        public async Task PaymentDataShouldBeUpdatedWhenGivenProperInput()
        {
            string testUserId = "testUserId";
            var testUser = new Mock<ApplicationUser>();
            var mockPayment = new Mock<Payment>();

            mockPayment.Object.Amount = 50;
            mockPayment.Object.DateOfPayment = System.DateTime.Now;
            mockPayment.Object.Description = "Картотека";
            mockPayment.Object.UserId = null;

            var paymentsMockRepo = new Mock<IDeletableEntityRepository<Payment>>();
            var paymentsList = new List<Payment>();

            paymentsMockRepo.Setup(x => x.All()).Returns(paymentsList.AsQueryable());
            paymentsMockRepo.Setup(x => x.AddAsync(It.IsAny<Payment>())).Callback(
                (Payment payment) => paymentsList.Add(payment));

            var usersMockRepo = new Mock<IDeletableEntityRepository<ApplicationUser>>();
            var usersList = new List<ApplicationUser>();
            usersMockRepo.Setup(x => x.AddAsync(It.IsAny<ApplicationUser>())).Callback(
                (ApplicationUser user) => usersList.Add(user));

            await usersMockRepo.Object.AddAsync(testUser.Object);
            await paymentsMockRepo.Object.AddAsync(mockPayment.Object);

            var paymentsService = this.InitializeService(paymentsMockRepo);

            var input = new PaymentInputModel
            {
                Amount = 100,
                DateOfPayment = DateTime.Parse("10.3.2021"),
                Description = "Седмично заплащане",
                UserId = testUserId,
            };

            await paymentsService.UpdateAsync(mockPayment.Object.Id, input);
            var formattedData = mockPayment.Object.DateOfPayment.ToString(format: "dd/M/yyyy");

            Assert.Equal(input.Amount, mockPayment.Object.Amount);
            Assert.Equal(input.DateOfPayment.ToString(format: "dd/M/yyyy"), formattedData);
            Assert.Equal(input.Description, mockPayment.Object.Description);
            Assert.Equal(input.UserId, mockPayment.Object.UserId);
        }

        private PaymentsService InitializeService(Mock<IDeletableEntityRepository<Payment>> paymentsMockRepo)
        {
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);
            return new PaymentsService(paymentsMockRepo.Object, this.mapper);
        }
    }
}
