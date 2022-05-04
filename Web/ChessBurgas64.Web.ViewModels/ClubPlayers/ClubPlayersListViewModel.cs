namespace ChessBurgas64.Web.ViewModels.ClubPlayers
{
    using System.Collections.Generic;

    public class ClubPlayersListViewModel : EntityInListViewModel
    {
        public ICollection<ClubPlayerViewModel> ClubPlayers { get; set; }
    }
}
