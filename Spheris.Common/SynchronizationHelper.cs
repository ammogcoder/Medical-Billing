using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Spheris.Common
{
    public static class SynchronizationHelper
    {
        public static bool IsWeeklyRefresh;
        public static bool IsSemiMonthlyRefresh;
        public static bool IsMonthlyRefresh;

        public static DateRange EstablishRangeToRun(DateTime lastRunEndTime)
        {
            DateRange nextRun = new DateRange();
            DateTime lowerBoundOverride;
            DateTime upperBoundOverride;

            // See if there's a value in the config file for BeginOnDateOverride.
            DateTime.TryParse(System.Configuration.ConfigurationManager.AppSettings["BeginOnDateOverride"], out lowerBoundOverride);
            if (lowerBoundOverride != null && lowerBoundOverride != DateTime.MinValue)
            {
                nextRun.BeginsOn = lowerBoundOverride;
            }
            else
            {
                // If a refresh argument was used, calculate the refresh start date.
                DateTime refreshBeginsOn = DateTime.MaxValue;
                if (IsWeeklyRefresh)
                {
                    refreshBeginsOn = CalculateWeeklyRefreshStartDate();
                }
                else if (IsSemiMonthlyRefresh)
                {
                    refreshBeginsOn = CalculateSemiMonthlyRefreshStartDate();
                }
                else if (IsMonthlyRefresh)
                {
                    refreshBeginsOn = CalculateMonthlyRefreshStartDate();
                }

                // Start on the earlier of either the refresh begin date or the last run end date.
                if (refreshBeginsOn < lastRunEndTime)
                {
                    nextRun.BeginsOn = refreshBeginsOn;
                }
                else
                {
                    nextRun.BeginsOn = lastRunEndTime;
                }
            }

            // See if there's a value in the config file for EndBeforeDateOverride.
            // Otherwise, use the value that was previously passed into the class.
            DateTime.TryParse(System.Configuration.ConfigurationManager.AppSettings["EndBeforeDateOverride"], out upperBoundOverride);
            if (upperBoundOverride != null && upperBoundOverride != DateTime.MinValue)
            {
                nextRun.EndsBefore = upperBoundOverride;
            }
            else
            {
                // Make the end date start at midnight of the current day.
                nextRun.EndsBefore = DateTime.Today;
                // Make sure that the EndBefore time is no less than 2 hrs from right now to allow for delays in SR score 
                // delivery and replication.
                TimeSpan ts = DateTime.Now - nextRun.EndsBefore;
                if (ts.Hours < 2)
                {
                    nextRun.EndsBefore = DateTime.Now.AddHours(-2);
                }
            }

            return nextRun;
        }

        public static void CheckForRefreshArgs(string[] args)
        {
            // Check for weekly.
            int cnt = args.Count(n => n.ToUpper().Trim() == "WEEKLY");
            IsWeeklyRefresh = (cnt > 0);

            // Check for semimonthly.
            cnt = args.Count(n => n.ToUpper().Trim() == "SEMIMONTHLY");
            IsSemiMonthlyRefresh = (cnt > 0);

            // Check for monthly.
            cnt = args.Count(n => n.ToUpper().Trim() == "MONTHLY");
            IsMonthlyRefresh = (cnt > 0);
        }

        private static DateTime CalculateWeeklyRefreshStartDate()
        {
            // Return the Monday of the previous week.
            return DateTime.Today.AddDays(-7 - (int)DateTime.Today.DayOfWeek);
        }

        private static DateTime CalculateSemiMonthlyRefreshStartDate()
        {
            // Return the first day of the current month.
            return DateTime.Today.AddDays(-1 * (DateTime.Today.Day - 1));
        }

        private static DateTime CalculateMonthlyRefreshStartDate()
        {
            // Return the first day of the previous month.
            return CalculateSemiMonthlyRefreshStartDate().AddMonths(-1);
        }
    }
}
