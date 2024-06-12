namespace BookingSystem.Domain.Extensions
{
    public static class TimeOnlyExtension
    {
        public static DateTime ToDateTime(this TimeOnly timeOnly, DateOnly date)
        {
            var dateTime = new DateTime(date.Year, date.Month, date.Day);
            dateTime += timeOnly.ToTimeSpan();

            return dateTime;
        }
    }
}
