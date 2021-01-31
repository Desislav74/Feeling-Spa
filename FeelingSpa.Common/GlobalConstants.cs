namespace FeelingSpa.Common
{
    public static class GlobalConstants
    {
        public const string SystemName = "FeelingSpa";

        public const string AdministratorRoleName = "Administrator";

        public const string SalonManagerRoleName = "Manager";

        public static class AccountsSeeding
        {
            public const string Password = "123456";

            public const string AdminEmail = "desko@desko.com";

            public const string SalonManagerEmail = "manager@manager.com";
        }

        public static class DataValidations
        {
            public const int TitleMaxLength = 30;

            public const int TitleMinLength = 5;

            public const int ContentMinLength = 20;

            public const int NameMaxLength = 80;

            public const int CategoryNameMaxLength = 20;

            public const int CityNameMaxLength = 50;

            public const int ServiceNameMaxLength = 30;

            public const int NameMinLength = 2;

            public const int CategoryNameMinLength = 2;

            public const int CityNameMinLength = 3;

            public const int ServiceNameMinLength = 3;

            public const int DescriptionMinLength = 20;

            public const int AddressMaxLength = 100;

            public const int AddressMinLength = 5;

            public const int AuthorMaxLength = 100;
        }

        public static class ErrorMessages
        {
            public const string DateTime = "Please select valid data and time";

            public const string Rating = "Please choose a valid number of stars from 1 to 5.";
        }

        public static class DateTimeFormats
        {
            public const string DateFormat = "dd-MM-yyyy";

            public const string TimeFormat = "h:mmtt";

            public const string DateTimeFormat = "dd-MM-yyyy h:mmtt";
        }
    }

}
