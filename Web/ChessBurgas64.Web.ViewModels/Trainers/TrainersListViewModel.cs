namespace ChessBurgas64.Web.ViewModels.Trainers
{
    using System.Collections.Generic;

    public class TrainersListViewModel : EntityInListViewModel
    {
        public IEnumerable<TrainerViewModel> Trainers { get; set; }
    }
}
