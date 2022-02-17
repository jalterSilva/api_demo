using System;
using System.Globalization;

namespace GSP.API.Core.Helpers.ExtensionMethods
{
    public static class DatetimeExtensions
    {
        public static string ToBrazilianDateFormat(this DateTime dateTime) => dateTime.ToString("dd/MM/yyyy");

        public static string ToBrazilianDateTimeFormat(this DateTime dateTime) => dateTime.ToString("dd/MM/yyyy HH:mm:ss");

        public static string ToEnglishDateFormat(this DateTime dateTime) => dateTime.ToString("MM/dd/yyyy");

        public static string ToEnglishDateTimeFormat(this DateTime dateTime) => dateTime.ToString("MM/dd/yyyy hh:mm tt", CultureInfo.InvariantCulture);
    }
}
