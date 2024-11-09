using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace devarts.Helpers
{
    public struct DateTimeSpan
    {
        private readonly int years;
        private readonly int months;
        private readonly int days;
        private readonly int hours;
        private readonly int minutes;
        private readonly int seconds;
        private readonly int milliseconds;

        public DateTimeSpan(int years, int months, int days, int hours, int minutes, int seconds, int milliseconds)
        {
            this.years = years;
            this.months = months;
            this.days = days;
            this.hours = hours;
            this.minutes = minutes;
            this.seconds = seconds;
            this.milliseconds = milliseconds;
        }

        public int Years { get { return years; } }
        public int Months { get { return months; } }
        public int Days { get { return days; } }
        public int Hours { get { return hours; } }
        public int Minutes { get { return minutes; } }
        public int Seconds { get { return seconds; } }
        public int Milliseconds { get { return milliseconds; } }

        enum Phase { Years, Months, Days, Done }

        public static DateTimeSpan CompareDates(DateTime date1, DateTime date2)
        {
            if (date2 < date1)
            {
                var sub = date1;
                date1 = date2;
                date2 = sub;
            }

            DateTime current = date1;
            int years = 0;
            int months = 0;
            int days = 0;

            Phase phase = Phase.Years;
            DateTimeSpan span = new DateTimeSpan();
            int officialDay = current.Day;

            while (phase != Phase.Done)
            {
                switch (phase)
                {
                    case Phase.Years:
                        if (current.AddYears(years + 1) > date2)
                        {
                            phase = Phase.Months;
                            current = current.AddYears(years);
                        }
                        else
                        {
                            years++;
                        }
                        break;
                    case Phase.Months:
                        if (current.AddMonths(months + 1) > date2)
                        {
                            phase = Phase.Days;
                            current = current.AddMonths(months);
                            if (current.Day < officialDay && officialDay <= DateTime.DaysInMonth(current.Year, current.Month))
                                current = current.AddDays(officialDay - current.Day);
                        }
                        else
                        {
                            months++;
                        }
                        break;
                    case Phase.Days:
                        if (current.AddDays(days + 1) > date2)
                        {
                            current = current.AddDays(days);
                            var timespan = date2 - current;
                            span = new DateTimeSpan(years, months, days, timespan.Hours, timespan.Minutes, timespan.Seconds, timespan.Milliseconds);
                            phase = Phase.Done;
                        }
                        else
                        {
                            days++;
                        }
                        break;
                }
            }

            return span;
        }
    }

    public class AgeHelper
    {
        public string HowOld(string bornDate, string deathDate)
        {
            try
            {
                if (string.IsNullOrEmpty(deathDate))
                {
                    string pluralizeMonth = "";
                    DateTimeSpan dateSpan;
                    try
                    {
                        dateSpan = DateTimeSpan.CompareDates(DateTime.ParseExact(bornDate, "dd.MM.yyyy", CultureInfo.InvariantCulture), DateTime.Now);
                    }
                    catch
                    {
                        dateSpan = DateTimeSpan.CompareDates(DateTime.ParseExact(bornDate, "yyyy.MM.dd", CultureInfo.InvariantCulture), DateTime.Now);
                    }

                    switch (dateSpan.Months)
                    {
                        case 0:
                            pluralizeMonth = "miesięcy";
                            break;
                        case 1:
                            pluralizeMonth = "miesiąc";
                            break;                            
                        case 2:
                            pluralizeMonth = "miesiące";
                            break;
                        case 3:
                            pluralizeMonth = "miesiące";
                            break;
                        case 4:
                            pluralizeMonth = "miesiące";
                            break;
                        case 5:
                            pluralizeMonth = "miesięcy";
                            break;
                        case 6:
                            pluralizeMonth = "miesięcy";
                            break;
                        case 7:
                            pluralizeMonth = "miesięcy";
                            break;
                        case 8:
                            pluralizeMonth = "miesięcy";
                            break;
                        case 9:
                            pluralizeMonth = "miesięcy";
                            break;
                        default:
                            pluralizeMonth = "miesięcy";
                            break;

                    }

                    if (dateSpan.Years == 0)
                    {
                        return dateSpan.Months + " " + pluralizeMonth + " i " + dateSpan.Days + " dni";
                    }
                    if (dateSpan.Years == 1)
                    {
                        return dateSpan.Years + " rok, " + dateSpan.Months + " " + pluralizeMonth + " i " + dateSpan.Days + " dni";
                    }
                    if (dateSpan.Years > 1 && dateSpan.Years <= 4)
                    {
                        return dateSpan.Years + " lata, " + dateSpan.Months + " " + pluralizeMonth + " i " + dateSpan.Days + " dni";
                    }
                    if (dateSpan.Years >= 5)
                    {
                        return dateSpan.Years + " lat, " + dateSpan.Months + " " + pluralizeMonth + " i " + dateSpan.Days + " dni";
                    }

                    return dateSpan.Years + " lat, " + dateSpan.Months + " " + pluralizeMonth + " i " + dateSpan.Days + " dni";
                }
                /// piesek nie żyje - liczymy czas do momentu śmierci
                else
                {
                    string pluralizeMonth = "";
                    DateTimeSpan dateSpan;
                    try
                    {
                        dateSpan = DateTimeSpan.CompareDates(DateTime.ParseExact(bornDate, "dd.MM.yyyy", CultureInfo.InvariantCulture), DateTime.ParseExact(deathDate, "dd.MM.yyyy", CultureInfo.InvariantCulture));
                    }
                    catch
                    {
                        dateSpan = DateTimeSpan.CompareDates(DateTime.ParseExact(bornDate, "yyyy.MM.dd", CultureInfo.InvariantCulture), DateTime.ParseExact(deathDate, "dd.MM.yyyy", CultureInfo.InvariantCulture));
                    }

                    switch (dateSpan.Months)
                    {
                        case 0:
                            pluralizeMonth = "miesięcy";
                            break;
                        case 1:
                            pluralizeMonth = "miesiąc";
                            break;
                        case 2:
                            pluralizeMonth = "miesiące";
                            break;
                        case 3:
                            pluralizeMonth = "miesiące";
                            break;
                        case 4:
                            pluralizeMonth = "miesiące";
                            break;
                        case 5:
                            pluralizeMonth = "miesięcy";
                            break;
                        case 6:
                            pluralizeMonth = "miesięcy";
                            break;
                        case 7:
                            pluralizeMonth = "miesięcy";
                            break;
                        case 8:
                            pluralizeMonth = "miesięcy";
                            break;
                        case 9:
                            pluralizeMonth = "miesięcy";
                            break;
                        default:
                            pluralizeMonth = "miesięcy";
                            break;

                    }

                    if (dateSpan.Years == 0)
                    {
                        return dateSpan.Months + " " + pluralizeMonth + " i " + dateSpan.Days + " dni";
                    }
                    if (dateSpan.Years == 1)
                    {
                        return dateSpan.Years + " rok, " + dateSpan.Months + " " + pluralizeMonth + " i " + dateSpan.Days + " dni";
                    }
                    if (dateSpan.Years > 1 && dateSpan.Years <= 4)
                    {
                        return dateSpan.Years + " lata, " + dateSpan.Months + " " + pluralizeMonth + " i " + dateSpan.Days + " dni";
                    }
                    if (dateSpan.Years >= 5)
                    {
                        return dateSpan.Years + " lat, " + dateSpan.Months + " " + pluralizeMonth + " i " + dateSpan.Days + " dni";
                    }

                    return dateSpan.Years + " lat, " + dateSpan.Months + " " + pluralizeMonth + " i " + dateSpan.Days + " dni";
                }
            }
            catch (Exception ex)
            {
                return "";//" - brak danych." + ex.ToString() + "Data - " + bornDate;
            }
        }

        public int HowManyYears(string bornDate)
        {
            try
            {
                DateTimeSpan dateSpan;
                try
                {
                    dateSpan = DateTimeSpan.CompareDates(DateTime.ParseExact(bornDate, "dd.MM.yyyy", CultureInfo.InvariantCulture), DateTime.Now);
                }
                catch
                {
                    dateSpan = DateTimeSpan.CompareDates(DateTime.ParseExact(bornDate, "yyyy.MM.dd", CultureInfo.InvariantCulture), DateTime.Now);
                }

                //Console.WriteLine("Years: " + dateSpan.Years);
                //Console.WriteLine("Months: " + dateSpan.Months);
                //Console.WriteLine("Days: " + dateSpan.Days);
                //Console.WriteLine("Hours: " + dateSpan.Hours);
                //Console.WriteLine("Minutes: " + dateSpan.Minutes);
                //Console.WriteLine("Seconds: " + dateSpan.Seconds);
                //Console.WriteLine("Milliseconds: " + dateSpan.Milliseconds);

                return dateSpan.Years;
            }
            catch (Exception ex)
            {
                return 0;//" - brak danych." + ex.ToString() + "Data - " + bornDate;
            }
        }
    }
}