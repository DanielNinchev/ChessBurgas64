namespace ChessBurgas64.Web.ViewModels.Users
{
    using System.Collections.Generic;

    using ChessBurgas64.Data.Models;
    using ChessBurgas64.Services.Mapping;
    using ChessBurgas64.Web.ViewModels.Groups;
    using ChessBurgas64.Web.ViewModels.Members;
    using ChessBurgas64.Web.ViewModels.Payments;
    using ChessBurgas64.Web.ViewModels.Trainers;

    public class UserProfileViewModel : UserTableViewModel, IMapFrom<ApplicationUser>
    {
        public int FideRating { get; set; }

        public MemberViewModel Member { get; set; }

        public TrainerViewModel Trainer { get; set; }

        public ICollection<GroupViewModel> Groups { get; set; }

        public ICollection<PaymentViewModel> Payments { get; set; }
    }
}
