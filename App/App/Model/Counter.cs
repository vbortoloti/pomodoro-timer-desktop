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
        public bool isSeconds = false;
        public bool isRunning = false;
        public DateTime PlannedEnd;
        public event EventHandler<EventArgs> TimerEnd;
        TimeSpan elapsedTime;
        public void Play()
        {
            if (PlannedEnd == new DateTime())
            {
                if(isSeconds)
                    PlannedEnd = DateTime.Now.AddSeconds(countDuration);
                else
                    PlannedEnd = DateTime.Now.AddMinutes(countDuration);
            }
            if (elapsedTime != new TimeSpan())
            {
                PlannedEnd = DateTime.Now.AddMinutes(elapsedTime.Minutes).AddSeconds(elapsedTime.Seconds);
            }
            isRunning = true;
            TimerEvent.Tick += TimerTick;
            TimerEvent.Start();
        }

        public void Pause()
        {
            isRunning = false;
            TimerEvent.Stop();
        }

        public void Reset()
        {
            isRunning = false;
            CountView.CountText = $"{countDuration}:00";
            TimerEvent.Stop();
            elapsedTime = new TimeSpan();
            PlannedEnd = new DateTime();
        }
        
        void TimerTick(object sender, EventArgs e)
        {
            elapsedTime = PlannedEnd - DateTime.Now;
            CountView.CountText = elapsedTime.ToString("mm\\:ss");
            if (elapsedTime.Minutes == 0 && elapsedTime.Seconds == 0)
            {
                TimerEvent.Tick -= TimerTick;
                OnTimerEnd();
                return;
            }
        }

        private void OnTimerEnd()
        {
            Reset();
            TimerEnd(this, EventArgs.Empty); 
        }

        public Counter(int timeInMinutes)
        {
            countDuration = timeInMinutes;
            TimerEvent.Interval = TimeSpan.FromMilliseconds(100);
            TimerEnd += CounterManager.OnTimerEnd;
        }

        public static void SetView(CountDownViewModel view)
        {
            CountView = view;
        }
    }
}
