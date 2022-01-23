namespace ChessBurgas64.Data.Models
{
    public class LessonMember
    {
        public int Id { get; set; }

        public int LessonId { get; set; }

        public virtual Lesson Lesson { get; set; }

        public int MemberId { get; set; }

        public virtual Member Member { get; set; }
    }
}
