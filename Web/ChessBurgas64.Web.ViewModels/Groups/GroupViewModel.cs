namespace ChessBurgas64.Web.ViewModels.Groups
{
    using System.Collections.Generic;

    using ChessBurgas64.Web.ViewModels.Members;
    using ChessBurgas64.Web.ViewModels.Trainers;

    public class GroupViewModel : GroupTableViewModel
    {
        public virtual TrainerViewModel Trainer { get; set; }

        public virtual ICollection<MemberViewModel> Members { get; set; }
    }
}
