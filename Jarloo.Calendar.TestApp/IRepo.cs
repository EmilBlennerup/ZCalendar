using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Jarloo.Calendar.TestApp
{
    public interface IRepo
    {
        void SaveDate(Day day);
        string CheckDayForNotes(DateTime date);
        IEnumerable<Day> GetNotesForDays(IEnumerable<Day> dates);
    }
}
