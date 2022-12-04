using App.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace App.Model
{
    public class Counter
    {
        public int Count { get; private set; } = 300;
        public DispatcherTimer Timer;

        private bool timerStarted = false;

        CountDownViewModel CountView;
        DateTime time = DateTime.Now;
        public void InitializeTimer()
        {
            timerStarted = true;
            Timer = new DispatcherTimer();
            Timer.Interval = TimeSpan.FromMilliseconds(100);
            Timer.Tick += TimerTick;
            Timer.Start();
            time = DateTime.Now;

        }
        void TimerTick(object sender, EventArgs e)
        {
            DateTime timeNow = DateTime.Now;

        }

        public Counter(int timeInSeconds, CountDownViewModel countView)
        {
            Count = timeInSeconds;
            CountView = countView;
        }
    }
}
