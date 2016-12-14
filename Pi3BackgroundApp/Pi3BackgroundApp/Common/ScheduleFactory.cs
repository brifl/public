using System;
using System.Collections.Generic;
using System.Linq;

namespace Pi3BackgroundApp.Common
{
    internal interface IScheduleFactory
    {
        ISchedule RepeatingScheduleFor(
            HashSet<ScheduleFactory.Month> months = null, 
            HashSet<DayOfWeek> days = null, 
            HashSet<int> hours = null, 
            HashSet<int> minutes = null,
            HashSet<int> seconds = null);

        ISchedule SingleScheduleFor(DateTime when);
    }

    internal class ScheduleFactory : IScheduleFactory
    {
        private readonly IMetronome _metronome;

        public ScheduleFactory(IMetronome metronome)
        {
            _metronome = metronome;
        }

        public ISchedule RepeatingScheduleFor(HashSet<Month> months = null, HashSet<DayOfWeek> days = null, HashSet<int> hours = null, HashSet<int> minutes = null,
             HashSet<int> seconds = null)
        {
            months = months ?? AllMonths;
            days = days ?? AllDays;
            hours = hours ?? AllHours;
            minutes = minutes ?? AllMinutes;
            seconds = seconds ?? AllSeconds;

            return new RepeatingSchedule(_metronome.TimeEverySecond, 
                new HashSet<int>(months.Cast<int>()), days, hours, minutes, seconds);
        }

        public ISchedule SingleScheduleFor(DateTime when)
        {
            return new DoOnceSchedule(_metronome.TimeEverySecond, when);
        }

        public static HashSet<int> AllHours => new HashSet<int>
        {
            0, 1, 2, 3, 4, 5, 6, 7,
            8, 9, 10, 11, 12, 13, 14, 15,
            16, 17, 18, 19, 20, 21, 22, 23
        };

        public static HashSet<int> FirstOnly => new HashSet<int> {0};

        public static HashSet<int> AllMinutes => new HashSet<int>
        {
            0, 1, 2, 3, 4, 5, 6, 7, 8, 9,
            10, 11, 12, 13, 14, 15, 16, 17, 18, 19,
            20, 21, 22, 23, 24, 25, 26, 27, 28, 29,
            30, 31, 32, 33, 34, 35, 36, 37, 38, 39,
            40, 41, 42, 43, 44, 45, 46, 47, 48, 49,
            50, 51, 52, 53, 54, 55, 56, 57, 58, 59
        };
        public static HashSet<int> AllSeconds => new HashSet<int>
        {
            0, 1, 2, 3, 4, 5, 6, 7, 8, 9,
            10, 11, 12, 13, 14, 15, 16, 17, 18, 19,
            20, 21, 22, 23, 24, 25, 26, 27, 28, 29,
            30, 31, 32, 33, 34, 35, 36, 37, 38, 39,
            40, 41, 42, 43, 44, 45, 46, 47, 48, 49,
            50, 51, 52, 53, 54, 55, 56, 57, 58, 59
        };

        public static HashSet<Month> AllMonths => new HashSet<Month>
        {
            Month.January,
            Month.February,
            Month.March,
            Month.April,
            Month.May,
            Month.June,
            Month.July,
            Month.August,
            Month.September,
            Month.October,
            Month.November,
            Month.December
        };

        public static HashSet<DayOfWeek> AllDays => new HashSet<DayOfWeek>
        {
            DayOfWeek.Sunday,
            DayOfWeek.Monday,
            DayOfWeek.Tuesday,
            DayOfWeek.Wednesday,
            DayOfWeek.Thursday,
            DayOfWeek.Friday,
            DayOfWeek.Saturday
        };

        public enum Month
        {
            January = 1,
            February,
            March,
            April,
            May,
            June,
            July,
            August,
            September,
            October,
            November,
            December
        }

        private class RepeatingSchedule : ISchedule
        {
            private readonly HashSet<int> _months;
            private readonly HashSet<DayOfWeek> _days;
            private readonly HashSet<int> _hours;
            private readonly HashSet<int> _minutes;
            private readonly HashSet<int> _seconds;
            private readonly ISubscribable<DateTime> _timeEverySecond;
            private IDisposable _subscription;

            public RepeatingSchedule(ISubscribable<DateTime> timeEverySecond, 
                HashSet<int> months, HashSet<DayOfWeek> days, 
                HashSet<int> hours, HashSet<int> minutes, HashSet<int> seconds)
            {
                _timeEverySecond = timeEverySecond;
                _months = months;
                _days = days;
                _hours = hours;
                _minutes = minutes;
                _seconds = seconds;
            }

            public void RunWhenDue(Action action)
            {
                _subscription = _timeEverySecond.Subscribe(now =>
                {
                    if (IsDue(now))
                    {
                        action.Invoke();
                    }
                });
            }

            private bool IsDue(DateTime now)
            {
                return _months.Contains(now.Month)
                       && _days.Contains(now.DayOfWeek)
                       && _hours.Contains(now.Hour)
                       && _minutes.Contains(now.Minute)
                       && _seconds.Contains(now.Second);
            }

            public void Dispose()
            {
                _subscription?.Dispose();
            }
        }

        private class DoOnceSchedule : ISchedule
        {
            private readonly ISubscribable<DateTime> _metronome;
            private readonly DateTime _whenTime;
            private bool _canRun = false;
            private IDisposable _subscription;

            public DoOnceSchedule(ISubscribable<DateTime> metronome, DateTime whenTime)
            {
                _metronome = metronome;
                _whenTime = whenTime;
            }

            public void RunWhenDue(Action action)
            {
                _canRun = true;
                _subscription = _metronome.Subscribe(RunOnce);
            }

            private void RunOnce(DateTime now)
            {
                if (_canRun && (now >= _whenTime))
                {
                    _canRun = false;
                    _subscription.Dispose();
                    _subscription = null;
                }
            }

            public void Dispose()
            {
                _canRun = false;
                _subscription?.Dispose();
            }
        }

        public static HashSet<int> EveryNIn60(int everyN)
        {
            var in60 = new HashSet<int>();
            for (int i = 0; i < 60; i++)
            {
                if (i % everyN == 0)
                {
                    in60.Add(i);
                }
            }
            return in60;
        }
    }
}