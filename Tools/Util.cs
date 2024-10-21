using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Management_App.Tools
{
    internal class Util
    {
        public static bool IsDateInRange(DateTime date, DateTime startDate, DateTime endDate)
        {
            return date >= startDate && date <= endDate;
        }

        public static bool PeriodsDoNotOverlap(DateTime start1, DateTime end1, DateTime start2, DateTime end2)
        {
            // Periods overlap if one starts before the other ends and ends after the other starts.
            return !(end1 < start2 || end2 < start1); // returns false if they do overlap, since the !
        }
    }
}
