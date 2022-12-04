using App.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace App.Model
{
    
    public class Counter
    {
        public DispatcherTimer TimerEvent = new DispatcherTimer();
        public int countDuration { get; set; }

        static CountDownViewModel CountView;

        public DateTime PlannedEnd;

        TimeSpan elapsedTime;
        public void Play()
        {
            if (PlannedEnd == new DateTime())
            {
                PlannedEnd = DateTime.Now.AddMinutes(countDuration);
            }
            if(elapsedTime != new TimeSpan())
            {
                PlannedEnd = DateTime.Now.AddMinutes(elapsedTime.Minutes).AddSeconds(elapsedTime.Seconds);
            }
            TimerEvent.Tick += TimerTick;
            TimerEvent.Start();
        }

        public void Pause()
        {
            TimerEvent.Stop();
        }

        public void Reset()
        {
            CountView.CountText = $"{countDuration}:00";
            TimerEvent.Stop();
            elapsedTime = new TimeSpan();
            PlannedEnd = new DateTime();
        }
        void TimerTick(object sender, EventArgs e)
        {
            elapsedTime = PlannedEnd - DateTime.Now;
            CountView.CountText = $"{elapsedTime.Minutes}:{elapsedTime.Seconds}";

        }

        public Counter(int timeInMinutes)
        {
            countDuration = timeInMinutes;
            TimerEvent.Interval = TimeSpan.FromMilliseconds(100);
        }

        public static void SetView(CountDownViewModel view)
        {
            CountView = view;
        }
    }
}
