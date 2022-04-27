namespace ChessBurgas64.Web.ViewModels
{
    using System.ComponentModel.DataAnnotations;

    public class HistoryInputModel
    {
        [Required]
        public string Text { get; set; }
    }
}
