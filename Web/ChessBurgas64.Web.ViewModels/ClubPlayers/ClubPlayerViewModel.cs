namespace ChessBurgas64.Web.ViewModels.ClubPlayers
{
    using ChessBurgas64.Data.Models;
    using ChessBurgas64.Services.Mapping;

    public class ClubPlayerViewModel : IdHoldingModel, IMapFrom<ClubPlayer>
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string FideTitle { get; set; }

        public string ImageImageUrl { get; set; }
    }
}
