using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jarloo.Calendar;
namespace Points
{
    
    public static class Rules
    {
        //declare rules for days of week and times of day
        public static double SundayA = 15.2;
        public static double SundayN = 14.8;
        public static double SundayC = 13.45;
               
        public static double MondayA = 8.0;
        public static double MondayN = 13.2;
        public static double MondayC = 8.5;
               
        public static double TusedayA = 8.0;
        public static double TusedayN = 13.2;
        public static double TusedayC = 8.5;
               
        public static double WednesdayA = 8.0;
        public static double WednesdayN = 13.2;
        public static double WednesdayC = 8.5;
               
        public static double ThursdayA = 8.0;
        public static double ThursdayN = 13.2;
        public static double ThursdayC = 8.5;
               
        public static double FridayA = 8.0;
        public static double FridayN = 18.0;
        public static double FridayC = 11.5;
               
        public static double SaturdayA = 15.2;
        public static double SaturdayN = 18.0;
        public static double SaturdayC = 14.25;
        
        //Calculate points for a day given the date and the tour name.
        public static double GetPointsForDay(Day d)
        {
            var dayType = d.Date.DayOfWeek;
            var tourType = d.Notes;
            if (tourType != null)
            {
                switch (dayType)
                {
                    case DayOfWeek.Sunday:
                        if (tourType.ToLower() == "a")
                        {
                            return Rules.SundayA;
                        }
                        else if (tourType.ToLower() == "c")
                        {
                            return Rules.SundayC;
                        }
                        else if (tourType.ToLower() == "n")
                        {
                            return Rules.SundayN;
                        }
                        break;
                    case DayOfWeek.Monday:
                        if (tourType.ToLower() == "a")
                        {
                            return Rules.MondayA;
                        }
                        else if (tourType.ToLower() == "c")
                        {
                            return Rules.MondayC;
                        }
                        else if (tourType.ToLower() == "n")
                        {
                            return Rules.MondayN;
                        }
                        break;
                    case DayOfWeek.Tuesday:
                        if (tourType.ToLower() == "a")
                        {
                            return Rules.TusedayA;
                        }
                        else if (tourType.ToLower() == "c")
                        {
                            return Rules.TusedayC;
                        }
                        else if (tourType.ToLower() == "n")
                        {
                            return Rules.TusedayN;
                        }
                        break;
                    case DayOfWeek.Wednesday:
                        if (tourType.ToLower() == "a")
                        {
                            return Rules.WednesdayA;
                        }
                        else if (tourType.ToLower() == "c")
                        {
                            return Rules.WednesdayC;
                        }
                        else if (tourType.ToLower() == "n")
                        {
                            return Rules.WednesdayN;
                        }
                        break;
                    case DayOfWeek.Thursday:
                        if (tourType.ToLower() == "a")
                        {
                            return Rules.ThursdayA;
                        }
                        else if (tourType.ToLower() == "c")
                        {
                            return Rules.ThursdayC;
                        }
                        else if (tourType.ToLower() == "n")
                        {
                            return Rules.ThursdayN;
                        }
                        break;
                    case DayOfWeek.Friday:
                        if (tourType.ToLower() == "a")
                        {
                            return Rules.FridayA;
                        }
                        else if (tourType.ToLower() == "c")
                        {
                            return Rules.FridayC;
                        }
                        else if (tourType.ToLower() == "n")
                        {
                            return Rules.FridayN;
                        }
                        break;
                    case DayOfWeek.Saturday:
                        if (tourType.ToLower() == "a")
                        {
                            return Rules.SaturdayA;
                        }
                        else if (tourType.ToLower() == "c")
                        {
                            return Rules.SaturdayC;
                        }
                        else if (tourType.ToLower() == "n")
                        {
                            return Rules.SaturdayN;
                        }
                        break;
                    default:
                        //nop
                        break;

                }
            }
            return 0.0;
        }
    }

    public class ParseMonth
    {
        public double pointsOfMonth { get; set; }

        public ParseMonth(IEnumerable<Day> days)
        {
            pointsOfMonth = 0;
            foreach (Day d in days)
            {
                pointsOfMonth += Rules.GetPointsForDay(d);
            }
        }
    }
}
