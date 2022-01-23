namespace ChessBurgas64.Web.ViewModels.Announcements
{
    using System.ComponentModel.DataAnnotations;

    public class AnnouncementViewModel
    {
        [Required]
        [MinLength(4)]
        public string Title { get; set; }

        [Required]
        [MinLength(10)]
        public string Text { get; set; }

        public string AuthorId { get; set; }

        [Required]
        public int CategoryId { get; set; }
    }
}
