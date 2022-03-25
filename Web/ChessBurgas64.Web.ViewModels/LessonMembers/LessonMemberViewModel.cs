namespace ChessBurgas64.Web.ViewModels.LessonMembers
{
    using ChessBurgas64.Data.Models;
    using ChessBurgas64.Services.Mapping;
    using ChessBurgas64.Web.ViewModels.Lessons;
    using ChessBurgas64.Web.ViewModels.Members;

    public class LessonMemberViewModel : IMapFrom<LessonMember>
    {
        public int Id { get; set; }

        public int LessonId { get; set; }

        public LessonViewModel Lesson { get; set; }

        public string MemberId { get; set; }

        public MemberViewModel Member { get; set; }
    }
}
