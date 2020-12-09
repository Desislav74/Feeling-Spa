namespace FeelingSpa.Services.DateTimeParser
{
    using System;
    using System.Globalization;

    public class DateTimeParserService : IDateTimeParserService
    {
        public System.DateTime ConvertStrings(string date, string time)
        {
            string dateString = date + " " + time;
            string format = "dd-MM-yyyy h:mmtt";

            System.DateTime dateTime = DateTime.ParseExact(dateString, format, CultureInfo.InvariantCulture);

            return dateTime;
        }
    }
}
