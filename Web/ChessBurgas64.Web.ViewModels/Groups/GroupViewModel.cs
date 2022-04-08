namespace ChessBurgas64.Web.ViewModels.Groups
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;

    using ChessBurgas64.Web.ViewModels.GroupMembers;
    using ChessBurgas64.Web.ViewModels.Trainers;

    public class GroupViewModel : GroupTableViewModel
    {
        public virtual TrainerViewModel Trainer { get; set; }

        public ICollection<GroupMemberViewModel> Members { get; set; }

        public string TrainerUserFirstName { get; set; }

        public string TrainerUserLastName { get; set; }

        [NotMapped]
        public string TrainerName => $"{this.TrainerUserFirstName} {this.TrainerUserLastName}";
    }
}
