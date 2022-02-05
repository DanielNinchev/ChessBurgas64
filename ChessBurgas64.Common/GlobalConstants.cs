﻿namespace ChessBurgas64.Common
{
    public static class GlobalConstants
    {
        public const string SystemName = "ChessBurgas64";
        public const string ClubName = "БУРГАС 64";

        public const string AdministratorRoleName = "Administrator";

        // Profile
        public const string ProfileManagementTitle = "Управление на профила";
        public const string ProfileManagementSubtitle = "Тук можете да промените входящите си настройки.";
        public const string ProfileDataUpdatedMsg = "Вашите профилни данни са обновени успешно!";

        // Personal data view
        public const string PersonalDataPolicyInfo = "Профилът Ви съдържа лични данни, които Вие сте ни предоставили. От текущата страница можете да ги изтеглите, както и да ги изтриете безвъзвратно. Всичката Ваша информация, която сте ни поверили, се използва единствено за определените цели, описани по-долу.";
        public const string PersonalDataPolicyTitle = "Политика за поверителност на личните Ви данни";
        public const string PersonalDataDeleteWarning = "Изтриването на данните Ви е необратимо - те никога няма да могат да бъдат възстановени.";
        public const string BirthDateDataUsageInfo = "Датите на раждане са ни необходими, за да можем да разпределяме учениците по възрастови групи при тренировки, както и за записване в състезания, в които възрастовата група е от значение.";
        public const string SchoolDataUsageInfo = "Информацията за училището, в което съответният ученик школува, ни дава възможност да разпределяме отбори за ученически първенства, както и да издаваме извинителни бележки за отсъствие от учебни часове при участия в състезания.";
        public const string AddressDataUsageInfo = "Адресът Ви ни е нужен, за да знаем местонахожданието Ви, като по този начин имаме по-добра организация при пътувания за извънградски турнири.";
        public const string ForWhatDoWeUseYourData = "За какво използваме Вашите данни?";

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
        public const string EmailResetFailed = "Вашият email не е променен.";
        public const string NewEmail = "Нов email";
        public const string ChangeEmail = "Смяна на email";

        // Registration
        public const string RegistrationConfirmationInstructions = "Моля, проверете в кутията на своя email за изпратен от нас линк, за да потвърдите своята регистрация.";

        // Email confirmation
        public const string EmailSuccessfulConfirmationMsg = "Благодарим Ви, че потвърдихте своя email!";
        public const string EmailSuccessfulChangeMsg = "Вие успешно променихте своя email.";
        public const string EmailChangeConfirmationTitle = "Потвърдете новия си email.";
        public const string Resend = "Препращане";
        public const string Save = "Запазване";

        // Password
        public const string ForgottenPasswordConfirmationInstructions = "Моля, проверете в кутията на своя email за изпратен от нас линк, за да потвърдите, че наистина желаете да промените паролата си.";
        public const string CurrentPassword = "Текуща парола";
        public const string NewPassword = "Нова парола";
        public const string ConfirmNewPassword = "Повторете новата парола";

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
        public const string AdminTools = "Администраторски настройки";
        public const string Exit = "Изход";
        public const string WelcomeMsg = "Здравейте,";

        // Profile navigation
        public const string Profile = "Личен профил";
        public const string Authentication = "Разпознаване";
        public const string PersonalData = "Лични данни";

        // File paths
        public const string AnnouncementImagesPath = "/images/announcements/";

        // Pagination
        public const string Next = "Следваща";
        public const string Previous = "Предишна";

        // Buttons
        public const string DeleteAccount = "Изтриване на потребителя";
        public const string DownloadPersonalData = "Изтегляне на личните данни";

        // Images
        public static readonly string[] AllowedImageExtensions = new[] { "gif", "jpg", "png" };
    }
}
