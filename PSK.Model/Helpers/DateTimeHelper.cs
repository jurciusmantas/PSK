using System;

namespace PSK.Model.Helpers
{
    public static class DateTimeHelper
    {
        public static int GetQuarter(this DateTime date) => (date.Month + 2) / 3;
    }
}
