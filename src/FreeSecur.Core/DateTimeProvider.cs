using System;

namespace FreeSecur.Core
{
    internal class DateTimeProvider : IDateTimeProvider
    {
        private const DateTimeKind _dateTimeKind = DateTimeKind.Utc;

        public DateTime Now => DateTime.Now;
        public DateTime GetDateTime(int year, int month, int day, int hour, int minute, int second)
        {
            return new DateTime(year, month, day, hour, minute, second, _dateTimeKind);
        }
        public DateTime GetDateTime(int year, int month, int day)
        {
            return GetDateTime(year, month, day, default, default, default);
        }
    }
}
