namespace ChessBurgas64.Web.ViewModels.Trainers
{
    using System.Collections.Generic;

    public class TrainersListViewModel : PublicEntityInListViewModel
    {
        public IEnumerable<TrainerViewModel> Trainers { get; set; }
    }
}
