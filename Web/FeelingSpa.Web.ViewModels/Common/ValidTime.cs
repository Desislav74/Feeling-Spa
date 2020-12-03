namespace FeelingSpa.Web.ViewModels.Common
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Globalization;

    public class ValidTime : RequiredAttribute
    {
        public override bool IsValid(object value)
        {
            var timeString = value as string;

            if (string.IsNullOrEmpty(timeString))
            {
                return false;
            }

            bool parsed = DateTime.TryParseExact(
                timeString,
                "h:mmtt",
                CultureInfo.InvariantCulture,
                style: DateTimeStyles.AssumeUniversal,
                result: out _);
            if (!parsed)
            {
                return false;
            }

            return true;
        }
    }
}
