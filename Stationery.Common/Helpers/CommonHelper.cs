namespace Stationery.Common.Helpers
{
    using Stationery.Common.Enums;
    using System;
    using System.ComponentModel;
    using System.Globalization;
    using System.Linq;
    using System.Linq.Expressions;

    /// <summary>
    /// RedzoneHelper
    /// </summary>
    public static class CommonHelper
    {
        #region Methods

        /// <summary>
        /// To the enum.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static T ToEnum<T>(this string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return default(T);
            }

            return (T)Enum.Parse(typeof(T), value, true);
        }

        /// <summary>
        /// To the enum description.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static string ToEnumDescription<T>(this int value)
        {
            Type type = typeof(T);
            var memInfo = type.GetMember(type.GetEnumName(value));
            var descriptionAttribute = memInfo[0]
                .GetCustomAttributes(typeof(DescriptionAttribute), false)
                .FirstOrDefault() as DescriptionAttribute;

            if (descriptionAttribute != null)
            {
                return descriptionAttribute.Description;
            }

            return string.Empty;
        }

        /// <summary>
        /// Adds the ordering.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <param name="source">The source.</param>
        /// <param name="keySelector">The key selector.</param>
        /// <param name="descending">if set to <c>true</c> [descending].</param>
        /// <returns></returns>
        public static IOrderedQueryable<T> AddOrdering<T, TKey>(this IQueryable<T> source, Expression<Func<T, TKey>> keySelector, bool descending)
        {
            // If it's not ordered yet, use OrderBy/OrderByDescending.
            if (source.Expression.Type != typeof(IOrderedQueryable<T>))
            {
                return descending ? source.OrderByDescending(keySelector)
                                  : source.OrderBy(keySelector);
            }

            IOrderedQueryable<T> ordered = source as IOrderedQueryable<T>;

            // Already ordered, so use ThenBy/ThenByDescending
            return descending ? ordered.ThenByDescending(keySelector)
                              : ordered.ThenBy(keySelector);
        }

        // This presumes that weeks start with Monday.
        // Week 1 is the 1st week of the year with a Thursday in it.
        public static int GetIso8601WeekOfYear(DateTime time)
        {
            // Seriously cheat.  If its Monday, Tuesday or Wednesday, then it'll 
            // be the same week# as whatever Thursday, Friday or Saturday are,
            // and we always get those right
            DayOfWeek day = CultureInfo.InvariantCulture.Calendar.GetDayOfWeek(time);
            if (day >= DayOfWeek.Monday && day <= DayOfWeek.Wednesday)
            {
                time = time.AddDays(3);
            }

            // Return the week of our adjusted day
            return CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(time, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
        }

        /// <summary>
        /// Calculates the start time.
        /// </summary>
        /// <returns></returns>
        public static int GetUtcOffset(DateTime eventTime)
        {
            TimeZoneInfo destinationTimeZone = TimeZoneInfo.FindSystemTimeZoneById("W. Europe Standard Time");
            return destinationTimeZone.GetUtcOffset(eventTime).Hours;
        }

        /// <summary>
        /// Calculates the start time.
        /// </summary>
        /// <returns></returns>
        public static string CalculateStartTime()
        {
            //TimeZoneInfo destinationTimeZone = TimeZoneInfo.FindSystemTimeZoneById("W. Europe Standard Time");
            //var difference = destinationTimeZone.GetUtcOffset(DateTime.Now).Hours;
            //return ToEnumDescription<TimeRange>(16 - difference);

            return "16:00";
        }

        /// <summary>
        /// Calculates the start time.
        /// </summary>
        /// <returns></returns>
        public static string CalculateEndTime()
        {
            return "17:00";
        }

        /// <summary>
        /// Calculates the start time.
        /// </summary>
        /// <returns></returns>
        public static DateTime? ConvertTimeFromUtc(DateTime? dateTime)
        {
            if (dateTime.HasValue)
            {
                TimeZoneInfo destinationTimeZone = TimeZoneInfo.FindSystemTimeZoneById("W. Europe Standard Time");
                return TimeZoneInfo.ConvertTimeFromUtc(dateTime.Value, destinationTimeZone);
            }

            return dateTime;
        }

        /// <summary>
        /// Calculates the start time.
        /// </summary>
        /// <returns></returns>
        public static DateTime? ConvertTimeToUtc(DateTime? dateTime)
        {
            if (dateTime.HasValue)
            {
                return TimeZoneInfo.ConvertTimeToUtc(dateTime.Value);
            }

            return dateTime;
        }

        /// <summary>
        /// Gets the distance from lat lon in km.
        /// </summary>
        /// <param name="lat1">The lat1.</param>
        /// <param name="lon1">The lon1.</param>
        /// <param name="lat2">The lat2.</param>
        /// <param name="lon2">The lon2.</param>
        /// <returns></returns>
        public static double GetDistanceFromLatLonInKm(double lat1, double lon1, double lat2, double lon2)
        {
            var R = 6371; // Radius of the earth in km
            var dLat = Deg2Rad(lat2 - lat1);  // deg2rad below
            var dLon = Deg2Rad(lon2 - lon1);
            var a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
              Math.Cos(Deg2Rad(lat1)) * Math.Cos(Deg2Rad(lat2)) *
              Math.Sin(dLon / 2) * Math.Sin(dLon / 2);
            var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            var d = R * c; // Distance in km
            return d;
        }

        /// <summary>
        /// Deg2s the RAD.
        /// </summary>
        /// <param name="deg">The deg.</param>
        /// <returns></returns>
        private static double Deg2Rad(double deg)
        {
            return deg * (Math.PI / 180);
        }

        #endregion Methods
    }
}