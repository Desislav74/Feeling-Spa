namespace FeelingSpa.Services.DateTimeParser
{
    using System;
    using System.Globalization;

    using FeelingSpa.Common;

    public class DateTimeParserService : IDateTimeParserService
    {
        public System.DateTime ConvertStrings(string date, string time)
        {
            string dateString = date + " " + time;
            string format = GlobalConstants.DateTimeFormats.DateTimeFormat;

            System.DateTime dateTime = DateTime.ParseExact(dateString, format, CultureInfo.InvariantCulture);

            return dateTime;
        }
    }
}
