namespace Restaurant.Common;

public static class IntExtensions
{
    public static double? GetProgressPercentage(this int newValue, int oldValue)
    {
        if (oldValue == 0)
        {
            return null;
        }
        return (newValue * 100 / oldValue) - 100;
    }

    public static double? GetPercentage(this int value, int total)
    {
        if (value ==  0)
        {
            return 0;
        }
        if (total == 0)
        {
            return null;
        }
        return (value * 100) / total;
    }
}
