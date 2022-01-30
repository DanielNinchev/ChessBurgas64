namespace ChessBurgas64.Common
{
    public static class GlobalConstants
    {
        public const string SystemName = "ChessBurgas64";
        public const string ClubName = "БУРГАС 64";

        public const string AdministratorRoleName = "Administrator";

        // Enums
        public const string Male = "Мъж";
        public const string Female = "Жена";

        public const string BeginnerPuzzle = "Първо ниво - за начинаещи";
        public const string EasyPuzzle = "Второ ниво - ниска трудност";
        public const string AveragePuzzle = "Трето ниво - средна трудност";
        public const string HardPuzzles = "Четвърто ниво - висока трудност";
        public const string ExtremePuzzle = "Пето ниво - майсторски";

        // Announcements
        public const int AnnouncementsPerPage = 6;

        // Categories
        public const string AnnouncementsCategory = "Обяви";
        public const string NewsCategory = "Новини";
        public const string ArticlesCategory = "Статии";

        // Index
        public const string HomeTitle = "ШАХМАТЕН КЛУБ \"БУРГАС 64\"";

        // Navigation
        public const string AnnouncementsNav = "Новини";
        public const string CreateAnnouncementNav = "Напиши";
        public const string HomeNav = "Начало";
        public const string TrainersNav = "Треньори";
        public const string CompetitorsNav = "Състезатели";
        public const string ContactsNav = "Контакти";
        public const string GalleryNav = "Галерия";
        public const string HistoryNav = "История";
        public const string LoginNav = "Вход";
        public const string RegistrationNav = "Регистрация";
        public const string ProfileNav = "Профил";
        public const string ScheduleNav = "График";

        // File paths
        public const string AnnouncementImagesPath = "/images/announcements/";

        // Pagination
        public const string Next = "Следваща";
        public const string Previous = "Предишна";

        // Images
        public static readonly string[] AllowedImageExtensions = new[] { "gif", "jpg", "png" };
    }
}
