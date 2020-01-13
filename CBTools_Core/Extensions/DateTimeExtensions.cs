using System;

namespace CBTools_Core.Extensions {
    public static class DateTimeExtensions {
        private const double leapOffset = 0.242190;
        private const double tropicalYearLength = 365 + leapOffset;

        public static string RoughTimeSpan(DateTime start, DateTime end, double justNowMinutes = 0, bool spellOut = true) {
            double minutes = Math.Abs((end - start).TotalMinutes);
            if (justNowMinutes > 0 && minutes < justNowMinutes)
                return "just now";//Short curcuit here if possible

            double seconds = Math.Abs((end - start).TotalSeconds);
            if (seconds < 2) {
                return (spellOut ? "a" : "1") + " second";
            }
            else if (seconds < 60) {
                return (spellOut ? ((int)seconds).SpellOut() : seconds.ToString("0")) + " seconds";
            }
            else {
                if (minutes < 2) {
                    return (spellOut ? "a" : "1") + " minute";
                }
                else if (minutes < 60) {
                    return (spellOut ? ((int)minutes).SpellOut() : minutes.ToString("0")) + " minutes";
                }
                else {
                    double hours = Math.Abs((end - start).TotalHours);
                    if (hours < 2)
                        return (spellOut ? "an" : "1") + " hour";
                    if (hours < 19) {
                        return (spellOut ? ((int)hours).SpellOut() : hours.ToString("0")) + " hours";
                    }
                    else {
                        double days = Math.Abs((end - start).TotalDays);
                        if (days < 2) {
                            return (spellOut ? "a" : "1") + " day";
                        }
                        else if (days < 7) {
                            return (spellOut ? ((int)days).SpellOut() : days.ToString("0")) + " days";
                        }
                        else {
                            double weeks = days / 7;
                            if (weeks < 2) {
                                return (spellOut ? "a" : "1") + " week";
                            }
                            else if (days < 31) {
                                return (spellOut ? ((int)weeks).SpellOut() : weeks.ToString("0")) + " weeks";
                            }
                            else {
                                double months = days / (365.25 / 12d);
                                return months < 2
                                    ? (spellOut ? "a" : "1") + " month"
                                    : months < 12
                                    ? (spellOut ? ((int)months).SpellOut() : months.ToString("0")) + " months"
                                    : months < 14
                                    ? (spellOut ? "a" : "1") + " year"
                                    : months < 24
                                    ? (spellOut ? ((int)months).SpellOut() : months.ToString("0")) + " months"
                                    : (spellOut ? ((int)(months / 12d)).SpellOut() : (months / 12d).ToString("0")) + " years";
                            }
                        }
                    }
                }
            }
        }

        public static string RoughAge(this DateTime time, bool UTC = false, double justNowMinutes = 0, bool spellOut = true) => RoughTimeSpan((UTC ? DateTime.UtcNow : DateTime.Now), time, justNowMinutes, spellOut);

        //private static string RoughTimeDifference(this DateTime time, DateTime end, double justNowMinutes = 0, bool spellOut = true) => justNowMinutes < Math.Abs((end - time).TotalMinutes) ? "just now" : ("about " + RoughTimeSpan(time, end, justNowMinutes, spellOut) + (end > time ? " from now" : " ago"));

        public static double TotalWeeks(this TimeSpan time) => time.TotalDays / 7;

        public static double TotalMonths(this TimeSpan time) => time.TotalMonths() * 12;

        public static double TotalYears(this TimeSpan time) => time.TotalDays / tropicalYearLength;

        /// <summary>
        /// Returns true if the given date falls on a leap year
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static bool IsLeapYear(this DateTime date) => IsLeapYear(date.Year);
        private static bool IsLeapYear(int year) => year % 4 == 0 && !(year % 100 == 0 && year % 400 != 0);

        /// <summary>
        /// Calculate age from current system time, accounting for leap years
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static int Age(this DateTime date) => date.Age(DateTime.Now);

        /// <summary>
        /// Calculate age from a given point in time, accounting for leap years
        /// </summary>
        /// <param name="date"></param>
        /// <param name="ageFrom">When to treat as current time.</param>
        /// <returns></returns>
        public static int Age(this DateTime date, DateTime ageFrom) {
            double offset = 0;
            int offsetYear = date.Year;
            for (int i = 0; i < 4; i++) {
                if (IsLeapYear(offsetYear + i)) {
                    break;
                }
                offset += leapOffset;
            }

            double age = ((ageFrom - date).TotalDays - offset) / tropicalYearLength;

            return (int)Math.Floor(age);
        }
    }
}
