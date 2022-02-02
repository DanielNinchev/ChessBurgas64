namespace ChessBurgas64.Common
{
    public static class GlobalConstants
    {
        public const string SystemName = "ChessBurgas64";
        public const string ClubName = "БУРГАС 64";

        public const string AdministratorRoleName = "Administrator";

        // Email messages
        public const string EmailConfirmationMsg = "Моля, потвърдете Вашия email, натискайки върху този линк: ";
        public const string EmailConfirmationTopic = "Потвърждаване на email";
        public const string RegistrationConfirmationMsg = "Моля, потвърдете Вашата регистрация, натискайки върху този линк: ";
        public const string RegistrationConfirmationTopic = "Потвърждаване на регистрация";
        public const string PasswordResetConfirmationMsg = "Моля, потвърдете желанието си да промените паролата си, натискайки върху този линк: ";
        public const string PasswordResetConfirmationTopic = "Промяна на парола";
        public const string PasswordResetSuccess = "Вие успешно променихте паролата си!";
        public const string Reset = "Промяна";
        public const string ResendEmailConfirmationInstructions = "Моля, проверете в кутията на своя email за изпратен от нас линк, за да потвърдите своя email.";

        // Registration
        public const string RegistrationConfirmationInstructions = "Моля, проверете в кутията на своя email за изпратен от нас линк, за да потвърдите своята регистрация.";

        // Email confirmation
        public const string EmailSuccessfulConfirmationMsg = "Благодарим Ви, че потвърдихте своя email!";
        public const string EmailSuccessfulChangeMsg = "Вие успешно променихте своя email.";
        public const string EmailChangeConfirmationTitle = "Потвърдете новия си email.";
        public const string Resend = "Препращане";

        // Password
        public const string ForgottenPasswordConfirmationInstructions = "Моля, проверете в кутията на своя email за изпратен от нас линк, за да потвърдите, че наистина желаете да промените паролата си.";

        // User
        public const string Email = "Email";
        public const string FirstName = "Име";
        public const string MiddleName = "Презиме";
        public const string LastName = "Фамилия";
        public const string BirthDate = "Дата на раждане";
        public const string Gender = "Пол";
        public const string Password = "Парола";
        public const string RepeatPass = "Повторете паролата";
        public const string School = "Училище";
        public const string Address = "Адрес";
        public const string PhoneNumber = "Телефон за връзка";
        public const int PasswordMaxLength = 100;
        public const int PasswordMinLength = 6;
        public const int NameMaxLength = 20;
        public const int NameMinLength = 2;
        public const int AddressMaxLength = 100;
        public const int AddressMinLength = 10;
        public const int SchoolMaxLength = 100;
        public const int SchoolMinLength = 10;

        // Registration view
        public const string RegistrationLabel = "РЕГИСТРАЦИЯ";

        // Login view
        public const string RememberMe = "Запомни ме";
        public const string LogInTitle = "Влезте в профила си";
        public const string ForgottenPassword = "Забравена парола?";
        public const string RegisterAsANewUser = "Създайте нов профил";
        public const string ResendEmailConfirmation = "Препращане на потвърждението по email";

        // Forgotten password view
        public const string ForgottenPasswordTitle = "Забравена парола";
        public const string EnterEmailLabel = "Въведете своя email";
        public const string SendButton = "Изпращане";
        public const string ForgottenPasswordInfo = "На въведения от Вас email ще получите съобщение с препратка към формата за смяна на паролата Ви.";

        // Enums
        public const string Male = "Мъжки";
        public const string Female = "Женски";

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
