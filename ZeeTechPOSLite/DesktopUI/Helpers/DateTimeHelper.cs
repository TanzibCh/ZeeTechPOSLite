using System;
using System.Collections.Generic;
using System.Text;

namespace DesktopUI.Helpers
{
    public class DateTimeHelper
    {
        public DateTime ConvertToLocalTime(string timeValue)
        {
            DateTime utcTime = Convert.ToDateTime(timeValue);
            DateTime localTime = utcTime.ToLocalTime();

            return localTime;
        }
    }
}