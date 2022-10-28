namespace Restaurant.Common.PersianDate
{
    public static partial class Utility
    {
        public static string GetPersianDateString(this DateTime dateTime)
        {
            try
            {
                var pc = new System.Globalization.PersianCalendar();
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
}
