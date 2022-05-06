namespace ChessBurgas64.Common
{
    public static class GlobalConstants
    {
        // =================== DATA MODELS ============================================================================
        // Announcements
        public const string AnnouncementDescription = "Кратко описание";
        public const int AnnouncementDescriptionMaxLength = 90;
        public const int AnnouncementDescriptionMinLength = 10;
        public const int AnnouncementTextMinLength = 250;
        public const int AnnouncementTextRowCount = 30;
        public const int AnnouncementTitleMaxLength = 90;
        public const int AnnouncementTitleMinLength = 4;
        public const int AnnouncementsInRow = 3;
        public const int AnnouncementsPerPage = 12;
        public const string Author = "Автор";
        public const string ChooseCategory = "*изберете категория";
        public const string PublishedOn = "Публикувано на";
        public const string Photos = "Снимки";
        public const string SearchAnnouncements = "Търсене на новини";

        // AnnouncementCategories
        public const string AnnouncementsCategory = "Обяви";
        public const string NewsCategory = "Новини";
        public const string ArticlesCategory = "Статии";

        // NotableMembers
        public const string AddNotableMember = "Добавяне на изявен член";
        public const string EditNotableMember = "Промяна на данните за изявен член";
        public const string IsPartOfGovernance = "Част от ръководството: ";
        public const string ListIndex = "Пореден номер";
        public const string NameAndSurname = "Име и фамилия";
        public const string NotableMembers = "Изявени настоящи и бивши състезатели";
        public const int NotableMembersDescriptionMaxLength = 5000;
        public const int NotableMembersDescriptionMinLength = 1000;
        public const int NotableMembersNameMaxLength = 100;
        public const int NotableMembersNameMinLength = 2;
        public const int NotableMembersPerPage = 10;

        // Enums
        public const string Male = "Мъжки";
        public const string Female = "Женски";
        public const string BeginnerPuzzle = "Първо ниво - за начинаещи";
        public const string EasyPuzzle = "Второ ниво - ниска трудност";
        public const string AveragePuzzle = "Трето ниво - средна трудност";
        public const string HardPuzzle = "Четвърто ниво - висока трудност";
        public const string ExtremePuzzle = "Пето ниво - майсторски";

        // Groups
        public const string ChooseGroup = "*изберете група";
        public const string None = "Няма";

        // Lessons
        public const string AdditionalNotes = "Допълнителни бележки";
        public const string AddLesson = "Отбележи проведено занятие";
        public const string AttendedStudents = "Присъствали ученици";
        public const string AttendedStudentsCount = "Присъствали [брой ученици]";
        public const string ConductedLessons = "Проведени занятия";
        public const string CreateLesson = "Отбелязване на проведено занятие";
        public const string DateAndTime = "Дата и час";
        public const string EditConductedLessonData = "Промяна на данните за проведено занятие";
        public const string Lesson = "Занятие";
        public const string NoMarkedAttendances = "Няма отбелязани присъствия за това занятие.";
        public const int NotesTextAreaRowCount = 10;
        public const string Topic = "Тема";
        public const int TopicMaxLength = 20;

        // Members
        public const string ChooseMember = "*изберете ученик";

        // Payments
        public const string AddPayment = "Отбележи плащане";
        public const string CreatePayment = "Отбелязване на плащане";
        public const string DateOfPayment = "Дата на плащане";
        public const string EditPaymentDataTitle = "Промяна на данните за плащане";
        public const string PaidAmount = "Платена сума [лв.]";
        public const string PaidFor = "Основание";
        public const int PaidForMinLength = 3;
        public const int PaidForMaxLength = 20;
        public const string Payment = "Плащане";
        public const string Payments = "Плащания";

        // PuzzleCategories
        public const string DrawIn1 = "Реми в 1 ход";
        public const string DrawIn2 = "Реми в 2 хода";
        public const string DrawIn3 = "Реми в 3 хода";
        public const string FindBestContinuation = "Открийте най-доброто продължение";
        public const string MateIn1 = "Мат в 1 ход";
        public const string MateIn2 = "Мат в 2 хода";
        public const string MateIn3 = "Мат в 3 хода";
        public const string WinningMaterialIn1 = "Печалба на материал в 1 ход";
        public const string WinningMaterialIn2 = "Печалба на материал в 2 хода";
        public const string WinningMaterialIn3 = "Печалба на материал в 3 хода";

        // Puzzles
        public const string AddingNewPuzzle = "Добавяне на нова задача";
        public const string Difficulty = "Трудност";
        public const string DifficultyChoice = "*избор на трудност";
        public const string EditPuzzle = "Промяна на задача";
        public const string Number = "Пореден номер";
        public const string Objective = "Условие";
        public const string Points = "Точки";
        public const string PositionImage = "Изображение на позицията";
        public const string PuzzleNumber = "Задача №";
        public const int PuzzleObjectiveMaxLength = 50;
        public const int PuzzleObjectiveMinLength = 3;
        public const int PuzzlesPerPage = 6;
        public const int PuzzlePointsMultiplier = 10;
        public const int PuzzleSolutionMinLength = 2;
        public const string SearchPuzzles = "Търсене на задачи";
        public const string Solution = "Решение";

        // Trainers
        public const string ChooseTrainer = "*изберете треньор";
        public const int TrainersPerPage = 3;

        // Users
        public const string Address = "Адрес";
        public const int AddressMaxLength = 100;
        public const int AddressMinLength = 10;
        public const string BirthDate = "Дата на раждане";
        public const string ClubData = "Клубни данни";
        public const string ClubStatusChoice = "*избор на клубен статут";
        public const string Email = "Email";
        public const string Gender = "Пол";
        public const string Id = "ID";
        public const string FideRating = "FIDE рейтинг";
        public const string FideTitle = "FIDE титла";
        public const string FideTitleChoice = "*избор на FIDE титла";
        public const string FirstName = "Име";
        public const string LastName = "Фамилия";
        public const string MiddleName = "Презиме";
        public const int NameMaxLength = 20;
        public const int NameMinLength = 2;
        public const string Password = "Парола";
        public const int PasswordMaxLength = 20;
        public const int PasswordMinLength = 6;
        public const string PhoneNumber = "Телефон за връзка";
        public const string RepeatPass = "Повторете паролата";
        public const string School = "Училище";
        public const int SchoolMaxLength = 100;
        public const int SchoolMinLength = 10;

        // Videos
        public const string CreateVideo = "Публикуване на видео";
        public const string EditVideo = "Промяна на видео";
        public const string VideoDescription = "Описание";
        public const int VideosPerPage = 12;
        public const string Videos = "Видеа";
        public const string VideoUrl = "Линк към видеото";
        public const string SearchVideos = "Търсене на видеа";

        // VideoCategory
        public const string ChessGames = "Анализ на партии";
        public const string ChessEndgames = "Ендшпили";
        public const string ChessMiddlegames = "Мителшпили";
        public const string ChessOpenings = "Дебюти";
        public const string ChessStreamings = "Предавания";
        public const string ForBeginners = "За начинаещи";

        // =========== EMAIL MESSAGES ===============================================================================================
        public const string ChangeEmail = "Смяна на email";
        public const string EmailConfirmationMsg = "Моля, потвърдете Вашия email, натискайки върху този линк: ";
        public const string EmailConfirmationTopic = "Потвърждаване на email";
        public const int EmailFormTextAreaRowCount = 10;
        public const string NewEmail = "Нов email";
        public const string RegistrationConfirmationMsg = "Моля, потвърдете Вашата регистрация, натискайки върху този линк: ";
        public const string RegistrationConfirmationTopic = "Потвърждаване на регистрация";
        public const string PasswordResetConfirmationMsg = "Моля, потвърдете желанието си да промените паролата си, натискайки върху този линк: ";
        public const string PasswordResetConfirmationTopic = "Промяна на парола";
        public const string PasswordResetSuccess = "Вие успешно променихте паролата си!";
        public const string ResendEmailConfirmationInstructions = "Моля, проверете в кутията на своя email за изпратен от нас линк, за да го потвърдите.";
        public const string Reset = "Промяна";
        public const string SendsTheFollowingMessage = "изпраща следното съобщение:";
        public const string StatusValidationMsg = "заяви следния клубен статут: ";
        public const string StatusValidationTopic = "Потвърждаване на клубен статут";

        // =========== HTML ELEMENTS ================================================================================================
        // Buttons
        public const string Accept = "Потвърди";
        public const string Add = "Добавяне";
        public const string Close = "Затваряне";
        public const string Create = "Създай";
        public const string Decline = "Отхвърли";
        public const string Delete = "Изтриване";
        public const string DeleteAccount = "Изтриване на потребителя";
        public const string DownloadPersonalData = "Изтегляне на личните данни";
        public const string Edit = "Промяна";
        public const string Next = "Следваща";
        public const string No = "Не";
        public const string Previous = "Предишна";
        public const string Refresh = "Обновяване";
        public const string Search = "Търсене";
        public const string SearchBtn = "Търсене...";
        public const string Send = "Изпращане";
        public const string View = "Преглед";
        public const string Yes = "Да";

        // Labels
        public const string AddAdditionalPhotos = "Добави допълнителни снимки";
        public const string AddMainPhoto = "Добави основна снимка";
        public const string AddProfilePicture = "Добави профилна снимка";
        public const string AreYouSureYouWantToDeleteThis = "Наистина ли желаете да изтриете това съдържание?";
        public const string Categories = "Категории";
        public const string Category = "Категория";
        public const string Confirmation = "Потвърждение";
        public const string Date = "Дата";
        public const string Description = "Описание";
        public const string DoYouWantToSaveChanges = "Желаете ли да запазите промените?";
        public const string Text = "Текст";
        public const string Title = "Заглавие";

        // Navigation
        public const string AdminTools = "Администратор";
        public const string AnnouncementsNav = "Новини";
        public const string CompetitorsNav = "Състезатели";
        public const string ContactsNav = "Контакти";
        public const string CreateAnnouncementNav = "Новина";
        public const string CreatePuzzleNav = "Шахматна задача";
        public const string CreateVideoLessonNav = "Видео урок";
        public const string Exit = "Изход";
        public const string GalleryNav = "Галерия";
        public const string GroupsNav = "Ученически групи";
        public const string HistoryNav = "История";
        public const string HomeNav = "Начало";
        public const string LoginNav = "Вход";
        public const string RegisteredUsers = "Регистрирани потребители";
        public const string ProfileNav = "Профил";
        public const string Publish = "Публикувай";
        public const string PuzzlesNav = "Шахматни задачи";
        public const string RegistrationNav = "Регистрация";
        public const string ManagementNav = "Ръководство";
        public const string VideosNav = "Видео уроци";
        public const string WelcomeMsg = "Здравейте,";

        // ============= SYSTEM AND ADMINISTRATION ====================================================================
        public const string AdminEmail = "daniel.ninchev@gmail.com";
        public const string ChessClub = "Шахматен клуб";
        public const string ClubName = "\"Бургас 64\"";
        public const string SystemName = "ШК \"Бургас 64\"";
        public const string Welcome = "Добре дошли!";

        // File paths
        public const string AnnouncementImagesPath = "/images/announcements/";
        public const string NotableMembersImagesPath = "/images/notableMembers/";
        public const string PuzzleImagesPath = "/images/puzzles/";
        public const string TrainerImagesPath = "/images/trainers/";

        // Roles
        public const string AdministratorRoleName = "Администратор";
        public const string MemberRoleName = "Член";
        public const string TrainerRoleName = "Треньор";

        // =========== VIEWS ======================================================================================================
        // Access view
        public const string AccessDenied = "Неразрешен достъп!";
        public const string YouDoNotHavePermissionForThisResource = "Нямате достъп до това съдържание.";

        // Add group member view
        public const string AddGroupMember = "Добавяне на член към групата";

        // Contacts view
        public const string ClubLocationDescription = "Клубът се намира в жилищен комплекс \"Лазур\" в заграждението на спортната база на \"Нефтохимик\", точно срещу залата за акробатика. Влиза се през бялата врата до портала и се продължава напред, докато се стигне бялата едноетажна сграда.";
        public const string ContactUs = "За връзка с нас:";
        public const string Location = "Местоположение";
        public const string Message = "Съобщение";
        public const string ViewLargerMap = "Разгледайте картата в уголемен размер";
        public const string ThankYouForYourMessage = "Благодарим Ви, че се свързахте с нас. Ще разгледаме съобщението Ви и ще Ви отговорим възможно най-скоро.";
        public const string SendUsAMessage = "Оставете ни съобщение";

        // Edit users view
        public const string EditUsersClubData = "Промяна на клубни данни";
        public const string EditUsersProfileData = "Промяна на профилни данни";
        public const string LearnedOpenings = "Научени дебюти";
        public const string LastPuzzleLevel = "Последна решена задача";

        // Email confirmation view
        public const string EmailSuccessfulConfirmationMsg = "Благодарим Ви, че потвърдихте своя email!";
        public const string EmailSuccessfulChangeMsg = "Вие успешно променихте своя email.";
        public const string EmailChangeConfirmationTitle = "Потвърдете новия си email.";
        public const string Resend = "Препращане";
        public const string Save = "Запазване";
        public const string StatusDeclarationInstructions = "Регистрирахте се успешно! Съвсем скоро заявеният от Вас клубен статут ще бъде разгледан и одобрен при съответствие.";
        public const string ToStatusDeclaration = "Към заявка на статут";
        public const string StatusDeclarationTitle = "Клубен статут";
        public const string StatusDeclarationSubtitle = "Тук можете да заявите промяна на клубния си статут.";

        // Forgotten password view
        public const string EnterEmailLabel = "Въведете своя email";
        public const string ForgottenPasswordInfo = "На въведения от Вас email ще получите съобщение с препратка към формата за смяна на паролата Ви.";
        public const string ForgottenPasswordTitle = "Забравена парола";

        // Group view
        public const string AddMembersToGroup = "Добавяне членове към групата";
        public const string Day = "Ден";
        public const string ChooseDay = "*избор на ден";
        public const string EditGroupData = "Промяна на групови данни";
        public const string GroupData = "Групови данни";
        public const string GroupMembers = "Членове на групата";
        public const string GroupName = "Име на групата";
        public const string HighestRating = "ЕЛО max";
        public const string Hour = "Час";
        public const string LowestRating = "ЕЛО min";
        public const string StudentsCount = "Брой ученици";
        public const string TrainingDay = "Тренировъчен ден";
        public const string TrainingHour = "Тренировъчен час";

        // History view
        public const string HistoryOfChessInBurgas = "История на шахмата в Бургас и региона";

        // Index view
        public const string HomeTitle = "ШАХМАТЕН КЛУБ \"БУРГАС 64\"";

        // Login view
        public const string ForgottenPassword = "Забравена парола?";
        public const string LogInTitle = "Влезте в профила си";
        public const string RegisterAsANewUser = "Създайте нов профил";
        public const string RememberMe = "Запомни ме";
        public const string ResendEmailConfirmation = "Препращане на потвърждението по email";

        // Password view
        public const string ConfirmNewPassword = "Повторете новата парола";
        public const string CurrentPassword = "Текуща парола";
        public const string ForgottenPasswordConfirmationInstructions = "Моля, проверете в кутията на своя email за изпратен от нас линк, за да потвърдите, че наистина желаете да промените паролата си.";
        public const string NewPassword = "Нова парола";

        // Personal data view
        public const string AddressDataUsageInfo = "Адресът Ви ни е нужен, за да знаем местоживеенето Ви, като по този начин ще имаме по-добра организация при пътувания за извънградски турнири.";
        public const string BirthDateDataUsageInfo = "Датите на раждане са ни необходими, за да можем да разпределяме учениците по възрастови групи при тренировки, както и за записване в състезания, в които възрастовата група е от значение.";
        public const string DateOfJoiningTheClubInfo = "Датата на записване в клуба ни дава възможност да проследяваме напредъка на учениците.";
        public const string EmailDataUsageInfo = "Чрез електронната Ви поща можем да комуникираме и да Ви изпращаме информация, свързана с турнири, задачи, обяви и други клубни дейности. Тя е задължителна за управлението на Вашия потребителски профил. Ние не предоставяме Вашия email на трети лица.";
        public const string ForWhatDoWeUseYourData = "За какво използваме Вашите данни?";
        public const string PersonalDataDeleteWarning = "Изтриването на данните Ви е необратимо - те никога няма да могат да бъдат възстановени!";
        public const string PersonalDataPolicyInfo = "Профилът Ви съдържа лични данни, които Вие сте ни предоставили. От текущата страница можете да ги изтеглите, както и да ги изтриете безвъзвратно. Всичката Ваша информация, която сте ни поверили, се използва единствено за определените цели, описани по-долу.";
        public const string PersonalDataPolicyTitle = "Политика за поверителност на личните Ви данни";
        public const string PhoneDataUsageInfo = "Телефонът Ви е задължителен за регистрацията, тъй като за нас е изключително важно да имаме контакт с родител на всяко дете, посещаващо занятия, както и с всеки член на клуба. Не предоставяме телефона Ви на трети лица.";
        public const string SchoolDataUsageInfo = "Информацията за училището, в което съответният ученик школува, ни дава възможност да разпределяме отбори за ученически първенства, както и да издаваме извинителни бележки за отсъствие от учебни часове при участия в състезания.";

        public const string Authentication = "Разпознаване";
        public const string ClubStatus = "Клубен статут";
        public const string PaymentsMade = "Направени плащания";
        public const string PersonalData = "Лични данни";
        public const string Privacy = "Поверителност";
        public const string Profile = "Профил";
        public const string TrainingSchedule = "Тренировъчен график";

        // Profile view
        public const string ProfileDataUpdatedMsg = "Вашите профилни данни са обновени успешно!";
        public const string ProfileManagementTitle = "Управление на профила";
        public const string ProfileManagementSubtitle = "Тук можете да промените входящите си настройки.";

        // Registration confirmation view
        public const string RegistrationConfirmationInstructions = "Моля, проверете в кутията на своя email за изпратен от нас линк, за да потвърдите своята регистрация.";

        // Registration view
        public const string RegistrationLabel = "Регистрация";

        // Search
        public const string SearchByCategories = "Търсене по категории";
        public const string SearchByName = "Търсене по име";
        public const string NoMatchesFound = "Няма открити съвпадения!";

        // Status view
        public const string ChampionStatus = "Шампион";
        public const string ChampionStatusInfo = "Шампионите са състезатели, отличили се с високи постижения в държавни шахматни турнири. Този статут се дава след спечелено първо място на държавно първенство или две първи места от друг голям турнир. Шампионите се освобождават от месечна такса.";
        public const string ClubStatusConfirmed = "Клубният Ви статут е потвърден!";
        public const string ClubStatusInfo = "Моля, прочетете всяка една от опциите и изберете Вашия клубен статут. След завършване на регистрацията ние ще разгледаме заявения от Вас статут и при съответствие до 1 ден ще го одобрим. Необходимо е след това да попълните допълнителни данни от менюто \"Клубни настройки\" в профила Ви.";
        public const string CompetitorStatus = "Състезател";
        public const string CompetitorStatusInfo = "Състезателите са ученици, които проявяват голям интерес и активно участват в шахматни състезания. Този статут се дава, след като ученикът е взел участие в поне 3 извънградски турнира за 1 година и е картотекиран към нашия клуб. Състезателите получават възможност да посещават по-често тренировки за същия размер на месечната такса.";
        public const string PendingApproval = "Новопостъпващ";
        public const string PendingApprovalInfo = "Отбележете тази опция, ако досега не сте посещавали тренировки при нас и не сте заплатили месечна такса, но желаете да го направите. След регистрацията е необходимо да изберете предпочитани дни за посещение от менюто \"Клубни настройки\" във Вашия профил.";
        public const string StudentStatus = "Ученик";
        public const string StudentStatusInfo = "Изберете тази опция, ако вече сте заплатили месечна такса. Занятия за учениците се провеждат присъствено и онлайн всеки делник, като всяко дете се включва в група и следва съответния график. Учениците заплащат месечна такса в размер на 50 лв, която включва поне 2 тренировки седмично.";
        public const string ValidateStatus = "Заяви избрания статут";
        public const string ValidateStatusInfo = "При натискане на бутона Вие ще ни изпратите заявка за потвърждаване на статута Ви, а ние при съответствие ще я одобрим.";

        // Students view
        public const string Actions = "Действия";
        public const string AddNewGroup = "Добави нова група";
        public const string AttendanceCount = "Посещения";
        public const string BirthDateDataTable = "Роден/а";
        public const string ClubRating = "Клубно ЕЛО";
        public const string CreatingNewGroup = "Създаване на нова група";
        public const string DateOfJoiningCurrentGroup = "Дата на включване в групата";
        public const string DateOfJoiningTheClub = "Дата на записване в клуба";
        public const string Group = "Група";
        public const string LastAttendance = "Последно посещение";
        public const string PhoneDataTable = "Телефон";
        public const string Students = "Ученици";
        public const string StudentsPage = "StudentsPage";
        public const string UserClubDataNotAddedYet = "Все още не са въведени клубни данни за потребителя.";

        // ============ STATIC CONSTANTS ============================================================================================
        // Images
        public static readonly string[] AllowedImageExtensions = new[] { "gif", "jpeg", "jpg", "png" };
    }
}
