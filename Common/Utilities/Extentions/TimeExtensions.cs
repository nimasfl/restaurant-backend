namespace Restaurant.Common;

public static class TimeExtensions
{
    public static string ParseMinToHourDuration(this int min)
    {
        var hour = min / 60;
        var minute = min % 60;
        var hourString = hour.ToString().PadLeft(2, '0');
        var minuteString = minute.ToString().PadLeft(2, '0');
        return $"{hourString}:{minuteString}";
    }
}

