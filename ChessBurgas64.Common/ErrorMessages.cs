namespace ChessBurgas64.Common
{
    public static class ErrorMessages
    {
        public const string InvalidImageExtension = "Invalid image extension: ";

        public const string PasswordsDoNotMatch = "Паролите не съвпадат!";
        public const string EmailChangeError = "Възникна проблем при промяната на Вашия email.";
        public const string EmailConfirmationError = "Възникна проблем при потвърждението на Вашия email.";
        public const string UsernameChangeError = "Възникна проблем при промяната на Вашето потребителско име.";

        public const string InvalidLoginAttempt = "Грешно потребителско име или парола!";

        // Input errors ====================================================================================================
        public const string InvalidCaptcha = "Грешка! Моля, потвърдете, че не сте робот!";
        public const string InvalidInputData = "Грешка! Неправилно попълнени данни! Изписвайте новия си email правилно и потвърдете, че не сте робот.";
        public const string InvalidEmail = "Грешка! Невалиден email! Моля, поставете действителен такъв.";
        public const string InvalidPassword = "Грешка! Невалидна парола!";
        public const string InvalidPhoneNumber = "Грешка! Невалиден телефонен номер! Моля, поставете действителен такъв.";
        public const string InvalidUrl = "Грешка! Невалиден линк! Моля, поставете действителен такъв.";
        public const string ThatFieldIsRequired = "Това поле е задължително!";
        public const string ThatFieldRequiresNumberOfCharacters = "Това поле трябва да бъде с дължина между {2} и {1} символа.";
    }
}
