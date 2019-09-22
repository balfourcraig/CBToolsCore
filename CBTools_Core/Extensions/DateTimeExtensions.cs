using System;
using System.Collections.Generic;
using System.Text;

namespace CBTools_Core.Extensions
{
    public static class DateTimeExtensions
    {
        public static string RoughTimeSpan(DateTime start, DateTime end, double justNowMinutes = 0, bool spellOut = true)
        {
            double minutes = Math.Abs((end - start).TotalMinutes);
            if (justNowMinutes > 0 && minutes < justNowMinutes)
                return "just now";//Short curcuit here if possible

            double seconds = Math.Abs((end - start).TotalSeconds);
            if (seconds < 2)
                return (spellOut ? "a" : "1") + " second";
            else if (seconds < 60)
                return (spellOut ? ((int)seconds).SpellOut() : seconds.ToString("0")) + " seconds";
            else
            {
                if (minutes < 2)
                    return (spellOut ? "a" : "1") + " minute";
                else if (minutes < 60)
                    return (spellOut ? ((int)minutes).SpellOut() : minutes.ToString("0")) + " minutes";
                else
                {
                    double hours = Math.Abs((end - start).TotalHours);
                    if (hours < 2)
                        return (spellOut ? "an" : "1") + " hour";
                    if (hours < 19)
                        return (spellOut ? ((int)hours).SpellOut() : hours.ToString("0")) + " hours";
                    else
                    {
                        double days = Math.Abs((end - start).TotalDays);
                        if (days < 2)
                            return (spellOut ? "a" : "1") + " day";
                        else if (days < 7)
                            return (spellOut ? ((int)days).SpellOut() : days.ToString("0")) + " days";
                        else
                        {
                            double weeks = days / 7;
                            if (weeks < 2)
                                return (spellOut ? "a" : "1") + " week";
                            else if (days < 31)
                                return (spellOut ? ((int)weeks).SpellOut() : weeks.ToString("0")) + " weeks";
                            else
                            {
                                double months = days / (365.25 / 12d);
                                if (months < 2)
                                    return (spellOut ? "a" : "1") + " month";
                                else if (months < 12)
                                    return (spellOut ? ((int)months).SpellOut() : months.ToString("0")) + " months";
                                else if (months < 14)
                                    return (spellOut ? "a" : "1") + " year";
                                else if (months < 24)
                                    return (spellOut ? ((int)months).SpellOut() : months.ToString("0")) + " months";
                                else
                                    return (spellOut ? ((int)(months / 12d)).SpellOut() : (months / 12d).ToString("0")) + " years";
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

        public static double TotalYears(this TimeSpan time) => time.TotalDays / 365.25;


    }
}
