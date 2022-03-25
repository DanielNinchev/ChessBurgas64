namespace ChessBurgas64.Data.Models
{
    using ChessBurgas64.Data.Common.Models;

    public class LessonMember : BaseDeletableModel<int>
    {
        public int LessonId { get; set; }

        public virtual Lesson Lesson { get; set; }

        public string MemberId { get; set; }

        public virtual Member Member { get; set; }
    }
}
