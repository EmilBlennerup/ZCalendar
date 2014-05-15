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
using Jarloo.Calendar.DataRepo;
using Points;

namespace Jarloo.Calendar.TestApp
{
    public partial class MainWindow : Window
    {
        private Jarloo.Calendar.DataRepo.IRepo mRepo;
        private double totalPointMonth; 
        public MainWindow()
        {
            totalPointMonth = 0.0;
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
            InitCalendar();
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
            var editedDays= mRepo.GetNotesForDays(daysOfMont);
            var t = new ParseMonth(editedDays);
            tbPoints.Text = t.pointsOfMonth.ToString();
        }

        private void InitCalendar()
        {
            
            int year = (int)cboYear.SelectedItem;

            int month = DateTime.Now.Month;

            DateTime targetDate = new DateTime(year, month, 1);

            Calendar.BuildCalendar(targetDate);
            var daysOfMont = Calendar.Days.Where<Day>(d => d.Date.Month == month);
            var editedDays = mRepo.GetNotesForDays(daysOfMont);
            var t = new ParseMonth(editedDays);
            totalPointMonth = t.pointsOfMonth;
            tbPoints.Text = t.pointsOfMonth.ToString();
        }
        private static Day currentDay;
        private bool init = true;
        private Day previousDay;
        private void Calendar_DayChanged(object sender, DayChangedEventArgs e)
        {
            
                //create shallow copy of date to be removed
                
                Day d2 = new Day();
                d2.Date = e.Day.Date;
                
                //user is deleting a entry
                var entry = mRepo.CheckDayForNotes(e.Day.Date);
                d2.Notes = entry;
                var pointsToRemove = Rules.GetPointsForDay(d2);
                totalPointMonth -= pointsToRemove;
                
            
            //save the text edits to persistant storage
            mRepo.SaveDate(e.Day);
            var newPoint = Rules.GetPointsForDay(e.Day);
                        
            totalPointMonth += newPoint;
            tbPoints.Text = totalPointMonth.ToString();
        }

        private void MenuItem_PointsValueClick(object sender, RoutedEventArgs e)
        {

            SetPointsValue s = new SetPointsValue();
            var res = s.ShowDialog();
            e.Handled = true;

        }

        private void MenuItem_HollidayClick(object sender, RoutedEventArgs e)
        {
            InjectHolidays i = new InjectHolidays();
            i.ShowDialog();
            e.Handled = true;
        }

        

    }       
}