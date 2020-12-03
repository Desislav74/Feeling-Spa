﻿namespace FeelingSpa.Web.ViewModels.Common
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Globalization;

    using FeelingSpa.Common;

    public class ValidDate : RequiredAttribute
    {
        public override bool IsValid(object value)
        {
            var dateString = value as string;

            if (string.IsNullOrEmpty(dateString))
            {
                return false;
            }

            DateTime dt;
            bool parsed = DateTime.TryParseExact(
                dateString,
                "dd-MM-yyyy",
                CultureInfo.InvariantCulture,
                style: DateTimeStyles.AssumeUniversal,
                result: out dt);
            if (!parsed)
            {
                return false;
            }

            if (dt < DateTime.UtcNow.Date)
            {
                return false;
            }

            return true;
        }
    }
}
