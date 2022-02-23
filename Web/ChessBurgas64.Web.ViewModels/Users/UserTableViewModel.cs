namespace ChessBurgas64.Web.ViewModels.Users
{
    using System;

    using ChessBurgas64.Data.Models;
    using ChessBurgas64.Services.Mapping;

    public class UserTableViewModel : IMapFrom<ApplicationUser>
    {
        public string Id { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public DateTime BirthDate { get; set; }

        public string Gender { get; set; }

        public string ClubStatus { get; set; }
    }
}
