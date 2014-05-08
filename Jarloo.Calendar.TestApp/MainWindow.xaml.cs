/*
    Jarloo
    http://www.jarloo.com
 
    This work is licensed under a Creative Commons Attribution-ShareAlike 3.0 Unported License  
    http://creativecommons.org/licenses/by-sa/3.0/ 

*/

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows;
using System.Linq;
using System.Xml.Linq;
using System.IO;

namespace Jarloo.Calendar.TestApp
{
    public partial class MainWindow : Window
    {
        private IRepo mRepo;
        public MainWindow()
        {
            InitializeComponent();
            mRepo = new XMLRepo();

            List<string> months = new List<string> { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };
            cboMonth.ItemsSource = months;

            for (int i = -50; i < 50; i++)
            {
                cboYear.Items.Add(DateTime.Today.AddYears(i).Year);
            }

            cboMonth.SelectedItem = months.FirstOrDefault(w => w == DateTime.Today.ToString("MMMM"));
            cboYear.SelectedItem = DateTime.Today.Year;

            cboMonth.SelectionChanged += (o, e) => RefreshCalendar();
            cboYear.SelectionChanged += (o, e) => RefreshCalendar();
        }

        private void RefreshCalendar()
        {
            if (cboYear.SelectedItem == null) return;
            if (cboMonth.SelectedItem == null) return;

            int year = (int)cboYear.SelectedItem;

            int month = cboMonth.SelectedIndex + 1;

            DateTime targetDate = new DateTime(year, month, 1);

            Calendar.BuildCalendar(targetDate);
            var daysOfMont = Calendar.Days.Where<Day>(d => d.Date.Month == month);
            mRepo.GetNotesForDays(daysOfMont);
        }
        private static DateTime currentDay;
        private bool postDone = false;
        private Day previousDay;
        private void Calendar_DayChanged(object sender, DayChangedEventArgs e)
        {
            //save the text edits to persistant storage
            if (postDone)
            {
                if (currentDay != e.Day.Date)
                {
                    mRepo.SaveDate(previousDay);
                    currentDay = e.Day.Date;
                    postDone = false;
                }

            }
            else
            {
                currentDay = e.Day.Date;
                previousDay = e.Day;
                postDone = true;
            }
        }

    }       
}