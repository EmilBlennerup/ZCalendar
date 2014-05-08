using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Xml.XPath;

namespace Jarloo.Calendar.TestApp
{
    public class XMLRepo : IRepo
    {
        private XElement repo;
        private XElement Repo
        {
            get
            {
                return repo ?? (repo = ReadBaseRepo());
            }
        }
        private static string repoPath = @"c:\Temp\repo.xml";
        private XElement ReadBaseRepo()
        {
            if (!File.Exists(repoPath))
            {
                var r = new XElement("Calendar");
                r.Save(repoPath);

            }
            var root = XElement.Load(repoPath);
            return root;
        }

        public void SaveDate(Day day)
        {
            var appointement = new XElement("Appointment");
            appointement.Add(new XElement("Date", day.Date.ToString("yyyy-MM-dd")));
            appointement.Add(new XElement("Content", day.Notes));
            Repo.Add(appointement);
            Repo.Save(repoPath);
        }

        public string CheckDayForNotes(DateTime date)
        {
            var entries = Repo.XPathSelectElement(string.Format("Appointment/Date[text()='{0}']/../Content", date.ToString("yyyy-MM-dd")));
            if (entries != null)
            {
            
                return entries.Value;
            }
            return null;
        }

        public IEnumerable<Day> GetNotesForDays(IEnumerable<Day> dates)
        {
            var d = dates.ToArray();
            var monthDayCollection = new List<Day>();
            foreach (var s in d)
            {
                var note = CheckDayForNotes(s.Date);
                if (note != null)
                {
                    s.Notes = note;
                    monthDayCollection.Add(s);
                }
            }
            return monthDayCollection;
        }
    }
    
}
