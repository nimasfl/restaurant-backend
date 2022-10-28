using System.Globalization;

namespace Restaurant.Common;

public static class DateTimeExtensions
{
    public static DateTime GetDateOfFirstDayInJalaliMonth(this DateTime dateTime)
    {
        var pc = new PersianCalendar();
        var month = pc.GetMonth(dateTime).ToString().PadLeft(2, '0');
        var date = "01";
        var year = pc.GetYear(dateTime).ToString().PadLeft(4, '0');
        var jalaliDate = year + "/" + month + "/" + date;
        return DateTime.ParseExact(jalaliDate, "yyyy/MM/dd", new CultureInfo("fa-IR"));
    }

    public static DateTime GetDateOfLastDayInJalaliMonth(this DateTime dateTime)
    {
        var pc = new PersianCalendar();
        var year = pc.GetYear(dateTime);
        var month = pc.GetMonth(dateTime);
        var yearString = year.ToString().PadLeft(4, '0');
        var monthString = month.ToString().PadLeft(2, '0');
        var date = pc.GetDaysInMonth(year, month);
        var jalaliDate = yearString + "/" + monthString + "/" + date;
        return DateTime.ParseExact(jalaliDate, "yyyy/MM/dd", new CultureInfo("fa-IR"));
    }

    public static bool IsOccasionDateInFewDay(this DateTime date, int days)
    {
        var now = DateTime.Now;
        for (var i = 1; i <= days; i++)
        {
            var destination = now.AddDays(i);
            if (destination.Day == date.Day && destination.Month == date.Month)
            {
                return true;
            }
        }

        return false;
    }

    public static bool IsOccasionDate(this DateTime date)
    {
        var now = DateTime.Now;
        if (now.Day == date.Day && now.Month == date.Month)
        {
            return true;
        }

        return false;
    }

    public static DateTime ToAbsoluteStart(this DateTime dateTime)
    {
        var date = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(dateTime.Date, "UTC");
        var tehranTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Iran Standard Time");
        var tehranTime = TimeZoneInfo.ConvertTimeFromUtc(date, tehranTimeZone);
        return TimeZoneInfo.ConvertTimeToUtc(tehranTime.Date, tehranTimeZone);
    }

    public static DateTime ToAbsoluteEnd(this DateTime dateTime)
    {
        return ToAbsoluteStart(dateTime).AddDays(1).AddTicks(-1);
    }

    public static DateTime ToIranTimeZone(this DateTime dateTime)
    {
        return TimeZoneInfo.ConvertTimeBySystemTimeZoneId(dateTime, "UTC", "Iran Standard Time");
    }

    public static DateTime ToUtcTimeZone(this DateTime dateTime)
    {
        return TimeZoneInfo.ConvertTimeBySystemTimeZoneId(dateTime, "Iran Standard Time", "UTC");
    }
    
    public static string GetJalaliDateString(this DateTime dateTime)
    {
        try
        {
            var pc = new PersianCalendar();
            var month = pc.GetMonth(dateTime).ToString().PadLeft(2, '0');
            var date = pc.GetDayOfMonth(dateTime).ToString().PadLeft(2, '0');
            var year = pc.GetYear(dateTime).ToString().PadLeft(4, '0');
            return year + "/" + month + "/" + date;
        }
        catch
        {
            return "";
        }
    }
}
